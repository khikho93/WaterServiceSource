using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Windows.Media.Imaging;
using System.IO;

namespace TAWAGISFinder.SQLiteHelper
{
  public class ProblemHistory
    {
        [SQLite.PrimaryKey,SQLite.AutoIncrement]
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string CreationDate { get; set; }
        public string ImageSource { get; set; }
        public string IdUser { get; set; }
        public ProblemHistory()
        { }
        public ProblemHistory(string imagename, string imageSource,string userID)
        {
            ImageName = imagename;
            CreationDate = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff tt");
            ImageSource = imageSource;
            IdUser=userID;
        }
    }
}
