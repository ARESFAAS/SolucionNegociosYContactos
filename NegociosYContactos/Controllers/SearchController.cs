using NegociosYContactos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NegociosYContactos.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index()
        {
            // TODO: obtener listado de items mas buscados (6)
            return View();
        }

        public ActionResult Room(string searchWord)
        {
            ViewBag.Title = searchWord;
            // TODO: identificar si searchWord es categoria, negocio o producto
            // TODO: dependiendo del tipo searchWord, redireccionar a las vistas de categoria, negocio o producto
            // TODO: Obtener lista de negocios paginada y con filtro por tipo, producto o categoria
            SearchListViewModel result = new SearchListViewModel();
            result.SearchList = new List<SearchViewModel>();
            result.SearchList.Add(new SearchViewModel { Item = "Negocio1" });
            result.SearchList.Add(new SearchViewModel { Item = "Negocio2" });
            result.SearchList.Add(new SearchViewModel { Item = "Negocio3" });
            result.SearchList.Add(new SearchViewModel { Item = "Negocio4" });
            result.SearchList.Add(new SearchViewModel { Item = "Negocio5" });
            result.SearchList.Add(new SearchViewModel { Item = "Negocio6" });
            result.SearchList.Add(new SearchViewModel { Item = "Negocio7" });
            result.SearchList.Add(new SearchViewModel { Item = "Negocio8" });
            result.SearchList.Add(new SearchViewModel { Item = "Negocio9" });
            result.SearchList.Add(new SearchViewModel { Item = "Negocio10" });
            result.SearchList.Add(new SearchViewModel { Item = "Negocio11" });
            result.SearchList.Add(new SearchViewModel { Item = "Negocio12" });
            result.SearchList.Add(new SearchViewModel { Item = "Negocio13" });
            result.SearchList.Add(new SearchViewModel { Item = "Negocio14" });
            result.SearchList.Add(new SearchViewModel { Item = "Negocio15" });
            return View(result);
        }
    }
}