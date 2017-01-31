using Apcis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Apcis.App_Start
{
    public class DbConfig
    {

        public static void Seed()
        {
            Apcis.Models.ApplicationDbContext context = new ApplicationDbContext();

            context.Remove<Prestation_Territoire>();
            context.Remove<Territoire>();
            context.Remove<Pole>();
            context.Remove<Prestation>();



            var stains = new Models.Territoire() { Nom = "Stains" };
            var bagnolet = new Models.Territoire() { Nom = "Bagnolet" };
            var epinay = new Models.Territoire() { Nom = "Epinay" };
            context.Territoires.AddOrUpdate(t => t.ID, stains, bagnolet, epinay);
            context.Poles.AddOrUpdate(p => p.ID, new Models.Pole() { Nom = "Laaaaaaa la" });
            context.Poles.AddOrUpdate(p => p.ID, new Models.Pole() { Nom = "NOOOppppOO!" });
            context.SaveChanges();
            Apcis.DAL.TerritoireDAO.BagnoletID = bagnolet.ID;
            Apcis.DAL.TerritoireDAO.EpinayID = epinay.ID;
            Apcis.DAL.TerritoireDAO.StainsID = stains.ID;

        }
    }
}