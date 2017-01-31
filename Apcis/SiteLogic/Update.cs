using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Apcis.Models;

namespace Apcis.SiteLogic
{
    public class UpdateSet<U> : List<Update<U>>
    {

        public UpdateSet(ICollection<U> before) : this(before, null) { }

        public UpdateSet(ICollection<U> before, ICollection<U> after)
        {
           
            before.Each((value, index) =>
            {
                U tryGetAfter;
                try
                { tryGetAfter = after.ElementAt(index); }
                catch
                { tryGetAfter = value; }

                Add(new Update<U>() { Before = value, After = tryGetAfter });
            });
        }

        public List<U> Before
        {
            get
            {
                return this.Select(become => become.Before).ToList();
            }
        }

        public List<U> After
        {
            get
            {
                return this.Select(become => become.After).ToList();
            }
        }
    }

    public class Update<T>
    {
        public T Before { get; set; }
        public T After { get; set; }
    }
}