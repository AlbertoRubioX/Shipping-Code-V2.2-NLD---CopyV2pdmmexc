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
    public partial class Buscar : Form
    {
        Datos Consultar = new Datos();
        public Buscar()
        {
            InitializeComponent();
        }

     

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.txtlote.Text == "")
            {
                MessageBox.Show("Introducir lote a buscar", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.grid.DataSource = this.Consultar.BuscarLote(this.txtlote.Text);
                this.grid.AutoResizeColumns();
                if (this.grid.RowCount == 0)
                {
                    MessageBox.Show("No hay tarimas del lote " + this.txtlote.Text + " en el area de embarques", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

        }
        private void grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void Buscar_Load(object sender, EventArgs e)
        {
            grid.ForeColor = Color.Black;
            grid.Font = new Font("Microsoft Sans Serif", 9);
            toolStripStatusLabel1.Text = "Usuario=  " + GlobalVar.usuario;
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    }

