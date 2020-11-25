using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TraiDoan.Models;

namespace TraiDoan.Controllers
{
    public class CheckLoginController : Controller
    {
        DatabaseContext db = new DatabaseContext();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["IDUser"] != null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Home", action = "Index"}));
            }
            else if (Session["Admin"] != null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Categories", action = "Index" }));
                Content("Vui long su dung tai khoan Admin");
            }
            base.OnActionExecuting(filterContext);
        }

    }
}