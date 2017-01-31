using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Apcis.Models;
using Apcis.ViewModels;
using Apcis.Html;

using Apcis.DAL;

using Apcis.Scoope;
using Apcis.SiteLogic;

namespace Apcis.Controllers
{

    public class PrestationsController : Controller
    {
       
        public ActionResult Nouvelle()
        {
            var vm = new PrestationVM();

            vm.Joins = IsJoinedDictionary.Default<Territoire>();
            DropDowns.Poles = DropDowns.Default<Pole>("Choisir une!");
              
            return View(vm);
        }

        [HttpPost]
        public ActionResult Nouvelle(PrestationVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Erreur");
            }

            PrestationDAO.Persist(vm.Entity, vm.PoleId, vm.Joins);

            return View(vm);

        }

       
    }
}