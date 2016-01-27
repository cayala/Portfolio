using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace XSSGive.Controllers.Attributes
{
    public class JsonpResult : JsonResult
    {
        object data = null;

        public JsonpResult()
        {

        }

        public JsonpResult(object data)
        {
            this.data = data;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context != null)
            {
                HttpResponseBase response = context.HttpContext.Response;
                HttpRequestBase request = context.HttpContext.Request;

                string callbackfunction = request["callback"];
                if (String.IsNullOrEmpty(callbackfunction))
                {
                    throw new Exception("Callback function name must be provided in the request!");
                }

                response.ContentType = "application/x-javascript";

                if (data != null)
                {
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    response.Write(string.Format("{0}({1});", callbackfunction, serializer.Serialize(data)));
                }
            }
        }
    }
}