using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD
{
    public class Conexion
    {
        #region atributos
        private OdbcConnection conn;
        private OdbcCommand cmd;
        private string cadena;
        private string DSN;
        private string UID;
        private string PWD;
        #endregion

        public Conexion()
        {

            //this.DSN = "Informix Prueba";
            this.DSN = "Informix Produccion";
            this.UID = "bantic";
            this.PWD = "bantic";
            this.cadena = "DSN=" + DSN + ";UID=" + UID + ";PWD=" + PWD;

        }

        public void openConnection()
        {
            try
            {
                conn = new OdbcConnection(cadena);
                conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void closeConnection()
        {
            try
            {
                conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet ejecutarDS(string query)
        {
            this.cmd = new OdbcCommand(query, conn);
            OdbcDataAdapter odbcAdapter = new OdbcDataAdapter(cmd);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            DataSet dSet = new DataSet();
            odbcAdapter.Fill(dSet);
            conn.Close();
            return dSet;
        }
        public DataTable ejecutar(string query)
        {
            this.cmd = new OdbcCommand(query, conn);
            OdbcDataAdapter odbcAdapter = new OdbcDataAdapter(cmd);
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            DataTable dTab = new DataTable();
            odbcAdapter.Fill(dTab);
            conn.Close();
            return dTab;
        }

        //public DataTable searchFactOdoo(string query)
        //{
        //    this.cmd = new OdbcCommand(query, conn);
        //    OdbcDataAdapter odbcAdapter = new OdbcDataAdapter(cmd);
        //    if (conn.State == ConnectionState.Open)
        //    {
        //        conn.Close();
        //    }
        //    conn.Open();
        //    DataTable dTab = new DataTable();
        //    odbcAdapter.Fill(dTab);
        //    conn.Close();
        //    return dTab;
        //}
        public DataTable ejecutarFactura(int ptransaccion,int pcaja,DateTime pfecha,int registro){
            OdbcCommand cmd = new OdbcCommand("{call spObtenerDatosFactura(?,?,?,?)}",conn); //cambiar call factura2
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ptransaccion", ptransaccion);
            cmd.Parameters.AddWithValue("@pcaja", pcaja);
            cmd.Parameters.AddWithValue("@pfecha", DateTime.ParseExact(pfecha.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@pregistro",registro);
      
            DataTable dataTable = new DataTable();
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            } 
            conn.Open();
            OdbcDataAdapter adp = new OdbcDataAdapter(cmd);
            
            adp.Fill(dataTable);
            conn.Close();
            return dataTable;

        }
        public DataTable registrarFacturaOdoo(int reg_est,int fact_nro,DateTime fecha_fact,int fact_siat,DateTime fecha_siat,string estado)
        {
            DataTable dataTable = new DataTable();
            try {


                OdbcCommand cmd = new OdbcCommand("{call sp_registra_factura_odoo(?,?,?,?,?,?)}", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@reg_est",Convert.ToInt32(reg_est));
                cmd.Parameters.AddWithValue("@fac_nro", Convert.ToInt32(fact_nro));
                cmd.Parameters.AddWithValue("@fac_fec", DateTime.ParseExact(fecha_fact.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture));
                cmd.Parameters.AddWithValue("@siat_fac",Convert.ToInt32(fact_siat));
                cmd.Parameters.AddWithValue("@siat_fec", DateTime.ParseExact(fecha_siat.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture));
                cmd.Parameters.AddWithValue("@estado", estado);




                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OdbcDataAdapter adp = new OdbcDataAdapter(cmd);

                adp.Fill(dataTable);
                conn.Close();
              
            }
            catch (Exception)
            {
                //objConexion.closeConnection();
              //  throw;
            }
            return dataTable;

        }

        public DataTable updateFacturaOdoo(int reg_est, int fact_nro, DateTime fecha_fact, int fact_siat, DateTime fecha_siat, string estado)
        {
            DataTable dataTable = new DataTable();
            // string fechaFactura = fecha_fact.Month + "/" + fecha_fact.Day + "/" + fecha_fact.Year;
            DateTime fechaFactura = DateTime.ParseExact(fecha_fact.ToString("yyyy-MM-dd"), "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string fechFactura = fecha_fact.Month + "-" + fecha_fact.Day + "-" + fecha_fact.Year;
            try
            {
                string query= "update factura_siat set fact_siat= " + fact_siat + ", estado='" + estado + "' where num_fact=" + fact_nro+" and fecha_fact="+fechFactura;

                OdbcCommand cmd = new OdbcCommand(query, conn);

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OdbcDataAdapter adp = new OdbcDataAdapter(cmd);

                adp.Fill(dataTable);
                conn.Close();

            }
            catch (Exception)
            {
                //objConexion.closeConnection();
                //  throw;
            }
            return dataTable;

        }

        public DataTable getUserValidator(string nro_cajero)
        {
            DataTable dataTable = new DataTable();
           

            try
            {
                string query = "select * from usuario_odoo where nro_cajero= " +nro_cajero;

                OdbcCommand cmd = new OdbcCommand(query, conn);

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OdbcDataAdapter adp = new OdbcDataAdapter(cmd);

                adp.Fill(dataTable);
                conn.Close();

            }
            catch (Exception)
            {
                //objConexion.closeConnection();
                //  throw;
            }
            return dataTable;

        }

        public DataTable searchBoleta(int idexterno, string date)
        {
            DataTable dataTable = new DataTable();
            // DateTime fecha_ = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            
            try
            {
                string query = "select first 1 * from academi.pagos_qr pgr inner join academi.solicitud_qr sqr on pgr.qrid=sqr.id_qr where sqr.reg= "+ idexterno + " order by sqr.fecha_envio desc,pgr.paymentdate desc,pgr.transactionid desc";
                    //"select first 1 * from academi.solicitud_qr sqr inner join academi.pagos_qr pqr ON sqr.id_qr=pqr.qrid where sqr.fecha_qr='" +date+"' and sqr.reg=" + idexterno+ " order by sqr.fecha_qr desc";

                OdbcCommand cmd = new OdbcCommand(query, conn);

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                OdbcDataAdapter adp = new OdbcDataAdapter(cmd);

                adp.Fill(dataTable);
                conn.Close();

            }
            catch (Exception)
            {
                //objConexion.closeConnection();
                //  throw;
            }
            return dataTable;
        }

        public string Cadena
        {
            get { return cadena; }
            set { cadena = value; }
        }
        public string DSN1
        {
            get { return DSN; }
            set { DSN = value; }
        }

        public string UID1
        {
            get { return UID; }
            set { UID = value; }
        }


        public string PWD1
        {
            get { return PWD; }
            set { PWD = value; }
        }
    }
}
