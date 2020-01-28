using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using GymSite.Models;
using GymSite.Models.Views;
using GymSite.Relations;

namespace GymSite.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration Configuration;
        private MyDBContext Context;

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
                    return RedirectToAction("ClientInfo",id);
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
                Cost = hasA ? p.Cost : default,
                HasAbonement = hasA
            };
            return View (ci);
        }

        public IActionResult Index()
        {
            return View();
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
