using Apcis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.DAL
{
    public class ActiviteDAO
    {
        public static bool Exists(string name)
        {
            return FindByName(name).Count() > 0;
        }

        public static IEnumerable<Activite> FindByName(string name)
        {
            return DAO.GetDbSet<Activite>().Where(x => x.Nom == name);
        }

        public static Activite FindByNameWhereTerritoire(string name, int territoireId)
        {
            var all = FindByName(name);
            return all.SingleOrDefault(a => a.TerritoireID == territoireId);
        }

        public static void Persist(Activite activite, IEnumerable<int> territoireIds)
        {
            
            territoireIds.Each(id =>
            {
                PrestationTerritoireDAO.AddActivite(activite.PrestationID, id, new Activite()
                { Nom = activite.Nom, PrestationID = activite.PrestationID, TerritoireID = id });

            });
        }

        public static Activite WherePrestationTerritoire(int prestationId, int territoireId)
        {
            var ret = DAO.db.Activites.Where(a => a.TerritoireID == territoireId && a.PrestationID == prestationId).AsEnumerable();
            return ret.Single(x => typeof(ActiviteElement) != x.GetType() );
        }
        
        public static void AddElement(Activite activite, ActiviteElement element)
        {
            activite.Elements = activite.Elements ?? new List<ActiviteElement>();
            element.SetActiviteParent(activite);
            activite.Elements.Add(element);
            DAO.Update(activite);
        }

        public static int AddElementTime(int activiteElementId, JourEtHeure jourEtHeure)
        {
            var element = DAO.Find<ActiviteElement>(activiteElementId);
            element.Details.Add(new ActiviteElementDetails() { JourEtHeure = jourEtHeure });

            DAO.Update(element);

            return element.Details.Last().ID;
        }

        public static void AddElementIntervenant(int activiteElementDetailsId, Intervenant intervenant)
        {
            var details = DAO.Find<ActiviteElementDetails>(activiteElementDetailsId);
            details.Intervenant = intervenant;
            DAO.Update(details);
        }
    }
}