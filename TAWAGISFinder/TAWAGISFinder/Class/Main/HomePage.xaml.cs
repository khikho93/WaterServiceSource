using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Controls.Maps;
using System.Device.Location;
using TAWAGISFinder.Class.ImplementMap;
using System.Windows.Media;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Maps;
using Windows.Devices.Geolocation;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Maps.Controls;
using TAWAGISFinder.SQLiteHelper;
using TAWAGISFinder.common;
using TAWAGISFinder.Class.Customer;
using System.IO;

namespace TAWAGISFinder.Class.Main
{
    public partial class MenuPage : PhoneApplicationPage
    {
        PositionStatus _mapCurrentStatus= PositionStatus.NotAvailable;
        System.Device.Location.GeoCoordinate _yourLocation=null;
        ContentControl _markIcon = null;
        StackPanel _currentCustomerContentTapped=null;
        List<common.CustomerInfo> _customers = null;
        common.MapsProcess _mapProcess;
        common.MapStatusInfo _mapStatus;
        DataSqliteModel _SqLite;
        System.Collections.ObjectModel.ObservableCollection<ProblemHistory> _ImagesData=null;
        CustomerImages UCImages =null;
        CustomerInfo _currentCustomer;
        TAWAGISFinder.Class.DataParsing.JSONParsing _JsonParsing;
        
        public MenuPage()
        {
            InitializeComponent();
            myMapControl.Mode = new RoadMode();
            _mapProcess = new common.MapsProcess();
            _mapProcess.RouteChanged += _mapProcess_RouteChanged;
            _mapProcess.LocatorPositionChanged += _mapProcess_LocatorPositionChanged;
            _mapProcess.LocatorStatusChanged += _mapProcess_LocatorStatusChanged;
            _mapProcess.Initialize();

            _mapStatus = new common.MapStatusInfo();
            MapStatusNoti.DataContext = _mapStatus;

            _SqLite = new DataSqliteModel();
            _ImagesData = _SqLite.ReadAllProblemHistory();

            UCImages = new CustomerImages();
            LayoutRoot.Children.Add(UCImages);

            _JsonParsing = new DataParsing.JSONParsing();

            MapsDirectionsTask mapsDirectionsTask = new MapsDirectionsTask();

            // You can specify a label and a geocoordinate for the end point.
            GeoCoordinate spaceNeedleLocation = new GeoCoordinate(16.4692079, 107.567869);
             LabeledMapLocation spaceNeedleLML = new LabeledMapLocation("Space Needle", spaceNeedleLocation);

             GeoCoordinate spaceNeedleLocation1 = new GeoCoordinate(16.470819, 107.571595);
             LabeledMapLocation spaceNeedleLML1 = new LabeledMapLocation("Space Needle2", spaceNeedleLocation1);

            // If you set the geocoordinate parameter to null, the label parameter is used as a search term.
           // LabeledMapLocation spaceNeedleLML = new LabeledMapLocation("Space Needle", null);
            mapsDirectionsTask.Start = spaceNeedleLML1;
            mapsDirectionsTask.End = spaceNeedleLML;

            // If mapsDirectionsTask.Start is not set, the user's current location is used as the start point.

            mapsDirectionsTask.Show();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Uri path = new Uri("ms-appx:///CustomerDataInfoSample.txt", UriKind.RelativeOrAbsolute);
            var file = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(path);
            using (StreamReader reader = new StreamReader(await file.OpenStreamForReadAsync()))
            {
                string jsonText = reader.ReadToEnd();
                _customers=_JsonParsing.GetCustomers(jsonText);
            }
            RoadmapLayer.DataContext = _customers;
        }
        
        private void btn_locationcustomer(object sender, EventArgs e)
        {
          
        }

        private void btn_takecamera(object sender, EventArgs e)
        {
            // Start at Microsoft in Redmond, Washington.
        }

