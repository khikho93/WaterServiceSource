using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TAWAGISFinder.Models;
using SQLite;
using System.IO.IsolatedStorage;
using TAWAGISFinder.common;
using Newtonsoft.Json;

namespace TAWAGISFinder.Class.Customer
{
    public partial class DetailCustomerPage : PhoneApplicationPage
    {
        private static string filetxt_api = "customer.txt";
        public DetailCustomerPage()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);
            String navigationMessage;
            base.OnNavigatedTo(e);
            if (NavigationContext.QueryString.TryGetValue("cid", out navigationMessage))
            {

                string customerid = NavigationContext.QueryString["cid"];
                this.loadDetailCustomer(customerid);
            }
            else
            {
                this.loadDetailCustomer("");
            }

        }
        public void loadDetailCustomer(string customerid)
        {
            ListCustomer db_customer = new ListCustomer();
            var api_saved = CFunction.ReadFile(filetxt_api);
            if (api_saved != "")
            {
                var listcustomer = JsonConvert.DeserializeObject<List<ListCustomer>>(api_saved);
                db_customer = (from item in listcustomer where item.Customer_Id == customerid select item).FirstOrDefault();

            }
            lbHoTen.DataContext = db_customer.HoTen;
            lbDiaChi.DataContext = db_customer.DiaChi;
            lbSDT.DataContext = db_customer.SDT;
            lbLoTrinh.DataContext = db_customer.routeId;
            /* if (IsolatedStorageSettings.ApplicationSettings.Contains("last_routename"))
                lbLoTrinh.DataContext = IsolatedStorageSettings.ApplicationSettings["last_routename"];
            else {
                ListRoute route = new ListRoute();
                var data_route = route.show(db_customer.routeId);
                lbLoTrinh.DataContext = data_route[0].name;
            }*/
            lbDanhBa.DataContext = db_customer.MaDanhBo;

        }
    }
}