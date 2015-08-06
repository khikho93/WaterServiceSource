using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAWAGISFinder.common
{
    class CustomerInfo
    {
        public string CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string ExecuteUnit { get; set; }
        public int ClockBrand { get; set; }
        public DateTime SetupDate { get; set; }
        public string ServiceName { get; set; }
        public string Nominally { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double Yield1 { get; set; }
        public double Yield2 { get; set; }
        public double Yield3 { get; set; }
        public double Yield4 { get; set; }
        public double Yield5 { get; set; }
        public double Yield6 { get; set; }
        public double Yield7 { get; set; }
        public double Yield8 { get; set; }
        public double Yield9 { get; set; }
        public double Yield10 { get; set; }
        public double Yield11 { get; set; }
        public double Yield12 { get; set; }
        public int OldIndexing { get; set; }
        public int NewIndexing { get; set; }
        public double RoadMapAreaID { get; set; }
        public double RoadMapID { get; set; }
        public double BodyClockNumber { get; set; }
        public GeoCoordinate CustomerLocation
        {
            get
            {
                var value = new GeoCoordinate(Latitude, Longitude);
                return value;
            }
        }
        public string Address { get; set; }
       
    }
}
