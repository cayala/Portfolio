using ElasticSearch.Models;
using ElasticSearch.Repo;
using ElasticSearch.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ElasticSearch.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        private static IElasticRepo _repo = new ElasticRepo();
        private static ILogstashRepo _lRepo = new LogstashRepo();
        private ISearchService _searchService = new SearchService(_repo, _lRepo);

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetHistory(string search)
        {
            return Json(_searchService.ReadRepo(search), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAllResults() {
            return Json(_searchService.GetAllRestaurants(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveNewRestaurant(Restaurant r)
        {
            return Json(_searchService.AddToIndex(r));
        }

        [HttpPost]
        public ActionResult RemoveRestaurant(string rName) 
        {
            return Json(_searchService.DeleteFromIndex(rName));    
        }

        [HttpPost]
        public ActionResult UpdateRestaurant(string rName, string lName) 
        {
            return Json(_searchService.UpdateIndex(rName, lName));
        }
	}
}