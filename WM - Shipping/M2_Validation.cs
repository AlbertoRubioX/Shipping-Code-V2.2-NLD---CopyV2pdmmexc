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
using System.Data.Odbc;
using System.Collections;

namespace WindowsFormsApplication1
{
    
    public partial class M2_Validation : Form
    {
        public M2_Validation()
        {
            InitializeComponent();
        }
        Datos Consultar = new Datos();
         public string id_empalme,tray;
         public double cbf_n;
         public int items_empalme, Totalitem;
         ArrayList incrementarVal = new ArrayList();
        private void M2_Validation_Load(object sender, EventArgs e)
        {
            txtlote.CharacterCasing = CharacterCasing.Upper;
            txtlote.Focus();
        }
         private void M2_Validation_FormClosed(object sender, FormClosedEventArgs e)
         {
             Menu menu1 = new Menu();
             menu1.Show();
         }
         private void btn_add_Click(object sender, EventArgs e)
         {
             String duplic = "N";
             if ((txtlote.Text == "") || (txtqty.Text == ""))
             {
                    MessageBox.Show("Favor de agregar valor completo", "Verificar");
             }
             else
             {

                 if (data_con.Rows.Count > 0)
                 {

                     for (int i = 0; i <= data_con.Rows.Count - 1; i++)
                     {
                         if (txtlote.Text == Convert.ToString(data_con.Rows[i].Cells[0].Value))
                         {
                             duplic = "Y";
                         }
                     }

                     if (duplic == "Y")
                     {
                         MessageBox.Show("Ese lote ya esta en la lista de empalme");
                     }
                     else
                     {

                         agregar_cbf((object)sender, (EventArgs)e);
                         this.data_con.Rows.Add(txtlote.Text, txtqty.Text, cbf_n);
                             txtqty.Text = "";
                             txtlote.Text = "";
                             txtlote.Focus();

                     }


                 }
                 else
                 {
                         agregar_cbf((object)sender, (EventArgs)e);
                         this.data_con.Rows.Add(txtlote.Text, txtqty.Text,cbf_n );
                         txtqty.Text = "";
                         txtlote.Text = "";
                         txtlote.Focus();
                 }
             }

         
             
}

         private void agregar_cbf(object sender, EventArgs e)
         {
            string prod;
            Consultar.M2ValidacionTray(ref tray, txtlote.Text, GlobalVar.Compania);
            prod = (tray);
        
                     OdbcConnection conexion = new OdbcConnection("Dsn=QDSN_AS400SYS;uid=shipmex;pwd=mexship");
                     double hight1, width1, lenght1;
                     string sql3 = "Select IMHGHT,IMWDTH,IMLNGT from KBM400MFG.FKITMSTR where IMPN='" + prod + "' and IMCO=" + GlobalVar.Compania + "";
                     OdbcCommand comando11 = new OdbcCommand(sql3, conexion);
                     OdbcDataReader reader1 = null;
                     try
                     {
                         conexion.Open();
                         reader1 = comando11.ExecuteReader();
                         if (reader1.Read())
                         {
                             hight1 = Convert.ToDouble(reader1["IMHGHT"].ToString());
                             width1 = Convert.ToDouble(reader1["IMWDTH"].ToString());
                             lenght1 = Convert.ToDouble(reader1["IMLNGT"].ToString());
                             cbf_n  = (hight1 * width1 * lenght1) / 1728;
                         }


                     }
                     catch
                     {
                         conexion.Close();
                         MessageBox.Show("CBF error por favor notifique al administrador del sistema");
                     }
                     finally
                     {
                         conexion.Close();
                     }
        }
                
