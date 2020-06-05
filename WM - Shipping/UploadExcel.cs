using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;

namespace WindowsFormsApplication1
{
   

    public partial class UploadExcel : Form
    {
        Datos Consultar = new Datos();
        private ArchivoExcel Excel = new ArchivoExcel();
        string RutaArchivo = string.Empty;
        string MensajeError = string.Empty;
        string pn, lote, brach, total, tipo;
        int idcarga, idemb;
        public UploadExcel()
        {
            InitializeComponent();
            txtArchivo.ReadOnly = true;
        }

        private void btnCargarDatosArchivo_Click(object sender, EventArgs e)
        {
            if (ControlDeErrores(txtArchivo, txtArchivo.Text) && ControlDeErrores(cboHojaExcel, cboHojaExcel.Text.Trim()))
            {
                if (cboHojaExcel.Text != "-- SELECCIONE UNA HOJA DE EXCEL --")
                    Excel.LeerArchivoExcel(RutaArchivo, dgvDatos, cboHojaExcel.Text.Trim());
            }
            else
                MessageBox.Show(MensajeError, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSeleccionarArchivo_Click(object sender, EventArgs e)
        {
            OpenFileDialog Archivo = new OpenFileDialog();
            Archivo.Title = "Buscar archivo...";
            Archivo.Filter = "Excel Files |*.xlsx";
            Archivo.InitialDirectory = @"C:\";

            if (Archivo.ShowDialog() == DialogResult.OK)
            {
                if (dgvDatos.Rows.Count > 0)
                    dgvDatos.DataSource = null;

                RutaArchivo = Archivo.FileName;
                txtArchivo.Text = Path.GetFileName(Archivo.FileName);
                Excel.ObtenerHojasDeExcel(cboHojaExcel, RutaArchivo);
            }
        }

        private void btnsubir_Click(object sender, EventArgs e)
        {
            string ssqltable = "uploadfedex";
            string myexceldataquery = "select * from [" + cboHojaExcel.Text.Trim() + "]";

            try
            {
                string sexcelconnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + RutaArchivo.Trim() + ";Extended Properties=Excel 12.0;";
                //string ssqlconnectionstring = "Data Source =NLPRDSSS1;Initial Catalog = ShippingSystem; User Id = nldprod; Password=nldprod;";

                string ssqlconnectionstring = "Data Source =NLPRDLOCALDB1;Initial Catalog = ShippingSystem; User Id = nldprodtest; Password=T3stPrdN19L;";

                string sclearsql = "delete from " + ssqltable + " where trans_date > = convert(varchar(10),getdate(),110)";
                SqlConnection sqlconn = new SqlConnection(ssqlconnectionstring);
                SqlCommand sqlcmd = new SqlCommand(sclearsql, sqlconn);
                sqlconn.Open();
                sqlcmd.ExecuteNonQuery();
                sqlconn.Close();

                OleDbConnection oledbconn = new OleDbConnection(sexcelconnectionString);
                OleDbCommand oledbcmd = new OleDbCommand(myexceldataquery, oledbconn);

                oledbconn.Open();
                OleDbDataReader dr = oledbcmd.ExecuteReader();
                SqlBulkCopy bulkcopy = new SqlBulkCopy(ssqlconnectionstring);
                bulkcopy.DestinationTableName = ssqltable;
                while(dr.Read())
                {
                    bulkcopy.WriteToServer(dr);
                }
                Consultar.updatexcel();

                oledbconn.Close();
                dgvDatos.Rows.Clear();
                grid();
                
            }
            catch (Exception Error)
            {
                throw new Exception("Error: " + Error.Message);
            }

        }

        void grid()
        {
            dtvgridSubido.DataSource = Consultar.ExcelSubido();
            dtvgridSubido.AutoResizeColumns();
            dtvgridSubido.ForeColor = Color.Black;
            dtvgridSubido.Font = new Font("Microsoft Sans Serif", 9f);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Consultar.updatefedexmapa(txtship.Text, txtpartnumber.Text, txtlote.Text, txtbrach.Text, txttotal.Text, Convert.ToInt32(txtidembarque.Text));

            txtidembarque.Text = "";
            txtlote.Text = "";
            txtoidcarga.Text = "";
            txtpartnumber.Text = "";
            txttotal.Text = "";
            txtbrach.Text = "";
            txtship.Text = "";
            txtidembarque.Enabled = true;
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtidembarque.Text = "";
            txtlote.Text = "";
            txtoidcarga.Text = "";
            txtpartnumber.Text = "";
            txttotal.Text = "";
            txtbrach.Text = "";
            txtship.Text = "";
            txtidembarque.Enabled = true;

        }

        private bool ControlDeErrores(Control TipoControl, string Cadena)
        {
            if (TipoControl is TextBox && !ValidarCadena(Cadena))
            {
                MensajeError = "Favor de seleccionar el archivo de excel " + Environment.NewLine + "con los datos a retrabajar";
                btnSeleccionarArchivo.Focus();
                return false;
            }
            else if (TipoControl is ComboBox && !ValidarCadena(Cadena))
            {
                MensajeError = "Favor de seleccionar la hoja de Excel " + Environment.NewLine + "donde se encuentra los datos";
                TipoControl.Focus();
                return false;
            }
            else
                return true;
        }

        private bool ValidarCadena(string cadena)
        {
            if (!string.IsNullOrWhiteSpace(cadena))
            {
                if (cadena.Length > 0)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private void UploadExcel_Load(object sender, EventArgs e)
        {
            grid();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Consultar.ObtenerIdembarqueFedex(ref idemb, ref pn, ref lote, ref brach, ref total, ref tipo, ref idcarga, txtidembarque.Text);

            txtbrach.Text = brach;
            txtlote.Text = lote;
            txtpartnumber.Text = pn;
            txtship.Text = tipo;
            txttotal.Text = total;
            txtoidcarga.Text = Convert.ToString(idcarga);
            txtidembarque.Enabled = false;
        }
    }
}
