using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyTourney.Bll;
using System.Web.Mvc;
using System.Web.Routing;

namespace EasyTourney.Filters
{
    public class IsAdminFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (AuthBll.isAdmin())
            {
                return;
            }

            filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {{ "Controller", "Home" },
                                      { "Action", "Index" } });

            base.OnActionExecuting(filterContext);
        }
    }
}