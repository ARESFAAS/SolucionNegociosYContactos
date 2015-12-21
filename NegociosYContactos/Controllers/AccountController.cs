using NegociosYContactos.Models;
using System;
using System.Web.Mvc;

namespace NegociosYContactos.Controllers
{
    public class AccountController : BaseController
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register() {
            ViewBag.Message = "Negocios y Contactos - Registro";
            return View("Index");
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

        public ActionResult Login()
        {
            ViewBag.Message = "Negocios y Contactos - Ingreso";
            return View();
        }

        public ActionResult LoginUser(User user)
        {
            Data.Classes.IData data = new Data.Classes.Data();
            // El usuario existe, se realiza autenticación    
            User userAuthenticated = data.GetUserForLogin(user);
            if (userAuthenticated != null)
            {
                userAuthenticated.IsAuthenticated = true;
                userAuthenticated.Message = "¡¡¡Correcto, ya estás autenticado, continua navegando nuestro sitio!!!";
                user.Message = "¡¡¡Correcto, ya estás autenticado, continua navegando nuestro sitio!!!";
                this.UserAutenticated = userAuthenticated;
            }
            else
            {
                user.Message = "No pudimos procesar tu solicitud, probablemente aun no estás inscrito";
            }

            return Json(new { Message = user.Message });
        }
    }
}