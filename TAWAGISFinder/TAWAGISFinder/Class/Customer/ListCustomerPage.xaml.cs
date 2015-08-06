using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO.IsolatedStorage;
using System.Text;
//using TAWAGISFinder.Class.Entity;
using System.Diagnostics;
using TAWAGISFinder.Models;
using TAWAGISFinder.common;
using TAWAGISFinder.Class.Login;

namespace TAWAGISFinder.Class.Main
{
    public partial class ListCustomerPage : PhoneApplicationPage
    {


        public List<ListCustomer> listcustomer = new List<ListCustomer>();
        private static string filetxt_api = "customer.txt";

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);
            String navigationMessage;
            base.OnNavigatedTo(e);
            string routeid = "";
            string routename = "";
            this.loadLstCustomer(routeid);
            /* if (NavigationContext.QueryString.TryGetValue("rid", out navigationMessage))
             {

                 routeid = NavigationContext.QueryString["rid"];
                 routename = NavigationContext.QueryString["rname"];
                 IsolatedStorageSettings.ApplicationSettings["last_routeid"] = routeid;
                 IsolatedStorageSettings.ApplicationSettings["last_routename"] = routename;
                 MessageBox.Show("Danh sách khách hàng lộ trình " + routename);
                 this.loadLstCustomer(routeid);
             }
             else
             {
                 if (IsolatedStorageSettings.ApplicationSettings.Contains("last_routeid") && IsolatedStorageSettings.ApplicationSettings.Contains("last_routename"))
                 {
                     routeid = IsolatedStorageSettings.ApplicationSettings["last_routeid"].ToString();
                     routename = IsolatedStorageSettings.ApplicationSettings["last_routename"].ToString();
                     MessageBox.Show("Danh sách khách hàng lộ trình " + routename);
                     this.loadLstCustomer(routeid);
                 }
                 else 
                     MessageBox.Show("Lộ trình không xác định");
             }*/

        }
        public ListCustomerPage()
        {
            InitializeComponent();
        }
        private async void loadLstCustomer(string routeid)
        {
            List<ListCustomer> listcustomer = new List<ListCustomer>();
            string api_saved = CFunction.ReadFile(filetxt_api);
            if (api_saved != "")
            {
                listcustomer = JsonConvert.DeserializeObject<List<ListCustomer>>(api_saved);
                MessageBox.Show("Đang load dữ Liệu Offline");

            }
            if (listcustomer.Count() > 0)
            {
                list_Customer.ItemsSource = listcustomer;
            }
            else
            {
                var url = "http://103.7.40.133:1000/Default.aspx?cmd=GetListCustomer&router_id=" + routeid + "&keyword=&user_id=1&token=" + AccountAction.token;
               bool url_avaiable = await CFunction.checkAvaiableUrl(url);
                if (url_avaiable)
                {
                    var httpClient = new HttpClient();
                    string strCustomer = await httpClient.GetStringAsync(url);
                    //customer = JsonConvert.DeserializeObject<List<CustomerDetail>>(strCustomer);         
                    strCustomer = "[ {\"HoTen\": \"Đỗ Trọng Vinh\", \"DiaChi\": \"34, Phạm Hồng Thái, P4, Q1\", \"SDT\": \"0987654321\", \"Customer_Id\": \"2\", \"MaDanhBo\": \"MDB-0000002\", \"HieuDHN\": \"HDHN-0304493\", \"NgayTC\": \"6/30/2015 12:00:00 AM\", \"TenDV\": \"Cty tư vấn kỹ thuật\", \"DanhBoKTKS\": \"DB-0000002\", \"longitude\": \"106.699219\", \"latitude\": \"13.923404\" }, { \"HoTen\": \"Nguyễn Văn Bé\", \"DiaChi\": \"41, Lê Duẫn, P5, Q3\", \"SDT\": \"0987654321\", \"Customer_Id\": \"3\", \"MaDanhBo\": \"MDB-0000003\", \"HieuDHN\": \"HDHN-0104493\", \"NgayTC\": \"6/30/2015 12:00:00 AM\", \"TenDV\": \"Cty tư vấn kỹ thuật\", \"DanhBoKTKS\": \"DB-0000003\", \"longitude\": \"106.699219\", \"latitude\": \"13.923404\" }, { \"HoTen\": \"Trần Văn Trí\", \"DiaChi\": \"12, Lê Hồng Phong, P4, Q1\", \"SDT\": \"0987654321\", \"Customer_Id\": \"1\", \"MaDanhBo\": \"MDB-0000001\", \"HieuDHN\": \"HDHN-0309493\", \"NgayTC\": \"6/30/2015 12:00:00 AM\", \"TenDV\": \"Cty tư vấn kỹ thuật\", \"DanhBoKTKS\": \"DB-0000001\", \"longitude\": \"106.699219\", \"latitude\": \"13.923404\" } ]";
                    CFunction.WriteFile(filetxt_api, strCustomer);
                    listcustomer = JsonConvert.DeserializeObject<List<ListCustomer>>(strCustomer);
                    try
                    {
                        listcustomer = JsonConvert.DeserializeObject<List<ListCustomer>>(strCustomer);
                        if (listcustomer == null)
                        {
                            MessageBox.Show("Không có dữ liệu trả về");
                        }
                        list_Customer.ItemsSource = listcustomer;

                    }
                    catch
                    {
                        List<ApiResponse> res = JsonConvert.DeserializeObject<List<ApiResponse>>(strCustomer);
                        foreach (var item in res)
                        {
                            MessageBox.Show(item.slug + " " + item.content);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không thể kết nối đến máy chủ");
                }

            }
        }

        private void list_Customer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListCustomer detailcustomer = (ListCustomer)list_Customer.SelectedItem;
            if (detailcustomer != null)
            {
                string url = String.Format("/Class/Customer/DetailCustomerPage.xaml?cid={0}", detailcustomer.Customer_Id);
                NavigationService.Navigate(new Uri(url, UriKind.Relative));
            }

        }
        private void btn_search(object sender, EventArgs e)
        {
            List<ListCustomer> listcustomer = new List<ListCustomer>();
            string api_saved = CFunction.ReadFile(filetxt_api);
            if (api_saved != "")
            {
                listcustomer = JsonConvert.DeserializeObject<List<ListCustomer>>(api_saved);
                List<ListCustomer> list_search = new List<ListCustomer>();
                foreach (var item in listcustomer)
                {
                    if (item.HoTen.IndexOf(txtSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        list_search.Add(item);
                    }
                }
                list_Customer.ItemsSource = list_search;
                if (list_search == null)
                    MessageBox.Show("Không tìm thấy khách hàng");
                if (txtSearch.Text == "")
                    list_Customer.ItemsSource = listcustomer;
            }
            else
            {
                MessageBox.Show("Dữ Liệu Không Tồn Tại");
            }
        }
    }
}