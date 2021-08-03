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
    public partial class solicitar_password : Form
    {
        Datos Consultar = new Datos();
        public string _Validacion;
        public bool _Supervisor = false;
        public solicitar_password()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _Validacion = txtpos.Text.ToString();
            if (_Validacion.Length == 0)
                txtpos.Focus();
            else
            {
                if (Consultar.M3password(_Validacion) == false)
                {
                    MessageBox.Show("Contraseña Incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtpos.Text = "";
                    txtpos.Focus();
                }
                else
                {
                    _Supervisor = true;
                    this.Close();

                }
            }
            
        }
        private void solicitar_password_Load(object sender, EventArgs e)
        {
            txtpos.Focus();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
