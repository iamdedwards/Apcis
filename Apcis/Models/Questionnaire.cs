using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.Models
{
    public class Question
    {
        public int ID { get; set; }
        public string Texte { get; set; }
        public virtual ICollection<Reponse> ReponsesPossibles { get; set; }
    }

    public class Reponse
    {
        public int ID { get; set; }
        public string Texte { get; set; }
    }

    public class PublicReponse
    {
        public int ID { get; set; }
        public Public Public { get; set; }
        public Reponse Reponse { get; set; }
    }

    public class Questionnaire
    {
        public int ID { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<PublicReponse> Answers { get; set; }
    }
}