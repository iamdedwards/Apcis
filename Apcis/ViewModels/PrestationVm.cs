using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apcis.Models;
using System.Web.Mvc;
using System.Linq.Expressions;
using Apcis.Html;
using Apcis.DTO;
using Apcis.SiteLogic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Apcis.Resources;

namespace Apcis.ViewModels
{


    public class ViewModel<T>
    {
        public T Entity { get; set; }
    }

    public class IntervenantVM : ViewModel<Intervenant>
    {
        public int TerritoireId { get; set; }
        public int PrestationId { get; set; }
        public int ActiviteId { get; set; }
        public int JourEtHeureId { get; set; }
        public int ActiviteElementId { get; set; }
        public int ActiviteElementDetailsId { get; set; }

    }

    public class JourEtHeureVM : ViewModel<JourEtHeure>
    {
        public int TerritoireId { get; set; }
        public int PrestationId { get; set; }
        public int ActiviteElementId { get; set; }
        public int ActiviteElementDetailsId { get; set; }
    }

    public class TerritoireVM : ViewModel<Territoire>
    {
        public int PrestationId { get; set; }
        public bool Joined { get; set; }
    }

    public class IdentifiableNameable
    {
        public int ID { get; set; }
        public string Nom { get; set; }
    }

    public class PrestationVM : ViewModel<Prestation>
    {
        [Required]
        public Dictionary<string, bool> Joins { get; set; }

        [Required]
        public int PoleId { get; set; }


        [Required]
        [Masculine()]
        public string Nom
        {
            get { return Entity.Nom; }
        }

       
        
    }

    public class ActiviteTerritoireVM : ViewModel<Activite>
    {
        private int _elements = 1;
        public int Elements { get { return _elements; } set { _elements = value; } }
    }

    public class ActiviteElementVM : ViewModel<ActiviteElement>
    {
        public int VMID { get; set; }
        public bool IsSet { get; set; }

     
    }



   

}

