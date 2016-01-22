using System;
using System.Collections.Generic;

namespace NegociosYContactos.Models
{
    public class SearchViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public string Style { get; set; }
        public DateTime InitDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Premium { get; set; }
        public bool Active { get; set; }
        public string Address { get; set; }
        public int IdCategory { get; set; }
    }

    public class SearchListViewModel
    {
        public IList<SearchViewModel> SearchList { get; set; }
    }

    public class SearchListPaginationModel
    {
        public int Page { get; set; }
        public int TamPage { get; set; }
        public int IdCategory { get; set; }
        public string SearchWord { get; set; }
    }

    public class EconomicIndicatorViewModel
    {
        public string Concept { get; set; }
        public string Type { get; set; } // dolar - euro ...
    }

    public class NewsViewModel
    {
        public string Concept { get; set; }
        public string Type { get; set; } // deportes - social ...
    }

    public enum TermType
    {
        Business = 0,
        Category = 1
    }

    public class ProductOrderWeb {
        public string BusinessName { get; set; }
        public BusinessProductWeb Product { get; set; }
        public int Id { get; set; }
        public int OrderType { get; set; } // 1. pedirProducto,  2. Ser contactado
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }
    }
}
