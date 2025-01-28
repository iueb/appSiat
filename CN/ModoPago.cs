using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN
{
    public class ModoPago
    {
       public string metodo_pago_id { get; set; }
       public decimal monto { get; set; }
       public string diario_ref { get; set; }
       public string glosa { get; set; }



        public List<ModoPago> getWModosPagos(string modo_pago,decimal monto_mpago,string diario_ref,string glosa_data)
        {
            try
            {
                List<ModoPago> listModoPagos = new List<ModoPago>();
                listModoPagos.Add(new ModoPago() {
                    metodo_pago_id = modo_pago,
                    monto = monto_mpago,
                    diario_ref = diario_ref,
                    glosa = glosa_data
                });

                return listModoPagos;
            }
            catch (Exception)
            {
                throw;
            }
        }

      
    }
}
