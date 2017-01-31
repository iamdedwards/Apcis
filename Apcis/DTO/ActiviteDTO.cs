using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.DTO
{
    public abstract class Identifiable
    {
        public int ID { get; set; }
    }

    public abstract class Nameable : Identifiable
    {
        public string Nom { get; set; }
    }

    public class ActiviteDTO : Nameable { }

    public class TerritoireDTO : Nameable { }
}