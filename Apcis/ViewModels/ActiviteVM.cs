using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apcis.Models;
using System.ComponentModel.DataAnnotations;

namespace Apcis.ViewModels
{
    public class ActiviteVM : ViewModel<Activite>
    {
        [Required]
        public int PoleId { get; set; }

        [Required]
        public int PrestationId { get { return Entity.PrestationID; } }

        [Required]
        public Dictionary<string, bool> TerritoireJoins { get; set; }

    }
}