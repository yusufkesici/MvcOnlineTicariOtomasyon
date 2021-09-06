using MvcTicariOtomasyon.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcTicariOtomasyon.Controllers
{
    [Authorize]
    public class PersonelListeController : Controller
    {
        Context c = new Context();
        // GET: PersonelListe
        public ActionResult Index()
        {
            var personeller = c.Personels.ToList();
            return View(personeller);
        }
    }
}