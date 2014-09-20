using System;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace PPPDDD.ApplicationServices.Gambling.ApplicationServices.FrameworkHooks
{
    public class TransactionFilter : ActionFilterAttribute, IActionFilter
    {
        // executes before controller
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            // start a transaction
            var t = new TransactionScope();
            HttpContext.Current.Items["transaction"] = t;
            base.OnResultExecuting(filterContext);
        }

        // executes after controller
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // close the transaction created at start of this request
            var t = (TransactionScope)HttpContext.Current.Items["transaction"];
            t.Complete();

            base.OnActionExecuted(filterContext);
        }
    }
}