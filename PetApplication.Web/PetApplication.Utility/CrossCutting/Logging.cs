using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PetApplication.Utility
{
    public class AppErrorHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //Integrate additional Exception log framework
            Log.LogError(filterContext.Exception);

            HttpContext httpContext = HttpContext.Current;
            httpContext.Response.Redirect("~/Base/Error");
        }
    }

    public class AuditAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            Log.LogAudit((request.IsAuthenticated) ?
                          filterContext.HttpContext.User.Identity.Name : "Anonymous",
                          request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                          request.RawUrl, "Entry");

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            Log.LogAudit((request.IsAuthenticated) ?
                          filterContext.HttpContext.User.Identity.Name : "Anonymous",
                          request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? request.UserHostAddress,
                          request.RawUrl, "Exit");

            base.OnActionExecuted(filterContext);
        }

    }
}
