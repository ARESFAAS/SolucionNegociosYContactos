using NegociosYContactos.Models;
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
    }
}