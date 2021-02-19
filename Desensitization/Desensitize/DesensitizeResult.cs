using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Desensitization.Desensitize
{
    /// <summary>
    /// 自定义DesensitizeResult
    /// </summary>
    public class DesensitizeResult : ActionResult
    {
        public DesensitizeResult(object data)
        {
            JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            Data = data;
        }

        public Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }

        public object Data { get; set; }

        public JsonRequestBehavior JsonRequestBehavior { get; set; }

        /// <summary>
        /// When set MaxJsonLength passed to the JavaScriptSerializer.
        /// </summary>
        public int? MaxJsonLength { get; set; }

        /// <summary>
        /// When set RecursionLimit passed to the JavaScriptSerializer.
        /// </summary>
        public int? RecursionLimit { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }
            HttpResponseBase response = context.HttpContext.Response;

            if (!String.IsNullOrEmpty(ContentType))
            {
                response.ContentType = ContentType;
            }
            else
            {
                response.ContentType = "application/json";
            }
            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }
            if (Data != null)
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (MaxJsonLength.HasValue)
                {
                    serializer.MaxJsonLength = MaxJsonLength.Value;
                }
                if (RecursionLimit.HasValue)
                {
                    serializer.RecursionLimit = RecursionLimit.Value;
                }
                response.Write(serializer.Serialize(Data));
            }
        }
    }
}