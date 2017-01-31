using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;

namespace Apcis.Models
{
    // Vous pouvez ajouter des données de profil pour l'utilisateur en ajoutant d'autres propriétés à votre classe ApplicationUser, consultez http://go.microsoft.com/fwlink/?LinkID=317594 pour en savoir davantage.

    public class Adresse
    {
        public int ID { get; set; }
        public Territoire Region { get; set; }
        public string TexteAdresse { get; set; }
        public string CodePostale { get; set; }
    }

    public class ApplicationUser : IdentityUser
    {
        public System.DateTime DateCreation { get; set; }
        public string Prenom { get; set; }
        public string Nom { get; set; }
        public Adresse Adresse { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Notez que authenticationType doit correspondre à l'instance définie dans CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Ajouter des revendications d’utilisateur personnalisées ici
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DebugConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        


        public DbSet<Territoire> Territoires { get; set; }
        public DbSet<Pole> Poles { get; set; }
        public DbSet<Prestation> Prestations { get; set; }
        public DbSet<Activite> Activites { get; set; }
        public DbSet<Prestation_Territoire> Prestation_Territoire { get; set; }
        public DbSet<ActiviteElement> ActiviteElements { get; set; }
      

        //public DbSet<Etudiant> Etudiants { get; set; }
        //public DbSet<Public> Public { get; set; }
        //public DbSet<Activite> Activite { get; set; }
        //public DbSet<Dispositif> Dispositifs { get; set; }
        //public DbSet<Intervenant> Intervenants { get; set; }
        //public DbSet<JourEtHeure> Creneaux { get; set; }
        //public DbSet<ListeAttente> ListeAttente { get; set; }
        //public DbSet<Parent> Parents { get; set; }
        //public DbSet<Partenaire> Partenaires { get; set; }
       
        //public DbSet<VuePoleTerritoire> VuesPoleTerritoire { get; set; }

        //public DbSet<ResultatPrestation> Resultats { get; set; }

        //public DbSet<StatusType> StatusType { get; set; }
        //public DbSet<Status> Statuses { get; set; }
        //public DbSet<PublicCommentaire> Observations { get; set; }
        //public DbSet<RegionIndicateur> DetailsRegions { get; set; }
        //public DbSet<GroupementIndicateur> IndiceGroupement { get; set; }
        //public DbSet<Indicateur> Indicateurs { get; set; }
        //public DbSet<VueIndicateurRegion> VuesIndicateurs { get; set; }
        //public DbSet<HistoriqueIndicateur> HistoriqueIndicateurs { get; set; }
        //public DbSet<Rappel> Rappels { get; set; }
        //public DbSet<Questionnaire> Questionnaires { get; set; }
        //public DbSet<Question> Questions { get; set; }
        //public DbSet<Reponse> Reponses { get; set; }
        //public DbSet<PublicReponse> ReponsesPublic { get; set; }
        //public DbSet<Ecole> Ecoles { get; set; }
        //public DbSet<ColourActiviteElement> SiteColorActiviteElements { get; set; }
        //public DbSet<CurrentColourActiviteElement> CurrentColorActiviteElement { get; set; }

        public System.Data.Entity.DbSet<Apcis.Models.Contacte> Contactes { get; set; }
    }
}