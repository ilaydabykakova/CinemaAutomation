using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinemaAutomation.Models;
using CinemaAutomation.Helpers;

namespace CinemaAutomation.Controllers
{
    [LoginAttiributes(GirisKontroluMu = true)]
    public class BiletlerController : BaseController // Ortak bir controller yaptık, route bilgilerini ViewData ile alıyoruz.
    {
        // GET: Salonlar
        public ActionResult Index()
        {
            return View();
        }
    }
}