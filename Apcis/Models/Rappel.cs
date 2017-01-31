using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.Models
{
    public class Rappel
    {
        public int ID { get; set; }
        public ICollection<ApplicationUser> User { get; set; }
        public ICollection<Contacte> Contacte { get; set; }
        public string Message { get; set; }
    }
}