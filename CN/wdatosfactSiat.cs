using CD;
using integracionUEB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CN
{
    public class wdatosfactSiat
    {

        #region Atributos
        public string nrofact { get; set; }
        public string url { get; set; }
        public string db { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public Customer customer { get; set; }
        // public string? almacen { get; set; }="Santa Cruz"
        public string hash { get; set; }
        public int sucursal { get; set; } = 0;
        public string almacen { get; set; }
        public string razon_social { get; set; }
        public string nit { get; set; }

        public int pdc { get; set; }
 
        public decimal monto_total { get; set; }
        public string periodo { get; set; }
        public List<wdetallefactSiat> detalle { get; set; }
        public List<ModoPago> tipo_pago { get; set; }
        public string modo_pago { get; set; }

        //public string glosa_pago { get; set; }
        public string complemento { get; set; }

        public string cuenta_analitica { get; set; }

        public string cuenta_analitica_desc { get; set; }

        public string usuario_ref { get; set; }

        public string nro_cheque { get; set; }

        public List<Asiento> asiento_cierre { get; set; }

        public wdatosfactSiat GetFacturaSiat(string nrofact, DateTime fechafact, int nroCaja,int codeEstudiante)
        {
            wdatosfactSiat objWdatosfact = new wdatosfactSiat();
            try
            {
               // DateTime fecha = DateTime.ParseExact("2024-05-23", "yyyy-MM-dd", CultureInfo.InvariantCulture);
                //  string fechaFactura = fechafact.Day + "/" + fechafact.Month + "/" + fechafact.Year;
                //string fechaFactura = fechafact.Month + "/" + fechafact.Day + "/" + fechafact.Year;
                //string fechaFactura = fechafact.Month + "/" + 24 + "/" + fechafact.Year;
                //string fechaFactura = "2024-05-14";
                Conexion objConexion = new Conexion();
              

                objConexion.openConnection();
                //DataTable dTab = objConexion.ejecutarFactura(Convert.ToInt32(nrofact), nroCaja,fecha,codeEstudiante);
                DataTable dTab = objConexion.ejecutarFactura(Convert.ToInt32(nrofact), nroCaja, DateTime.ParseExact(fechafact.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture), codeEstudiante);
                objConexion.closeConnection();


                //var nrofacts = dTab.Rows[0]["dfnrofact"];
                //var i = nrofacts.GetType();

                if (dTab.Rows.Count > 0)
                {
                    var email = "";
                    var genero = 0;
                    var celular = 0;
                    var telefono = 0;
                    var street = "";
                    var idcity = "";
                    var city = "";
                    var razon_social = "";
                    var vat = "";
                    var idexterno = 0;
                    var almacen = "";
                    var bussines_name = "";
                    var nit = "";
                    var caja = 0;
                    var monto_total = Convert.ToDecimal(0);
                    var nro_control = "";
                    var glosa = "";
                    var modo_pago = "";
                    var cuenta_analitica = "";
                    var cuenta_analitica_desc = "";
                    var nro_cajero = "";
                    DateTime fecha_deposito;

                    // Objeto Regex para encontrar ci de extranjeros
                    string patron_ext = @"\bE-\b|\b-E\b|\bE\b";

                    Regex regex = new Regex(patron_ext);

                    foreach (DataRow r in dTab.Rows)
                    {

                        //detalle factura
                        idexterno = Convert.ToInt32(r["dcreg"]);
                        nro_control = r["dfcodcontrol"].ToString().Trim();
                        cuenta_analitica = r["dccuenta"].ToString().Trim();
                        cuenta_analitica_desc = r["dccuenta"].ToString().Trim();
                        nro_cajero = r["dfnrocajero"].ToString();
                        try
                        {
                            email = r["dcemail"].ToString().Trim();
                        }
                        catch (Exception e)
                        {
                            email = "";
                        }

                        try
                        {
                            genero = Convert.ToInt32(r["dcgenero"]);
                        }
                        catch (Exception e)
                        {
                            genero = 0;
                        }

                        try
                        {
                            street = r["dcdireccion"].ToString().Trim();
                        }
                        catch (Exception e)
                        {
                            street = "";
                        }
                        try
                        {
                            idcity = r["dcciudad"].ToString().Trim();
                        }
                        catch (Exception e)
                        {
                            idcity = "";
                        }
                        try
                        {
                            bussines_name = r["dcrazon_social"].ToString().Trim();
                        }
                        catch (Exception e)
                        {
                            bussines_name = "";
                        }
                        try
                        {
                            nit = r["dfnit"].ToString().Trim();
                        }
                        catch (Exception e)
                        {
                            nit = "";
                        }
                        try
                        {
                            glosa = r["dfglosa"].ToString().Trim();
                        }
                        catch (Exception e)
                        {
                            glosa = "";
                        }
                        try
                        {
                            caja = Convert.ToInt32(r["dfnrocaja"]);
                        }
                        catch (Exception e)
                        {
                            caja = 1;
                        }
                        try
                        {
                            nit = r["dfnit"].ToString().Trim();
                        }
                        catch (Exception e)
                        {
                            nit = "";
                        }
                        try
                        {
                            razon_social = r["dfnombfact"].ToString().Trim();
                        }
                        catch (Exception e)
                        {
                            razon_social = "";
                        }
                        try
                        {
                            almacen = r["dfalmacen"].ToString().Trim();
                        }
                        catch (Exception e)
                        {
                            almacen = "";
                        }
                        try
                        {
                            vat = r["dcci"].ToString().Trim();
                        }
                        catch (Exception e)
                        {
                            vat = "" ;
                        }
                        try {
                            celular = Convert.ToInt32(r["dccelular"]);
                        }
                        catch (Exception e) {
                            celular = 0;
                        }
                        try
                        {
                            telefono = Convert.ToInt32(r["dctelefono"]);
                        }
                        catch (Exception e)
                        {
                            telefono = 0;
                        }
                        try
                        {
                            monto_total = Convert.ToDecimal(r["dfmonto"]);
                        }
                        catch (Exception e) {
                            Console.WriteLine("No se ha introducido un monto valido o mayor a 0 ");
                        }

                        if (r["dpmetpago"].ToString() == "Z")
                        {
                            modo_pago = r["dpbanco"].ToString();
                            nro_cheque = nro_cheque = r["dpnrobol"].ToString().Trim();
                        }
                        // Pagos QR
                        else if (r["dpmetpago"].ToString() == "L")
                        {
                            modo_pago = r["dpbanco"].ToString();
                            try
                            {
                                DateTime dateValue = (DateTime)r["dffechafact"];
                                DataTable dTab_ = objConexion.searchBoleta(idexterno, dateValue.ToString("dd-MM-yyyy"));
                                if (dTab_.Rows.Count > 0)
                                {
                                    //nro_cheque = r["dpnrobol"].ToString().Trim();
                                    foreach (DataRow row_ in dTab_.Rows)
                                    {
                                        nro_cheque = row_["transactionid"].ToString().Trim();
                                    }

                                }

                            }
                            catch (Exception)
                            {
                                nro_cheque = "";
                            }

                        }
                        else {
                            modo_pago = r["dpmetpago"].ToString();
                            try
                            {
                                DataTable dTab_ = objConexion.searchBoleta(idexterno, r["dffechafact"].ToString().Trim());
                                if (dTab_.Rows.Count > 0)
                                {
                                    //nro_cheque = r["dpnrobol"].ToString().Trim();
                                    foreach (DataRow row_ in dTab_.Rows)
                                    {
                                        nro_cheque = row_["transactionid"].ToString().Trim();
                                    }

                                }

                            }
                            catch (Exception)
                            {
                                nro_cheque = "";
                            }
                        }

                        int cuenta = 0;
                        string cuentaref = "NA";
                        objWdatosfact.nrofact = r["dfnrofact"].ToString();
                        objWdatosfact.url = ConfigurationManager.AppSettings["urlOdoo"].ToString();
                        objWdatosfact.db = ConfigurationManager.AppSettings["dbOdoo"].ToString();
                        //objWdatosfact.username = ConfigurationManager.AppSettings["adminOdoo"].ToString();
                        //objWdatosfact.password = ConfigurationManager.AppSettings["claveadminOdoo"].ToString();
                        DataTable dtUser = objConexion.getUserValidator(nro_cajero);
                        try
                        {
                            objWdatosfact.username = dtUser.Rows[0]["usuario_odoo"].ToString();
                            objWdatosfact.password = dtUser.Rows[0]["usuario_clave"].ToString();
                        }
                        catch (Exception e)
                        {
                            objWdatosfact.username = ConfigurationManager.AppSettings["adminOdoo"].ToString();
                            objWdatosfact.password = ConfigurationManager.AppSettings["claveadminOdoo"].ToString();
                        }

                        //datos cliente
                        objWdatosfact.customer = new Customer();
                        objWdatosfact.customer.name = r["dcnombre"].ToString().Trim();
                        objWdatosfact.customer.email = email;

                        objWdatosfact.customer.phone = telefono;
                        objWdatosfact.customer.mobile = celular;
                        objWdatosfact.customer.gender = genero;
                        objWdatosfact.customer.street = street;
                        objWdatosfact.customer.country_id = "BO";
                        objWdatosfact.customer.state_id = idcity;
                        objWdatosfact.customer.city = city;
                        objWdatosfact.customer.business_name = bussines_name;
                        objWdatosfact.customer.vat = vat.ToString().Trim();
                        objWdatosfact.customer.vat = Regex.Replace(objWdatosfact.customer.vat, @"\s", "");
                        objWdatosfact.customer.regimen_id = "Persona natural";
                        objWdatosfact.customer.id_externo = idexterno;

                        //objWdatosfact.customer.siat_tdi_id = vat.Contains("E-") | vat.Contains("-E") | vat.Contains("E") ? "2" : "1";
                        if (nit != "" & vat != "") {
                            if (Regex.IsMatch(nit, patron_ext) | Regex.IsMatch(vat, patron_ext))
                            {
                                objWdatosfact.customer.siat_tdi_id = "2";
                            }
                            else {
                                objWdatosfact.customer.siat_tdi_id =vat.Contains("-") || nit.Contains("-") ? "4" : "1";
                                //objWdatosfact.customer.siat_tdi_id = nit.Contains("-") ? "4" : "1";
                            }

                        }
                        else if (nit != "" & vat == "")
                        {
                            if (Regex.IsMatch(nit, patron_ext))
                            {
                                objWdatosfact.customer.siat_tdi_id = "2";
                            }
                            else
                            {
                                 objWdatosfact.customer.siat_tdi_id = nit.Contains("-") ? "4" : "1";
                                //jWdatosfact.customer.siat_tdi_id = "4";
                            }


                        }
                        else if(vat !="" & nit != "")
                        {
                            if (Regex.IsMatch(vat, patron_ext))
                            {
                                objWdatosfact.customer.siat_tdi_id = "2";
                            }
                            else
                            {
                                 objWdatosfact.customer.siat_tdi_id = vat.Contains("-") ? "4" : "1";
                              //objWdatosfact.customer.siat_tdi_id = "4";
                            }
                        }


                        objWdatosfact.sucursal = 0;
                        objWdatosfact.almacen = almacen;

                        objWdatosfact.razon_social = razon_social;
                        objWdatosfact.nit = nit;
                        objWdatosfact.nit = Regex.Replace(objWdatosfact.nit, @"\s", "");
                        //objWdatosfact.nit = Regex.Replace(objWdatosfact.nit, @"\.", "");
                        objWdatosfact.pdc = caja;
                        objWdatosfact.hash = Hashing.CreateMD5(objWdatosfact.nrofact + fechafact + idexterno);
                        //objWdatosfact.hash = "BA4DD8A3890F5F854EEAF167A7A78CFA";
                        objWdatosfact.nro_cheque = nro_cheque;

                        //objWdatosfact.monto_total = Convert.ToDecimal(r["dftotal_fac"]);
                        objWdatosfact.monto_total = monto_total;
                        objWdatosfact.periodo = getFullNameMonth(fechafact.Year, fechafact.Month, fechafact.Day).ToUpper().Substring(0, 3);
                        objWdatosfact.complemento = "null";
                        //factura detalle
                        wdetallefactSiat objWdetalleSiat = new wdetallefactSiat();
                        objWdatosfact.detalle = new List<wdetallefactSiat>();
                        objWdatosfact.detalle = objWdetalleSiat.getWdetallefactSiat(objWdatosfact.nrofact, nro_control, glosa);
                        //objWdatosfact.modo_pago = "1";
                        objWdatosfact.modo_pago = modo_pago;

                        //Modo Pago
                        ModoPago modoPagos = new ModoPago();

                        objWdatosfact.tipo_pago = modoPagos.getWModosPagos(objWdatosfact.modo_pago, objWdatosfact.monto_total, modo_pago, glosa);
                        objWdatosfact.cuenta_analitica = cuenta_analitica;
                        objWdatosfact.cuenta_analitica_desc = cuenta_analitica_desc;
                        objWdatosfact.usuario_ref = nro_cajero;
                        List<Asiento> asientos = new List<Asiento>();
                        Asiento asiento = new Asiento();
                        asiento.diario = "Apertura de Carrera";
                        asiento.cuenta_haber = ConfigurationManager.AppSettings["cuentaxp"].ToString();
                        asiento.cuenta_debe = ConfigurationManager.AppSettings["cuentaxc"].ToString();
                        asiento.monto = Convert.ToDecimal(r["dfmonto"]);
                        asiento.glosa = "Apertura de Carrera";
                        asientos.Add(asiento);
                        objWdatosfact.asiento_cierre = asientos;


                        //}
                        //   catch (Exception)
                        //   {
                        //       email = r["dcemail"].ToString().Trim();
                        //       genero = 0;
                        //       celular = 0;
                        //       telefono = 0;
                        //       street = r["dcdireccion"].ToString().Trim();
                        //       idcity = r["dcciudad"].ToString().Trim();
                        //       city = r["dcciudad"].ToString().Trim();
                        //       bussines_name = r["dcrazon_social"].ToString().Trim();
                        //       vat = r["dcci"].ToString();
                        //       idexterno = Convert.ToInt32(r["dcreg"]);
                        //       almacen = r["dfalmacen"].ToString();
                        //       razon_social = r["dfnombfact"].ToString().Trim();
                        //       nit = r["dfnit"].ToString().Trim();
                        //       caja = Convert.ToInt32(r["dfnrocaja"]);
                        //       monto_total = Convert.ToDecimal(r["dfmonto"]);
                        //       nro_control = r["dfcodcontrol"].ToString();
                        //       glosa = r["dfglosa"].ToString();
                        //       //  modo_pago = r["dpmetpago"].ToString();
                        //       cuenta_analitica = r["dccuenta"].ToString().Trim();
                        //       //cuenta_analitica = r["dccuenta"].ToString();
                        //       cuenta_analitica_desc = r["dccuenta"].ToString().Trim();
                        //       nro_cajero = r["dfnrocajero"].ToString();
                        //       if (r["dpmetpago"].ToString()=="Z") {
                        //           modo_pago = r["dpbanco"].ToString();
                        //           nro_cheque = r["dpnrobol"].ToString().Trim();
                        //       }
                        //       else if (r["dpmetpago"].ToString() == "L")
                        //       {
                        //           modo_pago = r["dpbanco"].ToString();
                        //           try
                        //           {
                        //               DateTime dateValue = (DateTime)r["dffechafact"];
                        //               DataTable dTab_ = objConexion.searchBoleta(idexterno, dateValue.ToString("dd-MM-yyyy"));
                        //               if (dTab_.Rows.Count > 0)
                        //               {
                        //                   //nro_cheque = r["dpnrobol"].ToString().Trim();
                        //                   foreach (DataRow row_ in dTab_.Rows)
                        //                   {
                        //                       nro_cheque = row_["transactionid"].ToString().Trim();
                        //                   }

                        //               }
                        //               else
                        //               {
                        //                   nro_cheque = nro_cheque = r["dpnrobol"].ToString().Trim();
                        //               }

                        //           }
                        //           catch (Exception)
                        //           {
                        //               nro_cheque = "";
                        //           }


                        //       }

                        //       else
                        //       {
                        //           modo_pago = r["dpmetpago"].ToString();
                        //           try
                        //           {
                        //               DataTable dTab_ = objConexion.searchBoleta(idexterno, r["dffechafact"].ToString().Trim());
                        //               if (dTab.Rows.Count > 0)
                        //               {
                        //                   //nro_cheque = r["dpnrobol"].ToString().Trim();
                        //                   foreach (DataRow row in dTab.Rows)
                        //                   {
                        //                       nro_cheque = row["transactionid"].ToString().Trim();
                        //                   }

                        //               }

                        //           }
                        //           catch (Exception)
                        //           {
                        //               nro_cheque = "";
                        //           }

                        //       }


                        //       // throw;
                        //   }







                        //objWdatosfact.customer.siat_tdi_id = "2";
                        //fin de datos cliente 




                    }
                }
                
               

               

                //sending to API


                //objConexion.closeConnection();

               
            }
            catch (Exception)
            {
                //objConexion.closeConnection();
                throw new Exception("Campos nulos o vacios en la base de datos");
                
                //throw new Exception('Ocurrio un error'); ;
            }
            return objWdatosfact;
        }

        public string getFullNameMonth(int year,int month,int day){

            DateTime date = new DateTime(year, month, day);
            return date.ToString("MMM");
        }
       
 

        public bool existeFacturaOdoo(int nrofact, DateTime fecha)
        {
            bool res = false;
            //DateTime fechaf = DateTime.ParseExact(fecha.ToString("yyyy-MM-dd hh:mm:ss"), "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture);
           // string dated = fecha.Year + "-" + fecha.Month + "-" + fecha.Day;
            string dated = fecha.Month + "-" + fecha.Day + "-" + fecha.Year;
            try
            {
                Conexion objConexion = new Conexion();
                DateTime fecha_fact = new DateTime(fecha.Year, fecha.Month, fecha.Day);
                //string query = "SELECT  *  " +
                //             "FROM factura_siat " +
                //             "WHERE num_fact = " + nrofact + " and fecha_fact ="+dated;
                
                string query = "SELECT  *  " +
                           "FROM factura_siat " +
                           "WHERE num_fact = " + nrofact;

                objConexion.openConnection();
                DataTable dTab = objConexion.ejecutar(query);
                objConexion.closeConnection();
                if(dTab.Rows.Count>0) {
                    res = true;
                }
              //  objConexion.closeConnection();
            }
            catch (Exception)
            {
                //objConexion.closeConnection();
                throw;
            }
            return res;
        }
        public bool existeFacturaOdooFecha(int nrofact, DateTime fecha,int txtCode)
        {
            bool res = false;
           // DateTime fechaf = DateTime.ParseExact(fecha.ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture);
            //string dated = fecha.Year + "-" + fecha.Month + "-" + fecha.Day;
            //string dated = fecha.Year + "-" + fecha.Month + "-" + fecha.Day;
          //ateTime fechaf = DateTime.ParseExact(fecha.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string fechFactura = fecha.Day + "-" + fecha.Month+ "-" + fecha.Year;
            try
            {
                Conexion objConexion = new Conexion();
                //  string fecha_fact = DateTime.Now.ToString("yyyy-MM-dd");
               //string fdate= date("m/d/Y", strtotime(fecha);
                //string query = "SELECT  *  " +
                //             "FROM factura_siat " +
                //             "WHERE estado='a' and num_fact = " + nrofact + " and fecha_fact = { d '2023-08-01' }";
                string query = "select first 1 fecha_fact from factura_siat where estado = 'a' and num_fact = " + nrofact + " and registro='"+txtCode+ "' order by fecha_fact desc";
                objConexion.openConnection();
                DataTable dTab = objConexion.ejecutar(query);
                objConexion.closeConnection();
                DateTime rres = (DateTime)dTab.Rows[0]["fecha_fact"];
                if (dTab.Rows.Count > 0 && (DateTime.Compare(rres,fecha)==0))
                {
                   
                    res = true;
                }
                //  objConexion.closeConnection();
            }
            catch (Exception)
            {
                //objConexion.closeConnection();
                throw;
            }
            return res;
        }

        public bool existeFacturaOdoo(int nrofact, DateTime fecha, int txtCode)
        {
            bool res = false;
            // DateTime fechaf = DateTime.ParseExact(fecha.ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture);
            //string dated = fecha.Year + "-" + fecha.Month + "-" + fecha.Day;
            string dated = fecha.Month + "-" + fecha.Day + "-" + fecha.Year;
            try
            {
                Conexion objConexion = new Conexion();
                string fecha_fact = DateTime.Now.ToString();
                //string query = "SELECT  *  " +
                //             "FROM factura_siat " +
                //             "WHERE estado='a' and num_fact = " + nrofact + " and fecha_fact ="+ dated;
                string query = "SELECT  *  " +
                            "FROM factura_siat " +
                            "WHERE estado='a' and num_fact = " + nrofact + " and registro =" + txtCode;
                objConexion.openConnection();
                DataTable dTab = objConexion.ejecutar(query);
                objConexion.closeConnection();
                if (dTab.Rows.Count > 0)
                {
                    res = true;
                }
                //  objConexion.closeConnection();
            }
            catch (Exception)
            {
                //objConexion.closeConnection();
                throw;
            }
            return res;
        }
    }
}
#endregion