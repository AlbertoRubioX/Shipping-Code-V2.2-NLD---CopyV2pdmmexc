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
   
    public partial class ReporteStatus : Form
    {
        Datos Consultar = new Datos();
        ArchivoExcel Archivo = new ArchivoExcel();
        DataTable dtinfo = new DataTable();
        string Fecha = string.Empty;
        const string TituloArchivo = "REPORTE DE GENERAL DE LOCALIZACIONES";
        public ReporteStatus()
        {
            InitializeComponent();
        }


        private void Localizacion()
        {
            this.cbxloc.DataSource = this.Consultar.Localizaciones("T");
            this.cbxloc.ValueMember = "getName";
            this.cbxloc.DisplayMember = "getName";
            this.cbxloc.SelectedIndex = 0;
        }

        private void bgwCrearExcel_DoWork(object sender, DoWorkEventArgs e)
        {
            Consultar.ObtenerFormatoArchivo(ref this.Fecha);
            Archivo.CrearArchivoExcel("General -" + Fecha.Trim(), dtinfo, "REPORTE DE GENERAL DE LOCALIZACIONES", bgwCrearExcel);
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarDatosGridView();
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (!bgwCrearExcel.IsBusy)
            {
                if (dtvResultado.Rows.Count > 0)
                {
                    CrearArchivoExcel();
                }
                else
                {
                    MessageBox.Show("No existen registros en el listado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }


        private void CargarDatosGridView()
        {
            if (cbxloc.SelectedValue.ToString().Trim() == "-- TODOS--")
            {
                dtvResultado.DataSource = Consultar.ReporteGeneral(1, cbxloc.SelectedValue.ToString().Trim(), GlobalVar.Compania);
                dtinfo = Consultar.ReporteGeneral(1, cbxloc.SelectedValue.ToString().Trim(), GlobalVar.Compania);
                lblRegistros.Text = dtvResultado.Rows.Count.ToString();
            }
            else
            {
                dtvResultado.DataSource = Consultar.ReporteGeneral(2, cbxloc.SelectedValue.ToString().Trim(), GlobalVar.Compania);
                dtinfo = Consultar.ReporteGeneral(2, cbxloc.SelectedValue.ToString().Trim(), GlobalVar.Compania);
                lblRegistros.Text = dtvResultado.Rows.Count.ToString();
            }
        }

        private void CrearArchivoExcel()
        {
            this.pbDescargaExcel.Value = 0;
            this.bgwCrearExcel.RunWorkerAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ReporteStatus_Load(object sender, EventArgs e)
        {
            Localizacion();
        }
    }
}
