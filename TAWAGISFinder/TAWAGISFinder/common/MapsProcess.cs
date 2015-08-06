using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Phone.Maps.Services;
using Microsoft.Phone.Controls.Maps;
using System.Windows.Media;
using Windows.Devices.Geolocation;
using System.Windows;

namespace TAWAGISFinder.common
{
    class MapsProcess
    {
        bool allowShowNoti = true;
        PositionStatus _oldStatus = PositionStatus.NotAvailable;
        static Geolocator _locator = null;
        public void Initialize()
        {
            _locator = new Geolocator();
            _locator.DesiredAccuracyInMeters = 5;
            _locator.MovementThreshold = 0.1;
            _locator.StatusChanged += _locator_StatusChanged;
            _locator.PositionChanged += _locator_PositionChanged;
        }

        private void _locator_PositionChanged(Geolocator sender, PositionChangedEventArgs args)
        {
            OnLocatorPositionChanged("LocationPosition", new System.Device.Location.GeoCoordinate(args.Position.Coordinate.Latitude, args.Position.Coordinate.Longitude));
        }

        private void _locator_StatusChanged(Geolocator sender, StatusChangedEventArgs args)
        {
            _oldStatus = args.Status;
            OnLocatorStatusChanged("LocatorStatus", _oldStatus);
        }

        /// <summary>
        /// add direction points to show direction
        /// </summary>
        /// <param name="fromLocation"></param>
        /// <param name="toLocation"></param>
        /// <returns></returns>
        public void Direction(System.Device.Location.GeoCoordinate fromLocation, System.Device.Location.GeoCoordinate toLocation)
        {
            RouteQuery routeQuery = new RouteQuery();
            routeQuery.Waypoints = new List<System.Device.Location.GeoCoordinate>() { fromLocation, toLocation };
            routeQuery.QueryCompleted += routeQuery_QueryCompleted;
            routeQuery.TravelMode = TravelMode.Driving;
            routeQuery.QueryAsync();
        }

        /// <summary>
        /// Direction Route completed
        /// add cross points to polyline
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void routeQuery_QueryCompleted(object sender, QueryCompletedEventArgs<Route> e)
        {
            LocationCollection loCollect = new LocationCollection();
            for (int i = 0; i < e.Result.Geometry.Count; i++)
            {
                loCollect.Add(e.Result.Geometry[i]);
            }
            MapPolyline routeline = new MapPolyline()
            {
                StrokeThickness=8,
                Locations=loCollect,
                Stroke=new SolidColorBrush(Colors.Red)
            };
            OnRouteChanged("RouteChanged", routeline);
        }

        /// <summary>
        /// RouteChanged events is changed
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="lines"></param>
        void OnRouteChanged(string propertyName, MapPolyline lines)
        {
            if (RouteChanged != null)
                RouteChanged(lines, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Locator Status Changed Event
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="posStatus"></param>
        void OnLocatorStatusChanged(string propertyName, PositionStatus posStatus)
        {
           // if (LocatorStatusChanged != null)
                LocatorStatusChanged(posStatus, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Locator Position changed event
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="newLocation"></param>
        void OnLocatorPositionChanged(string propertyName, System.Device.Location.GeoCoordinate newLocation)
        {
            if (LocatorPositionChanged != null)
                LocatorPositionChanged(newLocation, new PropertyChangedEventArgs(propertyName));
        }


        public event PropertyChangedEventHandler RouteChanged;
        public event EventHandler LocatorStatusChanged;
        public event PropertyChangedEventHandler LocatorPositionChanged;
    }

    public class MapStatusInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string _statusColor, _statusName;
        public string StatusColor
        {
            get { return _statusColor; }
            private set
            {
                if (_statusColor != value)
                {
                    _statusColor = value;
                    OnPropertyChanged("StatusColor");
                }
            }
        }
        public string StatusName
        {
            get { return _statusName; }
            private set
            {
                if (_statusName != value)
                {
                    _statusName = value;
                    OnPropertyChanged("StatusName");
                }
            }
        }
        public void UpdateStatus(PositionStatus newStatus)
        {
            switch (newStatus)
            {
                case PositionStatus.Disabled:
                    StatusColor = "Red";
                    StatusName = "Chức năng định vị đã tắt";
                    return;
                case PositionStatus.Initializing:
                    StatusColor = "Yellow";
                    StatusName = "Đang định vị...";
                    return;
                case PositionStatus.NoData:
                    StatusColor = "Red";
                    StatusName = "Không có dữ liệu";
                    return;
                case PositionStatus.NotAvailable:
                    StatusColor = "Red";
                    StatusName = "Chức năng không có sẵn";
                    return;
                case PositionStatus.NotInitialized:
                    StatusColor = "Red";
                    StatusName = "Không thể định vị";
                    return;
                case PositionStatus.Ready:
                    StatusColor = "Green";
                    StatusName = "Đã sẵn sàng";
                    return;
            }
        }
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
