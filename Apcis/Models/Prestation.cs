using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Apcis.Models
{

    public interface IIdentifiable
    {
         int ID { get; }
    }


    public class Resultat_Activite_Territoire 
    {
        public int ID { get; set; }
        public Activite Activite { get; set; }
        public Territoire Territoire { get; set; }
        public Resultat Resultat { get; set; }
        public string Details { get; set; }
        public List<Public> Public { get; set; }
    }

    public class Resultat
    {
        public int ID { get; set; }
        public string Nom { get; set; }
    }

    public class Partenaire_Resultat_Activite_Territoire
    {
        public int ID { get; set; }
        public Partenaire Partenaire { get; set; }
        public List<Resultat_Activite_Territoire> Resultat_Activite_Territoire { get; set; }
    }

    public class Prestation : IIdentifiable, IDisplayable
    {
        public int ID { get; set; }
        public string Nom { get; set; }
        public Pole Pole { get; set; }

        public string Display() { return Nom; }
    }


    public class Prestation_Territoire 
    {
        public int ID { get; set; }
        public string Lieu { get; set; }
        public Territoire Territoire { get; set; }
        public Prestation Prestation { get; set; }
        public List<Activite> Activites { get; set; }  
    }

    public class Activite_Territoire_Public
    {
        public int ID { get; set; }
        public Activite Activite { get; set; }
        public Territoire Territoire { get; set; }
        public List<Public> Public { get; set; }
    }

    public class Activite : IIdentifiable, IDisplayable
    {
        public int ID { get; set; }
        public string Nom { get; set; } //RULE: NOM not distinct; replicated for each territoire;
        public int PrestationID { get; set; }
        public int TerritoireID { get; set; }
        public virtual ICollection<ActiviteElement> Elements { get; set; }
        public string Display() { return Nom; }
    }

    public class ActiviteId_TerritoireId
    {
        public int ActiviteId { get; set; }
        public int TerritoireId { get; set; }
    }

    public class Activite_Terrioires
    {
        public List<ActiviteId_TerritoireId> Activites_Territoires { get; set; }
    }

    public class ActiviteElement : Activite
    {
        public string Description { get; set; }
        public int PourcentageDuTemps { get; set; }
        public Activite ActiviteParent { get; set; }
        public List<ActiviteElementDetails> Details { get; set; }


        public ActiviteElement SetActiviteParent(Activite activiteParent)
        {
            ActiviteParent = activiteParent;
            PrestationID = activiteParent.PrestationID;
            TerritoireID = activiteParent.TerritoireID;
            return this;
        }
    }


    public class ActiviteElementDetails
    {
        public int ID { get; set; }
        public JourEtHeure JourEtHeure { get; set; }
        public Intervenant Intervenant { get; set; }

    }





    public enum Jour
    {
        Lundi = 1,
        Mardi = 2,
        Mercredi = 3,
        Jeudi = 4,
        Venredi = 5,
        Samedi = 6,
        Dimanche = 7
    };

    

    public interface IDisplayable
    {
        string Display();
    }   
   
    public class JourEtHeure : IIdentifiable, IDisplayable
    {
        public int ID { get; set; }
        public TimeSpan Debut { get; set; }
        public TimeSpan Fin { get; set; }
        public Jour Jour { get; set; }

        public string Display()
        {
            return string.Format("{0} : {1} - {2}", Jour, Debut, Fin);
        }
    }

    public class Intervenant : IIdentifiable, IDisplayable
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int ID { get; set; }
        public string Profession { get; set; }
        public string Role { get; set; }
        public bool Volontariat { get; set; }

        public string Display()
        {
            return string.Format("{0}, {1}", Nom.ToUpper(), Prenom);
        }
    }
}