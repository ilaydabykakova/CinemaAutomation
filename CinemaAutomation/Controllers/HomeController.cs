using CinemaAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinemaAutomation.Models;
using CinemaAutomation.Models.Modeller;

namespace CinemaAutomation.Controllers
{
    [LoginAttiributes(GirisKontroluMu = true)]
    public class HomeController : BaseController // Ortak bir controller yaptık, route bilgilerini ViewData ile alıyoruz.
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult BiletKaydet(string BiletNo, string Ad, string Soyad, string Telefon, int FilmID)
        {
            bool Islem = true;
            using (CinemaAutomationEntities db = new CinemaAutomationEntities())
            {
                try
                {
                    var Musteri = new Musteriler()
                    {
                        Ad = Ad,
                        Soyad = Soyad,
                        Telefon = Telefon
                    };
                    var Bilet = new Biletler()
                    {
                        BiletNo = BiletNo,
                        FilmID = FilmID,
                        Musteriler = Musteri
                    };
                    db.Biletler.Add(Bilet);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    Islem = false;
                    throw;
                }
            }
            return Json(Islem);
        }
        public JsonResult cikis()
        {
            try
            {
                return Json(Uyelik.Cikis());
            }
            catch (Exception)
            {
                return Json(false);
                throw;
            }
        }
    }
}