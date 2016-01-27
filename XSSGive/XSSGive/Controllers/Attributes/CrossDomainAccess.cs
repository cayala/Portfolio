using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace XSSGive.Controllers.Attributes
{
    public class CrossDomainAccess : ActionFilterAttribute
    {
        private static Dictionary<string, string> _hostKeys;

        public CrossDomainAccess()
        {
            if (_hostKeys == null)
            {
                var listOfKeys = ConfigurationManager.AppSettings.AllKeys;
                foreach (var k in listOfKeys)
                {
                    if (k.StartsWith("CrossDomain.", StringComparison.OrdinalIgnoreCase))
                    {
                        _hostKeys.Add(k, ConfigurationManager.AppSettings[k]);
                    }
                }
            }
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (HttpContext.Current.Request.UrlReferrer.Authority == _hostKeys["CrossDomain." + HttpContext.Current.Request.UrlReferrer.Host + ":" + HttpContext.Current.Request.UrlReferrer.Port])
                {
                        filterContext.RequestContext.HttpContext.Response.AddHeader("Access-Control-Allow-Origin", "*");
                }
            base.OnActionExecuted(filterContext);
        }

        public static Dictionary<string,string> setDictionary()
        {
            var listOfKeys = ConfigurationManager.AppSettings.AllKeys;
            var diction = new Dictionary<string, string>();
            foreach (var k in listOfKeys)
            {
                if (k.StartsWith("CrossDomain.", StringComparison.OrdinalIgnoreCase))
                {
                    diction.Add(k, ConfigurationManager.AppSettings[k]);
                }
            }
            _hostKeys = diction;

            return _hostKeys;
        }
    }
}
