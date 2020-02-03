using System;
using System.Collections.Generic;
using System.Linq;
using GymSite.Models;
using GymSite.Relations;
using Microsoft.AspNetCore.Mvc;

namespace GymSite.Controllers
{
    public class TrainerController : Controller
    {
        private MyDBContext Context;

        public TrainerController(MyDBContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            List<Trainer> model = Context.Trainers.GetList;
            return View(model);
        }
        [HttpGet]
        public IActionResult AddTrainer()
        {
            Trainer t = new Trainer();
            return View(t);
        }

        [HttpPost]
        public IActionResult AddTrainer(Trainer model)
        {
            if (!String.IsNullOrEmpty(model.FirstName) || !String.IsNullOrEmpty(model.LastName))
            {
                model.ID = 0;
                int res = Context.Trainers.Update(Context.Trainers.ParseToInstert(model));
                if (res == 0) return RedirectToAction("Error", "Home");
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult EditTrainer(int? id)
        {
            if (id == null || id == 0) return RedirectToAction("Error", "Home");
            Trainer model = Context.Trainers.GetList.FirstOrDefault(x => x.ID == id);
            if (model == default || model.ID == 0) return RedirectToAction("Error", "Home");
            return View(model);
        }

        [HttpPost]
        public IActionResult EditTrainer(Trainer model)
        {
            if (model.ID == 0) return RedirectToAction("Error", "Home");
            int res = Context.Trainers.Update(Context.Trainers.ParseToUpdate(model,"ID"));
            if (res>0) return RedirectToAction("Index");
            return View(model);
        }
    }
}