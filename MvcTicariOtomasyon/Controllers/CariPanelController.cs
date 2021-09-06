using MvcTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTicariOtomasyon.Controllers
{
    [Authorize]
    public class CariPanelController : Controller
    {
        Context c = new Context();
        // GET: CariPanel
        
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var deger = c.Carilers.FirstOrDefault(x => x.CariMail == mail);
            ViewBag.m = mail;
            return View(deger);
        }

        
        [HttpPost]
        
        public ActionResult Güncelle(Cariler cari)
        {
            Cariler cari2 = c.Carilers.Find(cari.CariID);
            cari2.CariAd = cari.CariAd;
            cari2.CariSoyad = cari.CariSoyad;
            cari2.CariSehir = cari.CariSehir;
            cari2.CariSifre = cari.CariSifre;
            cari2.CariMail = cari.CariMail;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            Cariler cari = c.Carilers.Where(x => x.CariMail == mail).FirstOrDefault();
            var satislar = c.SatisHarekets.Where(x => x.CariID == cari.CariID).ToList();
            return View(satislar);
        }
    }
}