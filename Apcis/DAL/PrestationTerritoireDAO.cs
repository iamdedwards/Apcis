using Apcis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.DAL
{
    public static class PrestationTerritoireDAO
    {
        public static Prestation_Territoire Join(int prestationId, int territoireId)
        {
            var join = new Prestation_Territoire() { Prestation = DAO.db.Prestations.Find(prestationId), Territoire = DAO.db.Territoires.Find(territoireId) };
            DAO.Persist(join);
            return join;
        }

        public static Prestation_Territoire Join(Prestation prestation, Territoire territoire)
        {
            var join = new Prestation_Territoire() { Prestation = prestation, Territoire = territoire };
            DAO.Persist(join);
            return join;
        }

        public static void RemoveJoin(int prestationId, int territoireId)
        {
            var join = DAO.db.Prestation_Territoire.Single(j => j.Prestation.ID == prestationId && j.Territoire.ID == territoireId);
            DAO.Delete(join);
        }

        public static IEnumerable<Prestation_Territoire> WhereTerritoire(int id)
        {
            return DAO.GetDbSet<Prestation_Territoire>().Where(pt => pt.Territoire.ID == id);
        }

        public static IEnumerable<Prestation_Territoire> WherePrestation(int id)
        {
            return DAO.GetDbSet<Prestation_Territoire>().Where(pt => pt.Prestation.ID == id);
        }



        public static Prestation_Territoire Where(int prestationId, int territoireId)
        {
            return DAO.GetDbSet<Prestation_Territoire>().Single(pt => pt.Prestation.ID == prestationId && pt.Territoire.ID == territoireId);
        }

        public static Prestation_Territoire Where(int prestationId, int territoireId, Func<Prestation_Territoire> callBackIfNull)
        {
            var ret = DAO.GetDbSet<Prestation_Territoire>().SingleOrDefault(pt => pt.Prestation.ID == prestationId && pt.Territoire.ID == territoireId);
            if (ret == null)
            {
                return callBackIfNull();
            }
            return ret;
        }

        public static void AddActivite(int prestationId, int territoireId, Activite activite)
        {
            Func<Prestation_Territoire> joinIfNull = () => Join(prestationId, territoireId);

            var pt = Where(prestationId, territoireId, joinIfNull);
            
            var activites = pt.Activites ?? new List<Activite>();
            activites.Add(activite);
            DAO.Persist(activite);
            DAO.Update(pt);
        }

        public static IEnumerable<Prestation_Territoire> WherePrestation_Union_Territoires(int prestationId)
        {

            var with = DAO.db.Prestation_Territoire.Where(pt => pt.Prestation.ID == prestationId).AsEnumerable();

            var setIds = with.Select(s => s.Territoire.ID);

            var without = DAO.db.Territoires.Where(wo => !setIds.Contains(wo.ID)).AsEnumerable()
                .Select(t => new Prestation_Territoire() { Territoire = t, Prestation = null });

            return (with.Concat(without));
        }       
    }
}