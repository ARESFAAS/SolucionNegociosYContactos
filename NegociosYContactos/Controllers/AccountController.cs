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

        public ViewResult SaveUser(Models.User user)
        {
            Data.Classes.IData data = new Data.Classes.Data();
            user.Id = Guid.NewGuid().ToString();
            data.SaveUser(user);
            return View("Index");
        }
    }
}