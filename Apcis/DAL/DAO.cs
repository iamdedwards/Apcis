using Apcis.Models;
using Apcis.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;

namespace Apcis.DAL
{




  
      

        public static class DAO
        {
            public static ApplicationDbContext db = new ApplicationDbContext();

            public static Entity Find<Entity>(int id) where Entity : class
            {
                return db.Set(typeof(Entity)).Find(id) as Entity;
            }
            
            public static IEnumerable<Entity> FindByString<Entity>(string value, Expression<Func<Entity, string>> valueFinder) where Entity : class
            {
                var finder = valueFinder.Compile();
                return GetDbSet<Entity>().Where(x => finder(x) == value);
            }

            public static IEnumerable<Entity> FindByDisplayValue<Entity>(string nom) where Entity : class, IDisplayable
            {
                return GetDbSet<Entity>().Where(x => x.Display().ToUpper() == nom.ToUpper());
            }

        public static int Persist<Entity>(Entity e) where Entity : class
            {
                db.Set(typeof(Entity)).Add(e);
                return db.SaveChanges();
            }

            public static int Update<Entity>(Entity e) where Entity : class
            {
                db.Entry(e).State = System.Data.Entity.EntityState.Modified;
                return db.SaveChanges();
            }

            public static int Delete<Entity>(Entity e) where Entity : class
            {
                db.Entry(e).State = System.Data.Entity.EntityState.Deleted;
                return db.SaveChanges();
            }

            public static IQueryable<T> GetDbSet<T>() where T : class
            {
                return db.Set<T>().Cast<T>();
            }
        }

}