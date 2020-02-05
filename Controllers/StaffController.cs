using System.Collections.Generic;
using GymSite.Relations;
using Microsoft.AspNetCore.Mvc;

namespace GymSite.Controllers
{
    public class StaffController : Controller
    {
        private MyDBContext Context;

        public StaffController(MyDBContext context)
        {
            Context = context;
        }
        // GET
        public IActionResult Index()
        {
            return View(Context.Staffs.GetList);
        }
    }
}