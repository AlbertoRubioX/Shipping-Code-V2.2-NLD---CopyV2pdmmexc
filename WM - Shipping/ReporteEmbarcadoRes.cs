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
    public partial class ReporteEmbarcadoRes : Form
    {
        Datos Consultar = new Datos();
        ArchivoExcel Archivo = new ArchivoExcel();
        DataTable dtinfo = new DataTable();
        string Fecha = string.Empty;
        string FechaInicial;
        string FechaFinal;
        const string TituloArchivo = "REPORTE DE RESUMEN DE EMBARQUES";

        public ReporteEmbarcadoRes()
        {
            InitializeComponent();
        }
        

        private void bgwCrearExcel_DoWork(object sender, DoWorkEventArgs e)
        {
            Consultar.ObtenerFormatoArchivo(ref Fecha);
            Archivo.CrearArchivoExcel("General -" + Fecha.Trim(), dtinfo, "REPORTE DE RESUMEN DE EMBARQUES", bgwCrearExcel);
        }

        private void bgwCrearExcel_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbDescargaExcel.Value = e.ProgressPercentage;
        }

        private void bgwCrearExcel_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Archivo creado correctamente", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            FechaInicial = dateTimePicker1.Value.ToShortDateString() + " 00:00:01.000";
            FechaFinal = dateTimePicker2.Value.ToShortDateString() + " 23:59:59.000";
            if (DateTime.Parse(FechaInicial) >= DateTime.Parse(FechaFinal))
            {
                MessageBox.Show("Fecha inicial no debe ser mayor que la fecha final", "Revisar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                CargarDatosGridView();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void CargarDatosGridView()
        {
            dtvResultado.DataSource = Consultar.ShipReportEmbarcadoResumen(GlobalVar.Compania, FechaInicial.Trim(), FechaFinal.Trim());
            dtinfo = Consultar.ShipReportEmbarcadoResumen(GlobalVar.Compania, FechaInicial.Trim(), FechaFinal.Trim());
            lblRegistros.Text = dtvResultado.Rows.Count.ToString();
        }

        private void CrearArchivoExcel()
        {
            pbDescargaExcel.Value = 0;
            bgwCrearExcel.RunWorkerAsync();
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
                    MessageBox.Show("No existen registros en el listado", "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }

        private void ReporteEmbarcadoRes_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker2.Format = DateTimePickerFormat.Short;
        }
    }
}
