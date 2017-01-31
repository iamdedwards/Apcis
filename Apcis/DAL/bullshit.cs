//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Apcis.DAL
//{
//    public class bullshit
//    {
//    }

//    public static class QueryBuilderFor<TInJoinTable, TInJoinTable2>
//           where TInJoinTable : class, EntityIdentifiable, new()
//           where TInJoinTable2 : class, EntityIdentifiable, new()
//    {

//        private static ApplicationDbContext db = new ApplicationDbContext();

//        public static ReturningSetSelector<TJoinTable> UsingJoinTable<TJoinTable>() where TJoinTable : class, JoinTable<TJoinTable, TInJoinTable, TInJoinTable2>, EntityIdentifiable, new()
//        {
//            return new ReturningSetSelector<TJoinTable>();
//        }

//        public class ReturningSetSelector<TJoinTable> where TJoinTable : class, JoinTable<TJoinTable, TInJoinTable, TInJoinTable2>, EntityIdentifiable, new()
//        {


//            public WhereExpressionSelector<TJoinTable, TReturned> ReturningSet<TReturned>() where TReturned : class, EntityIdentifiable, new()
//            {

//                return new WhereExpressionSelector<TJoinTable, TReturned>();
//            }

//            public TJoinTable Single(int leftId, int rightId)
//            {

//                return new JoinHelperMethods<TJoinTable, TInJoinTable, TInJoinTable2, TInJoinTable, TInJoinTable2>().Single(leftId, rightId);

//            }


//        }

//        public class WhereExpressionSelector<TJoinTable, TReturned> where TJoinTable : class, JoinTable<TJoinTable, TInJoinTable, TInJoinTable2>, EntityIdentifiable, new()
//            where TReturned : class, EntityIdentifiable, new()

//        {
//            public IEnumerable<TReturned> Where<TWhereIdFound>(int id) where TWhereIdFound : class, EntityIdentifiable, new()
//            {


//                return new JoinHelperMethods<TJoinTable, TInJoinTable, TInJoinTable2, TWhereIdFound, TReturned>().TJoinInner(id);

//            }

//            public IEnumerable<TReturned> WhereNot<TWhereIdNotFound>(int id) where TWhereIdNotFound : class, EntityIdentifiable, new()
//            {
//                return new JoinHelperMethods<TJoinTable, TInJoinTable, TInJoinTable2, TWhereIdNotFound, TReturned>().TJoinInnerWithout(id);

//            }




//        }






//        public class JoinHelperMethods<TJoin, TInJoin, TInJoin2, TSearchedFor, TReturned>
//            where TJoin : class, JoinTable<TJoin, TInJoin, TInJoin2>, EntityIdentifiable, new()
//            where TReturned : class, EntityIdentifiable, new()
//            where TSearchedFor : class, EntityIdentifiable, new()

//        {

//            private Func<TJoin, TSearchedFor> tSearchedForFinder;
//            private Func<TJoin, TReturned> tReturnedFinder;

//            public JoinHelperMethods()
//            {
//                tSearchedForFinder = setTFinder<TSearchedFor>(new TJoin(), tSearchedForFinder);
//                tReturnedFinder = setTFinder(new TJoin(), tReturnedFinder);
//            }




//            public bool tSearchedForExists(TJoin joinTableElement, int id)
//            {
//                return (getTSearchedForByID(joinTableElement, id) != null);
//            }

//            public TSearchedFor getTSearchedForByID(TJoin joinTableElement, int id)
//            {
//                var element = GetTSearchedFor(joinTableElement);
//                if (element != null)
//                {
//                    if (element.ID == id)
//                        return element;
//                }
//                return null;
//            }

//            public TSearchedFor GetTSearchedFor(TJoin join)
//            {

//                return tSearchedForFinder(join);
//            }

//            public TReturned GetTReturned(TJoin join)
//            {
//                setTFinder<TReturned>(join, tReturnedFinder);
//                return tReturnedFinder(join);
//            }

//            private Func<TJoin, T> setTFinder<T>(TJoin join, Func<TJoin, T> func) where T : class, new()
//            {

//                if (join.FirstInnerEntity is T)
//                {
//                    func = (j) => { return j.FirstInnerEntity as T; };
//                }

//                if (join.SecondInnerEntity is T)
//                {
//                    func = (j) => { return j.SecondInnerEntity as T; };
//                }
//                return func;
//            }


//            private IEnumerable<T> ReturnHelper<T>(int id, List<T> listToReturn, Action<List<T>, TJoin> adderFunc)
//            {
//                var set = DAO.GetDbSet<TJoin>();
//                foreach (var s in set)
//                {
//                    if (tSearchedForExists(s, id))
//                        adderFunc(listToReturn, s);
//                }
//                return listToReturn;
//            }


//            public TJoin Single(int tSearchedForId, int tReturnedId)
//            {
//                var joined = db.Set<TJoin>().ToList();
//                foreach (var j in joined)
//                {
//                    if ((GetTSearchedFor(j).ID == tSearchedForId) &&
//                        (GetTReturned(j).ID == tReturnedId))
//                        return j;
//                }
//                return null;
//            }

//            public IEnumerable<TReturned> TJoinInner(int id)
//            {
//                return ReturnHelper(id, new List<TReturned>(), (list, tjoinEntity) => { list.Add(GetTReturned(tjoinEntity)); });
//            }


//            public IEnumerable<TReturned> TJoinInnerWithout(int id)
//            {
//                var innerEntities = TJoinInner(id);
//                var idsOfInnerEntities = innerEntities.Select(p => p.ID);
//                var tReturneds = DAO.GetDbSet<TReturned>();
//                var notJoined = tReturneds.Where(t => !idsOfInnerEntities.Contains(t.ID));
//                return notJoined;
//            }
//        }

//    }

//}


//public static void CreateJoin<T1, T2, T3>(int innerEntityId, int secondInnerEntityId)
//    where T1 : class, JoinTable<T1, T2, T3>, new()
//    where T2 : class
//    where T3 : class
//{
//    var innerEntity = Find<T2>(innerEntityId);
//    var secondInnerEntity = Find<T3>(secondInnerEntityId);
//    var join = new T1().SetRelations(innerEntity, secondInnerEntity);
//    Persist(join);

//}

//public class JoinBuilder<TJoinTable>
//{
//    TJoinTable JoinTable { get; set; }


//}

//public static class JoinHelper<TJoinTable, TJoinElement>
//    where TJoinTable : class, new()
//    where TJoinElement : class, new()
//{


//    public static void Join(TJoinTable joinTable, Action<TJoinTable, TJoinElement> setter, int id)
//    {
//        setter(joinTable, db.Set(typeof(TJoinElement)).Find(id) as TJoinElement);
//    }

//    public static void Join(TJoinTable joinTable, int id)
//    {
//        joinTable.GetType().GetProperties().Each((prop) => { if (prop is TJoinElement) prop.SetValue(joinTable, db.Set(prop.PropertyType).Find(id)); });
//    }



//}