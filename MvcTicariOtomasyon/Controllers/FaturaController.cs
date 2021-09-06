using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTicariOtomasyon.Models.Sınıflar;

namespace MvcTicariOtomasyon.Controllers
{
    [Authorize]
    public class FaturaController : Controller
    {
        Context c = new Context();
        // GET: Fatura
        public ActionResult Index()
        {
            var faturalar = c.Faturalars.ToList();
            return View(faturalar);
        }
        [HttpGet]
        public ActionResult FaturaEkle()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult FaturaEkle(Faturalar fatura)
        {
            fatura.Tarih =DateTime.Parse( DateTime.Now.ToString());
            fatura.Saat =DateTime.Parse( DateTime.Now.ToString());
            c.Faturalars.Add(fatura);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult FaturaGüncelle(int id)
        {
            Faturalar fatura = c.Faturalars.Find(id);
            return View(fatura);
        }
        [HttpPost]
        public ActionResult FaturaGüncelle(Faturalar fatura)
        {
            Faturalar fatura2 = c.Faturalars.Find(fatura.FaturaID);
            fatura2.FaturaSeriNo = fatura.FaturaSeriNo;
            fatura2.FaturaSıraNo = fatura.FaturaSıraNo;
            fatura2.TeslimAlan = fatura.TeslimAlan;
            fatura2.TeslimEden = fatura.TeslimEden;
            fatura2.VergiDairesi = fatura.VergiDairesi;
            fatura2.Tarih = fatura.Tarih;
            fatura2.Saat = fatura.Saat;

            c.SaveChanges();
            return RedirectToAction("Index");
        }
       
        public ActionResult FaturaDetay(int id)
        {
            Faturalar fatura = c.Faturalars.Find(id);
            ViewBag.faturasahibi = fatura.TeslimAlan;
            ViewBag.faturasahibiID= fatura.FaturaID;
           
            var faturakalemler = c.FaturaKalems.Where(x => x.FaturaID == id).ToList();

            return View(faturakalemler);
        }
        [HttpGet]
        public ActionResult FaturaKalemEkle(int id)
        {
            ViewBag.faturaid = id;
            return View();
        }
        [HttpPost]
        public ActionResult FaturaKalemEkle(FaturaKalem faturaKalem)
        {
            
            c.FaturaKalems.Add(faturaKalem);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}