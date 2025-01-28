using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CN
{
    public class Asiento
    {
        public string diario { get; set; }
        public string glosa { get; set; }
        public string cuenta_haber { get; set; } = "11208001";
        public string cuenta_debe { get; set; } = "21135001";
        public decimal monto { get; set; }
      
    }
}