         private void printpacking(object sender, EventArgs e)
         {
             PrintDocument p = new PrintDocument();
             p.PrintPage += delegate(object sender1, PrintPageEventArgs e1)
             {

                 Bitmap bm = new Bitmap(this.data_con.Width, this.data_con.Height);
                 this.data_con.DrawToBitmap(bm, new Rectangle(0, 0, this.data_con.Width, this.data_con.Height));

                 e1.Graphics.DrawString("Empalme#" + txtempalme.Text  , new Font("Arial Black", 26), new SolidBrush(Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
                 e1.Graphics.DrawImage(bm, 15, 100);
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
         private void btnfin_Click(object sender, EventArgs e)
         {
             string lote, cajas;
            int validador = 0;
            
            if (data_con.RowCount == 0)
             {
                 MessageBox.Show("'Empalme' no tiene lotes");
                 validador = 1;
                 
             }

             else
             {
                if (MessageBox.Show("Ha terminado la captura del empalme ?", "Confirmacion", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    get_empalme_size((object)sender, (EventArgs)e);
                    int filas;
                    filas = data_con.RowCount;


                    if (filas == items_empalme)
                    {

                        for (int i = 0; i <= data_con.Rows.Count - 1; i++)
                            
                        {
                            cajas = data_con.Rows[i].Cells[1].Value.ToString();
                            lote = data_con.Rows[i].Cells[0].Value.ToString();
                            if (Consultar.M2ValidationLote(lote, cajas, txtempalme.Text, GlobalVar.Compania) == true)
                            {
                                validador  =  0;
                            }
                            else
                            {
                                validador = 1;
                                   // No hacer nada , lote rechazado
                                    MessageBox.Show("Empalme incorrecto, verifique", "DISCREPANCIA", MessageBoxButtons.OK, MessageBoxIcon.Stop);                              
                                    break;
                                
                            }
                           
                        }
                        if (validador != 0)
                        {
                            return;
                        }
                    }
                    //    else
                    //    {
                    //        validador = 1;

                    //    }
                    //}
                    else
                    {
                        validador = 1;
                    }
                }
                else
                {
                    validador = 1;
                }
            }

            if (validador == 0)
             {
                 for (int i = 0; i <= data_con.Rows.Count - 1; i++)
                 {
                     cajas = data_con.Rows[i].Cells[1].Value.ToString();
                     lote = data_con.Rows[i].Cells[0].Value.ToString();
                     int cajas2 = Convert.ToInt32(cajas);
                     double cbf_total = Convert.ToDouble(data_con.Rows[i].Cells[2].Value .ToString());
                     cbf_total = cajas2 * cbf_total ;
                     Consultar.M2ValidationActualizacion(txtlocalizacion.Text, GlobalVar.nombre_user, Convert.ToInt32(cbf_total), lote, cajas, txtempalme.Text, GlobalVar.Compania);                   
                 }
                 MessageBox.Show("Empalme correcto. Dado de alta en " + txtlocalizacion.Text  ,"CORRECTO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                 printpacking ((object)sender, (EventArgs)e);
                 data_con.Rows.Clear();
                 txtlocalizacion.Text = "";
                     txtempalme.Text="";
                     txtlote.Text = "";
                     txtqty.Text  = "";
                     groupBox2.Enabled = true;
                     groupBox1.Visible = false;
                     txtlocalizacion.Focus();
             }
             else
             {
                 // No hacer nada , lote rechazado
                 MessageBox .Show ("Empalme incorrecto, verifique","DISCREPANCIA",MessageBoxButtons.OK,MessageBoxIcon.Stop  );
             }

           }
         private void get_empalme_size(Object sender, EventArgs e)
         {
            Consultar.M2ValidacionTotal(ref Totalitem, txtempalme.Text, GlobalVar.Compania);
            items_empalme = (Totalitem);
        }
        
         private void txtqty_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (e.KeyChar == (Char)Keys.Enter)
             {
                 btn_add_Click((object)sender, (EventArgs)e);
             }
         }

         private void txtlote_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (e.KeyChar == (Char)Keys.Enter)
             {
                 txtqty.Focus();
             }
         }
         private void button2_Click(object sender, EventArgs e)
         {
            if (txtlocalizacion.Text == "")
            {
                MessageBox.Show("Localizacion no puede ser Vacio");
            }
            else
            {
                groupBox2.Enabled = false;
                groupBox1.Visible = true;
                txtlote.Focus();
            }
        }
         
         private void txtlocalizacion_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (e.KeyChar == (Char)Keys.Enter)
             {
                 txtempalme.Focus();
             }
         }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtempalme_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (Char.IsNumber(e.KeyChar) || e.KeyChar == '\b')
             {
                 e.Handled = false;
             }
             else
             {
                 e.Handled = true;
             }
             if (e.KeyChar == (Char)Keys.Enter)
             {
                 button2_Click ((object)sender, (EventArgs)e);
             }
         }

         private void M2_Validation_Load_1(object sender, EventArgs e)
         {

             toolStripStatusLabel1.Text = "Usuario=  " + GlobalVar.usuario;
         }
       
 }


}
