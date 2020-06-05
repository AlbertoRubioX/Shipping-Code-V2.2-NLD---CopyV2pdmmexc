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
    public partial class remover_lote : Form
    {
        Datos Consultar = new Datos();
        public remover_lote()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtpos.Text == "1" || txtpos.Text == "2" || txtpos.Text == "3" || txtpos.Text == "4" || txtpos.Text == "5" || txtpos.Text == "6" || txtpos.Text == "7" || txtpos.Text == "8" || txtpos.Text == "9" || txtpos.Text == "10" ||
               txtpos.Text == "11" || txtpos.Text == "12" || txtpos.Text == "13" || txtpos.Text == "14" || txtpos.Text == "15" || txtpos.Text == "16" || txtpos.Text == "17" || txtpos.Text == "18" || txtpos.Text == "19" || txtpos.Text == "20" ||
               txtpos.Text == "21" || txtpos.Text == "22" || txtpos.Text == "23" || txtpos.Text == "24" || txtpos.Text == "25" || txtpos.Text == "26" || txtpos.Text == "27" || txtpos.Text == "28" || txtpos.Text == "29" || txtpos.Text == "30")
            {
                Consultar.Removerlote(this.txtpos.Text, GlobalVar.Compania);
                MessageBox.Show("Lote descargado", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();

            }
            else
            {
                MessageBox.Show("Favor de introducir un valor correcto (1-28)", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtpos.Text = "";
                txtpos.Focus();
            }
        }
        private void remover_lote_Load(object sender, EventArgs e)
        {

        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
