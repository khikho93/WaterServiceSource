using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.IO;
using Windows.Storage;

namespace TAWAGISFinder.Models
{
    public class ListCustomer
    {
        [SQLite.PrimaryKey]
        public string Customer_Id { get; set; }

        public string HoTen { get; set; }

        public string DiaChi { get; set; }

        public string SDT { get; set; }

        public string MaDanhBo { get; set; }

        public string HieuDHN { get; set; }

        public string NgayTC { get; set; }

        public string TenDV { get; set; }

        public string DanhBoKTKS { get; set; }

        public string longitude { get; set; }

        public string latitude { get; set; }

        public string routeId { get; set; }

        public string ImageCause1 { get; set; }

        public string ImageCause2 { get; set; }

        public string ImageCause3 { get; set; }

        public string ImageCause4 { get; set; }

        public string ImageCause5 { get; set; }

        public string Cause { get; set; }

        public string Update { get; set; }

        private static SQLiteConnection dbConn;

        public static void createTable()
        {            
            string db_path = Path.Combine(Path.Combine(ApplicationData.Current.LocalFolder.Path, "Tawagisfinder.sqlite"));//DataBase Name               
            dbConn = new SQLiteConnection(db_path);
            /** create table if not exists */
            dbConn.CreateTable<ListCustomer>();
        }

        public void insert()
        {
            dbConn.Insert(this);
        }
        public List<ListCustomer> show(string routeid)
        {
            return dbConn.Query<ListCustomer>("select * from ListCustomer where routeId = " + routeid).ToList<ListCustomer>();
        }
        public void delete(string routeid = "")
        {
            if (String.IsNullOrEmpty(routeid))
                dbConn.DeleteAll<ListCustomer>();
            else
            {
                var del_items = dbConn.Query<ListCustomer>("select * from ListCustomer where routeId = " + routeid).ToList<ListCustomer>(); ;
                if (del_items != null)
                {
                    foreach(var item in del_items)
                        dbConn.Delete(item);
                }
            }
            //dbConn.DeleteAll<ListCustomer>();
        }

        public ListCustomer showDetail(string customerid)
        {
            return dbConn.Query<ListCustomer>("select * from ListCustomer where Customer_Id=" + customerid).FirstOrDefault();
        }

        public void closeConn()
        {
            dbConn.Close();
        }
       
        

    }
}
