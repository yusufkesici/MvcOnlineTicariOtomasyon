using MvcTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcTicariOtomasyon.Controllers
{
    public class LoginController : Controller
    {

        Context c = new Context();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult PartialCari()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult PartialCari(Cariler cari)
        {
            
            c.Carilers.Add(cari);
            c.SaveChanges();
            return PartialView();
        }

        [HttpGet]
        public ActionResult CariLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CariLogin(Cariler cari)
        {
            var bilgiler = c.Carilers.FirstOrDefault(x => x.CariMail == cari.CariMail && x.CariSifre == cari.CariSifre);
            if (bilgiler!=null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.CariMail, false);
                Session["CariMail"] = bilgiler.CariMail.ToString();

                return RedirectToAction("Index","CariPanel");
            }
            return RedirectToAction("Index", "Login");
        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AdminLogin(Admin admin)
        {
            var bilgiler = c.Admins.FirstOrDefault(x => x.KullaniciAdi == admin.KullaniciAdi && x.Sifre == admin.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullaniciAdi, false);
                Session["KullaniciAdi"] = bilgiler.KullaniciAdi.ToString();

              return  RedirectToAction("Index", "Galeri");
            }
           


                return RedirectToAction("Index", "Login");
            
        }
    }
}