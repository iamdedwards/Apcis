using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Apcis.ViewModels
{
    public class UpdateListViewModel<T> where T : class, new()
    {
        public ICollection<T> OldValues;
        public ICollection<T> UpdateValues;
    }
}