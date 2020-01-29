using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using GymSite.Models;
using GymSite.Models.Views;
using GymSite.Relations;

namespace GymSite.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration Configuration;
        private MyDBContext Context;
        private DateTime WorkStart = new DateTime(1,1,1,7,0,0);
        private DateTime Workend = new DateTime(1,1,1,21,0,0);

        [ViewData]
        public bool EmptyResult {get; set;}

        public HomeController(IConfiguration configuration)
        {
            Configuration = configuration;
            string dbConn = Configuration.GetConnectionString("mysql_local");
            Context = new MyDBContext(new MyDBUse(MyDBType.mysql, dbConn));
            EmptyResult = false;
        }

        [HttpGet]
        public IActionResult Registration ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration (Client model)
        {
            if (!String.IsNullOrEmpty(model.FirstName) &&
                !String.IsNullOrEmpty(model.LastName) &&
                model.DateOfBirth < DateTime.Now.ToLocalTime())
            {
                int id = Context.Clients.Update(Context.Clients.ParseToInstert(model));
                if (id>0)
                    return RedirectToAction("ClientInfo","Home", new {id});
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ClientInfo (int? id)
        {
            if (id == null) throw new Exception("ID is not set!");
            Client c = Context.Clients.GetList.First(x => x.ID == id);
            if (c == null || c.ID == 0) throw new Exception($"Client[{id}] is not found!");
            Abonement a = Context.Abonements.GetList.FirstOrDefault(x =>
                x.ID_Client == c.ID);
            bool hasA = (a != null && a.ID != 0);
            Price p;
            if (hasA){
                p = Context.Prices.GetList.FirstOrDefault(x => x.ID == a.ID_Price);
            } else {
                p = new Price();
            }
            ClientInfo ci = new ClientInfo{
                ID = c.ID,
                LastName = c.LastName,
                FirstName = c.FirstName,
                Gender = c.Gender,
                DateOfBirth = c.DateOfBirth,
                StartDate = hasA ? a.StartDate : default,
                EndDate = hasA ? a.EndDate : default,
                Description = hasA ? p.Description : default,
                Cost = hasA ? a.TotalPrice : default,
                HasAbonement = hasA
            };
            return View (ci);
        }

        public IActionResult TrainClient (int? id)
        {
            if (id == null || id == 0) return RedirectToAction("Error");
            Client c = Context.Clients.GetList.FirstOrDefault(x => x.ID == id);
            if (c==null) return RedirectToAction("Error");
            Abonement a = Context.Abonements.GetList.FirstOrDefault(x => x.ID_Client == c.ID);
            Price p = Context.Prices.GetList.FirstOrDefault(x => x.ID == a.ID_Price);
            //List<Training> trs = Context.TrainGroups
            ClientInfo ci = new ClientInfo{
                ID = c.ID,
                LastName = c.LastName,
                FirstName = c.FirstName,
                Gender = c.Gender,
                DateOfBirth = c.DateOfBirth,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                ID_Price = p.ID,
                Description = p.Description,
                Cost = a.TotalPrice,
                HasAbonement = true
            };
            return View(ci);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddCard(int? idClient)
        {
            if (idClient != null)
            {
                Client c = Context.Clients.GetList.First(x => x.ID == idClient);
                Abonement a = new Abonement();
                CardView model = new CardView
                {
                    ID_Client = (int)idClient,
                    FullName = c.LastName + ", "+ c.FirstName,
                    Age = (DateTime.Now - c.DateOfBirth).Days / 365,
                    ID = 0,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7), // Let it be default
                    ID_Price = 0
                };
                SelectList prices = new SelectList(Context.Prices.GetList,"ID","Description");
                ViewData["Prices"] = prices;
                return View(model);
            } else
            return View();
        }
        [HttpPost]
        public IActionResult AddCard(CardView model)
        {
            Price p = Context.Prices.GetList.First(x => x.ID == model.ID_Price);
            int tot = (int)(model.EndDate - model.StartDate).TotalDays * p.Cost;
            Abonement a = new Abonement{
                ID = 0,
                ID_Client = model.ID_Client,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                ID_Price = model.ID_Price,
                TotalPrice = tot
            };
            int id = Context.Abonements.Update(Context.Abonements.ParseToInstert(a));
            if (id>0) return RedirectToAction("ClientInfo", new {id = a.ID_Client});

            return View(model);
        }
        public IActionResult Calendar (DateTime? when)
        {
            if (when == null) when = DateTime.Now.Date;
            ViewData["CurrDate"] = Convert.ToDateTime(when).ToString("yyyy-MM-dd");
            List<TrainingView> model = new List<TrainingView>();
            List<Training> t = Context.Trainings.GetList.Where(x =>
                x.StartTime.Date == when).ToList();
            List<Trainer> tr = Context.Trainers.GetList;
            List<Price> gr = Context.Prices.GetList;
            // Magic actions
            for (int i = WorkStart.Hour; i < Workend.Hour; i++)
            {
                TrainingView res = new TrainingView{ 
                    // Empty field by default
                    ID = i,
                    ID_Training = 0,
                    ID_Trainer = 0,
                    TrainerName = "",
                    GroupTypeName = "",
                    ClientsCount = 0
                };
                Training tm = t.FirstOrDefault(x => x.StartTime.Hour == i);
                if (tm !=null) if (tm.ID != 0) {
                    // Training exists
                    Trainer trainer = tr.FirstOrDefault(x =>
                        x.ID == tm.ID_Trainer);
                    Price group = gr.FirstOrDefault( x =>
                        x.ID == tm.ID_Price);
                    res.ID_Trainer = trainer.ID;
                    res.TrainerName = trainer.LastName+", "+trainer.FirstName;
                    res.GroupTypeName = group.Description;
                    res.ClientsCount = Context.TrainGroups.GetList.Where(x =>
                        x.ID_Training == tm.ID).Count();
                }
                model.Add(res);
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult SearchResult(string LastName)
        {
            if (!String.IsNullOrEmpty(LastName))
            {
                List<Client> cs = Context.Clients.GetList.Where(x => 
                    x.LastName.ToUpper() == LastName.ToUpper()).ToList();
                if (cs.Count>0) 
                    return View(cs);
            }
            EmptyResult = true;
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
