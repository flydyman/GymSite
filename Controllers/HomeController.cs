using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using GymSite.Models;
using GymSite.Models.Views;
using GymSite.Relations;

namespace GymSite.Controllers
{
    public class HomeController : Controller
    {
        //private IConfiguration Configuration;
        private MyDBContext Context;
        private DateTime WorkStart = new DateTime(1, 1, 1, 7, 0, 0);
        private DateTime Workend = new DateTime(1, 1, 1, 21, 0, 0);

        public bool EmptyResult { get; set; }

        public HomeController(MyDBContext context)
        {
            //Configuration = configuration;
            /// Home
            //string dbConn = Configuration.GetConnectionString("mysql_home");
            /// Work
            //string dbConn = Configuration.GetConnectionString("mysql_8");
            //Context = new MyDBContext(new MyDBUse(MyDBType.mysql, dbConn));
            // NEW WAY!!!
            Context = context;
            EmptyResult = false;
        }
        public IActionResult Index()
        {
            ViewData["EmptyResult"] = EmptyResult;
            return View();
        }
        [HttpPost]
        public IActionResult SearchResult(string LastName)
        {
            if (!String.IsNullOrEmpty(LastName))
            {
                List<Client> cs = Context.Clients.GetList.Where(x =>
                    x.LastName.ToUpper() == LastName.ToUpper()).ToList();
                if (cs.Count > 0)
                    EmptyResult = false;
                    return View(cs);
            }
            EmptyResult = true;
            ViewData["EmptyResult"] = EmptyResult;
            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
