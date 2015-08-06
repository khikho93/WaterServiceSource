using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAWAGISFinder.Class.Login
{
    public class UserDataActionServer
    {
       private static string  MAIN_URL = "http://103.7.40.133:1000/Default.aspx?";
        //pragma mark - function login
        public static string getlogin(string username,string pass)
        {
            return string.Format("{0}cmd=CheckLogin&user={1}&pass={2}", MAIN_URL, username, pass);
        }
        //pragma mark - change password
        public static string changePassword(string username,string oldpass,string newpass,string token)
        {
            return string.Format("{0}cmd=ChangePassword&user={1}&pass={2}&newpass={3}&token={4}", MAIN_URL, username, oldpass, newpass, token);
        }
        //pragma upload image error
        public static string uploadPhotoEvent(string token,string  customer)
        {
            return string.Format("{0}token={1}&customer_id={2}", MAIN_URL, token,customer);
        }
        //pragma mark - get list route user
        public static string getListRouter(int userid,string token)
        {
            return string.Format("{0}cmd=GetListRoute&user_id={1}&token={2}", MAIN_URL, userid, token);
        }
        //pragma mark - get list customer with route
        public static string getListCustomerWithIdRoute(string routerId,string keyword,int userId,string tokenId)
        {
            return string.Format("{0}cmd=GetListCustomer&router_id={1}&keyword={2}&user_id={3}&token={4}", MAIN_URL, routerId, keyword, userId, tokenId);
        }
        //pragma mark - get list customer with route
        public static string getListCustomerWithIdRoute(string routerId,string keyword,string userid,string tokenId)
        {
            return string.Format("{0}cmd=GetListCustomer&router_id={1}&keyword={2}&user_id=%d&token={3}", MAIN_URL, routerId, keyword, userid, tokenId);
        }
        //pragma mark - update location user
        public static string updateLocation(float longlocation,float latlocation,string customerId,string token)
        {
            return string.Format("{0}cmd=UpdatePositionCustomer&longitude={1}&latitude={2}&customer_id={3}&token={4}", MAIN_URL, longlocation, latlocation, customerId, token);
        }
        //pragma mark - utilization use water 
        public static string showutilizationusewater(int customerId,int month,int Year,string tokenId)
        {
            return string.Format("{0}cmd=GetIndexesByCustomer&customer_id={1}&month={2}&year={3}&token={4}", MAIN_URL, customerId, month, Year, tokenId);
        }
        public static string logout(string token)
        {
            return string.Format("{0}cmd=Logout&token={1}", MAIN_URL, token);
        }
        //pragma mark - get all customer
        public static string GetallCustomerOffline(int userid,string token)
        {
            var date = DateTime.Now.ToString("MM/dd/yyyy").Split('/');
            int year = Int32.Parse(date[2].ToString());
            int month = Int32.Parse(date[0].ToString());
            return string.Format("{0}cmd=GetListCustomer&router_id=&user_id={1}&keyword=&token={2}&month={3}&year={4}&IsOnline=0", MAIN_URL, userid, token, month, year);
        }
        public static string SearchCustomerOnline(int userid,string key,string token)
        {
            var date = DateTime.Now.ToString("MM/dd/yyyy").Split('/');
            int year = Int32.Parse(date[2].ToString());
            int month = Int32.Parse(date[0].ToString());
            return string.Format("{0}cmd=GetListCustomer&router_id=&user_id=&keyword={1}&token={2}&month={3}&year={4}&IsOnline=1", MAIN_URL, key, token, month, year);

        }
        //pragma mark - post update data
        public static string PostData(string token,string address,string comment,float longitude,float latitude)
        {
            return string.Format("{0}cmd=UploadFile&DBDongHoNu=&token={1}&address={2}&comment={3}&longitude={4}&latitude={5}", MAIN_URL, token, address, comment, longitude, latitude);
        }
    }
}
