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
    public partial class CrearCargaFedex : Form
    {
        public CrearCargaFedex()
        {
            InitializeComponent();
        }


        Datos Consultar = new Datos();
        int idcajafedex,envio;
        private void btnCrear_Click(object sender, EventArgs e)
        {
            if (txtcaja.Text == "")
            {
                MessageBox.Show("Llenar informacion completa ( Caja/Tipo de Envio)", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                Consultar.IngresarFedex(GlobalVar.Compania, txtcaja.Text);
                Consultar.ObtenerIdCargaFedex(ref idcajafedex);
                MessageBox.Show("Carga Agregada, ID_CARGA =" + idcajafedex, "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Fedex fedex = new Fedex();
                this.Visible = false;
                fedex.ShowDialog(this);
                fedex.Dispose();
                this.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
