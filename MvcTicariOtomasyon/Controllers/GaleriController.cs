using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcTicariOtomasyon.Models.Sınıflar;

namespace MvcTicariOtomasyon.Controllers
{
    [Authorize]
    public class GaleriController : Controller
    {
        Context c = new Context();
        // GET: Galeri
       
        public ActionResult Index()
        {
            var gorseller = c.Uruns.ToList();
            return View(gorseller);
        }
    }
}