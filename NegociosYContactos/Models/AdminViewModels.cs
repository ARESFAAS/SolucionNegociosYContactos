using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegociosYContactos.Models
{
    public class UploadFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; }
        public string DeleteUrl { get; set; }
        public string DeleteType { get; set; }
    }

    public class BusinessUserWeb
    {
        public string IdUser { get; set; }
        public int IdBusiness { get; set; }
    }

    public class BusinessProductWeb
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string UrlImage { get; set; }
        public int IdBusiness { get; set; }
    }

    public class BusinessCategory {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class BusinessWeb
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
        public BusinessUserWeb User { get; set; }
        public List<BusinessProductWeb> Products { get; set; }
        public BusinessCategory Category { get; set; }
    }    
}