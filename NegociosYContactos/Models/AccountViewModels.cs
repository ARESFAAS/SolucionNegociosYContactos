using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegociosYContactos.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool Locked { get; set; }
        public int AccessFailedCount { get; set; }
        public string UserName { get; set; }
        public bool IsAuthenticated { get; set; }
        public string LoginProvider { get; set; }
        public string UserImage { get; set;  }
        public string Message { get; set; }   
        public string UserCountry { get; set; }    
    }
}
