using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAWAGISFinder.common;

namespace TAWAGISFinder.Class.DataParsing
{
    class JSONParsing
    {
        public List<CustomerInfo> GetCustomers(string dataParsing)
        {
            List<CustomerInfo> customers = new List<CustomerInfo>();
            try
            {
                JObject jObject = JObject.Parse(dataParsing);
                JArray customersArray = (JArray)jObject["Data"];
                if (customersArray != null && customersArray.Count > 0)
                {
                    foreach (var value in customersArray)
                    {
                        customers.Add(new CustomerInfo()
                        {
                            Address = (string)value["DiaChi"],
                            BodyClockNumber = double.Parse((string)value["SoThanDong"]),
                            ClockBrand = int.Parse((string)value["HieuDongHo"]),
                            CustomerID = (string)value["DBDongHoNu"],
                            CustomerName = (string)value["TenThueBao"],
                            ExecuteUnit = (string)value["DonViThiCo"],
                            Latitude = double.Parse((string)value["ToaDoY"]),
                            Longitude = double.Parse((string)value["ToaDoX"]),
                            NewIndexing = int.Parse((string)value["CSMoi"]),
                            Nominally = (string)value["Nominally"],
                            OldIndexing = int.Parse((string)value["CSCu"]),
                            PhoneNumber = (string)value["SoDienThoa"],
                            RoadMapAreaID = double.Parse((string)value["MaKVLoTrin"]),
                            RoadMapID = double.Parse((string)value["MaLoTrinh"]),
                            ServiceName = (string)value["TenDV"],
                            SetupDate = DateTime.Parse((string)value["NgayLapDat"]),
                            Yield1 = double.Parse((string)value["SL1"]),
                            Yield2 = double.Parse((string)value["SL2"]),
                            Yield3 = double.Parse((string)value["SL3"]),
                            Yield4 = double.Parse((string)value["SL4"]),
                            Yield5 = double.Parse((string)value["SL5"]),
                            Yield6 = double.Parse((string)value["SL6"]),
                            Yield7 = double.Parse((string)value["SL7"]),
                            Yield8 = double.Parse((string)value["SL8"]),
                            Yield9 = double.Parse((string)value["SL9"]),
                            Yield10 = double.Parse((string)value["SL10"]),
                            Yield11 = double.Parse((string)value["SL11"]),
                            Yield12 = double.Parse((string)value["SL12"]),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                var c = ex;
            }
            return customers;
        }
    }
}
