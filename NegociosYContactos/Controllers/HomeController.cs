using System;
using System.Net.Mail;
using System.Web.Mvc;

namespace NegociosYContactos.Controllers
{
    public class HomeController : BaseController
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

        public ViewResult SendMail(string name, string email, string textMessage)
        {
            try
            {
                SendMailBase("Contacto de cliente: " + name, email, textMessage);
                return View("Contact");
            }
            catch (Exception)
            {

                throw;
            }            
        }

        public ActionResult Terms()
        {
            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }

        public ActionResult PartialTerms()
        {
            return PartialView("_Terms");
        }

        public ActionResult PartialPrivacyPolicy()
        {
            return PartialView("_PrivacyPolicy");
        }
    }
}