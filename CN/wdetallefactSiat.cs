using CD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN
{
    public class wdetallefactSiat
    {
        #region Atributos
        public int item { get; set; }
        public string descripcion { get; set; }
        public int qty { get; set; }
        public decimal price_unit { get; set; }
        // public string? carrera { get; set; }
        public decimal sub_total { get; set; }
        #endregion


        //public wdetallefact()
        //{
        //}
        //public wdetallefact(string dnrofact, string dcodcontrol, string dnroitem, string descmotivo, string monto)
        //{
        //    this.dnrofact = dnrofact;
        //    this.dcodcontrol = dcodcontrol;
        //    this.dnroitem = dnroitem;
        //    this.descmotivo = descmotivo;
        //    this.monto = monto;
        //}

        public List<wdetallefactSiat> getWdetallefactSiat(string nrofact, string cod_Control,string glosa)
        {
            try
            {
                Conexion objConexion = new Conexion();
                string query = "SELECT * " +
                                "FROM wdetallefact2 " +
                                "WHERE dnrofact = " + nrofact +
                                " AND dcodcontrol = \'" + cod_Control + "\'";
                objConexion.openConnection();
                DataTable dTab = objConexion.ejecutar(query);
                objConexion.closeConnection();

                List<wdetallefactSiat> listWDetallefact = new List<wdetallefactSiat>();

                for(int i=0;i<1;i++) {
                    var item=0;
                    var description = "";
                    var qty = 0;
                    var price_unit = Convert.ToDecimal(0);
                    var sub_total = Convert.ToDecimal(0);

                    
                        item = Convert.ToInt32(dTab.Rows[i]["codmotivo"]);
                        description= dTab.Rows[i]["descmotivo"].ToString().Trim() + "\n" + glosa.Trim();
                        qty = Convert.ToInt32(dTab.Rows[i]["cantidad"]);
                        price_unit = Convert.ToDecimal(dTab.Rows[i]["monto"]);
                        sub_total = Convert.ToDecimal(dTab.Rows[i]["monto"]) * Convert.ToInt32(dTab.Rows[i]["cantidad"]);

                   
                    listWDetallefact.Add(new wdetallefactSiat()
                    {
                        
                        item = item,
                        descripcion = description,
                        qty = qty,
                        price_unit = price_unit ,
                        sub_total = sub_total


                    });
                 }
                for(int j=1;j<dTab.Rows.Count;j++) {
                    var itemj = 0;
                    var descriptionj = "";
                    var qtyj = 0;
                    var price_unitj = Convert.ToDecimal(0);
                    var sub_totalj = Convert.ToDecimal(0);

                   
                        itemj = Convert.ToInt32(dTab.Rows[j]["codmotivo"]);
                        descriptionj = dTab.Rows[j]["descmotivo"].ToString().Trim();
                        qtyj = Convert.ToInt32(dTab.Rows[j]["cantidad"]);
                        price_unitj = Convert.ToDecimal(dTab.Rows[j]["monto"]);
                        sub_totalj= Convert.ToDecimal(dTab.Rows[j]["monto"]) * Convert.ToInt32(dTab.Rows[j]["cantidad"]);
                    
                  
                    listWDetallefact.Add(new wdetallefactSiat()
                    {
                        item = itemj,
                        descripcion = descriptionj,
                        qty = qtyj,
                        price_unit = price_unitj,
                        sub_total = sub_totalj


                    });
                }

                return listWDetallefact;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void addItem() {
        
        }
        public string replace_coma(string dato)
        {
            string aux = dato.Replace(',', '.');
            return aux;
        }
        public string Convert_BolivianosDet(string dato)
        {
            decimal monto = Convert.ToDecimal(dato);
            monto = Math.Round(monto, 2);
            return Convert.ToString(monto);
        }
    }
}
