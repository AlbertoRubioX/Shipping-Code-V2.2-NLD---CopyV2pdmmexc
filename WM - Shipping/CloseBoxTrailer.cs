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
    public partial class CloseBoxTrailer : Form
    {
        Datos Consultar = new Datos();
        public CloseBoxTrailer()
        {
            InitializeComponent();
        }

         private void button1_Click_1(object sender, EventArgs e)
        {
            if (Consultar.CloseBoxTrailerInv(txtpos.Text) == false)
            {
                Consultar.CloseBoxTrailer(txtpos.Text, GlobalVar.Compania);
                MessageBox.Show("Id Cerrado", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
            else
            {
                MessageBox.Show("Caja ya tiene carga)", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtpos.Text = "";
                txtpos.Focus();
            }
        }

        private void btncerrar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
