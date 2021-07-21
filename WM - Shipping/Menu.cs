using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        private void labelingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            M1_Labeling label1 = new M1_Labeling();
            label1.ShowDialog();
        }
        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            M2_Consolidation consol = new M2_Consolidation();
            consol.ShowDialog();
        }
        private void Menu_Load(object sender, EventArgs e)
        {

            toolStripStatusLabel1.Text = "Bienvenido  " + GlobalVar.usuario;
            if (GlobalVar.n_acceso == 1)
            { 
            }
            else if (GlobalVar.n_acceso == 2)
            {
               // monitorToolStripMenuItem.Visible = false;
                supervisionToolStripMenuItem.Visible = false;   
            }
            else if (GlobalVar.n_acceso == 3)
            {
                monitorToolStripMenuItem.Visible = false;
                supervisionToolStripMenuItem.Visible = false;
                functionsToolStripMenuItem.Visible = false;
                manualToolStripMenuItem.Visible = false;
            }
        }
        private void reviseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            M2_Validation valid = new M2_Validation();
            valid.ShowDialog();   
        }
        private void cargarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            M3_Map cargar = new M3_Map();
            cargar.ShowDialog();
        }
        private void buscarLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Buscar busqueda = new Buscar();
            busqueda.ShowDialog();   
        }
        private void lotesParcialesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quiebre parciales = new quiebre();
            parciales.ShowDialog();
        }
        private void retrabajarCargaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Retrabajar_carga retrabaja = new Retrabajar_carga();
            retrabaja.ShowDialog();
        }
        private void modificarLocalizacionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Modificar_Localizacion modificar1 = new Modificar_Localizacion();
            modificar1.ShowDialog();
        }
        private void historicoDeCargasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cargas carga1 = new Cargas();
            carga1.ShowDialog();
        }
        private void verificarEmpalmeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            M2_MapaDetalle mapa = new M2_MapaDetalle();
            mapa.ShowDialog();   
        }
        private void regresarLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            regresar_lote regresar = new regresar_lote();
            regresar.ShowDialog ();
        }
        private void buscarLoteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
      
        }
        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void usuariosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Usuarios usuarios1 = new Usuarios();
            usuarios1.ShowDialog();
        }

        private void functionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pisoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GlobalVar.Compania == 110)
            {
                M4_Layout layout1 = new M4_Layout();
                layout1.ShowDialog();
            }
            else if(GlobalVar.Compania == 686)
            {
                M4_LayoutMXC layout1 = new M4_LayoutMXC();
                layout1.ShowDialog();
            }
        }

        private void cargasPersonaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grafico_Por_Persona grafica = new Grafico_Por_Persona();
            grafica.ShowDialog();
        }

        private void modificarDeTarimasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Correccion_tarimas_del_lote corregir = new Correccion_tarimas_del_lote();
            corregir.ShowDialog();
        }

        private void noCargarLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Detener_Lote detener = new Detener_Lote();
            detener.ShowDialog();
        }

        private void graficasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grafico_Por_Persona grafico = new Grafico_Por_Persona();
            grafico.ShowDialog();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void acercaDeShippingSystemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About acerca = new About();
            acerca.ShowDialog();
        }

        private void manualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string sFile = @"\\nlprdsSs1\Software-Productos\Shipping-SQL\Shipping System Manual.pdf";

            if (GlobalVar.Compania == 686)
                sFile = @"\\mxcprdfp1\Software_MXC\Shipping Sql version\Shipping System Manual.pdf";

            if (File.Exists (sFile) == true )
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = Path.Combine(Application.StartupPath, sFile);
                proc.Start();
                proc.Close();
            }
            else
            {
                MessageBox.Show("No se encuentra el archivo que contenga el manual de usuario en la red");
            }
        }

        private void loteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Buscar busqueda = new Buscar();
            busqueda.ShowDialog();
        }

        private void fedexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteFedex Rfedex = new ReporteFedex();
            Rfedex.ShowDialog();
        }

        private void embarcadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteEmbarcado Rembarcado = new ReporteEmbarcado();
            Rembarcado.ShowDialog();
        }

        private void generalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteStatus rstatus = new ReporteStatus();
            rstatus.ShowDialog();
        }

        private void subirFedexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadExcel ufedex = new UploadExcel();
            ufedex.ShowDialog();
        }

        private void fedexToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Fedex fedexx = new Fedex();
            fedexx.ShowDialog();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void cerrarCajaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseBoxTrailer Trailer = new CloseBoxTrailer();
            Trailer.ShowDialog();
        }

        private void reumenDeEmbarquesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReporteEmbarcadoRes RembarcadoRes = new ReporteEmbarcadoRes();
            RembarcadoRes.ShowDialog();
        }
    }
}
