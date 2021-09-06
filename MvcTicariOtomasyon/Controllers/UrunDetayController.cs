using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTicariOtomasyon.Models.Sınıflar;

namespace MvcTicariOtomasyon.Controllers
{
    [Authorize]
    public class UrunDetayController : Controller
    {
        // GET: UrunDetay
        Context c = new Context();
        public ActionResult Index(int id)
        {
            Class1 cs = new Class1();
            cs.Deger1 = c.Uruns.Where(x => x.UrunID == id).ToList();
            cs.Deger2 = c.Detays.Where(x => x.DetayID == id).ToList();

            return View(cs);
        }
    }
}