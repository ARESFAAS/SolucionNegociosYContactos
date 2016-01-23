using System;
using System.Text;
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
                StringBuilder body = new StringBuilder();
                body.Append("<html>");
                body.Append("<body>");
                body.Append("<div>");
                body.Append("<br/>");
                body.Append("<h2>Negocios y Contactos</h2>");
                body.Append("</div>");
                body.Append("<br>");
                body.Append("<div>");
                body.Append("<p>{0}</p>");
                body.Append("<p>{1}</p>");
                body.Append("<p>Cordialmente,</p>");
                body.Append("<p>Negocios y Contactos</p>");
                body.Append("</div>");
                body.Append("<br/>");
                body.Append("</body>");
                body.Append("</html>");

                SendMailBase(string.Format(body.ToString(), email, textMessage), "Negocios y Contactos - " + "Contacto de cliente: " + name, System.Configuration.ConfigurationManager.AppSettings["contactEmail"]);

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