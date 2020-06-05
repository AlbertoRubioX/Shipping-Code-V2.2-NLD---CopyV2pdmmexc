using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.VisualBasic;

namespace WindowsFormsApplication1
{
    
    public partial class M2_MapaDetalle : Form
    {
        Datos Consultar = new Datos();    
        public M2_MapaDetalle()
        {
            InitializeComponent();
        }
        private void btnok_Click(object sender, EventArgs e)
        {
            data1.DataSource = Consultar.M2MapaGrid(txtid.Text, GlobalVar.Compania);
            data1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }

      
      
        private void data1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int indice;
            indice = data1.CurrentCell.RowIndex;
            string qty;
            qty = Microsoft.VisualBasic.Interaction.InputBox("Introduzca cantidad correcta", "Verificar");
            int iNewQty = 0;
            if (!int.TryParse(qty, out iNewQty))
                iNewQty = 0;
            if (iNewQty > 0)
                Consultar.M2ActualizacionMapa(iNewQty, Convert.ToInt32(txtid.Text), data1.Rows[indice].Cells[2].Value.ToString(), GlobalVar.Compania);

            btnok_Click((object)sender, (EventArgs)e);
        }

        private void M2_MapaDetalle_Load(object sender, EventArgs e)
        {
            data1.ForeColor = Color.Black;
            data1.Font = new Font("Microsoft Sans Serif", 10);
            toolStripStatusLabel1.Text = "Usuario=  " + GlobalVar.usuario;
        }
        

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
