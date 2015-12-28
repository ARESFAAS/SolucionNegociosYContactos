using NegociosYContactos.CustomAttributes;
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

        public ActionResult SaveUser(User user)
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
                UserAutenticated = userAuthenticated;
                UserAutenticated.UserImage = user.UserImage;
            }
            else
            {
                user.Message = "No pudimos procesar tu solicitud, probablemente aun no estás registrado o los datos que ingresaste no son correctos, prueba otra vez !!!";
            }

            return Json(new { Message = user.Message, Authenticated = UserAutenticated });
        }

        public ActionResult LoginUserExternalProvider(User user)
        {
            Data.Classes.IData data = new Data.Classes.Data();
            if (!string.IsNullOrEmpty(user.Email))
            {
                // El usuario es nuevo, se intenta obtener el usuario a partir el email registrado      
                User userAuthenticated = data.GetUserForLoginExternal(user);
                if (userAuthenticated == null)
                {
                    user.Id = Guid.NewGuid().ToString();
                    UserAutenticated = data.SaveUser(user);
                    UserAutenticated.Message = "¡¡¡Correcto, ya estas registrado y autenticado, continua navegando nuestro sitio!!!";
                    UserAutenticated.IsAuthenticated = true;
                    UserAutenticated.UserImage = user.UserImage;
                    user.Message = "¡¡¡Correcto, ya estas registrado y autenticado, continua navegando nuestro sitio!!!";

                }
                else
                {
                    userAuthenticated.IsAuthenticated = true;
                    userAuthenticated.Message = "¡¡¡Correcto, ya estás autenticado, continua navegando nuestro sitio!!!";
                    user.Message = "¡¡¡Correcto, ya estás autenticado, continua navegando nuestro sitio!!!";
                    userAuthenticated.UserImage = user.UserImage;
                    UserAutenticated = userAuthenticated;
                }                
            }
            else
            {
                user.Message = "Lo sentimos... no tenemos sufientes datos para registrate con " + user.LoginProvider + " Intenta con otro proveedor de acceso o registrate directamente en nuestro sitio";
            }

            var completeUserData = false;
            
            if (UserAutenticated != null && UserAutenticated.IsAuthenticated)
            {
                if(string.IsNullOrEmpty(UserAutenticated.IdentificationNumber) ||
                    string.IsNullOrEmpty(UserAutenticated.IdentificationType) ||
                    string.IsNullOrEmpty(UserAutenticated.Email) ||
                    string.IsNullOrEmpty(UserAutenticated.Phone) ||
                    string.IsNullOrEmpty(UserAutenticated.UserName))
                {
                    completeUserData = true;
                }
                    
            }            
            
            return Json(new { Message = user.Message, Authenticated = UserAutenticated, CompleteUserData = completeUserData });
        }

        public ActionResult Logout() {
            UserAutenticated = null;
            return Json(new { Message = "Ok" });
        }

        public JsonResult UpdateUserCountry(string country)
        {
            if (UserAutenticated != null)
            {
                UserAutenticated.UserCountry = country;
            }
            Country = country;
            return Json(new { Country = country.ToUpper() });
        }

        [BasicAuth]
        public ActionResult Edit()
        {
            ViewBag.Message = "Negocios y Contactos - Actualización de Datos";
            return View(UserAutenticated);
        }

        [BasicAuth]
        public ActionResult EditUser(User user)
        {
            Data.Classes.IData data = new Data.Classes.Data();
            user.Id = UserAutenticated.Id;
            
            var result = data.EditUser(user);
            if (result.Message.Equals("emailExists"))
            {
                result.Message = "Lo sentimos... ya tenemos un usuario registrado con los mismos datos";
            }
            else
            {
                UserAutenticated.Email = result.Email;
                UserAutenticated.Password = result.Password;
                UserAutenticated.Phone = result.Phone;
                UserAutenticated.UserName = result.UserName;
                
                result.Message = "¡¡¡Correcto, ya completaste tu registro, continua navegando nuestro sitio!!!";
            }
            return Json(new { Message = result.Message });
        }
    }
}