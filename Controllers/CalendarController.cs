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
                TrainingView res = new TrainingView
                {
                    // Empty field by default
                    ID = i,
                    ID_Training = 0,
                    ID_Trainer = 0,
                    TrainerName = "",
                    GroupTypeName = "",
                    ClientsCount = 0,
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
                    res.TrainerName = trainer.LastName + ", " + trainer.FirstName;
                    res.GroupTypeName = group.Description;
                    res.ClientsCount = Context.TrainGroups.GetList.Where(x =>
                        x.ID_Training == tm.ID).ToList().Count();
                }
                model.Add(res);
            }
            return View(model);
        }
    }
}