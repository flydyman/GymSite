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
        public IActionResult New(DateTime? date, int? idClient)
        {
            if (date == null)
            {
                DateTime currDate = DateTime.Now;
                date = new DateTime(currDate.Year,
                    currDate.Month, currDate.Day,
                    currDate.Hour, 0, 0);
            }

            TrainingForClient model = new TrainingForClient()
            {
                ID = 0,
                EndTime = date.Value.AddHours(1),
                ID_Creator = 1,
                ID_Price = 0,
                ID_Trainer = 0,
                ID_Client = idClient == null ? 0 : Convert.ToInt32(idClient),
                StartTime = date.Value
            };
            SelectList groups = new SelectList(Context.Prices.GetList, "ID", "Description");
            ViewBag.Groups = groups;
            SelectList trainers = new SelectList(Context.Trainers.GetList, "ID", "LastName");
            ViewBag.Trainers = trainers;
            ViewBag.idClient = idClient;
            return View(model);
        }

        [HttpPost]
        public IActionResult New(TrainingForClient model)
        {
            model.ID = 0;
            model.ID_Creator = 1;
            int res = Context.Trainings.Update(Context.Trainings.ParseToInstert(model));
            if (res > 0)
            {
                // Add client if exists
                if (model.ID_Client != 0)
                {
                    Client c = Context.Clients.GetList.First(x => x.ID == model.ID_Client);
                    Training t = Context.Trainings.GetList.Last();
                    TrainGroup tg = new TrainGroup()
                    {
                        ID_Client = c.ID,
                        ID_Training = t.ID
                    };
                    int res2 = Context.TrainGroups.Update(Context.TrainGroups.ParseToInstert(tg));
                    if (res2 <= 0) return RedirectToAction("Index","Client", c.ID);
                }
                return RedirectToAction("Index", "Training", model.ID);
            }
            return View(model);
        }

        public IActionResult RemoveFromTraining(int idTraining, int idClient)
        {
            List<TrainGroup> res = (from tr in Context.TrainGroups.GetList
                    where tr.ID_Training == idTraining && tr.ID_Client == idClient
                    select tr).ToList();
            foreach (TrainGroup item in res)
            {
                int r = Context.TrainGroups.Update(Context.TrainGroups.ParseToDelete($"ID_Training = '{item.ID_Training}' AND ID_Client = '{item.ID_Client}'"));
                //Console.WriteLine($"Deleted {r}");
            }
            return RedirectToAction("Index", "Calendar");
        }

        public IActionResult DeleteTraining(int idTraining)
        {
            List<Client> clients = (from c in Context.Clients.GetList
                join tr in Context.TrainGroups.GetList on c.ID equals tr.ID_Client
                where tr.ID_Training == idTraining
                select c).ToList();
            foreach (var client in clients)
            {
                int r = Context.TrainGroups.Update(
                    Context.TrainGroups.ParseToDelete($"ID_Training = '{idTraining}' AND ID_Client = '{client.ID}'"));
            }

            Context.Trainings.Update(Context.Trainings.ParseToDelete("ID", $"{idTraining}"));
            return RedirectToAction("Index", "Calendar");
        }
    }
}