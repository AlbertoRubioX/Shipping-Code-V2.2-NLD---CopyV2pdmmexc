using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections;

namespace WindowsFormsApplication1
{
    public partial class M4_DockDetail : Form
    {
        Datos Consultar = new Datos();
        string id_cargaid, ciclo, destino1, lote1, num11, num12, qty1, pos_caja, empal,lblcargas, lblciclos, lbldestinos, lblcajas;
        ArrayList pos_cajas = new ArrayList();
        ArrayList lotes = new ArrayList();
        ArrayList num11s = new ArrayList();
        ArrayList num12s = new ArrayList();
        ArrayList qty1s = new ArrayList();
        ArrayList empals = new ArrayList();

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        public M4_DockDetail()
        {
            InitializeComponent();
        }

        private void M4_DockDetail_Load(object sender, EventArgs e)
        {
            //algonzalez - 06/03
            if (GlobalVar.Compania == 110)
                lbl_carga.Text = M4_Layout.carga_porcent;
            else
            {
                if (GlobalVar.Compania == 686)
                    lbl_carga.Text = M4_LayoutMXC.carga_porcent;
            }
            //algonzalez - 06/03

            cargar_enproceso((object) sender, (EventArgs) e);
        }
        private void cargar_enproceso(object sender, EventArgs e)
        {
            //algonzalez - 06/03
            int iRampaID = 0;
            if (GlobalVar.Compania == 110)
                iRampaID = M4_Layout.rampa_d;
            if (GlobalVar.Compania == 686)
                iRampaID = M4_LayoutMXC.rampa_d;
            //algonzalez - 06/03

            Consultar.M4DockDetail(ref lblcargas, ref lblciclos, ref lbldestinos, ref lblcajas, ref pos_cajas, ref lotes, ref num11s, ref num12s, ref qty1s, ref empals, iRampaID);
            // CONSULTA GUARDADA EN ACCESS CHECAR

            lblcarga.Text = lblcargas;
            lblciclo.Text = lblciclos;
            lbldestino.Text = lbldestinos;
            lblcaja.Text = lblcajas;
            int total = (pos_cajas.Count - 1);

            if (total >= 0 )
            {

                for (int i = 0; i <= total; i++)
                {
                    pos_caja += pos_cajas[i];
                    lote1 += lotes[i];
                    num11 += num11s[i];
                    num12 += num12s[i];
                    qty1 += qty1s[i];
                    empal += empals[i];

                    if (empal == "0" || empal == "")
                    {
                        switch (pos_caja)
                        {
                            case "1":
                                pos1.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "2":
                                pos2.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "3":
                                pos3.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "4":
                                pos4.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "5":
                                pos5.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "6":
                                pos6.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "7":
                                pos7.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "8":
                                pos8.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "9":
                                pos9.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "10":
                                pos10.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "11":
                                pos11.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "12":
                                pos12.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "13":
                                pos13.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "14":
                                pos14.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "15":
                                pos15.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "16":
                                pos16.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "17":
                                pos17.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "18":
                                pos18.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "19":
                                pos19.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "20":
                                pos20.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "21":
                                pos21.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "22":
                                pos22.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "23":
                                pos23.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "24":
                                pos24.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "25":
                                pos25.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "26":
                                pos26.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "27":
                                pos27.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "28":
                                pos28.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "29":
                                pos29.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                            case "30":
                                pos30.Text = lote1 + Environment.NewLine + num11 + " / " + num12 + Environment.NewLine + qty1 + " cs";
                                break;
                        }

                        pos_caja = "";
                        lote1 = "";
                        num11 = "";
                        num12 = "";
                        qty1 = "";
                        empal = "";

                    }
                    else
                    {
                        switch (pos_caja)
                        {
                            case "1":
                                pos1.Text = "Empalme # " + empal;
                                break;
                            case "2":
                                pos2.Text = "Empalme # " + empal;
                                break;
                            case "3":
                                pos3.Text = "Empalme # " + empal;
                                break;
                            case "4":
                                pos4.Text = "Empalme # " + empal;
                                break;
                            case "5":
                                pos5.Text = "Empalme # " + empal;
                                break;
                            case "6":
                                pos6.Text = "Empalme # " + empal;
                                break;
                            case "7":
                                pos7.Text = "Empalme # " + empal;
                                break;
                            case "8":
                                pos8.Text = "Empalme # " + empal;
                                break;
                            case "9":
                                pos9.Text = "Empalme # " + empal;
                                break;
                            case "10":
                                pos10.Text = "Empalme # " + empal;
                                break;
                            case "11":
                                pos11.Text = "Empalme # " + empal;
                                break;
                            case "12":
                                pos12.Text = "Empalme # " + empal;
                                break;
                            case "13":
                                pos13.Text = "Empalme # " + empal;
                                break;
                            case "14":
                                pos14.Text = "Empalme # " + empal;
                                break;
                            case "15":
                                pos15.Text = "Empalme # " + empal;
                                break;
                            case "16":
                                pos16.Text = "Empalme # " + empal;
                                break;
                            case "17":
                                pos17.Text = "Empalme # " + empal;
                                break;
                            case "18":
                                pos18.Text = "Empalme # " + empal;
                                break;
                            case "19":
                                pos19.Text = "Empalme # " + empal;
                                break;
                            case "20":
                                pos20.Text = "Empalme # " + empal;
                                break;
                            case "21":
                                pos21.Text = "Empalme # " + empal;
                                break;
                            case "22":
                                pos22.Text = "Empalme # " + empal;
                                break;
                            case "23":
                                pos23.Text = "Empalme # " + empal;
                                break;
                            case "24":
                                pos24.Text = "Empalme # " + empal;
                                break;
                            case "25":
                                pos25.Text = "Empalme # " + empal;
                                break;
                            case "26":
                                pos26.Text = "Empalme # " + empal;
                                break;
                            case "27":
                                pos27.Text = "Empalme # " + empal;
                                break;
                            case "28":
                                pos28.Text = "Empalme # " + empal;
                                break;
                            case "29":
                                pos29.Text = "Empalme # " + empal;
                                break;
                            case "30":
                                pos30.Text = "Empalme # " + empal;
                                break;
                        }
                        pos_caja = "";
                        lote1 = "";
                        num11 = "";
                        num12 = "";
                        qty1 = "";
                        empal = "";
                    }
                }

            }
            else

            {
               MessageBox.Show("Caja en rampa no tiene lotes cargados", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
    }
}

