using System;
using System.Collections.Generic;
using System.Linq;
using GymSite.Models;
using GymSite.Models.Views;
using GymSite.Relations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymSite.Controllers
{
    public class TrainingController : Controller
    {
        private MyDBContext Context;

        public TrainingController(MyDBContext context)
        {
            Context = context;
        }
        public IActionResult Index(int? id)
        {
            // Info about training
            if (id == null) return RedirectToAction("Index", "Calendar");
            Training tr = Context.Trainings.GetList.First(x => x.ID == id);
            Trainer t = Context.Trainers.GetList.First(x => x.ID == tr.ID_Trainer);
            List<TrainGroup> tg = Context.TrainGroups.GetList.Where(x => x.ID_Training == tr.ID).ToList();
            List<Client> cs = new List<Client>();
            foreach (var item in tg)
            {
                cs.Add(Context.Clients.GetList.First(x => x.ID == item.ID_Client));
            }

            Price p = Context.Prices.GetList.First(x => x.ID == tr.ID_Price);
            FullTrainingView model = new FullTrainingView
            {
                ID = tr.ID,
                TrainerName = $"{t.LastName}, {t.FirstName}",
                StartTime = tr.StartTime,
                Clients = cs,
                Group = p
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult New(DateTime? date)
        {
            if (date == null)
            {
                DateTime currDate = DateTime.Now;
                date = new DateTime(currDate.Year,
                    currDate.Month, currDate.Day,
                    currDate.Hour, 0, 0);
            }

            Training model = new Training()
            {
                ID = 0,
                EndTime = date.Value.AddHours(1),
                ID_Creator = 1,
                ID_Price = 0,
                ID_Trainer = 0,
                StartTime = date.Value
            };
            SelectList groups = new SelectList(Context.Prices.GetList, "ID", "Description");
            ViewBag.Groups = groups;
            SelectList trainers = new SelectList(Context.Trainers.GetList, "ID", "LastName");
            ViewBag.Trainers = trainers;
            return View(model);
        }

        [HttpPost]
        public IActionResult New(Training model)
        {
            model.ID = 0;
            model.ID_Creator = 1;
            int res = Context.Trainings.Update(Context.Trainings.ParseToInstert(model));
            if (res > 0) return RedirectToAction("Index", "Training", model.ID);
            return View(model);
        }
    }
}