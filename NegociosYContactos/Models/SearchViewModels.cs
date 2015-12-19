using System;
using System.Collections.Generic;

namespace NegociosYContactos.Models
{
    public class SearchViewModel
    {
        public string Item { get; set; }
        public string Type { get; set; } // categoria - negocio - producto
        public string Description { get; set; }
        public string UrlImage { get; set; }
        public string Value { get; set; }        
    }

    public class SearchListViewModel
    {
        public IList<SearchViewModel> SearchList { get; set; }
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
}
