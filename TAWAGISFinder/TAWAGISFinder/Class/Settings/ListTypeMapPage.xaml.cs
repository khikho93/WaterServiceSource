using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace TAWAGISFinder.Class.Settings
{
    public partial class ListTypeMapPage : PhoneApplicationPage
    {
        List<string> listmap = new List<string>() { "Google Map (Mặc định)", "Bing Map", "OpenStreetMap" };
        public ListTypeMapPage()
        {
            InitializeComponent();
            list_map.ItemsSource = listmap;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e) 
        {
            MessageBox.Show(list_map.SelectedItem.ToString());
        }
    }
}