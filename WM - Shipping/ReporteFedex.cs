using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class ReporteFedex : Form
    {
        public ReporteFedex()
        {
            InitializeComponent();
        }
        Datos Consultar = new Datos();
        DataTable dtinfo = new DataTable();
        ArchivoExcel Archivo = new ArchivoExcel();
        string Fecha = string.Empty;
        const string TituloArchivo = "REPORTE DE EMBARCADO FEDEX";
        int idcarga;
        string cajan, destino;

        private void LlenarComboEnvio()
        {
            cbxEnvio.DataSource = this.Consultar.LlenarEnvio("T");
            cbxEnvio.ValueMember = "getId";
            cbxEnvio.DisplayMember = "getName";
            cbxEnvio.SelectedIndex = 0;
        }



        private void ReporteFedex_Load(object sender, EventArgs e)
        {
            LlenarComboEnvio();
        }

        private void CargarDatosGridView()
        {
            dtvResultado.DataSource = Consultar.ShipReportEmbarcadoFedex(txtcarga.Text, Convert.ToString(cbxEnvio.SelectedValue));
            dtinfo = Consultar.ShipReportEmbarcadoFedex(txtcarga.Text, Convert.ToString(cbxEnvio.SelectedValue));
            lblRegistros.Text = dtvResultado.Rows.Count.ToString();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bgwCrearExcel_DoWork(object sender, DoWorkEventArgs e)
        {
            Consultar.ObtenerFormatoArchivo(ref this.Fecha);
            Archivo.CrearArchivoExcelFedex("General -" + this.Fecha.Trim(), this.dtinfo, "REPORTE DE EMBARCADO FEDEX", this.bgwCrearExcel, this.idcarga, this.cajan, this.destino);
        }

        private void bgwCrearExcel_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbDescargaExcel.Value = e.ProgressPercentage;
        }

        private void bgwCrearExcel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

            if (e.Error == null)
                MessageBox.Show("Archivo creado correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CrearArchivoExcelFedex()
        {
            pbDescargaExcel.Value = 0;
            bgwCrearExcel.RunWorkerAsync();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (this.txtcarga.Text == "")
            {
                MessageBox.Show("Insertar Id de Embarque", "Revisar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CargarDatosGridView();
                Consultar.ObtenerIdCargafedexExcel(txtcarga.Text, Convert.ToString(cbxEnvio.SelectedValue), ref idcarga, ref cajan, ref destino);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (!bgwCrearExcel.IsBusy)
            {
                if (dtvResultado.Rows.Count > 0)
                    CrearArchivoExcelFedex();
                else
                {
                    MessageBox.Show("No existen registros en el listado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  
                }
            }
        }
    }
}
