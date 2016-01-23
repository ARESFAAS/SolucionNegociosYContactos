using NegociosYContactos.Data.Classes;
using NegociosYContactos.Models;
using System.Text;
using System.Web.Mvc;

namespace NegociosYContactos.Controllers
{
    public class SearchController : BaseController
    {
        public SearchListPaginationModel SearchListPagination
        {
            get
            {
                if (Session["SearchListPagination"] == null)
                {
                    Session["SearchListPagination"] = new SearchListPaginationModel();
                }
                return (SearchListPaginationModel)Session["SearchListPagination"];
            }
            set
            {
                Session["SearchListPagination"] = value;
            }
        }

        // GET: Search
        public ActionResult Index()
        {
            // TODO: obtener listado de items mas buscados (6)            
            return View();
        }

        public JsonResult GetAutoComplete() {
            IData data = new Data.Classes.Data();
            return Json(new { DataAutoComplete = data.GetCategory() });
        }

        public ActionResult Room(string searchWord)
        {
            if (string.IsNullOrEmpty(searchWord)) {
                searchWord = string.Empty;
            }
            searchWord = searchWord.Replace('+', ' ');

            ViewBag.Title = searchWord;

            IData data = new Data.Classes.Data();
            int termType = data.TypeTerm_Get(searchWord);

            if (termType == 0) // Business
            {
                return View("Business",data.Business_Get(termType, searchWord));
            }
            else // Category
            {
                SearchListPagination.SearchWord = searchWord;
                SearchListPagination.IdCategory = termType;
                SearchListPagination.Page = 0;
                SearchListPagination.TamPage = 9;
                return View(data.BusinessList_Get(SearchListPagination.IdCategory, SearchListPagination.TamPage, SearchListPagination.Page));
            }
        }

        public ActionResult RoomResults(bool next)
        {
            if (next)
            {
                SearchListPagination.Page += 1;
            }
            else
            {
                SearchListPagination.Page -= 1;
            }
            if (SearchListPagination.Page < 0)
            {
                SearchListPagination.Page = 0;
            }

            ViewBag.Title = SearchListPagination.SearchWord;
            IData data = new Data.Classes.Data();
            return View("Room", data.BusinessList_Get(SearchListPagination.IdCategory, SearchListPagination.TamPage, SearchListPagination.Page));
        }

        public ActionResult PartialProductOrder(string businessName, string businessId, string productId)
        {
            IData data = new Data.Classes.Data();
            var order = data.ProductOrderGet(new ProductOrderWeb
            {
                BusinessName = businessName,
                Id = 0,
                Product = new BusinessProductWeb { Id = int.Parse(productId) },
                ContactEmail = string.Empty,
                ContactPhone = string.Empty,
                OrderType = 0
            });            
            return PartialView("_ProductOrder", order);
        }

        public JsonResult ProductOrderSave(string productId, string orderType, string contactPhone, string contactEmail)
        {
            IData data = new Data.Classes.Data();
            var result = data.ProductOrderSave(new ProductOrderWeb
            {
                Product = new BusinessProductWeb { Id = int.Parse(productId) },
                OrderType = int.Parse(orderType), // 1. Pedir el producto , 2. - ser contactado
                ContactEmail = contactEmail,
                ContactPhone = contactPhone
            });

            var businessUser = data.GetUserForMail(result.Product.Id);

            string name = "Apreciado " + businessUser.UserName + ":";
            string textMessage = "Hay un cliente que quiere ";
            if (result.OrderType == 1)
            {
                textMessage = textMessage + "pedir un producto o servicio. ";
            }
            else
            {
                textMessage = textMessage + "ser contactado por usted para hacer negocios. ";
            }
            textMessage = textMessage + "Los siguientes son los datos de contacto del cliente: <br />";
            textMessage = textMessage + string.Format("Email: {0}, Teléfono: {1}", contactEmail, contactPhone);
            textMessage = textMessage + "<br />Los siguientes son los datos del producto o servicio requerido: <br />";
            textMessage = textMessage + string.Format("Nombre del producto o servicio: {0}", result.Product.Name);
            
            StringBuilder body = new StringBuilder();
            body.Append("<html>");
            body.Append("<body>");
            body.Append("<div>");
            var urlLogo = Request.Url.GetLeftPart(System.UriPartial.Authority) + "/Content/images/LogoContactosYNegocios.png";
            body.Append("<img src=\"" + urlLogo + "\"/>");
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
            body.Append("<span style=\"font-size: x-large; font-family: Webdings; color: green;\">");
            body.Append("<span style=\"font-weight: bold; font-size: 24pt; font-style: italic; font-family: Webdings; color: green;\">P</span>");
            body.Append("</span>");
            body.Append("<span style=\"font-size: xx-small; font-family: Verdana; color: #339966;\"><span style=\"font-size: 8pt; font-family: Verdana;\">&nbsp;Por favor considere el medio ambiente antes de imprimir este correo electrónico!</span>");
            body.Append("</span>");
            body.Append("</html>");

            SendMailBase(string.Format(body.ToString(), name, textMessage), "Negocios y Contactos - " + "Solicitud de producto o servicio", businessUser.Email);

            return Json(new { Result = true, Message = "Su solicitud fue procesada y los datos fueron guardados." });
        }
    }
}