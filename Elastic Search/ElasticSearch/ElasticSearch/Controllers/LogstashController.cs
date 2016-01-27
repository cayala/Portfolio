using ElasticSearch.Repo;
using ElasticSearch.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElasticSearch.Controllers
{
    public class LogstashController : Controller
    {
        //
        // GET: /Logstash/
        private static IElasticRepo _repo = new ElasticRepo();
        private static ILogstashRepo _LRepo = new LogstashRepo();
        private static ISearchService _service = new SearchService(_repo, _LRepo);
        

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetLogs()
        {
            return Json(_service.GetAllLogstashLogs(), JsonRequestBehavior.AllowGet);
        }
	}
}