using Apcis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.DAL
{
    public class PoleDAO
    {
        public static Pole WherePrestation(int id)
        {
            return DAO.GetDbSet<Pole>()
                   .Single(x => x.Prestations.Select(get => get.ID).Contains(id));
        }

        public static Pole First()
        {
            return DAO.GetDbSet<Pole>().First();
        }
    }
}