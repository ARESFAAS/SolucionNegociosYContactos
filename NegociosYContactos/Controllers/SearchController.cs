using NegociosYContactos.Data.Classes;
using NegociosYContactos.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
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
            return Json(new { DataAutoComplete = data.GetCategoryAutoComplete() });
        }

        public ActionResult Room(string searchWord)
        {
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

        public ActionResult Business(int id, string name)
        {
            IData data = new Data.Classes.Data();         
            return View(data.Business_Get(id, name));
        }
    }
}