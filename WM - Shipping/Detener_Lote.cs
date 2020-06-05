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
    public partial class Detener_Lote : Form
    {
        Datos Consultar = new Datos();
        public Detener_Lote()
        {
            InitializeComponent();
        }
        private void Detener_Lote_Load(object sender, EventArgs e)
        {
            grid.ForeColor = Color.Black;
            grid.Font = new Font("Microsoft Sans Serif", 10);
            cargar_grid ((object) sender, (EventArgs) e);

        }
        private void cargar_grid(object sender, EventArgs e)
        {

            this.grid.DataSource = this.Consultar.CargarDetener();
            this.grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if ((txtlote1.Text == "") || (txtcomment.Text == ""))
            {
                MessageBox.Show("Llenar campos lote y comentario para continuar", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.Consultar.IngresarDetenerLote(GlobalVar.Compania, txtlote1.Text, txtcomment.Text, GlobalVar.usuario);
                MessageBox.Show("Lote agregado a listado", "Completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtcomment.Text = "";
                txtlote1.Text = "";
                cargar_grid(sender, e);
            }

        }

        private void btnactualizar_Click(object sender, EventArgs e)
        {
            cargar_grid((object)sender, (EventArgs)e);
        }

        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            string lote1 = grid.Rows[grid.CurrentCell.RowIndex].Cells[0].Value.ToString();
            if (MessageBox.Show("Confirma que desea quitar el lote " + lote1 + " de la lista de 'No cargar' ? ", "VERIFICAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Consultar.DeleteDetenerLote(lote1, GlobalVar.Compania);
                MessageBox.Show("Lote removido", "Completo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.cargar_grid(sender, e);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
