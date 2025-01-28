using CN;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.Linq.Expressions;
using CD;
using System.Data;
using Microsoft.Win32;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using Microsoft.SqlServer.Server;
using System.EnterpriseServices;

namespace CP
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

             tboxFecha.Text = DateTime.Today.ToString("mm/dd/yyyy");
            //tboxFecha.Text = DateTime.Today.ToString("dd-MM-yyyy");
            //if (tboxFecha.Text != "") {
            //    HyperLinkImprimir.Visible = true;
            //}
        }
        protected async void btEnviar_ClickOdoo(object sender, EventArgs e)
        {
            //DateTime today = DateTime.Today;

            tboxFecha.Text = DateTime.Today.ToString("dd'/'MM'/'yyyy", CultureInfo.InvariantCulture);
          
            Label1.Visible = false;
            HyperLinkImprimir.Visible = true;
            await this.loadFacturaSiat();
        }
        protected async Task<String> sendingToOdooAsync(wdatosfactSiat postObject,DateTime date)
        {

            // HyperLinkImprimir.Visible = true;
            HyperLinkImprimir.NavigateUrl = "";
            var client = new HttpClient();
            var stringObject = JsonConvert.SerializeObject(postObject);
            string url = "http://192.168.7.9:80/btc_odoo_ueb_api/invoice_prd.php";
            var jsonContent = new StringContent(stringObject, Encoding.UTF8);


            // response.EnsureSuccessStatusCode().WriteRequestToConsole();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = jsonContent;

            
                HttpResponseMessage response = await client.PostAsync(url, jsonContent).ConfigureAwait(false);

            if ((int)response.StatusCode == 500)
            {
                errorMensaje.Visible = true;
                errorMensaje.Attributes["class"] = "errorMessage";
                HyperLinkImprimir.Visible = false;
                LabelError.Text = "Ha ocurrido un error en el servidor"+ response.Content;
            }


            var responseContent = await response.Content.ReadAsStringAsync();
            //Object objectOdoo = JsonConvert.DeserializeObject<Object>(responseContent);
            Log log_res = new Log();
            log_res.input_data = stringObject;
            log_res.output_data = responseContent;
            JObject json = JObject.Parse(responseContent);
            var responseOdoo = json.ToString();
           


            //var client = new HttpClient();
            //var content = new StringContent(stringObject, Encoding.UTF8, "application/json");
            //var responseMessage = await client.PostAsync(url, content);
            // var response = responseMessage.ToString();
            if ((response.Content != null) && ((int)response.StatusCode !=500))
            {

                try
                {
                    JObject jsonres = JObject.Parse(responseOdoo);
                    //DateTime fecha_fact = DateTime.ParseExact(date.ToString("dd/MM/yyyy"), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    
                        DateTime fecha_fact = DateTime.ParseExact(date.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                 
                  
                    
                    //string fecha_factura = fecha_fact.Month + "/" + fecha_fact.Day + "/" + fecha_fact.Year;
                    //DateTime fecha_fact2 = new DateTime(fecha_fact.Year, fecha_fact.Month, fecha_fact.Day);
                    if (Convert.ToInt32(jsonres["estado"]) == 0)
                    {
                        int reg_est = postObject.customer.id_externo;
                        int fact_nro = Convert.ToInt32(postObject.nrofact);

                        var fact_siat = Convert.ToInt32(jsonres["respuesta"]["nro_factura"]);
                        var estado = (jsonres["mensaje"].ToString().Contains("Error al registrar el proceso de pago")) ? jsonres["mensaje"].ToString() : "a";

                        Conexion objConexion = new Conexion();
                        objConexion.openConnection();
                        if (!searchTransaction(postObject.nrofact, fecha_fact,reg_est))
                        {
                            DataTable dTab = objConexion.registrarFacturaOdoo(reg_est, fact_nro, fecha_fact, fact_siat, fecha_fact, estado);
                        }
                        else
                        {
                            DataTable dTab = objConexion.updateFacturaOdoo(reg_est, fact_nro, fecha_fact, fact_siat, fecha_fact, estado);
                        }


                        objConexion.closeConnection();
                    }
                    else {

                        int reg_est = postObject.customer.id_externo;
                        int fact_nro = Convert.ToInt32(postObject.nrofact);

                        var fact_siat = 0;
                        var estado = jsonres["mensaje"].ToString();

                        Conexion objConexion = new Conexion();
                        objConexion.openConnection();
                        if (!searchTransaction(postObject.nrofact, fecha_fact,reg_est))
                        {
                            DataTable dTab = objConexion.registrarFacturaOdoo(reg_est, fact_nro, fecha_fact, fact_siat, fecha_fact, estado);
                        }
                        else
                        {
                            DataTable dTab = objConexion.updateFacturaOdoo(reg_est, fact_nro, fecha_fact, fact_siat, fecha_fact, estado);
                        }


                        objConexion.closeConnection();
                    }

                   
                


                }
                catch (Exception)
                {
                    //objConexion.closeConnection();
                    throw;
                }

                string path = "C:\\Users\\facturaSiat\\facturasOdoo\\";
               // string path = "C:\\Users\\Cpd179\\facturasOdoo";
                string path_factura = postObject.customer.id_externo + "_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".txt";
                //// string text = postResult.ToString();
                string pathtext = path + path_factura;


                var firstv = JsonConvert.DeserializeObject(log_res.input_data.ToString());
                var sectv = JsonConvert.DeserializeObject(log_res.output_data.ToString());
                var newobject = JsonConvert.SerializeObject(firstv);
                var newobject2 = JsonConvert.SerializeObject(sectv);
              //  File.WriteAllText(pathtext, newobject + newobject2);
                //File.WriteAllText(pathtext, firstv.ToString() + sectv.ToString());
                //// Procedure registra factura Odoo


            }



            return responseOdoo;

            

           


        }
        private async Task loadFacturaSiat()
        {
            wdatosfactSiat objDatosFactSiat = new wdatosfactSiat();

            // DateTime fecha = DateTime.ParseExact(Convert.ToDateTime(tboxFecha.Text).ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            DateTime fecha = DateTime.ParseExact(Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd 00:00:00")).ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);

            //string fecha = 09 + "/" + 30 + "/" + 2022;
            //DateTime fecha = Convert.ToDateTime("2024-02-19");
            if (!searchTransactionEstado(tboxFactura.Text, fecha))
            {
                //objDatosFactSiat = objDatosFactSiat.GetFacturaSiat(tboxFactura.Text, fecha, Convert.ToInt32(tboxTCaja.Text), Convert.ToInt32(tboxCode.Text));
                objDatosFactSiat = objDatosFactSiat.GetFacturaSiat(tboxFactura.Text, fecha, Convert.ToInt32(tboxTCaja.Text), Convert.ToInt32(tboxCode.Text));

                await showLinkFactura(objDatosFactSiat, fecha, false);
            }
            //else if (searchTransactionEstadoFecha(tboxFactura.Text, fecha)) {
            //    objDatosFactSiat = objDatosFactSiat.GetFacturaSiat(tboxFactura.Text, fecha, Convert.ToInt32(tboxTCaja.Text), Convert.ToInt32(tboxCode.Text));

            //    await showLinkFactura(objDatosFactSiat, fecha, false);

            //}
            else{
                if (!searchTransactionEstadoFecha(tboxFactura.Text, fecha))
                {
                    objDatosFactSiat = objDatosFactSiat.GetFacturaSiat(tboxFactura.Text, fecha, Convert.ToInt32(tboxTCaja.Text), Convert.ToInt32(tboxCode.Text));
                    await showLinkFactura(objDatosFactSiat, fecha, false);
                }
                else{
                    errorMensaje.Visible = true;
                    errorMensaje.Attributes["class"] = "errorMessage";
                    HyperLinkImprimir.Visible = false;
                    LabelError.Text = "Ya se ha generado la factura para la transaccion y registro ingresado anteriomente!";
                }

            }

            //else 
            //{
            //    errorMensaje.Visible = true;
            //    errorMensaje.Attributes["class"] = "errorMessage";
            //    HyperLinkImprimir.Visible = false;
            //    LabelError.Text = "Ya se ha generado la factura para la transaccion y registro ingresado anteriomente!";
            //}
            

        }

        private async Task showLinkFactura(wdatosfactSiat objDatosFactSiat, DateTime fecha,bool is_update)
        {
            if (objDatosFactSiat.nrofact != null)
            {
                if (!is_update)
                {
                    //if (!searchTransactionEstado(objDatosFactSiat.nrofact, fecha))
                    //{

                        string res = await this.sendingToOdooAsync(objDatosFactSiat, fecha);
                        JObject jsonres = JObject.Parse(res);
                        var resp = jsonres["respuesta"].ToString();

                        if (resp != "")
                        {

                            errorMensaje.Visible = false;
                            Label1.Visible = true;
                            LabelExito.Visible = false;
                            HyperLinkImprimir.Visible = true;

                            //Campos no limpiado
                            //  tboxFactura.Text = "";

                            // tboxTCaja.Text = "";
                            // tboxFecha.Text = "" + DateTime.Today.Day + DateTime.Today.Month + DateTime.Today.Year;
                            JObject json_res = JObject.Parse(res);
                            var link = json_res["respuesta"]["link_invoice"];
                            char[] charsToTrim = { '"', '.' };
                            HyperLinkImprimir.NavigateUrl = link.ToString().Trim(charsToTrim);


                            //LabelError.Visible = false;


                        }
                        else
                        {
                            errorMensaje.Visible = true;
                            errorMensaje.Attributes["class"] = "errorMessage";
                            HyperLinkImprimir.Visible = false;
                            LabelError.Text = jsonres["mensaje"].ToString();
                        }

                    //}
                    //else
                    //{
                    //    errorMensaje.Visible = true;
                    //    errorMensaje.Attributes["class"] = "errorMessage";
                    //    HyperLinkImprimir.Visible = false;
                    //    LabelError.Text = "Ya existe una factura para la transaccion y registro ingresado.Intente nuevamente!";
                    //}

                }
                else
                {
                    string res = await this.sendingToOdooAsync(objDatosFactSiat, fecha);
                    JObject jsonres = JObject.Parse(res);
                    var resp = jsonres["respuesta"].ToString();
                    errorMensaje.Visible = true;
                    LabelExito.Visible = true;
                    // errorMensaje.Attributes["class"] = "succesMessage";
                    HyperLinkImprimir.Visible = false;
                   // LabelError.Text = "La actualizacion del pago se realizo exitosamente";

                }

                


            }
            else
            {
                errorMensaje.Visible = true;
                errorMensaje.Attributes["class"] = "errorMessage";
                HyperLinkImprimir.Visible = false;
                LabelError.Text = "No existe ninguna transaccion para la combinacion de campos.Intente nuevamente!";
                //  this.Page_Load();
            }
        }

        private bool searchTransaction(string nrofact, DateTime fecha,int reg)
        {
            wdatosfactSiat objDatosFactSiat = new wdatosfactSiat();
            bool is_repeat = objDatosFactSiat.existeFacturaOdoo(Convert.ToInt32(nrofact),fecha, Convert.ToInt32(tboxCode.Text));
            return is_repeat;
        }
        private bool searchTransactionEstadoFecha(string nrofact, DateTime fecha)
        {
            wdatosfactSiat objDatosFactSiat = new wdatosfactSiat();
            bool is_repeat = objDatosFactSiat.existeFacturaOdooFecha(Convert.ToInt32(nrofact), fecha,Convert.ToInt32(tboxCode.Text));
            return is_repeat;
        }
        private bool searchTransactionEstado(string nrofact, DateTime fecha)
        {
            wdatosfactSiat objDatosFactSiat = new wdatosfactSiat();
            bool is_repeat = objDatosFactSiat.existeFacturaOdoo(Convert.ToInt32(nrofact), fecha, Convert.ToInt32(tboxCode.Text));
            return is_repeat;
        }
        protected async void updateFacturacion(object sender, EventArgs e)
        {
            Label1.Visible = false;
            HyperLinkImprimir.Visible = false;
            errorMensaje.Visible = false;
            LabelError.Text = "";
            DateTime fechaactual =new DateTime(2022,12,12);
            string fechaformat = fechaactual.Month + "-" + fechaactual.Day +"-"+ fechaactual.Year;
            try
            {
                Conexion objConexion = new Conexion();
                ////getting factura no pagada
                ////string query = "SELECT  *  " +
                ////             "FROM factura_siat where fecha_fact="+fechaformat;
                //string fechaquery= "12-13-2022";
                ////string query = "SELECT  *  FROM factura_siat where fecha_fact=' "+ fech+"' ";
                string query = "SELECT  * FROM academi.factura_siat WHERE fecha_fact = ' "+fechaformat+" ' ";
                objConexion.openConnection();
                DataTable dTab = objConexion.ejecutar(query);
                objConexion.closeConnection();
            }
            catch (Exception)
            {
                //objConexion.closeConnection();
                throw;
            }

        }

        protected void tboxTCaja_TextChanged(object sender, EventArgs e)
        {

        }
    }
}