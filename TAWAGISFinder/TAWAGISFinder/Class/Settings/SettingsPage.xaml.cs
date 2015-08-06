using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TAWAGISFinder.Class.Settings;

namespace TAWAGISFinder.Class.Main
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void btnabout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Class/Settings/AboutPage.xaml", UriKind.Relative)); 
        }

        private void btnchangepass_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Class/Settings/ChangePasswordPage.xaml", UriKind.Relative));
        }

        private void btnlistroute_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Class/Settings/ListRoutePage.xaml", UriKind.Relative));
        }

        private void btnlistmap_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Class/Settings/ListTypeMapPage.xaml", UriKind.Relative));
        }
    }
}