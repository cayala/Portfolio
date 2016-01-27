using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Cors;
using XSSGive.Models;
using XSSGive.Controllers.Attributes;
using XSSGive.Service;

namespace XSSGive.Controllers
{
    
    public class XSSGiverController : Controller
    {
        //
        // GET: /XSSGiver/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CrossDomainManager()
        {
            return View();
        }
        //methods

        [HttpGet]
        public ActionResult getDomains()
        {
            CrossDomainService ds = new CrossDomainService();

            return Json(ds.getListOfDomains(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult addKey()
        {
            return null;
        }

        [HttpPost]
        public ActionResult deleteKey()
        {
            return null;
        }
        //CrossDomain tests
        [HttpGet]
        public JsonpResult GiveJSONPArray(int num)
        {
            List<int> listOfInt = new List<int>();

            for (var x = 0; x < num; x++)
            {
                listOfInt.Add(x);
            }

            JsonpResult result = new JsonpResult(listOfInt);

            return result;
        }
        //CORS
        [HttpGet]
        [CrossDomainAccess]
        public ActionResult GiveArray(int num)
        {
            List<int> listOfInt = new List<int>();

            for (var x = 0; x < num; x++)
            {
                listOfInt.Add(x);
            }

            return Json(listOfInt, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [CrossDomainAccess]
        public ActionResult PostNumberToArray(int num)
        {
            var x = 5;

            var total = num + x;

            List<int> listOfInt = new List<int>();

            for (x = 0; x < total; x++)
            {
                listOfInt.Add(x);
            }

            return Json(listOfInt, JsonRequestBehavior.AllowGet);
        }
        //AJAXS
        [HttpGet]
        public ActionResult ajaxGet(int num)
        {
            List<int> listOfInt = new List<int>();

            for (var x = 0; x < num; x++)
            {
                listOfInt.Add(x);
            }

            return Json(listOfInt, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult ajaxPost(int num)
        {
            var x = 5;

            var total = num + x;

            List<int> listOfInt = new List<int>();

            for (x = 0; x < total; x++)
            {
                listOfInt.Add(x);
            }

            return Json(listOfInt, JsonRequestBehavior.AllowGet);
        }
    }
}