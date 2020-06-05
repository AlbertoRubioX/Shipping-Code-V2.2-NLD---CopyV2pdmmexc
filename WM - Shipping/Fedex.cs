using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Fedex : Form
    {
        public Fedex()
        {
            InitializeComponent();
        }
        Datos Consultar = new Datos();
        private int qtys;
        private int ttarimas;
        private int idemb;
        private int resta;
        private string envio;
        private string qtyreq;
        private string wo;
        private string trays;
        string pn, lote, brach, total, tipo, embarcado;
        int idcarga, idemba,posi;


        private void btnncarga_Click(object sender, EventArgs e)
        {
            CrearCargaFedex fedex = new CrearCargaFedex();
            this.Visible = false;
            fedex.ShowDialog(this);
            fedex.Dispose();
            this.Visible = true;
        }


        private void grid()
        {
            Consultar.IdCargaFedex(txtId.Text);
            dtvgrid.DataSource = Consultar.ObtenerFedexID(txtId.Text);
            dtvgrid.AutoResizeColumns();
            dtvgrid.ForeColor = Color.Black;
            dtvgrid.Font = new Font("Microsoft Sans Serif", 9f);
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            if (txtposicion.Text == "")
            {
                MessageBox.Show("Introducir Posicion a buscar", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (Consultar.VerificacionPosicion(txtId.Text, txtposicion.Text))
            {
                MessageBox.Show("Posicion ya ocupada", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                resta = Convert.ToInt32(txtrestante.Text) - Convert.ToInt32(txtcantidad.Text);
                
                 if (Convert.ToInt32(txtcantidad.Text) < Convert.ToInt32(qtyreq))
                {
                    MessageBox.Show("Cantidad no puede ser Menor a la requerida", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (Convert.ToInt32(txtcantidad.Text) > Convert.ToInt32(qtyreq))
                {
                    MessageBox.Show("Cantidad no puede ser Mayor a la requerida", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                 else if (Convert.ToInt32(qtyreq) == Convert.ToInt32(txtcantidad.Text))
                {
                    Consultar.InvFedex(txtId.Text, txtlote.Text, wo, trays, txttarima1.Text, txttarima2.Text, txtcantidad.Text, envio, txtposicion.Text, GlobalVar.Compania);
                    Consultar.updatefedex(txtcantidad.Text, idemb, txtId.Text);
                    Consultar.updateinventario(resta, txttarima1.Text, txtlote.Text);
                    Consultar.updatefedexCaja(txtId.Text);
                    Limpiar();
                    grid();
                }
                else
                {
                    MessageBox.Show("Checar Cantidades", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void Limpiar()
        {
            txtenvio.Text = "";
            txtlote.Text = "";
            txtposicion.Text = "";
            txtrestante.Text = "";
            txttarima1.Text = "";
            txttarima2.Text = "";
            txtrestante.Text = "";
            txtcantidad.Text = "";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtidembarque.Enabled = true;
            txtidembarque.Text = "";
            txtlotefedex.Text = "";
            txtoidcarga.Text = "";
            txtpartnumber.Text = "";
            txttotal.Text = "";
            txtbrach.Text = "";
            txtship.Text = "";
            txtcargado.Text = "0";
            txtposition.Text = "";
            txtnlote.Text = "";
            txtnp.Text = "";
            txtretorno.Text = "";
            
        }

        private void btnGuardarAct_Click(object sender, EventArgs e)
        {
            Consultar.updatefedexmapaFinal(txtship.Text, txtnp.Text, txtnlote.Text, txtbrach.Text, txttotal.Text,txtcargado.Text, Convert.ToInt32(txtidembarque.Text));
            Consultar.updatefedexinventarioreturn(txtlotefedex.Text, txtretorno.Text, txtposition.Text);
            btnLimpiar_Click(sender, e);
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Introducir lote a buscar", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else if (Consultar.VerificacionEmbarcado(txtId.Text))
            {
                MessageBox.Show("Caja ya Emebarcada,Correrer reporte", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                grid();
            }
        }



      

        private void btnCerrarcarga_Click(object sender, EventArgs e)
        {
            Consultar.updatefedexCaja(this.txtId.Text);
            txtId.Text = "";
            Limpiar();
            dtvgrid.ClearSelection();
        }

        private void txttarima1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Consultar.FedexLote(ref idemb, txtlote.Text, ref ttarimas, txttarima1.Text, ref qtys, ref envio, ref qtyreq, ref wo, ref trays);
                txttarima2.Text = Convert.ToString(ttarimas);
                txtrestante.Text = Convert.ToString(qtys);
                txtenvio.Text = this.envio;
                txtcantidad.Text = Convert.ToString(qtyreq);
            }
        }

        private void Fedex_Load(object sender, EventArgs e)
        {
            txtId.Focus();
            if (GlobalVar.n_acceso == 1)
            {
                Actualizar.Visible = true;
                Actualizar.Enabled = true;
            }
            else
            {
                Actualizar.Visible = false;
                Actualizar.Enabled = false;
            }
               
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Consultar.ObtenerIdembarqueFedexinv(ref idemba,ref  posi ,ref pn, ref lote, ref brach, ref total, ref tipo, ref idcarga, ref embarcado, txtidembarque.Text);

            txtbrach.Text = brach;
            txtlotefedex.Text = lote;
            txtpartnumber.Text = pn;
            txtship.Text = tipo;
            txttotal.Text = total;
            txtoidcarga.Text = Convert.ToString(idcarga);
            txtposition.Text = Convert.ToString(posi);
            txtcargado.Text = "0";
            txtretorno.Text=  embarcado;
            txtidembarque.Enabled = false;
        }
    }
}