        private void btn_settings(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Class/Settings/SettingsPage.xaml", UriKind.Relative));
        }

        private void btn_listcustomer(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Class/Customer/ListCustomerPage.xaml", UriKind.Relative));
        }

        private void btn_insertData(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Class/ProcessData/ReceiveDataFromServerPage.xaml", UriKind.Relative));
        }

        private void btn_transferData(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Class/ProcessData/SynchronizDataPage.xaml", UriKind.Relative));
        }


        #region Maps events

        /// <summary>
        /// Set token for map
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapCotrol_Loaded(object sender, RoutedEventArgs e)
        {
            MapsSettings.ApplicationContext.ApplicationId = "";
            MapsSettings.ApplicationContext.AuthenticationToken = "";
        }

        /// <summary>
        /// add your mark location
        /// </summary>
        void AddYourLocation()
        {
            _markIcon = new ContentControl(){
                ContentTemplate=Resources["YourMarkIcon"] as DataTemplate
            };
            myLayer.AddChild(_markIcon, _yourLocation, PositionOrigin.BottomCenter);
        }

        /// <summary>
        /// Show customer Info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _yourMarkIcon_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
        }

        /// <summary>
        /// when route result changed, add route to map control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _mapProcess_RouteChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var route = sender as Microsoft.Phone.Controls.Maps.MapPolyline;
            RouteLayer.Children.Add(route);
        }

        /// <summary>
        /// Get map status and show notification out UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _mapProcess_LocatorStatusChanged(object sender, EventArgs e)
        {
            _mapCurrentStatus = (PositionStatus)sender;
            Dispatcher.BeginInvoke(() =>
            {
                _mapStatus.UpdateStatus(_mapCurrentStatus);
                if (ShowNoti_Ani.GetCurrentState() == System.Windows.Media.Animation.ClockState.Active || ShowNoti_Ani.GetCurrentState() == System.Windows.Media.Animation.ClockState.Filling)
                    ShowNoti_Ani.Stop();
                if(MapStatusNoti.Visibility== System.Windows.Visibility.Collapsed)
                    MapStatusNoti.Visibility = System.Windows.Visibility.Visible;
                ShowNoti_Ani.Begin();
            });
        }

        /// <summary>
        /// if your location is added-> update location
        /// else -> add your location mark
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _mapProcess_LocatorPositionChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            _yourLocation = sender as GeoCoordinate;
            if (_markIcon == null)
            Dispatcher.BeginInvoke(() =>
            {
                AddYourLocation();
                myMapControl.SetView(_yourLocation, 14, 0.2);
            });
            else
                Dispatcher.BeginInvoke(() =>
                {
                    Microsoft.Phone.Controls.Maps.MapLayer.SetPosition(_markIcon, _yourLocation);
                });
        }

        /// <summary>
        /// when your tapped customer point, show custommize menu to select a property
        /// in scene, i direction map from to your location to customer location
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomerPoint_Tapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            var customerContext = ((sender as Image).Parent as StackPanel).DataContext as common.CustomerInfo;
            _currentCustomer = customerContext;
            MarkIcon_Tapped(sender as Image);
        }

        /// <summary>
        /// Add mutiple points
        /// </summary>
        /// <param name="customers"></param>
        private void AddCustomersPoints(List<common.CustomerInfo> customers)
        {
            if (customers != null)
                RoadmapLayer.DataContext = customers;
        }

        /// <summary>
        /// Clear customers points
        /// </summary>
        private void ClearCustomersPoints()
        {
            RoadmapLayer.DataContext = null;
        }

        /// <summary>
        /// When show noti about 2 seconds, Noti control will hide and stop animation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowMapStatusNOtification_Completed(object sender, EventArgs e)
        {
            ShowNoti_Ani.Stop();
            MapStatusNoti.Visibility = System.Windows.Visibility.Collapsed;
        }

        /// <summary>
        /// Direction from your location to customer location is tapped
        /// </summary>
        private void Direction()
        {
            RouteLayer.Children.Clear();
            if (_mapCurrentStatus == PositionStatus.Disabled)
                MessageBox.Show("Chức năng định vị đã tắt, vui lòng cài đặt lại chức năng định vị để sử dụng chức năng này");
            else
            {
                if (_mapCurrentStatus == PositionStatus.Ready)
                {
                    _mapProcess.Direction(_yourLocation, _currentCustomer.CustomerLocation);
                    myMapControl.SetView(_currentCustomer.CustomerLocation, 13);
                }
            }
        }

        /// <summary>
        /// Show new location tapped, hide old location showing
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YourLocation_Tapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MarkIcon_Tapped(sender as Image);
        }

        /// <summary>
        /// Push pin Tapped
        /// </summary>
        /// <param name="currentInfo"></param>
        private void MarkIcon_Tapped(Image pushIcon)
        {
            if (_currentCustomerContentTapped != null)
                _currentCustomerContentTapped.Visibility = System.Windows.Visibility.Collapsed;
            var newCustomerContentTapped = pushIcon.DataContext as System.Windows.Controls.StackPanel;
            newCustomerContentTapped.Visibility = System.Windows.Visibility.Visible;
            _currentCustomerContentTapped = newCustomerContentTapped;
        }

        #endregion


        #region SQLite
        private void ShowProblemImages(CustomerInfo currentInfo)
        {
            UCImages.DataContext = from i in _ImagesData where i.IdUser==currentInfo.CustomerID select i;
            UCImages.Visibility = System.Windows.Visibility.Visible;
        }
        private void HideProblemImage()
        {
            UCImages.Visibility = System.Windows.Visibility.Collapsed;
        }
        private void ShowCamera()
        {
            var cameratask = new CameraCaptureTask();
            cameratask.Completed += Cameratask_Completed;
            cameratask.Show();
        }
        private async void Cameratask_Completed(object sender, PhotoResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                // create file Name 
                DateTime imageID = DateTime.Now;
                var imageSource = await Windows.Storage.ApplicationData.Current.LocalFolder.CreateFileAsync
                    (
                    string.Format(
                    "{0}{1}{2}{3}{4}{5}.jpeg",imageID.Year,imageID.Month,imageID.Day,imageID.Hour,imageID.Minute,imageID.Second
                    ),Windows.Storage.CreationCollisionOption.ReplaceExisting
                    );
                // copy file
                using (var imageSourceStream = await imageSource.OpenStreamForWriteAsync())
                {
                    await e.ChosenPhoto.CopyToAsync(imageSourceStream);
                    imageSourceStream.Close();
                }
                //insert to SQLITE
                if (!_SqLite.InsertProblemHistory(new ProblemHistory(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss.fff tt"),imageSource.Path, _currentCustomer.CustomerID)))
                    MessageBox.Show("không thể thêm ảnh, vui lòng thử lại sau");
                else
                    _ImagesData = _SqLite.ReadAllProblemHistory();
            }
        }
        #endregion

        private void YourLocationContent_Tapped(object sender, System.Windows.Input.GestureEventArgs e)
        {
            
        }

        private void CustomerMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerMenu.SelectedItem != null)
            {
                switch (CustomerMenu.SelectedIndex)
                {
                    case 0:// Customer Info
                        break;
                    case 1://Direction
                        Direction();
                        break;
                    case 2://yield water info
                        break;
                    case 3://update problem
                        break;
                    case 4://take problem photo
                        break;
                    case 5://update location
                        break;
                    case 6://show problem images
                        break;
                }
                CustomerMenu.Visibility = System.Windows.Visibility.Collapsed;
            }
            CustomerMenu.SelectedItem = null;
        }

        private void yourLocationContent_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            CustomerMenu.Visibility = System.Windows.Visibility.Visible;
        }
    }
}