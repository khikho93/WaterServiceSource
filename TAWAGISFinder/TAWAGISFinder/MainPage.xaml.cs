using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TAWAGISFinder.Resources;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO.IsolatedStorage;
//using TAWAGISFinder.Class.Entity;
using TAWAGISFinder.Models;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using TAWAGISFinder.common;
using TAWAGISFinder.Class.Login;

namespace TAWAGISFinder
{
    public partial class MainPage : PhoneApplicationPage
    {
        public static string user_login_id { set; get; }
        public static string token { set; get; }
        public static string filename = "infologin.txt";


        // Constructor
        public MainPage()
        {
            InitializeComponent();
           
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);
            IsolatedStorageSettings ISSeting = IsolatedStorageSettings.ApplicationSettings;
            if (ISSeting.Contains("token"))
            {
                //ISSeting.Remove();
                NavigationService.Navigate(new Uri("/Class/Main/HomePage.xaml", UriKind.Relative));                
            }
            //this.txt_username.Text = "";
            //this.txt_password.Password = "";
        }

        private async void btnlogin_Click(object sender, RoutedEventArgs e)
        {
            //sử dụng main page 
            var action = new AccountAction();
            var isTrue = await action.Login(txt_username.Text, txt_password.Password.ToString());
             //if(isTrue)
               NavigationService.Navigate(new Uri("/Class/Main/HomePage.xaml", UriKind.Relative));
           
        }

        private void btncancel_Click(object sender, RoutedEventArgs e)
        {
            IsolatedStorageSettings.ApplicationSettings.Clear();
            /*this.txt_username.Text = "";
            this.txt_password.Password = "";
            MessageBox.Show("Đăng Xuất thành công!");*/
        }



    }
}