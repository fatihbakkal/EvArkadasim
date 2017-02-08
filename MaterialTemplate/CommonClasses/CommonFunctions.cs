using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MaterialTemplate.CommonClasses
{
    public static class CommonFunctions
    {
        public static string GetUserName()
        {
            var session = HttpContext.Current.Session["User"].ToString().Split('|');
            return session[1];
        }

        public static int GetUserId()
        {
            var session = HttpContext.Current.Session["User"].ToString().Split('|');
            return Convert.ToInt32(session[0]);
        }
    }
}