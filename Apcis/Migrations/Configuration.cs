namespace Apcis.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Apcis.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Apcis.Models.ApplicationDbContext context)
        {
            context.Remove<Prestation_Territoire>();
            context.Remove<Territoire>();
            context.Remove<Pole>();
            context.Remove<Prestation>();


            var stains = new Models.Territoire() { Nom = "Stains" };
            var bagnolet = new Models.Territoire() { Nom = "Bagnolet" };
            var epinay = new Models.Territoire() { Nom = "Epinay" };
            context.Territoires.AddOrUpdate(t => t.ID, stains, bagnolet, epinay);
            context.Poles.AddOrUpdate(p => p.ID, new Models.Pole() { Nom = "Laaaaaaa la" });
            context.SaveChanges();
            Apcis.DAL.TerritoireDAO.BagnoletID = bagnolet.ID;
            Apcis.DAL.TerritoireDAO.EpinayID = epinay.ID;
            Apcis.DAL.TerritoireDAO.StainsID = stains.ID;



            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }


       


    }
}
