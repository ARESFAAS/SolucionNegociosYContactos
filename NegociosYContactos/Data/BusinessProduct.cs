//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NegociosYContactos.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class BusinessProduct
    {
        public BusinessProduct()
        {
            this.ProductOrder = new HashSet<ProductOrder>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string UrlImage { get; set; }
        public Nullable<int> IdBusiness { get; set; }
    
        public virtual Business Business { get; set; }
        public virtual ICollection<ProductOrder> ProductOrder { get; set; }
    }
}
