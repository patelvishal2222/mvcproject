using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
   using System;
using System.Web.Http.Filters;
namespace MvcApplication2.Models
{


public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
{
    public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
    {
        if (actionExecutedContext.Response != null)
            actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

        base.OnActionExecuted(actionExecutedContext);
    }
}
}