using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTicariOtomasyon.Models.Sınıflar;

namespace MvcTicariOtomasyon.Controllers
{
    [Authorize]
    public class DepartmanController : Controller
    {
        // GET: Departman
        Context c = new Context();
        public ActionResult Index()
        {
            var departmanlar = c.Departmans.Where(x=>x.Durum==true).ToList();
            return View(departmanlar);
        }
        [HttpGet]
        public ActionResult DepartmanEkle()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult DepartmanEkle(Departman departman)
        {
            departman.Durum = true;
            c.Departmans.Add(departman);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanSil(int id)
        {
            Departman departman = c.Departmans.Find(id);
            departman.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult DepartmanGüncelle(int id)
        {
            Departman departman = c.Departmans.Find(id);

            return View(departman);
        }
        [HttpPost]
        public ActionResult DepartmanGüncelle(Departman departman)
        {
            Departman departman2 = c.Departmans.Find(departman.DepartmanID);
            departman2.DepartmanAd = departman.DepartmanAd;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult DepartmanDetay(int id)
        {
            Departman departman = c.Departmans.Find(id);
            ViewBag.departmanAdı = departman.DepartmanAd;
            var personeller = c.Personels.Where(x => x.DepartmanID == id).ToList();
            return View(personeller);
        }
        public ActionResult DepartmanPersonelSatıs(int id)
        {
            Personel personel = c.Personels.Find(id);
            ViewBag.personel = personel.PersonelAd +" "+personel.PersonelSoyad;

            //Hangi personelin ne kadar satış yaptıgı
            var satis = c.SatisHarekets.Where(x => x.PersonelID == id).ToList();
            ViewBag.satısAdet = satis.Count().ToString();

            var degerler = c.SatisHarekets.Where(x => x.PersonelID == id).ToList();

            return View(degerler);
        }
    }
}