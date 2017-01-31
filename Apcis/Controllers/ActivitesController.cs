using Apcis.ViewModels;
using Apcis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Apcis.Html;
using System.Threading.Tasks;
using Apcis.DAL;
using Apcis.SiteLogic;

namespace Apcis.Controllers
{
    public class ActivitesController : Controller
    {
        #region initialiseViewData

        private void setPrestationDropDown(int poleId, int? prestationIdOrNull)
        {
            if (prestationIdOrNull.HasValue)
            {
                DropDowns.Prestations = DropDowns
                    .Filter<Prestation>
                    (x => x.Pole.ID == poleId, selected: prestationIdOrNull.Value);
            }
            else
            {
                DropDowns.Prestations = DropDowns
                    .Filter<Prestation>
                    (x => x.Pole.ID == poleId, placeholder: "choisir une!");
            }
        }


        private void setCheckboxes(ActiviteVM vm, int prestationIdOrDefault)
        {
            vm.TerritoireJoins = (prestationIdOrDefault > 0) ?

                vm.TerritoireJoins = IsJoinedDictionary.PreSelectFromData(TerritoireDAO.WherePrestation(prestationIdOrDefault)) :

                vm.TerritoireJoins = IsJoinedDictionary.Default<Territoire>();

        }

        private void setPoleDropDown(int poleId)
        {
            DropDowns.Poles = DropDowns.Default<Pole>(selected: poleId);
        }

        private ActiviteVM createViewData(int? prestationId, int? poleId)
        {
            var prestationIdOrDefault = prestationId.GetValueOrDefault(PrestationDAO.First().ID);

            var poleIdOrDefault = poleId.GetValueOrDefault(PoleDAO.WherePrestation(prestationIdOrDefault).ID);

            var vm = new ActiviteVM() { Entity = new Activite() { PrestationID = prestationIdOrDefault }, PoleId = poleIdOrDefault };
            setPrestationDropDown(vm.PoleId, prestationIdOrNull: prestationId);
            setPoleDropDown(vm.PoleId);
            setCheckboxes(vm, prestationId.GetValueOrDefault());
            return (vm);
        }

        #endregion

        [HttpPost]
        public PartialViewResult _Nouvelle(int? prestationId, int poleId)
        {
            return PartialView("Nouvelle", createViewData(prestationId, poleId));
        }

        [HttpGet]
        public ActionResult Nouvelle(int? prestationId, int? poleId)
        {
            return View("Nouvelle", createViewData(prestationId, poleId));
        }

        [HttpPost]
        public ActionResult Nouvelle(ActiviteVM vm)
        {
            if (ActiviteDAO.Exists(vm.Entity.Nom))
            {
                ViewBag.IsDuplicationConstraint = true;
                return View(vm);
            }
            var territoireIds = IsJoinedDictionary.ToIDSet<Territoire>(vm.TerritoireJoins);

            ActiviteDAO.Persist(vm.Entity, territoireIds);
            TempData["ids"] = territoireIds;
            ViewBag.Validate = true;
            return View(vm);
        }

      

    }
}