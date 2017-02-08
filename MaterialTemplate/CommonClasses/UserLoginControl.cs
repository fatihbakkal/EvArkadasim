using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaterialTemplate.CommonClasses
{
    public class UserLoginControl : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (HttpContext.Current.Session["User"] == null)
            {
                filterContext.HttpContext.Response.Redirect("~/Home/Index");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}