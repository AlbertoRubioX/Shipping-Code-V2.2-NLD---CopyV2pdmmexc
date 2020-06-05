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
    
    
    
    public partial class LogIn : Form
    {


        Datos Consultar = new Datos();
    
        public LogIn()
        {
            InitializeComponent();
        }

        private void LlenarComboCompania()
        {
            this.cbxcompania.DataSource = this.Consultar.Compania("T");
            this.cbxcompania.ValueMember = "getId";
            this.cbxcompania.DisplayMember = "getName";
            this.cbxcompania.SelectedIndex = 0;
        }


        private void btnentrar_Click(object sender, EventArgs e)
        {
            if ((txtuser.Text == "") || (txtpass.Text == ""))
            {
                MessageBox.Show("Favor de llenar informacion completa", "Revisar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                verificar_acceso((object)sender, (EventArgs)e);
            }
        }
        public void verificar_acceso(object sender, EventArgs e)
        {
            this.Consultar.Uservalidation(this.txtuser.Text, this.txtpass.Text, Convert.ToInt32(this.cbxcompania.SelectedValue));
            if (GlobalVar.User_Check == "Y")
            {
                Menu menu1 = new Menu();
                menu1.Show();
                this.Hide();    
            }

        }

        private void LogIn_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void LogIn_Load(object sender, EventArgs e)
        {
            this.txtuser.Focus();
            this.LlenarComboCompania();
        }

        private void txtpass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                btnentrar_Click((object)sender, (EventArgs)e);
            }
            else
            {
                
            }
        }

        private void txtuser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar) || e.KeyChar == '\b') 
            {
                e.Handled = false;
            }
            else
            {e.Handled =true  ;
            }
        }

        private void txtuser_TextChanged(object sender, EventArgs e)
        {
            if (txtuser.Text == "")
            {
                lbluser.Visible = true;
            }
            else
            {
                lbluser.Visible = false ;
            }
        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {
            if (txtpass.Text == "")
            {
                lblcontra.Visible = true;
            }
            else
            {
                lblcontra.Visible = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
