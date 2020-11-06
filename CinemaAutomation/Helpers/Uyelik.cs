using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CinemaAutomation.Models;

namespace CinemaAutomation.Helpers
{
    public class Uyelik
    {
        public static string CookieName = "CinemaAutomation";
        /// <summary>
        /// Yönetici girişi
        /// </summary>
        /// <param name="KullaniciAdi"></param>
        /// <param name="Sifre"></param>
        /// <returns></returns>
        public static bool Giris(string KullaniciAdi, string Sifre)
        {
            bool Islem = false;
            CinemaAutomationEntities db = new CinemaAutomationEntities();
            string Token = Guid.NewGuid().ToString();

            // Yönetici bulunuyor
            var Bul = db.Yoneticiler.Where(w => w.KullaniciAdi == KullaniciAdi & w.Sifre == Sifre).FirstOrDefault();

            if (Bul != null) // Yönetici var mı kontrol ediliyor.
            {
                try
                {
                    Bul.Token = Token;
                    Islem = true;
                    //giriş başarılı ise cookie kaydı yapılıyor
                    HttpCookie cookie = new HttpCookie(CookieName);
                    cookie["Token"] = Token;
                    cookie.Expires = DateTime.Now.AddDays(1); // Cookie aktiflik durumuna + 1 gün veriyoruz. Bir gün otomatik giriş yapıcak.
                    HttpContext.Current.Response.Cookies.Add(cookie);

                    // Token değeri veritabanına kayıt ediliyor.
                    db.SaveChanges();
                }
                catch (Exception)
                {
                    Islem = false;
                }
            }
            return Islem;
        }
        public static bool Cikis()
        {
            bool sonuc = true;
            try
            {
                HttpCookie yenicookie = new HttpCookie(CookieName);
                yenicookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Add(yenicookie);
            }
            catch (Exception)
            {
                sonuc = false;
            }
            return sonuc;
        }
        /// <summary>
        /// Giriş yapan yönetici bilgileri getirir
        /// </summary>
        /// <returns></returns>
        public static Yoneticiler Yonetici()
        {
            var AdminGetir = new Yoneticiler();
            if (HttpContext.Current.Request.Cookies[CookieName] != null)
            {
                if (HttpContext.Current.Request.Cookies[CookieName]["Token"] != null)
                {
                    string Token = HttpContext.Current.Request.Cookies[CookieName]["Token"].ToString();
                    AdminGetir = (new CinemaAutomationEntities()).Yoneticiler.Where(w => w.Token == Token).FirstOrDefault();
                }
            }
            return AdminGetir;
        }
        /// <summary>
        /// Üye giriş yaptımı kontrolü
        /// </summary>
        /// <returns></returns>
        public static bool GirisKontrol()
        {
            // cookie varmı?
            if (HttpContext.Current.Request.Cookies[CookieName] != null)
            {
                // Token cookie si alınıyor
                var CookieToken = HttpContext.Current.Request.Cookies[CookieName]["Token"];
                // cookiede veri varmı
                if (CookieToken != null)
                {
                    if ((new CinemaAutomationEntities()).Yoneticiler.Where(w => w.Token == CookieToken.ToString()).FirstOrDefault() != null)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}