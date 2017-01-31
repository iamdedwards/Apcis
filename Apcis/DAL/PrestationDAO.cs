using Apcis.Models;
using Apcis.SiteLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.DAL
{
    public class PrestationDAO
    {
        public static void Persist(Prestation prestation, int poleId, Dictionary<string, bool> territoires)
        {
            prestation.Pole = DAO.Find<Pole>(poleId);
            DAO.Persist(prestation);
            IEnumerable<Territoire> territoireSet = IsJoinedDictionary.ToDbSet<Territoire>(territoires);
            territoireSet.Each(t => PrestationTerritoireDAO.Join(prestation, t));
        }

        public static Prestation First()
        {
            return DAO.GetDbSet<Prestation>().First();
        }
    }
}