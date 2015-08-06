using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using Windows.Storage;
using System.Threading;
enum NetworkStatus
{

}

namespace TAWAGISFinder.common
{
    class CFunction
    {
       
        public static bool checkInterner()
        {

            return Microsoft.Phone.Net.NetworkInformation.NetworkInterface.NetworkInterfaceType !=
Microsoft.Phone.Net.NetworkInformation.NetworkInterfaceType.None;
        }

        public async static Task<bool> checkAvaiableUrl(string url)
        {
            var httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Head, url);
            HttpResponseMessage response = await httpClient.SendAsync(request);
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            else
                return false;
        }

        public static void WriteFile(string filename, string content)
        {
            IsolatedStorageFile ISF = IsolatedStorageFile.GetUserStoreForApplication();
            //create new file
            using (StreamWriter SW = new StreamWriter(new IsolatedStorageFileStream(filename, FileMode.Create, FileAccess.Write, ISF)))
            {
                SW.WriteLine(content);
                SW.Close();
            }
        }
        public static string ReadFile(string filename)
        {
            string result = "";
            IsolatedStorageFile ISF = IsolatedStorageFile.GetUserStoreForApplication();
            if (ISF.FileExists(filename))
            {
                IsolatedStorageFileStream FS = ISF.OpenFile(filename, FileMode.Open, FileAccess.Read);
                using (StreamReader SR = new StreamReader(FS))
                {
                    result = SR.ReadLine();
                }
            }
            return result;
        }
    }
}
