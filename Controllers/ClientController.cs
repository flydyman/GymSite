using System;
using System.Collections.Generic;
using System.Linq;
using GymSite.Relations;
using GymSite.Models;
using GymSite.Models.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymSite.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private MyDBContext db;
        private DateTime WorkStart = new DateTime(1, 1, 1, 7, 0, 0);
        private DateTime Workend = new DateTime(1, 1, 1, 21, 0, 0);

        public ClientController(MyDBContext context)
        {
            db = context;
        }
        
        public IActionResult Index(int? id)
        {
            // Info about Client
            if (id == null) throw new Exception("ID is not set!");
            Client c = db.Clients.GetList.First(x => x.ID == id);
            if (c == null || c.ID == 0) throw new Exception($"Client[{id}] is not found!");
            Abonement a = db.Abonements.GetList.FirstOrDefault(x =>
                x.ID_Client == c.ID);
            bool hasA;
            if (DateTime.Compare(a.EndDate.ToUniversalTime(), DateTime.Today.ToUniversalTime()) < 0)
            {
                db.Abonements.Update(db.Abonements.ParseToDelete("ID",$"{a.ID}"));
                hasA = false;
            }
            else
            {
                hasA = (a != null && a.ID != 0);
            }
            Price p;
            if (hasA)
            {
                p = db.Prices.GetList.FirstOrDefault(x => x.ID == a.ID_Price);
            }
            else
            {
                p = new Price();
            }
            ClientInfo ci = new ClientInfo
            {
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
            return View(ci);
        }
        [HttpGet]
        public IActionResult AddCard(int? idClient)
        {
            if (idClient != null)
            {
                Client c = db.Clients.GetList.First(x => x.ID == idClient);
                Abonement a = new Abonement();
                CardView model = new CardView
                {
                    ID_Client = (int)idClient,
                    FullName = c.LastName + ", " + c.FirstName,
                    Age = (DateTime.Now - c.DateOfBirth).Days / 365,
                    ID = 0,
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now.AddDays(7), // Let it be default
                    ID_Price = 0
                };
                SelectList prices = new SelectList(db.Prices.GetList, "ID", "Description");
                ViewData["Prices"] = prices;
                return View(model);
            }
            else
                return View();
        }
        [HttpPost]
        public IActionResult AddCard(CardView model)
        {
            Price p = db.Prices.GetList.First(x => x.ID == model.ID_Price);
            int tot = (int)(model.EndDate - model.StartDate).TotalDays * p.Cost;
            Abonement a = new Abonement
            {
                ID = 0,
                ID_Client = model.ID_Client,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                ID_Price = model.ID_Price,
                TotalPrice = tot
            };
            int id = db.Abonements.Update(db.Abonements.ParseToInstert(a));
            if (id > 0) return RedirectToAction("Index", "Client", new { id = a.ID_Client });

            return View(model);
        }
        public IActionResult Train(int? id, DateTime? when)
        {
            if (id == null || id == 0) return RedirectToAction("Error", "Home");
            if (when == null || when == DateTime.MinValue) when = DateTime.Now;
            NewTrainingView model = new NewTrainingView();
            model.CurrDate = (DateTime)when;
            model.client = db.Clients.GetList.FirstOrDefault(x => x.ID == id);
            if (model.client == null) return RedirectToAction("Error", "Home");
            Abonement a = db.Abonements.GetList.LastOrDefault(x => x.ID_Client == model.client.ID);
            model.AbonementLastDay = a.EndDate;
            model.Trainings = new List<TrainingView>();
            List<Training> t = db.Trainings.GetList.Where(x =>
                x.StartTime.Date == when.Value.Date).ToList();
            List<Trainer> tr = db.Trainers.GetList;
            List<Price> gr = db.Prices.GetList;
            // Magic actions
            for (int i = WorkStart.Hour; i < Workend.Hour; i++)
            {
                TrainingView res = new TrainingView
                {
                    // Empty field by default
                    ID = i,
                    ID_Training = 0,
                    ID_Trainer = 0,
                    TrainerName = "",
                    GroupTypeName = "",
                    ClientsCount = 0,
                    MaxClients = 0,
                    IsRight = true
                };
                Training tm = t.FirstOrDefault(x => x.StartTime.Hour == i);
                if (tm != null) if (tm.ID != 0)
                    {
                        // Training exists
                        Trainer trainer = tr.FirstOrDefault(x =>
                            x.ID == tm.ID_Trainer);
                        Price group = gr.FirstOrDefault(x =>
                           x.ID == tm.ID_Price);
                        res.ID_Trainer = trainer.ID;
                        res.ID_Training = tm.ID;
                        res.TrainerName = trainer.LastName + ", " + trainer.FirstName;
                        res.GroupTypeName = group.Description;
                        res.MaxClients = group.MaxClients;
                        res.ClientsCount = db.TrainGroups.GetList.Where(x =>
                            x.ID_Training == tm.ID).Count();
                        res.IsRight = a.ID_Price==tm.ID_Price ? true : false;
                    }
                model.Trainings.Add(res);
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult New(Client model)
        {
            if (!String.IsNullOrEmpty(model.FirstName) &&
                !String.IsNullOrEmpty(model.LastName) &&
                model.DateOfBirth < DateTime.Now.ToLocalTime())
            {
                int id = db.Clients.Update(db.Clients.ParseToInstert(model));
                if (id > 0)
                    return RedirectToAction("Index", "Client", new { id });
            }
            return View(model);
        }

        public IActionResult AddClientToTraining(int idClient, int idTraining)
        {
            /// TODO: check for Max Trains ///
            TrainGroup res = new TrainGroup()
            {
                ID_Client = idClient,
                ID_Training = idTraining
            };
            ViewBag.idTraining = idTraining;
            db.TrainGroups.Update(db.TrainGroups.ParseToInstert(res));
            return RedirectToAction("Index", "Calendar");
        }
    }
}