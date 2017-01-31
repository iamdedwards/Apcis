using Apcis.DAL;
using Apcis.Models;
using Apcis.ViewModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apcis.Controllers
{
    public class PrestationTerritoireController : Controller
    {
        // GET: PrestationTerritoire
       

        public void Join(int prestationId, int territoireId)
        {
            PrestationTerritoireDAO.Join(prestationId, territoireId);
        }

        public void RemoveJoin(int prestationId, int territoireId)
        {
            PrestationTerritoireDAO.RemoveJoin(prestationId, territoireId);
        }

        public PartialViewResult _Update(int prestationId)
        {
            var all = PrestationTerritoireDAO.WherePrestation_Union_Territoires(prestationId);

            var model = all.Select(a => new TerritoireVM()
            {
                Entity = a.Territoire,
                Joined = a.Prestation != null,
                PrestationId = prestationId
            });

            return PartialView("../PrestationTerritoire/Update", model);
        }

       
        


        public PartialViewResult UpdateTerritoireForPrestationAjax(int prestationId, int territoireId, string isJoined, string viewName)
        {
            bool isJoinedBool;

            if (!(bool.TryParse(isJoined, out isJoinedBool)))
            {
                return PartialView("Erreur");
            }

            if (!isJoinedBool)
            {
                Join(prestationId, territoireId);
            }

            if (isJoinedBool)
            {
               RemoveJoin(prestationId, territoireId);
            }
            return PartialView(viewName, new { prestationId = prestationId });
        }
    }
}