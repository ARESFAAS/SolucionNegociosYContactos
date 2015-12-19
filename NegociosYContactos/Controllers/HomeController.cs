using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NegociosYContactos.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Negocios y Contactos - Acerca de";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Negocios y Contactos - Contacto";
            return View();
        }
    }
}