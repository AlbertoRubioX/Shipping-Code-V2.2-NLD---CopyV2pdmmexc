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
    public partial class Correccion_tarimas_del_lote : Form
    {
        Datos Consultar = new Datos();
        public Correccion_tarimas_del_lote()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Consultar.CorrecionTarimas(this.txtqty.Text, this.txtlote.Text, GlobalVar.Compania);
            MessageBox.Show("Completado", "Completo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            this.Close();
        }

        private void Correccion_tarimas_del_lote_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
