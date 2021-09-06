using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTicariOtomasyon.Models.Sınıflar;
using PagedList;
using PagedList.Mvc;


namespace MvcTicariOtomasyon.Controllers
{
    [Authorize]
    public class UrunController : Controller
    {

        // GET: Urun
        Context c = new Context();
        public ActionResult Index(string p,int sayfa =1)
        {
            var urunler = c.Uruns.Where(x => x.Durum == true).ToList().ToPagedList(sayfa, 4);
            if (!string.IsNullOrEmpty(p))
            {
                 urunler = c.Uruns.Where(x => x.Durum == true && x.UrunAd.Contains(p)).ToList().ToPagedList(sayfa, 4);
            }
           
            return View(urunler);
        }
        public ActionResult UrunListe()
        {
            var urunler = c.Uruns.ToList();
            return View(urunler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();
            

            ViewBag.dgr = deger1;
            ViewBag.kategoriler = c.Kategoris.ToList();
            

            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Urun urun)
        {
            urun.Durum = true;
            c.Uruns.Add(urun);
          
            
                
                c.SaveChanges();
               
            

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UrunGuncelle(int id)
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriID.ToString()
                                           }).ToList();

            ViewBag.dgr = deger1;

            Urun urun = c.Uruns.Find(id);
            ViewBag.kategoriler = c.Kategoris.ToList();
            

            return View(urun);
        }
        [HttpPost]
        public ActionResult UrunGuncelle(Urun urun)
        {
           
            Urun urun2 = c.Uruns.Find(urun.UrunID);

            urun2.UrunAd = urun.UrunAd;
            urun2.Stok = urun.Stok;
            urun2.Marka = urun.Marka;
            urun2.AlisFiyat = urun.AlisFiyat;
            urun2.SatisFiyat = urun.SatisFiyat;
            urun2.KategoriID = urun.KategoriID;
            urun2.UrunGorsel = urun.UrunGorsel;
            urun2.Durum = true;

            c.SaveChanges();
            

            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var urun = c.Uruns.Find(id);
            urun.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}