﻿#pragma checksum "\\WIN-7HE5GALRCG8\Users\Public\Share\TAWAGISFinder\TAWAGISFinder\Class\Settings\ChangePasswordPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F2D72F5C8B9AAC401B98B30208CDF0C7"
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


namespace TAWAGISFinder.Class.Settings {
    
    
    public partial class ChangePasswordPage : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.StackPanel MainPanel;
        
        internal System.Windows.Controls.PasswordBox txt_passwordnew;
        
        internal System.Windows.Controls.PasswordBox txt_passwordagain;
        
        internal System.Windows.Controls.Button changepass_btn;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/TAWAGISFinder;component/Class/Settings/ChangePasswordPage.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.MainPanel = ((System.Windows.Controls.StackPanel)(this.FindName("MainPanel")));
            this.txt_passwordnew = ((System.Windows.Controls.PasswordBox)(this.FindName("txt_passwordnew")));
            this.txt_passwordagain = ((System.Windows.Controls.PasswordBox)(this.FindName("txt_passwordagain")));
            this.changepass_btn = ((System.Windows.Controls.Button)(this.FindName("changepass_btn")));
        }
    }
}

