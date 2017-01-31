using Apcis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.DAL
{
    public static class TerritoireDAO
    {
        public static int StainsID
        {
            get; set;
        }
        public static int BagnoletID
        {
            get; set;
        }
        public static int EpinayID
        {
            get; set;
        }

        public static IEnumerable<Territoire> WherePrestation(int id)
        {
            var territoires = DAO.GetDbSet<Prestation_Territoire>()
                .Where(pt => pt.Prestation.ID == id)
                .ToList()
                .Select(pt => pt.Territoire).ToList();
            return territoires;
        }

       

        public static IEnumerable<Territoire> Find(IEnumerable<int> ids)
        {
            var set = DAO.GetDbSet<Territoire>().AsEnumerable().Where(x => ids.Contains(x.ID));
            return set;
        }

        public static Territoire WhereActivite(int id)
        {
            return DAO.Find<Territoire>(DAO.Find<Activite>(id).TerritoireID);
        }

        public static IEnumerable<Territoire> WhereActiviteName(string name)
        {
            return Find(ActiviteDAO.FindByName(name).Select(a => a.TerritoireID));
        }

        public static IEnumerable<Territoire> All()
        {
            return DAO.GetDbSet<Territoire>();
        }
    }
}