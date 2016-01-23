using NegociosYContactos.Models;
using System;
using System.Net.Mail;
using System.Web.Mvc;

namespace NegociosYContactos.Controllers
{
    public class BaseController : Controller
    {
        public User UserAutenticated
        {
            get
            {
                return (User)Session["UserAuthenticated"];
            }
            set
            {
                Session["UserAuthenticated"] = value;
            }
        }

        public BusinessWeb BusinessWeb
        {
            get
            {
                if(Session["BusinessWeb"] == null)
                {
                    Session["BusinessWeb"] = new BusinessWeb();
                    ((BusinessWeb)Session["BusinessWeb"]).User = new BusinessUserWeb();
                    ((BusinessWeb)Session["BusinessWeb"]).Products = new System.Collections.Generic.List<BusinessProductWeb>();
                }
                return (BusinessWeb)Session["BusinessWeb"];
            }
            set
            {
                Session["BusinessWeb"] = value;
            }
        }

        public string Country
        {
            get
            {
                return (string)Session["Country"];
            }
            set
            {
                Session["Country"] = value;
            }
        }

        public void SendMailBase(string body, string subject, string to)
        {            
            var message = new MailMessage();
            message.To.Add(new MailAddress(to));
            message.Subject = subject;
            message.Body = body;
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
            }
        }
    }
}