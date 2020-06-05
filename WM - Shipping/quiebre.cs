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
    public partial class quiebre : Form
    {

        private Datos Consultar = new Datos();
        public int cajas;
        public int parciales;
        public string lote;
        public string WO;
        public string tray;
        public string destino;
        public string ciclo;
        public string user_recibe;
        public string fecha_recibe;
        public string hora_recibe;
        public string localizacion;
        public string lot;
        public string wos;
        public string trays;
        public string dest;
        public string ciclos;
        public string usuario;
        public string fdate;
        public string loc;
        public quiebre()
            
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            txt_p1.Text ="0";
            txt_p2.Text ="0";
            txt_p3.Text ="0";
            txt_p4.Text ="0";
            txt_p5.Text ="0";
            switch (cbparciales.Text)
            { 
               
                case "2" :
                    lbl1.Visible = true  ;
                    lbl2.Visible = true  ;
                    lbl3.Visible = false ;
                    lbl4.Visible = false ;
                    lbl5.Visible = false ;
                    txt_p1.Visible = true  ;
                    txt_p2.Visible = true  ;
                    txt_p3.Visible = false ;
                    txt_p4.Visible = false ;
                    txt_p5.Visible = false ;
                    parciales = 2;
                    break;
                case "3" :
                    lbl1.Visible = true  ;
                    lbl2.Visible = true  ;
                    lbl3.Visible = true  ;
                    lbl4.Visible = false ;
                    lbl5.Visible = false ;
                    txt_p1.Visible = true;
                    txt_p2.Visible = true ;
                    txt_p3.Visible = true  ;
                    txt_p4.Visible = false ;
                    txt_p5.Visible = false ;
                    parciales = 3;
                    break;
                case "4" :
                    lbl1.Visible = true  ;
                    lbl2.Visible = true  ;
                    lbl3.Visible = true  ;
                    lbl4.Visible = true  ;
                    lbl5.Visible = false ;
                    txt_p1.Visible = true ;
                    txt_p2.Visible = true ;
                    txt_p3.Visible = true ;
                    txt_p4.Visible = true ;
                    txt_p5.Visible = false ;
                    parciales = 4;
                    break;
                case "5" :
                    lbl1.Visible = true  ;
                    lbl2.Visible = true  ;
                    lbl3.Visible = true  ;
                    lbl4.Visible = true  ;
                    lbl5.Visible = true  ;
                    txt_p1.Visible = true ;
                    txt_p2.Visible = true ;
                    txt_p3.Visible = true ;
                    txt_p4.Visible = true ;
                    txt_p5.Visible = true ;
                    parciales = 5;
                    break;
                default :
                     lbl1.Visible = false ;
                    lbl2.Visible = false ;
                    lbl3.Visible = false ;
                    lbl4.Visible = false ;
                    lbl5.Visible = false ;
                    txt_p1.Visible = false ;
                    txt_p2.Visible = false ;
                    txt_p3.Visible = false ;
                    txt_p4.Visible = false ;
                    txt_p5.Visible = false ;
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Consultar.Quiebre(ref lot, ref wos, ref trays, ref dest, ref ciclos, ref usuario, ref fdate, ref loc, txtlote.Text, txttarima1.Text, txttarima2.Text, GlobalVar.Compania) == true)
            {
                lote = lot;
                WO = wos;
                tray = trays;
                destino = dest;
                ciclo = ciclos;
                user_recibe = usuario;
                localizacion = loc;
                groupBox1.Visible = true;
            }
            else
            {
                MessageBox.Show("No existe la tarima indicada en area de empalmes o ya fue dividida en parciales", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
               
            }

        }

        private void quiebre_Load(object sender, EventArgs e)
        {

            toolStripStatusLabel1.Text = "Usuario=  " + GlobalVar.usuario;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int i = 0;

            Consultar.Quiebreupdate(txt_p1.Text, txtlote.Text, txttarima1.Text, txttarima2.Text, GlobalVar.Compania);
            
                while (i < parciales - 1)
                    {
                        switch (i)
                        {
                            case 0:
                                Consultar.InsertarRegQuiebre('0', lote, Convert.ToInt32(WO), tray, Convert.ToInt32(txttarima1.Text), Convert.ToInt32(txttarima2.Text), Convert.ToInt32(txt_p2.Text), localizacion, destino, ciclo, GlobalVar.nombre_user, GlobalVar.Compania);
                                break;

                            case 1:
                                Consultar.InsertarRegQuiebre('1', lote, Convert.ToInt32(WO), tray, Convert.ToInt32(txttarima1.Text), Convert.ToInt32(txttarima2.Text), Convert.ToInt32(txt_p3.Text), localizacion, destino, ciclo, GlobalVar.nombre_user, GlobalVar.Compania);
                                break;

                            case 2:
                                Consultar.InsertarRegQuiebre('2', lote, Convert.ToInt32(WO), tray, Convert.ToInt32(txttarima1.Text), Convert.ToInt32(txttarima2.Text), Convert.ToInt32(txt_p4.Text), localizacion, destino, ciclo, GlobalVar.nombre_user, GlobalVar.Compania);
                                break;

                            case 3:
                                Consultar.InsertarRegQuiebre('3', lote, Convert.ToInt32(WO), tray, Convert.ToInt32(txttarima1.Text), Convert.ToInt32(txttarima2.Text), Convert.ToInt32(txt_p5.Text), localizacion, destino, ciclo, GlobalVar.nombre_user, GlobalVar.Compania);
                                break;

                            default:
                                break;
                        }
                        i++;
                    }

                MessageBox.Show("Tarima dividida en parciales", "Listo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.lbl1.Visible = false;
                this.lbl2.Visible = false;
                this.lbl3.Visible = false;
                this.lbl4.Visible = false;
                this.lbl5.Visible = false;
                this.txt_p1.Visible = false;
                this.txt_p2.Visible = false;
                this.txt_p3.Visible = false;
                this.txt_p4.Visible = false;
                this.txt_p5.Visible = false;
                this.txtlote.Text = "";
                this.txttarima1.Text = "";
                this.txttarima2.Text = "";
                this.groupBox1.Visible = false;
            }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
