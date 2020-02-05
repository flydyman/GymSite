using System;
using System.Collections.Generic;
using System.Linq;
using GymSite.Models;
using GymSite.Models.Views;
using GymSite.Relations;
using Microsoft.AspNetCore.Mvc;

namespace GymSite.Controllers
{
    public class CalendarController : Controller
    {
        private MyDBContext Context;
        private DateTime WorkStart = new DateTime(1, 1, 1, 7, 0, 0);
        private DateTime Workend = new DateTime(1, 1, 1, 21, 0, 0);


        public CalendarController(MyDBContext context)
        {
            Context = context;
        }
        // GET
        public IActionResult Index(DateTime? when)
        {
            if (when == null)
            {
                when = DateTime.Now;
            }
            
            ViewData["CurrDate"] = when;
            List<TrainingView> model = new List<TrainingView>();
            List<Training> t = Context.Trainings.GetList.Where(x =>
                x.StartTime.Day == when.Value.Day && 
                x.StartTime.Month == when.Value.Month &&
                x.StartTime.Year == when.Value.Year).ToList();
            List<Trainer> tr = Context.Trainers.GetList;
            List<Price> gr = Context.Prices.GetList;
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
                    res.ClientsCount = Context.TrainGroups.GetList.Where(x =>
                        x.ID_Training == tm.ID).ToList().Count();
                }
                model.Add(res);
            }
            return View(model);
        }

        public IActionResult ViewClients(int id)
        {
            List<TrainGroup> tg = Context.TrainGroups.GetList.Where(x => x.ID_Training == id).ToList();
            List<Client> model = new List<Client>();
            ViewBag.idTraining = id;
            foreach (var t in tg)
            {
                model.AddRange((from c in Context.Clients.GetList
                    where c.ID == t.ID_Client
                    select c).ToList());
            }
                
            return View(model);
        }
    }
}