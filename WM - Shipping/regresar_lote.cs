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
    public partial class regresar_lote : Form
    {
        Datos Consultar = new Datos();
        
        public string lote1,tarima1,localizacion1,fecha_entrada1,hora_entrada1,idtarima1;
        public regresar_lote()
        {
            InitializeComponent();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtlote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {

                button1_Click(sender, e);
            }
        }

        private void regresar_lote_Load(object sender, EventArgs e)
        {

            toolStripStatusLabel1.Text = "Usuario=  " + GlobalVar.usuario;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Consultar.Regresarlote(txtlote.Text, GlobalVar.Compania);
            grid.DataSource = Consultar.Regresarlote(txtlote.Text, GlobalVar.Compania);
            grid.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            txtlote.Text = "";

        }


        private void grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DialogResult resultado;
            lote1 = grid.Rows[grid.CurrentCell.RowIndex].Cells[1].Value.ToString();
            tarima1 = grid.Rows[grid.CurrentCell.RowIndex].Cells[2].Value.ToString();
            resultado= MessageBox.Show ("Confirma que se regresara la tarima " + tarima1 + " del lote " + lote1 + " ? ", "VERIFICAR", MessageBoxButtons.YesNo , MessageBoxIcon.Question );
            if (resultado == DialogResult.Yes)
            {
                localizacion1 = grid.Rows[grid.CurrentCell.RowIndex].Cells[3].Value.ToString();
                if (localizacion1 != "cargado")
                {
                    
                    idtarima1 = grid.Rows[grid.CurrentCell.RowIndex].Cells[0].Value.ToString();
                    fecha_entrada1 = grid.Rows[grid.CurrentCell.RowIndex].Cells[6].Value.ToString();
                    label2.Visible = false;
                    grid.Visible = false;
                    panel1.Visible = true;
                    label3.Text = "Se esta regresando la tarima " + tarima1 + " del lote " + lote1 + " ... ";


                }
                else
                {
                    MessageBox.Show("Lote ya esta cargado en trailer, debe descargarlo antes de regresar", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            label2.Visible = true;
            panel1.Visible = false;
            grid.Visible = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sql1, sql2;

            string hora1 = DateTime.Now.ToString("hh:mm:ss tt");
            string date1 = DateTime.Now.ToString("MM/dd/yy");
            Consultar.RegresarloteInsertar(GlobalVar.Compania, lote1, tarima1, fecha_entrada1, GlobalVar.nombre_user, txtregresa.Text);
            Consultar.DeleteTarima(this.idtarima1, GlobalVar.Compania);

                MessageBox.Show("Lote registrado, puede salir del area", "Regresar", MessageBoxButtons.OK, MessageBoxIcon.Information);

                int i=0;
                for (i = 0 ; i> grid.RowCount  ; i++)
                {
                    grid.Rows.Remove(grid.Rows[i]);
                }
                
                txtregresa.Text = "";
                txtlote.Text = "";
                label3.Text = "";
                panel1.Visible = false;

        }     

    }
}
