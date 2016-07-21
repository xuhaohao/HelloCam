using HelloCam.Models;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCam.Commons
{
    class Dbs
    {
        private readonly static string DbPath = string.Format(@"Data Source={0}db\bear.db;Version=3;Pooling=True", AppDomain.CurrentDomain.BaseDirectory);

        public static ObservableCollection<Params> ParamsCache;

        private static OrmLiteConnectionFactory dbFactory;

        static Dbs()
        {
            //DbConnection.Initialise(DbPath);
            //ParamsCache = new ObservableCollection<Params>();
            //using (TableAdapter<Params> adapter = TableAdapter<Params>.Open())
            //{
            //    var list = adapter.Select();
            //    foreach (Params item in list)
            //    {
            //        ParamsCache.Add(item);
            //    }
            //}

            dbFactory = new OrmLiteConnectionFactory(DbPath, SqliteDialect.Provider);
            ParamsCache = new ObservableCollection<Params>();
            using (var db = dbFactory.Open())
            {
                var list = db.Select<Params>();
                foreach (Params item in list)
                {
                    ParamsCache.Add(item);
                }
            }

        }

        public static void UpdateParamValue(Params param)
        {
            //using (TableAdapter<Params> adapter = TableAdapter<Params>.Open())
            //{
            //    adapter.CreateUpdate(param);
            //}
            using (var db = dbFactory.Open())
            {
                db.Update(param);
            }
        }
    }
}
