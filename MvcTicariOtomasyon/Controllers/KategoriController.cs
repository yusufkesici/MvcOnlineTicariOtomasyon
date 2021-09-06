using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTicariOtomasyon.Models.Sınıflar;

namespace MvcTicariOtomasyon.Controllers
{
    [Authorize]
    public class KategoriController : Controller
    {
        Context c = new Context();
        public ActionResult Index()
        {
           // Context c = new Context();
            var degerler = c.Kategoris.ToList();

            return View(degerler);
        }
        [HttpGet]
        public ActionResult KategoriEkle()
        {

            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori kategori)
        {
            c.Kategoris.Add(kategori);
            c.SaveChanges();

            return RedirectToAction("Index");            
        }
        public ActionResult KategoriSil(int id)
        {

            Kategori ktg = c.Kategoris.Find(id);
            c.Kategoris.Remove(ktg);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
       [HttpGet]
        public ActionResult KategoriGüncelle(int id)
        {
            Kategori kategori=c.Kategoris.Find(id);

            return View(kategori);
        }
        [HttpPost]
        public ActionResult KategoriGüncelle(Kategori k)
        {
            Kategori kategori = c.Kategoris.Find(k.KategoriID);
            kategori.KategoriAd=k.KategoriAd;
            c.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}