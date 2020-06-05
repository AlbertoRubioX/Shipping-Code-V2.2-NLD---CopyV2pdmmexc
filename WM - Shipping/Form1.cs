using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;


namespace WindowsFormsApplication1
{
    public partial class Cargas : Form
    {
        Datos Consultar = new Datos();
        public Cargas()
        {
            InitializeComponent();
        }

        private void Cargas_Load(object sender, EventArgs e)
        {
            grid1.ForeColor = Color.Black;
            grid1.Font = new Font("Microsoft Sans Serif", 10);

            toolStripStatusLabel1.Text = "Usuario=  " + GlobalVar.usuario;
            radioButton1.Checked = true;
            radioButton2.Checked = false;

            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker2.Format = DateTimePickerFormat.Short;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true && radioButton2.Checked == false)
            {
                dateTimePicker1.Visible = false;
                dateTimePicker2.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                lblmapa.Visible = true;
                txtmapa.Visible = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == true )
            {
                dateTimePicker1.Visible = true ;
                dateTimePicker2.Visible = true ;
                label1.Visible = true ;
                label2.Visible = true;
                lblmapa.Visible = false ;
                txtmapa.Visible = false ;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string FechaInicial = dateTimePicker1.Value.ToShortDateString() + " 00:00:01:000";
            string FechaFinal = dateTimePicker2.Value.ToShortDateString() + " 23:59:59:000";

            if (dateTimePicker2.Value < dateTimePicker1.Value)
            {
                MessageBox.Show("Fecha no puede ser menor a la inicial", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (radioButton1.Checked == true)
                {
                    grid1.DataSource = Consultar.HistoricoCarga(txtmapa.Text, GlobalVar.Compania);
                }
                else
                {
                    grid1.DataSource = Consultar.HistoricoCargaFecha(FechaInicial, FechaFinal, GlobalVar.Compania);
                }

                grid1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            }
        }

        private void grid1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string mapa_ = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            if (GlobalVar.Compania == 110)
            {
                if (File.Exists(@"\\mexfp1\medline\Dept. Recibo & Embarques\Mapas electronicos\Merged\MergedSqltest\MAPA_#" + mapa_ + ".pdf"))
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = @"\\mexfp1\medline\Dept. Recibo & Embarques\Mapas electronicos\Merged\MergedSqltest\MAPA_#" + mapa_ + ".pdf";
                    proc.Start();
                    proc.Close();
                }
                else
                {
                    MessageBox.Show("No se encuentra el archivo del MAPA para la carga #" + mapa_);
                }
            }
            else if(GlobalVar.Compania == 686)
            {
                if (File.Exists(@"\\mxcprdfp1\Software_MXC\ShippingSystem\Mapas_electronicos\Mergedsqltest\MAPA_#" + mapa_ + ".pdf"))
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.StartInfo.FileName = @"\\mxcprdfp1\Software_MXC\ShippingSystem\Mapas_electronicos\Mergedsqltest\MAPA_#" + mapa_ + ".pdf";
                    proc.Start();
                    proc.Close();
                }
                else
                {
                    MessageBox.Show("No se encuentra el archivo del MAPA para la carga #" + mapa_);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
