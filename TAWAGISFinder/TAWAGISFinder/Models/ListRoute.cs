using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Windows.Storage;
using System.IO;


namespace TAWAGISFinder.Models
{
    public class ListRoute
    {
        [SQLite.PrimaryKey]
        public string id { set; get; }
        public string name { set; get; }
        public string mau { set; get; }

        private SQLiteConnection dbConn;


        public void createTable()
        {
            string db_path = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "Tawagisfinder.sqlite"));//DataBase Name               
            dbConn = new SQLiteConnection(db_path);
            /** create table if not exists */
            dbConn.CreateTable<ListRoute>();

        }

        public void insert()
        {
            dbConn.Insert(this);
        }
        public List<ListRoute> show(string routeid = null)
        {
            if (String.IsNullOrEmpty(routeid) )
                return dbConn.Query<ListRoute>("select * from ListRoute ").ToList<ListRoute>();
            else
                return dbConn.Query<ListRoute>("select * from ListRoute where id = "+routeid).ToList<ListRoute>();
            //return dbConn.Table<ListRoute>().Where("id=5").ToList<ListRoute>();
            //return dbConn.Table<ListRoute>().ToList<ListRoute>();
        }
        public void delete()
        {
            dbConn.DeleteAll<ListRoute>();
        }
        public void afterTable()
        {
            dbConn.DropTable<ListRoute>();
            dbConn.CreateTable<ListRoute>();
            dbConn.Dispose();
            dbConn.Close();
        }
    }

}
