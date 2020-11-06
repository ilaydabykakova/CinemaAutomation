using CinemaAutomation.Helpers;
using System.Web.Mvc;

namespace CinemaAutomation.Controllers
{
    [LoginAttiributes(GirisKontroluMu = false)] // false gönderiyoruz: eğer giriş yapmadıysa gelsin buraya
    public class YoneticiController : Controller
    {
        // GET: Yonetici
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GirisYap(string KullaniciAdi, string Sifre)
        {
            return Json(Helpers.Uyelik.Giris(KullaniciAdi, Sifre));
        }
    }
}