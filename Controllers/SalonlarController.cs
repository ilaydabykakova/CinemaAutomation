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
    public class SalonlarController : BaseController // Ortak bir controller yaptık, route bilgilerini ViewData ile alıyoruz.
    {
        // GET: Salonlar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Yeni()
        {
            return View();
        }
        [ValidateInput(false)] // bu kod ile html e izin veriyoruz
        public JsonResult Ekle(string Baslik)
        {
            // using ile bağlantıyı açıyoruz işlem sonunda bağlantı kapanıyor.
            using (CinemaAutomationEntities db = new CinemaAutomationEntities())
            {
                // try-catch ile hata kontrolü yapıyoruz. hata verirse catch e düşecek ve olumsuz yanıt döneceğiz.
                try
                {
                    var Salon = new Salonlar()
                    {
                        SalonAdi = Baslik,
                    };
                    db.Salonlar.Add(Salon);
                    db.SaveChanges();
                    return Json(true);
                }
                catch (Exception)
                {
                    return Json(false);
                }
            }
        }
    }
}