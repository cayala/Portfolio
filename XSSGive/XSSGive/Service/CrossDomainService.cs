using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace XSSGive.Service
{
    public class CrossDomainService
    {
        public List<string> getListOfDomains()
        {
            var listOfKeys = ConfigurationManager.AppSettings.AllKeys;
            var diction = new Dictionary<string, string>();
            var _hostKeys = new Dictionary<string, string>();
            foreach (var k in listOfKeys)
            {
                if (k.StartsWith("CrossDomain.", StringComparison.OrdinalIgnoreCase))
                {
                    diction.Add(k, ConfigurationManager.AppSettings[k]);
                }
            }
            _hostKeys = diction;

            return _hostKeys.Keys.ToList();
        }
    }
}