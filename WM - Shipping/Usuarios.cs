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
    public partial class Usuarios : Form
    {
        Datos Consultar = new Datos();
        public Usuarios()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void alta_CheckedChanged(object sender, EventArgs e)
        {
            if (alta.Checked == true)
            {
                gpalta.Visible = true;
                gbbaja.Visible = false;
                txtempleado2.Text = "";
            }
            else
            {
                gpalta.Visible = false;
                gbbaja.Visible = true;
            }
        }

        private void Modificacion_CheckedChanged(object sender, EventArgs e)
        {
            if (Modificacion.Checked == true )
            {
                gpalta.Visible = false ;
                gbbaja.Visible = true ;
                txtempleado.Text = "";
                txtcontra1.Text = "";
                txtcontra2.Text = "";
                txtnombre.Text = "";
                cbacceso.Text = "1";
            }
            else
            {
                gpalta.Visible = true ;
                gbbaja.Visible = false ;
            }
        }

        private void btn_ok_alta_Click(object sender, EventArgs e)
        {
            int num1 = 0;

            if (((this.txtempleado.Text == "") || ((this.txtnombre.Text == "") || (this.txtcontra1.Text == ""))) || (this.txtcontra2.Text == ""))
            {
                num1 = 1;
            }
            //else
            //{
            //    num1 = (Convert.ToInt32(cbacceso.SelectedValue) == "");
            //}
            if (num1 != 0)
            {
                MessageBox.Show("Introducir datos completos", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (txtcontra1.Text != txtcontra2.Text)
            {
                MessageBox.Show("Contrasenas deben coincidir", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                this.Consultar.NuevoUsuario(Convert.ToInt32(txtempleado.Text), txtnombre.Text, txtcontra1.Text, Convert.ToInt32(cbacceso.SelectedValue), GlobalVar.Compania);
                MessageBox.Show("Empleado dado de alta", "Agregado", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.txtempleado.Text = "";
                this.txtnombre.Text = "";
                this.txtcontra1.Text = "";
                this.txtcontra2.Text = "";
                this.cbacceso.Text = "";
            }

        }

        private void btn_ok_baja_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Esta seguro que desea dar de baja al usuario " + this.txtempleado2.Text + " ?", "Verificar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Consultar.EliminarUsuario(Convert.ToInt32(this.txtempleado2.Text), GlobalVar.Compania);
                MessageBox.Show("Usuario dado de baja", "Baja", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.txtempleado2.Text = "";
            }


        }

        private void Usuarios_Load(object sender, EventArgs e)
        {

            toolStripStatusLabel1.Text = "Usuario=  " + GlobalVar.usuario;
            NivelUsuario();
        }
        private void NivelUsuario()
        {
            this.cbacceso.DataSource = this.Consultar.NivelUsuario("T");
            this.cbacceso.ValueMember = "getId";
            this.cbacceso.DisplayMember = "getName";
            this.cbacceso.SelectedIndex = 0;
        }


    }

}
