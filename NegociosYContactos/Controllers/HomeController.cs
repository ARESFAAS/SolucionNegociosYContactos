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
            var body = "<p>Email Desde: {0} ({1})</p><p>Mensaje:</p><p>{2}</p>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(System.Configuration.ConfigurationManager.AppSettings["contactEmail"])); //replace with valid value
            message.Subject = "Contacto de cliente: " + name;
            message.Body = string.Format(body, name, email, textMessage);
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {
                try
                {
                    smtp.Send(message);
                }
                catch (Exception)
                {
                    throw;
                }
                return View("Contact");
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