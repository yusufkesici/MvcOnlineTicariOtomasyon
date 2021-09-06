using MvcTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTicariOtomasyon.Controllers
{
    [Authorize]
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c = new Context();
        public ActionResult Index()
        {
            var personeller = c.Personels.ToList();
            return View(personeller);
        }
        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.Where(x=>x.Durum==true).ToList()
                                           select new SelectListItem
                                           {

                                               Text = x.DepartmanAd,
                                               Value = x.DepartmanID.ToString()
                                           }).ToList();
            

            ViewBag.dgr = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel personel)
        {
            

            personel.Durum = true;
            c.Personels.Add(personel);
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult PersonelGüncelle(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Departmans.ToList()
                                           select new SelectListItem
                                           {

                                               Text = x.DepartmanAd,
                                               Value = x.DepartmanID.ToString()
                                           }).ToList();
            deger1.Insert(0, new SelectListItem { Text = "--Seçiniz--", Value = "" });

            ViewBag.dgr = deger1;

            Personel personel = c.Personels.Find(id);


            return View(personel);
        }
        [HttpPost]
        public ActionResult PersonelGüncelle(Personel personel)
        {
            

            Personel personel2 = c.Personels.Find(personel.PersonelID);
            personel2.PersonelAd = personel.PersonelAd;
            personel2.PersonelSoyad = personel.PersonelSoyad;
            personel2.PersonelGorsel = personel.PersonelGorsel;
            personel2.DepartmanID = personel.DepartmanID;
            c.SaveChanges();


            return RedirectToAction("Index");
        }
    }
}