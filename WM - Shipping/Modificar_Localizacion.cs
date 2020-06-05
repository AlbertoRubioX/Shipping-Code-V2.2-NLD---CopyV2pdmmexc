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
    public partial class Modificar_Localizacion : Form
    {
        Datos Consultar = new Datos();
        string existe1;
        public Modificar_Localizacion()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (txtdestino.Text == "E1" || txtdestino.Text == "E2" || txtdestino.Text == "S1" || txtdestino.Text == "S2" || txtdestino.Text == "S3" || txtdestino.Text == "S4" || txtdestino.Text == "S5"
                || txtdestino.Text == "S6" || txtdestino.Text == "S7" || txtdestino.Text == "S8" || txtdestino.Text == "S9" || txtdestino.Text == "S10" || txtdestino.Text == "S11" || txtdestino.Text == "S12" || txtdestino.Text == "S13" || txtdestino.Text == "S14" || txtdestino.Text == "S15" || txtdestino.Text == "S16" || txtdestino.Text =="ADUANA")
            {
                if (txtdestino.Text  == txtorigen.Text)
                {
                    MessageBox.Show("Destino y Origen deben ser distintos para poder realizar la modificación" , "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                { 
                    
                    if (checkBox1.Checked==true)
                    {
                        if(Consultar.ModificarLocEmpalme(txtlote.Text, txtorigen.Text) == true)
                        {
                            existe1 = "Y";
                        }
                        else
                        {
                            existe1 = "N";
                        }
                    }
                    else
                    {
                        if(Consultar.ModificarLocLote(txtlote.Text, txtorigen.Text, Convert.ToInt32(txt1.Text), Convert.ToInt32(txt2.Text)) == true)
                        {
                            existe1 = "Y";
                        }
                        else
                        {
                            existe1 = "N";
                        }
                    }


                    if (existe1 == "Y")
                    {
                       

                        if (checkBox1.Checked == false)
                        {
                            Consultar.ModificarLocLoteAct(txtdestino.Text, txtlote.Text, txtorigen.Text, Convert.ToInt32(txt1.Text), Convert.ToInt32(txt2.Text));
                            resultado();
                        }
                        else
                        {
                            Consultar.ModificarLocEmpalmeAct(txtdestino.Text, txtlote.Text, txtorigen.Text);
                            resultado();
                        }

                       
                    }
                    else
                    {
                        MessageBox.Show("No se puede realizar la modificacion" + Environment .NewLine  + "Lote/Empalme no esta localizado en la rampa indicada o lote ya esta cargado en caja trailer", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
 
                }
            }
            else
            {
                MessageBox.Show("Introduzca valor de rampa/localizacion destino válida" + Environment.NewLine + "( Locaciones de 'S1' a 'S16' o 'Empalmes (E1/E2)' )", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation );
            }

        }

        private void resultado()
        {
            MessageBox.Show("Modificacion realizada", "Correcto", MessageBoxButtons.OK, MessageBoxIcon.Information);
            txt1.Text = "";
            txt2.Text = "";
            txtlote.Text = "";
            txtorigen.Text = "";
            txtdestino.Text = "";
            checkBox1.Checked = false;
        }




        private void Modificar_Localizacion_Load(object sender, EventArgs e)
        {

            toolStripStatusLabel1.Text = "Usuario=  " + GlobalVar.usuario;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                label1.Text = "Empalme :";
                label1.Location = new System.Drawing.Point(72, 73);
                label4.Visible = false;
                label5.Visible = false;
                txt1.Visible = false;
                txt2.Visible = false;
                txt1.Text = "";
                txt2.Text = "";
            }
            else
            {
                label1.Text = "Lote :";
                label1.Location = new System.Drawing.Point(106, 73);
                label4.Visible = true;
                label5.Visible = true ;
                txt1.Visible = true ;
                txt2.Visible = true ;
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
