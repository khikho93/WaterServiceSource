using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace TAWAGISFinder.SQLiteHelper
{
   public class DataSqliteModel
    {
        SQLiteConnection dbconn;

        public async Task<bool> onCreate(string DB_PATH)
        {
            try
            {
                if (!await CheckFileExists(DB_PATH))
                {
                    using (dbconn = new SQLiteConnection(DB_PATH))
                    {
                        dbconn.CreateTable<ProblemHistory>();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> CheckFileExists(string filename)
        {
            try
            {
                var store = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Get a Contact
        public ProblemHistory ReadProblemHistory(int Problemid)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var existingProblemHistoryList = dbConn.Query<ProblemHistory>("Select * from ProblemHistory where Id = " + Problemid).FirstOrDefault();
                return existingProblemHistoryList;
            }
        }
        //Get a List Conatact
        public ObservableCollection<ProblemHistory> ReadProblemHistory()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<ProblemHistory> myCollection = dbConn.Table<ProblemHistory>().ToList<ProblemHistory>();
                ObservableCollection<ProblemHistory> ContactList = new ObservableCollection<ProblemHistory>(myCollection);
                return ContactList;
            }
        }
        //Find a List Contact 
        public ObservableCollection<ProblemHistory> ReadProblemHistory(string CreationDate, string Location)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                List<ProblemHistory> myCollection = dbConn.Table<ProblemHistory>().Where(m => m.CreationDate == CreationDate).ToList();
                ObservableCollection<ProblemHistory> ProblemHistoryList = new ObservableCollection<ProblemHistory>(myCollection);
                return ProblemHistoryList;

            }
        }

        //Update Existing Contact
        public void UpdateProblemHistory(ProblemHistory problemhistory)
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                var exisitProblemHistory = dbConn.Query<ProblemHistory>("Select * from ProblemHistory where Id =" + problemhistory.Id).FirstOrDefault();
                if (exisitProblemHistory != null)
                {
                    exisitProblemHistory.ImageName = problemhistory.ImageName;
                    exisitProblemHistory.ImageSource = problemhistory.ImageSource;
                    exisitProblemHistory.CreationDate = problemhistory.CreationDate;
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Update(exisitProblemHistory);
                    });
                }

            }
        }
        //Insert a Contact to Database Table
        public bool InsertProblemHistory(ProblemHistory newProblemHistory)
        {
            try
            {
                using (var dbConn = new SQLiteConnection(App.DB_PATH))
                {
                    dbConn.RunInTransaction(() =>
                    {
                        dbConn.Insert(newProblemHistory);
                    });
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        //Delete a Contact
        public bool Delete(ProblemHistory deleteItem)
        {
            try
            {
                using (var dbConn = new SQLiteConnection(App.DB_PATH))
                {
                    var existingProblemHistory = dbConn.Query<ProblemHistory>("Select * from ProblemHistory where Id =" + deleteItem.Id).FirstOrDefault();
                    if (existingProblemHistory != null)
                    {
                        dbConn.RunInTransaction(() =>
                        {
                            dbConn.Delete(existingProblemHistory);
                            System.IO.File.Delete(deleteItem.ImageSource);
                        });
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public void DeleteAllContact()
        {
            using (var dbConn = new SQLiteConnection(App.DB_PATH))
            {
                dbConn.DropTable<ProblemHistory>();
                dbConn.CreateTable<ProblemHistory>();
                dbConn.Dispose();
                dbConn.Close();
            }
        }
        public ObservableCollection<ProblemHistory> ReadAllProblemHistory()
        {
            return ReadProblemHistory();
        }
    }
  
}
