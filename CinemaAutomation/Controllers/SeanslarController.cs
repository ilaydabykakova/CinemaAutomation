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
    public class SeanslarController : BaseController // Ortak bir controller yaptık, route bilgilerini ViewData ile alıyoruz.
    {
        // GET: Seanslar
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Yeni()
        {
            return View();
        }
        [ValidateInput(false)] // bu kod ile html e izin veriyoruz
        public JsonResult Ekle(string SeansAdi, DateTime? Tarih, TimeSpan? Saat)
        {
            // using ile bağlantıyı açıyoruz işlem sonunda bağlantı kapanıyor.
            using (CinemaAutomationEntities db = new CinemaAutomationEntities())
            {
                // try-catch ile hata kontrolü yapıyoruz. hata verirse catch e düşecek ve olumsuz yanıt döneceğiz.
                try
                {
                    var Seans = new Seanslar()
                    {
                        SeansAdi = SeansAdi,
                        Tarih = Tarih,
                        Saat = Saat,
                    };
                    db.Seanslar.Add(Seans);
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