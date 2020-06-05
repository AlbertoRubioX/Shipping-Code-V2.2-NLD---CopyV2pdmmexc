using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing.Printing;
using Microsoft.VisualBasic;


namespace WindowsFormsApplication1
{    
    public partial class M2_Consolidation : Form
    {
        public M2_Consolidation()
        {
            InitializeComponent();
        }

        Datos Consultar = new Datos();
        public static string id_empalme,sql, es_empalme;
        public string cargar1="Y",parcial, identificator;
        
        private void M2_Consolidation_Load(object sender, EventArgs e)
        {

            toolStripStatusLabel1.Text = "Usuario=  " + GlobalVar.usuario;
            data_con.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
            
        }
         private void cargar_grid(object sender, EventArgs e)
         {
            if (txtlote.Text == "")
            {
                grid1.DataSource = Consultar.M2CargarGridSinLote(cbloc.Text, GlobalVar.Compania);
            }
            else
            {
                grid1.DataSource = Consultar.M2CargarGrid(cbloc.Text, txtlote.Text, txtntarimas.Text, txtTtarimas.Text, GlobalVar.Compania);
                txtlote.Text = "";
                txtntarimas.Text = "";
                txtTtarimas.Text = "";
                txtlote.Focus();
            }
            grid1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.ColumnHeader);
        }
         private void detenido(object sender, EventArgs e)
         {
             string lote = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString();
           

                 if (Consultar.M1LabelLoteDetenido(lote) == true)
                 {
                     cargar1 = "N";
                 }
                 else
            {
                cargar1 = "Y";
            }
 
         }
         private void btn_add_Click(object sender, EventArgs e)
         {
             if (grid1.RowCount >= 1)
             {
                 string quantity = "", lote = "", ciclo1 = "", destino1 = "";
                 string id1 = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[0].Value.ToString();
                 lote = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString();
                 quantity = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[2].Value.ToString();
                 ciclo1 = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString();
                 destino1 = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString();
                 int duplic = 0;
                 detenido((object)sender, (EventArgs)e);
                 if (cargar1 == "N")
                 {
                     MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                 }
                 else
                 {
                      for (int i = 0; i <= data_con.Rows.Count - 1; i++)

                     if (lote == Convert.ToString(data_con.Rows[i].Cells[1].Value))
                     {
                         duplic++;
                     }

                 if (duplic >= 1)
                 {
                     MessageBox.Show("Ese lote ya esta agregado al empalme", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                 }
                 else
                 {
                     if (quantity == "0")
                     {
                         quantity = Microsoft.VisualBasic.Interaction.InputBox("Confime cantidad de cajas para Lote:" + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString(), "VERIFICAR");
                     }
                     int error = 0;
                     try
                     {
                         int qty2 = Convert.ToInt32(quantity);
                     }
                     catch
                     {
                         error = 1;
                     }
                     if (error == 1)
                     {
                         MessageBox.Show("La cantidad de cajas debe ser un valor numerico", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                     else
                     {
                         this.data_con.Rows.Add(id1, lote, quantity, ciclo1, destino1);
                         this.grid1.Rows.Remove(grid1.CurrentRow);
                         //ListBox1.Items.Remove(ListBox1.SelectedItems(x))
                     }
                 }
             }
            
                 }
             else
             {
                 MessageBox.Show("No hay lotes para agregar", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
             }
                    
}

         private void btnfin_Click(object sender, EventArgs e)
         {
           
             if (data_con.RowCount <= 1)
             {
                 MessageBox.Show("'Empalme' debe tener 2 lotes como mínimo", "Error ",MessageBoxButtons.OK,MessageBoxIcon.Error );
             }
             else
             {
                 if (MessageBox.Show("Ha terminado la consolidacion(empalme) ?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                 {
                     int discrepancia=0;
                     string ciclo, destino, ciclo_1, destino_1;
                     ciclo_1 = data_con.Rows[0].Cells[3].Value.ToString();
                     destino_1 = data_con.Rows[0].Cells[4].Value.ToString();
                     for (int i = 1; i <= data_con.RowCount - 1; i++)
                     {
                         ciclo = data_con.Rows[i].Cells[3].Value.ToString();
                         destino = data_con.Rows[i].Cells[4].Value.ToString();
                         if (ciclo == ciclo_1 && destino == destino_1)
                         {
                         }

                        else if (ciclo != ciclo_1 && destino != destino_1 || ciclo != ciclo_1 && destino == destino_1 || ciclo == ciclo_1 && destino != destino_1)
                        {
                            discrepancia = 2;
                        }
                        else
                         {
                             discrepancia++;
                         }
                     }
                     if (discrepancia == 0 || discrepancia == 2)
                     {
                         DialogResult resultado;
                         resultado = MessageBox.Show("Estas Mezclando la carga?", "Verificar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                         if (resultado == DialogResult.No)
                         {
                            MessageBox.Show("Los ciclos y destinos del empalme no coinciden... desea continuar con el empalme?", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.data_con.Rows.Clear();
                            return;
                         }
                     }
 
                         get_id_empalme((object)sender, (EventArgs)e);
                        
                 }
                 else
                 {

                 }
             }
           }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtlote_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                txtntarimas.Focus();

            }
        }

        private void get_id_empalme(object sender, EventArgs e)
         {
            this.Consultar.ObtenerIdEmpalme(ref identificator, GlobalVar.Compania);
            id_empalme = identificator;
            save_empalme(sender, e);
          
         }
         private void save_empalme(object sender, EventArgs e)
         {
             int rows1,i;
             string lote1, qty1,id;
             rows1 = data_con.RowCount;
             i = 0;
      do 
        {
            id = data_con.Rows[i].Cells[0].Value.ToString();
            lote1 = data_con.Rows[i].Cells[1].Value.ToString();
            qty1 = data_con.Rows[i].Cells[2].Value.ToString();
            Consultar.M2ActualizacionInventario(Convert.ToInt32(id_empalme), GlobalVar.nombre_user, Convert.ToInt32(qty1), Convert.ToInt32(id), GlobalVar.Compania);
            
            i++;
        } while (i < rows1  );
        
        sendtoprinter((object) sender, (EventArgs) e);//  por el momento no se mandara a imprimir el # de emplame, ya que lo muestra en pantalla.
        MessageBox.Show("Empalme # " +  id_empalme + " guardado");
        this.data_con.Rows.Clear();
        cargar_grid((object)sender, (EventArgs)e);

         }

         private void sendtoprinter(object sender, EventArgs e)
         {
             PrintDocument p = new PrintDocument();
             p.PrintPage += delegate(object sender1, PrintPageEventArgs e1)
             {
                 e1.Graphics.DrawString("Emp# " + id_empalme , new Font("Arial Black", 26), new SolidBrush(Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));

                 e1.Graphics.DrawString("*" + id_empalme + "*", new Font("3 of 9 Barcode", 40), new SolidBrush(Color.Black), 8, 45);


             };
             try
             {
                 p.Print();
             }
             catch (Exception ex)
             {
                 throw new Exception("Exception Occured While Printing", ex);
             }

         }

         private void button1_Click(object sender, EventArgs e)
         {
             cargar_grid((object)sender, (EventArgs)e);
         }

        
        
        
 }


}
