﻿#pragma checksum "E:\Learn\Project\WaterService\TAWAGISFinder\TAWAGISFinder\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8D2B99AB828140B71F086D83DF82EE75"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace TAWAGISFinder {
    
    
    public partial class MainPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.StackPanel MainPanel;
        
        internal System.Windows.Controls.TextBox txt_username;
        
        internal System.Windows.Controls.PasswordBox txt_password;
        
        internal System.Windows.Controls.Button login_btn;
        
        internal System.Windows.Controls.Button cancel_btn;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/TAWAGISFinder;component/MainPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.MainPanel = ((System.Windows.Controls.StackPanel)(this.FindName("MainPanel")));
            this.txt_username = ((System.Windows.Controls.TextBox)(this.FindName("txt_username")));
            this.txt_password = ((System.Windows.Controls.PasswordBox)(this.FindName("txt_password")));
            this.login_btn = ((System.Windows.Controls.Button)(this.FindName("login_btn")));
            this.cancel_btn = ((System.Windows.Controls.Button)(this.FindName("cancel_btn")));
        }
    }
}

