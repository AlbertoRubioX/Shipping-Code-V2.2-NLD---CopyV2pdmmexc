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
    public partial class M4_DetallePiso : Form
    {
        Datos Consultar = new Datos();
        public M4_DetallePiso()
        {
            InitializeComponent();
        }

        private void M4_DetallePiso_Load(object sender, EventArgs e)
        {
            lbllocalizacion.Text = M4_Layout.piso_d;
            if (GlobalVar.Compania == 110)
            {
                lbllocalizacion.Text = M4_Layout.piso_d;
                grid1.DataSource = Consultar.M4DetallePiso(M4_Layout.piso_d, GlobalVar.Compania);
                grid1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
            else if (GlobalVar.Compania == 686)
            {
                this.lbllocalizacion.Text = M4_LayoutMXC.piso_d;
                this.grid1.DataSource = this.Consultar.M4DetallePiso(M4_LayoutMXC.piso_d, GlobalVar.Compania);
                this.grid1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
