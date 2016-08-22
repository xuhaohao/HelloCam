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
            using (var db = dbFactory.Open())
            {
                db.Update(param);
            }
        }

        public static void Insert<T>(T t) {
            using (var db = dbFactory.Open())
            {
                db.Insert(t);
            }
        }

        public static void Delete<T>(T t) {
            using (var db = dbFactory.Open())
            {
                db.Delete(t);
            }
        }

        public static void Update<T>(T t)
        {
            using (var db = dbFactory.Open())
            {
                db.Update<T>(t);
            }
        }

        public static List<T> GetAll<T>(string condition)
        {
            using (var db = dbFactory.Open())
            {
                return db.Select<T>(condition);
            }
        }

        public static void Init()
        {
            using (var db = dbFactory.Open())
            {
                db.CreateTables(true, typeof(SubGroups), typeof(Persons),typeof(CheckInLog));
            }

        }

        public static void CheckInLog(string groupId, string personName, int action) {
            using (var db = dbFactory.Open())
            {
                CheckInLog checkInLog = new CheckInLog();
                checkInLog.GroupId = groupId;
                checkInLog.PersonName = personName;
                checkInLog.Action = action;
                checkInLog.TimeTag = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                db.Insert(checkInLog);
            }
        }
    }
}
