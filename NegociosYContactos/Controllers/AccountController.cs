using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NegociosYContactos.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SaveUser(Models.User user)
        {
            Data.Classes.IData data = new Data.Classes.Data();
            // El usuario es nuevo          
            if (data.GetUser(user) == null)
            {
                user.Id = Guid.NewGuid().ToString();
                data.SaveUser(user);
                user.Message = "¡¡¡Correcto, ya estas registrado, continua navegando nuestro sitio!!!";
            }
            else
            {
                user.Message = "Lo sentimos... ya tenemos un usuario registrado con los mismos datos";
            }
            return Json(new { Message = user.Message });
        }
    }
}