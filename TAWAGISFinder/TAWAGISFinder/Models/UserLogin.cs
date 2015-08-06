using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TAWAGISFinder.Models
{
    public class UserLogin
    {
        public string id { set; get; }
        public string username { set; get; }
        public string fullname { set; get; }
        public string email { set; get; }
        public string token { set; get; }
        public string department_id { set; get; }
        public string is_admin { set; get; }
        
        
    }

}
