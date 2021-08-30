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
    public partial class crear_carga : Form
    {
        public crear_carga()
        {
            InitializeComponent();
        }
        Datos Consultar = new Datos();
        
        public static string grabada="N", num_carga1, numero_caja1, ciclo,destino,notas,loc;
        public string rampa, rampa_vacia1 ="Y",_NoCaja;
        public int idcarga;
        public bool _bEdit =false;

       

        private void crear_carga_Load(object sender, EventArgs e)
        {
            LlenarComboRampa();
            LlenarComboDestino();
            LlenarComboCiclo();

            if (_bEdit)
            {
                lblCarga.Visible = true;
                txtcarga.Visible = true;
                btnBuscar.Visible = true;

                cbrampa.Enabled = false;
                cbdestino.Enabled = false;
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {

            if (txtcarga.Text == "")
            {
                MessageBox.Show("Introduzca el numero de carga", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtcarga.Focus();
            }
            else
            {
                txtcaja1.Clear();
                cbrampa.SelectedIndex = 0;
                cbciclo.SelectedIndex = 0;
                cbdestino.SelectedIndex = 0;
                txtnotas.Clear();

                if (Consultar.M3carganota(ref _NoCaja, ref destino, ref ciclo, ref rampa, ref notas, ref loc, txtcarga.Text.ToString()) == true)
                {
                    txtcaja1.Text = _NoCaja;
                    cbciclo.SelectedValue = ciclo;
                    cbdestino.SelectedValue = destino;
                    cbrampa.SelectedValue = rampa;
                    txtnotas.Text = notas;

                }
                else
                {
                    MessageBox.Show("Carga no disponible para edición", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtcarga.Clear();
                    txtcarga.Focus();
                }
            }
        }
        private void rampa_vacia(object sender, EventArgs e)
        {
            if (!Consultar.VerificacionRampa(Convert.ToString(cbrampa.SelectedValue), GlobalVar.Compania))
            {
                this.rampa_vacia1 = "Y";
            }
            else
            {
                this.rampa_vacia1 = "N";
                MessageBox.Show("Rampa Ocupada", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
        private void LlenarComboCiclo()
        {
            this.cbciclo.DataSource = this.Consultar.LlenarCiclo("T");
            this.cbciclo.ValueMember = "getId";
            this.cbciclo.DisplayMember = "getName";
            this.cbciclo.SelectedIndex = 0;
        }

        private void LlenarComboDestino()
        {
            this.cbdestino.DataSource = this.Consultar.LlenarDestino("T");
            this.cbdestino.ValueMember = "getId";
            this.cbdestino.DisplayMember = "getName";
            this.cbdestino.SelectedIndex = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        private void LlenarComboRampa()
        {
            this.cbrampa.DataSource = this.Consultar.LlenarRampa("T");
            this.cbrampa.ValueMember = "getId";
            this.cbrampa.DisplayMember = "getName";
            this.cbrampa.SelectedIndex = 0;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            int num_c;

            if (((txtcaja1.Text == "") || (Convert.ToString(cbciclo.SelectedValue) == "0")) || (Convert.ToString(cbdestino.SelectedValue) == "0"))
            {
                MessageBox.Show("Llenar informacion completa ( Caja/Destino/Ciclo/Rampa)", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (_bEdit)
                {
                    int iCarga = int.Parse(txtcarga.Text.ToString());
                    if(Consultar.ActualizaCaja(txtcaja1.Text, Convert.ToString(cbciclo.SelectedValue), txtnotas.Text, iCarga) > 0)
                    {
                        MessageBox.Show("Carga " + txtcarga.Text.ToString() + " Actualizada", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        numero_caja1 = txtcaja1.Text;

                        Verificacion_de_caja verificar1 = new Verificacion_de_caja();
                        verificar1._NoCaja = txtcaja1.Text.ToString();
                        verificar1.idcarga = iCarga;

                        verificar1.ShowDialog();

                        this.Close();
                    }
                }
                else
                {
                    rampa_vacia(sender, e);
                    if (rampa_vacia1 == "Y")
                    {
                        Consultar.InsertCaja(txtcaja1.Text, Convert.ToString(cbciclo.SelectedValue), Convert.ToString(cbdestino.SelectedValue), Convert.ToInt32(cbrampa.SelectedValue), txtnotas.Text, GlobalVar.Compania);
                        Consultar.ObtenerIdCarga(txtcaja1.Text, ref idcarga, GlobalVar.Compania);
                        num_c = idcarga;
                        if (num_c != 0)
                        {
                            M3_Map.numero_carga1 = idcarga.ToString();
                            grabada = "Y";
                            MessageBox.Show("Carga Agregada, ID_CARGA =" + M3_Map.numero_carga1, "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            numero_caja1 = txtcaja1.Text;
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Rampa seleccionada esta actualemte con carga en proceso", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
         
        }

        private void cbrampa_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbrampa.Text)
            {
                case "24" :
                case "25":
                case "26":
                    cbciclo .Text = "NO-ESTERIL";
                    cbdestino.Text = "LAREDO";
                    txtnotas.Text = "";
                    break;
                case "27":
                    cbciclo .Text = "NO-ESTERIL";
                    cbdestino.Text = "LAREDO";
                    txtnotas .Text = "B26";
                    break;
                case "28":
                    cbciclo .Text = "NO-ESTERIL";
                    cbdestino.Text = "LAREDO";
                    txtnotas .Text = "B09";
                    break;
                case "29":
                    cbciclo .Text = "NO-ESTERIL";
                    cbdestino.Text = "LAREDO";
                    txtnotas .Text = "B43";
                    break;
                case "30":
                    cbciclo .Text = "NO-ESTERIL";
                    cbdestino.Text = "LAREDO";
                    txtnotas .Text = "B89";
                    break;
                case "31":
                    cbciclo .Text = "NO-ESTERIL";
                    cbdestino.Text = "LAREDO";
                    txtnotas .Text = "B32";
                    break;
                case "32":
                    cbciclo .Text = "NO-ESTERIL";
                    cbdestino.Text = "LAREDO";
                    txtnotas .Text = "B33";
                    break;
                case "33":
                    cbciclo .Text = "NO-ESTERIL";
                    cbdestino.Text = "LAREDO";
                    txtnotas .Text = "B28";
                    break;
                case "34":
                    cbciclo .Text = "NO-ESTERIL";
                    cbdestino.Text = "LAREDO";
                    txtnotas .Text = "B75";
                    break;
                case "35":
                case "36":
                    cbciclo .Text = "LR";
                    cbdestino.Text = "LAREDO";
                    txtnotas.Text = "CMP";
                    break;
                case "37":
                    cbciclo .Text = "LR";
                    cbdestino.Text = "JACKSON";
                    txtnotas .Text = "JACKSON MR";
                    break;
                case "38":
                    cbciclo .Text = "THD";
                    cbdestino.Text = "LAREDO";
                    txtnotas.Text = "";
                    break;
                case "39":
                     cbciclo .Text = "LHM";
                    cbdestino.Text = "LAREDO";
                    txtnotas.Text = "";
                    break;
                case "40":
                     cbciclo .Text = "NPS";
                    cbdestino.Text = "NPS";
                    txtnotas.Text = "";
                    break;
                case "41":
                     cbciclo .Text = "LR";
                    cbdestino.Text = "LAREDO-HOT!";
                    txtnotas.Text = "";
                    break;
                case "42":
                case "43":
                case "44":
                case "45":
                case "46":
                    cbciclo.Text = "LR";
                    cbdestino.Text = "LAREDO";
                    txtnotas.Text = "";
                    break;

               
                 
            }
        }

       
    }
}
