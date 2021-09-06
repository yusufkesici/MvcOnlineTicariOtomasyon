using MvcTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTicariOtomasyon.Controllers
{
    [Authorize]
    public class SatisController : Controller
    {
        // GET: Satis
        Context c = new Context();
        public ActionResult Index()
        {
            var satislar = c.SatisHarekets.ToList();
            return View(satislar);
        }
        [HttpGet]
        public ActionResult SatisEkle()
        {
            List<SelectListItem> urunler = (from x in c.Uruns.Where(x=>x.Durum==true).ToList()
                                           select new SelectListItem
                                           {

                                               Text = x.UrunAd,
                                               Value = x.UrunID.ToString()
                                           }).ToList();
            
            List<SelectListItem> personeller = (from x in c.Personels.ToList()
                                            select new SelectListItem
                                            {

                                                Text = x.PersonelAd +" "+x.PersonelSoyad,
                                                Value = x.PersonelID.ToString()
                                            }).ToList();
            
            List<SelectListItem> cariler = (from x in c.Carilers.ToList()
                                                select new SelectListItem
                                                {

                                                    Text = x.CariAd + " " + x.CariSoyad,
                                                    Value = x.CariID.ToString()
                                                }).ToList();
           

            ViewBag.cariler = cariler;

            ViewBag.personeller = personeller;

            ViewBag.urunler = urunler;

            return View();
        }
        [HttpPost]
        public ActionResult SatisEkle(SatisHareket satis)
        {
            ViewBag.göster = true;
            satis.Tarih = DateTime.Parse(DateTime.Now.ToShortTimeString());
            c.SatisHarekets.Add(satis);

            Urun urun = c.Uruns.Find(satis.UrunID);
            int satilan=satis.Adet;
            urun.Stok = (short)(urun.Stok - satilan);
            if (urun.Stok < 0)
            {
                List<SelectListItem> urunler = (from x in c.Uruns.Where(x => x.Durum == true).ToList()
                                                select new SelectListItem
                                                {

                                                    Text = x.UrunAd,
                                                    Value = x.UrunID.ToString()
                                                }).ToList();

                List<SelectListItem> personeller = (from x in c.Personels.ToList()
                                                    select new SelectListItem
                                                    {

                                                        Text = x.PersonelAd + " " + x.PersonelSoyad,
                                                        Value = x.PersonelID.ToString()
                                                    }).ToList();

                List<SelectListItem> cariler = (from x in c.Carilers.ToList()
                                                select new SelectListItem
                                                {

                                                    Text = x.CariAd + " " + x.CariSoyad,
                                                    Value = x.CariID.ToString()
                                                }).ToList();


                ViewBag.cariler = cariler;

                ViewBag.personeller = personeller;

                ViewBag.urunler = urunler;

                ViewBag.göster = false;

                return View("SatisEkle");
                
            }
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        int ilkSatilan;
        
       
        [HttpGet]
        public ActionResult SatisGüncelle(int id)
        {
            List<SelectListItem> urunler = (from x in c.Uruns.Where(x => x.Durum == true).ToList()
                                            select new SelectListItem
                                            {

                                                Text = x.UrunAd,
                                                Value = x.UrunID.ToString()
                                            }).ToList();

            List<SelectListItem> personeller = (from x in c.Personels.ToList()
                                                select new SelectListItem
                                                {

                                                    Text = x.PersonelAd + " " + x.PersonelSoyad,
                                                    Value = x.PersonelID.ToString()
                                                }).ToList();

            List<SelectListItem> cariler = (from x in c.Carilers.ToList()
                                            select new SelectListItem
                                            {

                                                Text = x.CariAd + " " + x.CariSoyad,
                                                Value = x.CariID.ToString()
                                            }).ToList();


            ViewBag.cariler = cariler;

            ViewBag.personeller = personeller;

            ViewBag.urunler = urunler;

            

            SatisHareket satis = c.SatisHarekets.Find(id);
            ilkSatilan = satis.Adet;
            

            return View(satis);
        }
        [HttpPost]
        public ActionResult SatisGüncelle(SatisHareket satis)
        {

            SatisHareket satiseski = c.SatisHarekets.Find(satis.SatisID);
            ilkSatilan = satiseski.Adet;

            ViewBag.göster = true;

            

            Urun urun = c.Uruns.Find(satis.UrunID);
            int satilan = satis.Adet;
            int ilkDeger = urun.Stok;
            int sonDeger = (short)(urun.Stok - satilan);
            if ( satilan> ilkSatilan)
            {
                urun.Stok= (short)(urun.Stok - (satilan-ilkSatilan));
                c.SaveChanges();
            }
            if (satilan <= ilkSatilan)
            {
                urun.Stok = (short)(urun.Stok + (ilkSatilan-satilan));
                c.SaveChanges();
            }

            if (urun.Stok < 0)
            {
                List<SelectListItem> urunler = (from x in c.Uruns.Where(x => x.Durum == true).ToList()
                                                select new SelectListItem
                                                {

                                                    Text = x.UrunAd,
                                                    Value = x.UrunID.ToString()
                                                }).ToList();

                List<SelectListItem> personeller = (from x in c.Personels.ToList()
                                                    select new SelectListItem
                                                    {

                                                        Text = x.PersonelAd + " " + x.PersonelSoyad,
                                                        Value = x.PersonelID.ToString()
                                                    }).ToList();

                List<SelectListItem> cariler = (from x in c.Carilers.ToList()
                                                select new SelectListItem
                                                {

                                                    Text = x.CariAd + " " + x.CariSoyad,
                                                    Value = x.CariID.ToString()
                                                }).ToList();


                ViewBag.cariler = cariler;

                ViewBag.personeller = personeller;

                ViewBag.urunler = urunler;

                ViewBag.göster = false;

                return View("SatisGüncelle");

            }



            SatisHareket satis2 = c.SatisHarekets.Find(satis.SatisID);
            satis2.Adet = satis.Adet;
            satis2.PersonelID = satis.PersonelID;
            satis2.CariID = satis.CariID;
            satis2.UrunID = satis.UrunID;
            
            satis2.Fiyat = satis.Fiyat;
            satis2.ToplamTutar = satis.ToplamTutar;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SatisDetay(int id)
        {
            var satislar = c.SatisHarekets.Where(x=>x.SatisID==id).ToList();
            return View(satislar);
        }
    }
}