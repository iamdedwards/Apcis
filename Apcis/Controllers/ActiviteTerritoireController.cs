using Apcis.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apcis.DAL;
using Apcis.Models;
using Apcis.SiteLogic;
using System.Web.Mvc;

namespace Apcis.Controllers
{
    using Html;
    using ViewModel = ActiviteTerritoireVM;

    public class ActiviteTerritoireController : ApcisController
    {

        private string activiteElementsID = typeof(IEnumerable<ActiviteElementVM>).ToString();

        private IEnumerable<ActiviteElementVM> getElementsFromTmp()
        {
            return TempData[activiteElementsID] as IEnumerable<ActiviteElementVM>;
        }

        private void SaveElementsToTmp(IEnumerable<ActiviteElementVM> vms)
        {
            TempData[activiteElementsID] = vms;
        }

        private IEnumerable<ActiviteElementVM> updateElements(Func<IEnumerable<ActiviteElementVM>, IEnumerable<ActiviteElementVM>> modifier)
        {
            var modified = modifier(getElementsFromTmp());
            SaveElementsToTmp(modified);
            return modified;
        }


        [HttpPost]
        public ActionResult AddCreated(IEnumerable<ActiviteElementVM> vms)
        {
            vms.Each(el =>
            {
                var activite = ActiviteDAO.WherePrestationTerritoire(el.Entity.PrestationID, el.Entity.TerritoireID);
                ActiviteDAO.AddElement(activite, el.Entity);

            });
            return View("CreateElements", vms);
        }

        [HttpPost]
        public PartialViewResult SetElement(int vmid, string description, int percentage)
        {
            var elems = updateElements((x) =>
            {
                var elem = x.Single(y => y.VMID == vmid);
                elem.Entity.PourcentageDuTemps = percentage;
                elem.Entity.Description = description;
                elem.IsSet = true;
                return x;
            });
            DropDowns.Numbers = setDropDown(elems.Count(), percentage);
            return PartialView("CreateElements", elems);
        }



        [HttpGet]
        public PartialViewResult SetJourEtHeure(int activiteElementId)
        {
            var vm = new JourEtHeureVM() { ActiviteElementId = activiteElementId };
            return PartialView(vm);
        }

        [HttpPost]
        public PartialViewResult SetJourEtHeure(JourEtHeureVM vm)
        {
            vm.ActiviteElementDetailsId = ActiviteDAO.AddElementTime(vm.ActiviteElementId, vm.Entity);
            
            return PartialView(vm);
        }

        [HttpGet]
        public PartialViewResult SetIntervenant(int activiteElementId, int activiteElementDetailsId)
        {
            var element = DAO.Find<ActiviteElement>(activiteElementId);
            var times = element.Details.AsEnumerable().Select(e => e.JourEtHeure);
            DropDowns.Create(times, "choisir un!");
            DropDowns.Default<Intervenant>("choisir un ou saisir leur details en-dessous!");
            var vm = new IntervenantVM() { ActiviteElementId = activiteElementId, ActiviteElementDetailsId = activiteElementDetailsId };
            return PartialView(vm);
        }

        [HttpPost]
        public PartialViewResult SetIntervenant(IntervenantVM vm)
        {
            ActiviteDAO.AddElementIntervenant(vm.ActiviteElementDetailsId, vm.Entity);
            return PartialView(vm);
        }

        private IEnumerable<SelectListItem> setDropDown(int elements, int offset = 0)
        {
            var max = 100 - offset;

            var min = 5;

            return DropDowns.Numbers = DropDowns
                .SetNumbers("{0} %", min, max);
            
        }


        [HttpPost]
        public ActionResult Description(ViewModel vm)
        {
            
            DropDowns.Territoires = DropDowns
                .Create(TerritoireDAO.WhereActiviteName(vm.Entity.Nom), vm.Entity.TerritoireID);

            var activite = ActiviteDAO.FindByNameWhereTerritoire(vm.Entity.Nom, vm.Entity.TerritoireID);

            DropDowns.Numbers = setDropDown(vm.Elements);

            var elements = 
                new List<ActiviteElementVM>().Init(vm.Elements, (index) => 
                new ActiviteElementVM(){ VMID = index + 1, Entity = new ActiviteElement() });

            TempData[activiteElementsID] = elements;
            return View("CreateElements", elements);
        }

        

        [HttpGet]
        public ActionResult Description(string nom)
        {
            var territoireIds = TempData["ids"] as IEnumerable<int>;

            if (territoireIds == null)
                return RedirectToAction("Nouvelle", "Activites"); // should be details list

            DropDowns.Territoires = DropDowns
                .Create(TerritoireDAO.Find(territoireIds), "choisir une!");

            var vm = new ViewModel();
            vm.Entity = ActiviteDAO.FindByNameWhereTerritoire(nom, territoireIds.First());
            return View(vm);
        }

        public PartialViewResult UseTemplate(ViewModel vm)
        {
            return null;

        }

   
    }
}