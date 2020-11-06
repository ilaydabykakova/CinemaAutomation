using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace CinemaAutomation.Helpers
{
    public class LoginAttiributes : ActionFilterAttribute
    {
        public bool GirisKontroluMu { get; set; } = true; // Eğer true gönderiliyor ise giriş yaptığını, false gönderiliyor ise giriş yapmadığını kontrol ediyoruz.
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (GirisKontroluMu & !Uyelik.GirisKontrol())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Yonetici", action = "Index" }));
                return;
            }
            else if (!GirisKontroluMu & Uyelik.GirisKontrol())
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Index" }));
                return;
            }
        }
    }
}