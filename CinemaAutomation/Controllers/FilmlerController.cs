using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CinemaAutomation.Models;
using CinemaAutomation.Helpers;
using CinemaAutomation.Models.Modeller;

namespace CinemaAutomation.Controllers
{
    [LoginAttiributes(GirisKontroluMu = true)]
    public class FilmlerController : BaseController // Ortak bir controller yaptık, route bilgilerini ViewData ile alıyoruz.
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
        public JsonResult Ekle(string FilmAdi, int SalonID, int SeansID, string Yonetmen, string FilmTuru)
        {
            // using ile bağlantıyı açıyoruz işlem sonunda bağlantı kapanıyor.
            using (CinemaAutomationEntities db = new CinemaAutomationEntities())
            {
                // try-catch ile hata kontrolü yapıyoruz. hata verirse catch e düşecek ve olumsuz yanıt döneceğiz.
                try
                {
                    string DosyaIsmi = "";
                    var File = Request.Files;
                    // Resmin uzantısını alıyoruz. Split ile nokta ı baz alarak Array dizini oluşturuyoruz.
                    string[] Array = File[0].FileName.Split('.');
                    // Arraydaki son değeri almalıyız.
                    // Array daki değerleri alabilmemiz için Array[0] şeklinde yazmalıyız.
                    // Array içinde 3 kayıt varsa bunların numaraları 0 (sıfır) dan başlayacağı için 0,1,2 olacaktır.
                    // Aşağıda toplam kayıt değerini alıyoruz ve -1 yapıyoruz ki array da doğru değeri alalım.
                    var Uzanti = Array[Array.Length - 1];
                    // GUID ile yeni bir isim oluşturuyoruz.
                    string YeniAd = Guid.NewGuid().ToString();
                    // İsmi ve uzantıyı birleştiriyoruz.
                    DosyaIsmi = YeniAd + "." + Uzanti;
                    // Resmi klasöre kayıt ediyoruz.
                    File[0].SaveAs(System.Web.HttpContext.Current.Server.MapPath("~/Assets/Uploads/" + DosyaIsmi));
                    // Veritabanına DosyaIsmı değerini kayıt edeceğiz.

                    var Film = new Filmler()
                    {
                        FilmAdi = FilmAdi,
                        Resim = DosyaIsmi,
                        SalonID = SalonID,
                        SeansID = SeansID,
                        Yonetmen = Yonetmen,
                        FilmTuru = FilmTuru
                    };
                    db.Filmler.Add(Film);
                    db.SaveChanges();
                    return Json(true);
                }
                catch (Exception ex)
                {
                    return Json(false);
                }
            }
        }

        public JsonResult FilmGetir(int FilmID)
        {
            var FilmGetir = (new CinemaAutomationEntities()).Filmler.Where(w => w.ID == FilmID).FirstOrDefault();
            var Film = new ModelFilm
            {
                FilmAdı = FilmGetir.FilmAdi,
                FilmTuru = FilmGetir.FilmTuru,
                Resim = FilmGetir.Resim,
                Yonetmen = FilmGetir.Yonetmen
            };
            Film.Salon = new Salonlar()
            {
                ID = FilmGetir.Salonlar.ID,
                SalonAdi = FilmGetir.Salonlar.SalonAdi
            };
            Film.Seans = new Seanslar()
            {
                ID = FilmGetir.Seanslar.ID,
                SeansAdi = FilmGetir.Seanslar.SeansAdi,
                Tarih = FilmGetir.Seanslar.Tarih,
                Saat = FilmGetir.Seanslar.Saat,
            };
            return Json(Film);
        }
    }
}