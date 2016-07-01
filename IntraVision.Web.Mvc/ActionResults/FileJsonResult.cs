using System.Web.Mvc;

namespace IntraVision.Web.Mvc
{
    public class FileJsonResult : JsonResult
    {
        public FileJsonResult(object data)
            : base()
        {
            Data = data;
            JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Write("<textarea>");
            base.ExecuteResult(context);
            context.HttpContext.Response.Write("</textarea>");
            context.HttpContext.Response.ContentType = "text/html";
        }
    }
}