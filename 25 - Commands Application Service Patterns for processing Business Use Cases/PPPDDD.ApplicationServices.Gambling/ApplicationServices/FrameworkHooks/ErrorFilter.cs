using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PPPDDD.ApplicationServices.Gambling.ApplicationServices.FrameworkHooks
{
    public class ErrorFilter : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Exception.GetType() == typeof(ApplicationError))
            {
                // return specific message to user
                var msg = filterContext.Exception.Message;
                ErrorResponse(msg, filterContext);
            }
            else
            {
                // return generic message for security reasons
                var msg = "Sorry. Something really unexpected has occurred";
                ErrorResponse(msg, filterContext);
            }
        }

        public void ErrorResponse(string msg, ExceptionContext ec)
        {
            var routeData = new RouteValueDictionary(new { message = msg });
            var response = new RedirectToRouteResult("ErrorPage", routeData);
            ec.Result = response;
            ec.ExceptionHandled = true;
        }
    }


    public class ApplicationError : Exception { }
}