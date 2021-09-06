using MvcTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTicariOtomasyon.Controllers
{
    [Authorize]
    public class CariController : Controller
    {
        // GET: Cari
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Carilers.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult CariEkle()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult CariEkle(Cariler cari)
        {
            if (!ModelState.IsValid)
            {
                return View("CariEkle");
            }
            c.Carilers.Add(cari);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult CariSil(int id)
        {
            Cariler cari = c.Carilers.Find(id);
            c.Carilers.Remove(cari);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CariGüncelle(int id)
        {
            Cariler cari = c.Carilers.Find(id);
           
            return View(cari);
        }
        [HttpPost]
        public ActionResult CariGüncelle(Cariler cari)
        {
            Cariler cari2 = c.Carilers.Find(cari.CariID);
            cari2.CariAd = cari.CariAd;
            cari2.CariSoyad = cari.CariSoyad;
            cari2.CariSehir = cari.CariSehir;
            cari2.CariMail = cari.CariMail;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariSatis(int id)
        {
            var satislar = c.SatisHarekets.Where(x => x.CariID == id).ToList();

            Cariler cari = c.Carilers.Find(id);
            ViewBag.cari = cari.CariAd + " " + cari.CariSoyad;

            ViewBag.satınAlmaAdet= satislar.Count().ToString();

            return View(satislar);
        }
    }
}