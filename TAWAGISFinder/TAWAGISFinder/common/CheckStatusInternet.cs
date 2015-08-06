using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
enum InternetStatus
{
    Connected=0,
    NotConnect=1
}

namespace TAWAGISFinder.common
{
    class CheckStatusInternet
    {
        ConnectionProfile connectProfile = null ;
        public event EventHandler InternetStatusChanged;
        InternetStatus _currentStatus;
        public CheckStatusInternet()
        {
            connectProfile = NetworkInformation.GetInternetConnectionProfile();
        }
        public bool CheckInternetConnected()
        {

            try
            {
                if (connectProfile != null && connectProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess)
                {
                    _currentStatus = InternetStatus.Connected;
                    return true;
                }
                else
                {
                    Thread thread = new Thread(new ThreadStart(ListeningInternet));
                    thread.Start();
                    _currentStatus = InternetStatus.NotConnect;
                    ListeningInternet();
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        private void ListeningInternet()
        {
            while (true)
            {
                if (connectProfile != null && connectProfile.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess && _currentStatus== InternetStatus.NotConnect)
                {
                    _currentStatus = InternetStatus.Connected;
                    InternetStatusChanged(true, new EventArgs());
                }

            }
        }
    }
}
