using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using TAWAGISFinder.Class.Login;

namespace TAWAGISFinder.Class.Main
{
    public partial class SynchronizDataPage : PhoneApplicationPage
    {
        public SynchronizDataPage()
        {
            InitializeComponent();
        }

        private void btn_Dangxuat(object sender, RoutedEventArgs e)
        {
            AccountAction.Logout();
        }
    }
}