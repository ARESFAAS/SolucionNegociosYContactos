﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class ContactosyNegociosEntities : DbContext
    {
        public ContactosyNegociosEntities()
            : base("name=ContactosyNegociosEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<AspNetUsers> AspNetUsers { get; set; }
        public DbSet<Business> Business { get; set; }
        public DbSet<BusinessProduct> BusinessProduct { get; set; }
    
        public virtual ObjectResult<BusinessData_Get_Result> BusinessData_Get(string idUser)
        {
            var idUserParameter = idUser != null ?
                new ObjectParameter("IdUser", idUser) :
                new ObjectParameter("IdUser", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<BusinessData_Get_Result>("BusinessData_Get", idUserParameter);
        }
    }
}
