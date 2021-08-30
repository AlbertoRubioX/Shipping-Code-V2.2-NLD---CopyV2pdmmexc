using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
 

namespace WindowsFormsApplication1
{
    public partial class Retrabajar_carga : Form
    {
        Datos Consultar = new Datos();
        public Retrabajar_carga()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtcarga.Text == "")
            {
                MessageBox.Show("Por favor introduzca el numero de carga a retrabajar", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.Consultar.RetrabajarCarga(txtcarga.Text, GlobalVar.Compania);
                MessageBox.Show("Carga lista para retrabajarse", "Completo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                this.txtcarga.Text = "";
            }

        }

        private void Retrabajar_carga_Load(object sender, EventArgs e)
        {

        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
