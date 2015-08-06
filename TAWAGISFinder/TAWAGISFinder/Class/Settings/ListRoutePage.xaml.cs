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
using TAWAGISFinder.Models;
using TAWAGISFinder.common;
using System.Windows.Media;
using System.Windows.Data;
using TAWAGISFinder.Class.Login;

namespace TAWAGISFinder.Class.Settings
{
    public partial class ListRoutePage : PhoneApplicationPage
    {
        private static string filetxt_api = "route.txt";

        public ListRoutePage()
        {
            InitializeComponent();
            loadLstRoute();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            ListRoute route_detail = (ListRoute)list_route.SelectedItem;
            IsolatedStorageSettings ISSeting = IsolatedStorageSettings.ApplicationSettings;

            if (!ISSeting.Contains("route_selected"))
            {
                var index_route = new List<int>() { list_route.SelectedIndex };
                ISSeting.Add("route_selected", index_route);
            }
            else
            {
                List<int> lst_select = ISSeting["route_selected"] as List<int>;
                lst_select.Add(list_route.SelectedIndex);
                ISSeting["route_selected"] = lst_select;
            }
            ISSeting.Save();
            if (route_detail != null)
            {
                NavigationService.Navigate(new Uri("/Class/Customer/ListCustomerPage.xaml?rid=" + route_detail.id + "&rname=" + route_detail.name, UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Không xác định được lộ trình");
            }


        }
        private async void loadLstRoute()
        {
            IsolatedStorageSettings ISSeting = IsolatedStorageSettings.ApplicationSettings;
            List<ListRoute> route = new List<ListRoute>();
            string content_api = CFunction.ReadFile(filetxt_api);
            if (content_api != "")
                route = JsonConvert.DeserializeObject<List<ListRoute>>(content_api);


            if (route.Count() > 0)
            {
                for (var i = 0; i < route.Count(); i++)
                {
                    route[i].mau = "White";
                }
                if (ISSeting.Contains("route_selected"))
                {
                    List<int> lst_route_select = ISSeting["route_selected"] as List<int>;
                    foreach (var item in lst_route_select)
                    {
                        if (item > -1 && route[item] != null)
                            route[item].mau = "Red";
                    }
                }
                list_route.ItemsSource = route;
            }
            else
            {

                var url = "http://103.7.40.133:1000/Default.aspx?cmd=GetListRoute&user_id=" + AccountAction.user_login_id + "&token=" + AccountAction.token;
                bool url_avaiable = await common.CFunction.checkAvaiableUrl(url);
                if (url_avaiable)
                {
                    var httpClient = new HttpClient();
                    var strResult = await httpClient.GetStringAsync(url);
                    strResult = "[ { \"name\": \"Lê Duẫn\", \"id\": 5 }, { \"name\": \"Lê Lợi\", \"id\": 1 }, { \"name\": \"Nguyễn Huệ\", \"id\": 2 }, { \"name\": \"Phạm Hồng Thái\", \"id\": 6 }, { \"name\": \"Phan Chu Trinh\", \"id\": 3 }, { \"name\": \"Vạn Xuân\", \"id\": 4 } ]";//await httpClient.GetStringAsync(url);
                    CFunction.WriteFile(filetxt_api, strResult);
                    if (ISSeting.Contains("route_selected"))
                        ISSeting.Remove("route_selected");
                    try
                    {
                        route = JsonConvert.DeserializeObject<List<ListRoute>>(strResult);
                        for (var i = 0; i < route.Count(); i++)
                        {
                            route[i].mau = "White";
                        }
                        if (ISSeting.Contains("route_selected"))
                            ISSeting.Remove("route_selected");
                        list_route.ItemsSource = route;
                    }
                    catch
                    {
                        List<ApiResponse> res = JsonConvert.DeserializeObject<List<ApiResponse>>(strResult);
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




    }



}