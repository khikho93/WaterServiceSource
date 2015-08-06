using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using TAWAGISFinder.common;
using TAWAGISFinder.Models;

namespace TAWAGISFinder.Class.Login
{
    class AccountAction
    {
        private static string filename = "infologin.txt";
        private string olduser { get; set; }
        private string oldpass { get; set; }
        public static string user_login_id { set; get; }
        public static string token { set; get; }
        public static void Logout()
        {
            var ISSetting = IsolatedStorageSettings.ApplicationSettings;
            if (ISSetting.Contains("token"))
            {
                UserDataActionServer.logout((string)ISSetting["token"]);
                ISSetting.Remove("token");
                IsolatedStorageSettings.ApplicationSettings.Clear();
                var istorageFile = IsolatedStorageFile.GetUserStoreForApplication();
                if(istorageFile.FileExists(filename))
                    istorageFile.DeleteFile(filename);

            }
        }
        public async Task<bool> Login(string _user, string _pass)
        {
            CheckStatusInternet checkstatus = new CheckStatusInternet();
            checkstatus.InternetStatusChanged += Checkstatus_InternetStatusChanged;
            bool isNetWork = checkstatus.CheckInternetConnected();
            if (String.IsNullOrEmpty(_user) || String.IsNullOrEmpty(_pass))
            {
                MessageBox.Show("Nhập đầy đủ tên đăng nhập và tài khoản");
                return false;
            }
            else if (!isNetWork)
            {
                MessageBox.Show("Mất kết nối internet");
                return false;
            }
            else
            {
                // var url = "http://103.7.40.133:1000/Default.aspx?cmd=CheckLogin&user=" + txt_username.Text.ToString() + "&pass=" + txt_password.Password.ToString();
                var url = UserDataActionServer.getlogin(_user, _pass);
                bool url_avaiable = await common.CFunction.checkAvaiableUrl(url);
                if (url_avaiable)
                {
                    var httpClient = new HttpClient();
                    var strResult = await httpClient.GetStringAsync(url);
                    //strResult = "{ \"username\": \"nhannt\", \"fullname\": \"Nhan\", \"email\": \"nhandev1110@gmail.com \", \"token\": \"Nw6Yl2Gk0E2dYCiNiaMgUQ==\", \"department_id\": 1, \"id\": 1, \"is_admin\": false }";
                    if (strResult == "null")
                    {
                        MessageBox.Show("Tài khoản đăng nhập không chính xác");
                        return false;
                    }
                    else {
                        try {
                            IsolatedStorageSettings ISSeting = IsolatedStorageSettings.ApplicationSettings;
                            MessageBox.Show("Đăng Nhập Thành Công");
                            UserLogin user = JsonConvert.DeserializeObject<UserLogin>(strResult);
                            if (!ISSeting.Contains("token"))
                            {
                                ISSeting.Add("token", user.token);
                                ISSeting.Save();
                            }
                            user_login_id = user.id;
                            olduser = _user;
                            oldpass = _pass;
                            token = user.token;
                            CFunction.WriteFile(filename, strResult);
                            return true;
                        }
                        catch{
                            List<ApiResponse> res = JsonConvert.DeserializeObject<List<ApiResponse>>(strResult);
                            foreach (var item in res)
                            {
                                MessageBox.Show(item.slug + " " + item.content);
                            }
                            return false;
                        }

                    }
                }
                else {
                    MessageBox.Show("Không thể kết nối đến máy chủ");
                    return false;
                }
            }
            
        }
        private void Checkstatus_InternetStatusChanged(object sender, EventArgs e)
        {
            Logout();
            TryGetTokenUser(olduser, oldpass);
        }
        public async void TryGetTokenUser(string _olduser, string _oldpass)
        {
            try {
                var url = UserDataActionServer.getlogin(_olduser, _oldpass);
                bool url_avaiable = await common.CFunction.checkAvaiableUrl(url);
                if (url_avaiable)
                {
                    var httpClient = new HttpClient();
                    var strResult = await httpClient.GetStringAsync(url);

                    IsolatedStorageSettings ISSeting = IsolatedStorageSettings.ApplicationSettings;
                    UserLogin user = JsonConvert.DeserializeObject<UserLogin>(strResult);
                    if (!ISSeting.Contains("token"))
                    {
                        ISSeting.Add("token", user.token);
                        ISSeting.Save();
                    }
                    user_login_id = user.id;
                    olduser = _olduser;
                    oldpass = _oldpass;
                    token = user.token;
                    CFunction.WriteFile(filename, strResult);
                }

            }
            catch { }
          }
    }
}

