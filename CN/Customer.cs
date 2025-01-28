using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN
{
    public class Customer
    {
        public string name { get; set; }
        public string email { get; set; }
        public int phone { get; set; } = 0;
        public int mobile { get; set; } = 0;
        public int gender { get; set; }
        public string street { get; set; }
        public string country_id { get; set; }
        public string state_id { get; set; }
        public string city { get; set; }
        public string business_name { get; set; }
        public string vat { get; set; }
        
        public string complemento { get; set; }
        public string siat_tdi_id { get; set; } = "1";//tipo documento por defecto-1.-cedula de identidad
        public string regimen_id { get; set; }
        public int id_externo { get; set; }
    }
}
