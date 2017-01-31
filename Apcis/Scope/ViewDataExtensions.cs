using Apcis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Apcis.ScopeExperiments
{
    public class Scope
    {
        private IDictionary<string, object> _innerDictionary { get; set; }

        public Scope(IDictionary<string, object> innerDictionary)
        {
            
           
        }

        public   A Recover<A>(string uniqueID = null) where A : class
        {
            A recovered = _innerDictionary[typeof(A).ToString() + uniqueID.OrIfNull("")] as A;
            return recovered;
        }

        public   A Assign<A>(A anEntity, string uniqueID = null) where A : class
        {
            _innerDictionary[typeof(A).ToString() + uniqueID.OrIfNull("")] = anEntity;
            return anEntity;
        }

      

        public   bool Contains<A>(string uniqueID = null) where A : class
        {
            return _innerDictionary[typeof(A).ToString() + uniqueID.OrIfNull("")] != null;
        }


     
    }

   
    
}