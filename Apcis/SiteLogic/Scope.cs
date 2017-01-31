using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.SiteLogic
{
    public  class Scope
    {
        public IDictionary<string, object> InnerDictionary { get; private set; }

        private bool _useSessionStation = false;

        public Scope(IDictionary<string, object> innerDictionary)
        {
            InnerDictionary = innerDictionary;
        }

        public Scope(bool useSessionState)
        {
            _useSessionStation = useSessionState;
        }

        private  string ID<T>(string uniqueID)
        {
            if (uniqueID != null)
                return uniqueID;
            var id = typeof(T).ToString();
            return id;
        }

        public  void Remove<A>(string uniqueId = null) where A : class
        {
            if (Contains<A>(uniqueId.OrIfNull(typeof(A).ToString())))
            {
                if (_useSessionStation)
                {
                    HttpContext.Current.Session.Remove(uniqueId.OrIfNull(typeof(A).ToString()));
                }
                else
                {
                    InnerDictionary.Remove(uniqueId.OrIfNull(typeof(A).ToString()));
                }
            }
        }

        public  A Recover<A>(string uniqueID = null) where A : class
        {
            if (_useSessionStation)
                return HttpContext.Current.Session[ID<A>(uniqueID)] as A;
            else
                return InnerDictionary[ID<A>(uniqueID)] as A;

        }

        public  A Assign<A>(A anEntity, string uniqueID = null) where A : class
        {
            if (_useSessionStation)
                HttpContext.Current.Session[ID<A>(uniqueID)] = anEntity;
            else
                InnerDictionary[ID<A>(uniqueID)] = anEntity;
            return anEntity;
        }


        public  bool Contains<A>(string uniqueID = null) where A : class
        {

            var ret = (_useSessionStation) ? HttpContext.Current.Session[ID<A>(uniqueID)] : InnerDictionary[ID<A>(uniqueID)];
            return (ret == null) ? false : true;
        }

    }

}