using CinemaAutomation.Helpers;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace CinemaAutomation.Controllers
{
    [LoginAttiributes(GirisKontroluMu = false)]
    public class BaseController : Controller
    {
        public string Action = "";
        public string Controller = "";
        protected override void Initialize(RequestContext requestContext)
        {
            try
            {
                Action = requestContext.RouteData.Values["action"].ToString();
                Controller = requestContext.RouteData.Values["controller"].ToString();
                ViewData["RouteController"] = Controller;
                ViewData["RouteAction"] = Action;
            }
            catch (Exception) { }
            base.Initialize(requestContext);
        }
    }
}