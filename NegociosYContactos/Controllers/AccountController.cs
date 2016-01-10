using NegociosYContactos.CustomAttributes;
using NegociosYContactos.Data.Classes;
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
            var redirect = false;
            // El usuario es nuevo          
            if (data.GetUser(user) == null)
            {
                user.Id = Guid.NewGuid().ToString();
                var result = data.SaveUser(user);
                user.Message = "¡¡¡Correcto, ya estas registrado, continua navegando nuestro sitio!!!";
                redirect = true;              
            }
            else
            {
                user.Message = "Lo sentimos... ya tenemos un usuario registrado con los mismos datos";
            }
            return Json(new { Message = user.Message, RedirectLogin = redirect });
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

        [BasicAuth]
        public ActionResult Logout() {
            UserAutenticated = null;
            BusinessWeb = null;
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
                UserAutenticated.IdentificationType = result.IdentificationType;
                UserAutenticated.IdentificationNumber = result.IdentificationNumber;
                
                result.Message = "¡¡¡Correcto, ya completaste tu registro, continua navegando nuestro sitio!!!";
            }
            return Json(new { Message = result.Message });
        }

        public JsonResult SendPassword(string email)
        {
            string message = string.Empty;
            string name = string.Empty;
            string textMessage = string.Empty;
            try
            {
                if (UserAutenticated != null)
                {
                    if (!UserAutenticated.Email.Equals(email))
                    {
                        message = "Lo sentimos, los datos de correo electrónico que ingresaste no concuerdan con nuestros registros," +
                            " intenta nuevamente o comúnicate con nosotros a través de nuestro formulario de contacto";
                    }
                    else
                    {
                        message = "Por favor revisa tu correo electrónico, hemos enviado un recordatorio de tu contraseña";
                        name = "Apreciado " + UserAutenticated.UserName;
                        textMessage = "su contraseña es: " + UserAutenticated.Password;
                        SendMailBase(name, email, textMessage);
                    }
                }
                else
                {
                    User user = new Models.User();
                    user.Email = email;
                    IData data = new Data.Classes.Data();
                    user = data.GetUser(user);
                    if (user != null)
                    {
                        message = "Por favor revisa tu correo electrónico, hemos enviado un recordatorio de tu contraseña";
                        name = "Apreciado " + user.UserName;
                        textMessage = "su contraseña es: " + user.Password;
                        SendMailBase(name, email, textMessage);
                    }
                    else
                    {
                        message = "Lo sentimos, los datos de correo electrónico que ingresaste no concuerdan con nuestros registros," +
                            " intenta nuevamente o comúnicate con nosotros a través de nuestro formulario de contacto";
                    }
                }
                return Json(new { Message = message });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult ValidateUserName(string userName)
        {
            var result = false;
            var message = string.Empty;
            IData data = new Data.Classes.Data();
            result = data.ValidateUserName(userName);
            if (result)
            {
                message = "¡¡¡Lo sentimos, el nombre de usuario que intentas guardar ya alguien más lo tiene, prueba con otro!!!";
            }
            return Json(new { Result = result, Message = message });
        }
    }
}