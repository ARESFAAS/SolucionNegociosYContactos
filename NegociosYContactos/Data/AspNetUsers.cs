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
    
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            this.Business = new HashSet<Business>();
        }
    
        public string Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PhoneNumber { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public bool Locked { get; set; }
        public Nullable<bool> AcceptTerms { get; set; }
    
        public virtual ICollection<Business> Business { get; set; }
    }
}
