using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestThreadLibrary.Models;
using TestThreadLibrary.Service;

namespace TestThreading.Controllers
{
    public class TestThreadViewController : Controller
    {
        //
        // GET: /TestThreadView/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Test()
        {
            return View(new Threads());
        }

        [HttpPost]
        public ActionResult Test(Threads model)
        {
            ThreadHandler thread = new ThreadHandler();

            //TestThreadLibrary.Service.TaskSchedule.TaskSchedules();
          var results = thread.MinMaxAvgThread(model);

            return View(results);
        }
	}
}