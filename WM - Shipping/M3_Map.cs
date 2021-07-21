using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Microsoft.VisualBasic;
using System.Net.Mail;
using Microsoft.Office.Interop;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Data.Odbc;
using System.Text.RegularExpressions;
using System.Collections;





namespace WindowsFormsApplication1
{
    public partial class M3_Map : Form
    {
        public static string  id_carga1,incompleto="N",message1="",numero_de_rampa,numero_carga1,reasignar="N",numero_loc;
        public string cargar1="Y", sqlrowcount, empalme, cajas_ch,name_txt,ciclo,destino1, correo="N", notas, loc, rampa, posicion1, producto, lote, cajas, tarimas, cargador, lote_1, localizacion_1, cajas1, lote1, cajas_prqoh1, conteo, totalt, num11, num12, qty1, pos_caja, empal, loct, pospdf, traypdf, lottpdf, qtypdf, tarimapdf, usuariopdf, Validacion;
        Datos Consultar = new Datos();
        public int errorsave,posicion, answer, envio,puerto;
        public double  cbf_n, total;
        public bool Cancel = false;
        public string server, smtp_user, mapa, discre;
        public Button btn;
        ArrayList lblcargas = new ArrayList();
        ArrayList lblciclos = new ArrayList();
        ArrayList lbldestinos = new ArrayList();
        ArrayList lblcajas = new ArrayList();
        ArrayList pos_cajas = new ArrayList();
        ArrayList lotes = new ArrayList();
        ArrayList num11s = new ArrayList();
        ArrayList num12s = new ArrayList();
        ArrayList qty1s = new ArrayList();
        ArrayList empals = new ArrayList();
        ArrayList pos = new ArrayList();
        ArrayList tray = new ArrayList();
        ArrayList lott = new ArrayList();
        ArrayList qty = new ArrayList();
        ArrayList tarima = new ArrayList();
        ArrayList usuario = new ArrayList();
        ArrayList lot = new ArrayList();
        ArrayList lotfs = new ArrayList();
        ArrayList carga = new ArrayList();
        ArrayList conteoM3 = new ArrayList();
        ArrayList lotM3 = new ArrayList();
        ArrayList totalM3 = new ArrayList();


        int year = DateTime.Now.Year;

        public M3_Map()
        {
            InitializeComponent();
        }

        private void txtlote_Leave_1(object sender, EventArgs e)
        {
            String lote1;
            if (txtlote.TextLength >= 9)
            {
                lote1 = txtlote.Text;
                if (lote1.Substring(8, 1) == "A" || lote1.Substring(8, 1) == "a" || lote1.Substring(8, 1) == "z" || lote1.Substring(8, 1) == "Z" || lote1.Substring(8, 1) == "b" || lote1.Substring(8, 1) == "B" || lote1.Substring(8, 1) == "c" || lote1.Substring(8, 1) == "C" || lote1.Substring(8, 1) == "x" || lote1.Substring(8, 1) == "X" || lote1.Substring(8, 1) == "y" || lote1.Substring(8, 1) == "Y")
                {

                    txtlote.Text = lote1.Substring(0, 9);
                    button1_Click((object)sender, (EventArgs)e);
                }
                else
                {
                    txtlote.Text = lote1.Substring(0, 8);
                    button1_Click((object)sender, (EventArgs)e);
                }
            }
            else if (txtlote.TextLength == 8)
            {
                button1_Click((object)sender, (EventArgs)e);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            if (txtlote.Text == "")
            {
                MessageBox.Show("Introduzca el numero de Lote", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            else
            {

                actualizar_cbf(sender, e);
                if (txtcaja.Text == "" || txtrampa.Text =="" || txtcarga.Text == "")
                {

                    MessageBox.Show("Favor de llenar datos de rampa y caja", "REVISAR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    cargar_enproceso(sender, e);
                    cargar_rampa(sender, e);
                }
            }
            if (txtlocalizacion.Text == "S5" || txtlocalizacion.Text == "S6" || txtlocalizacion.Text == "S8" || txtlocalizacion.Text == "S9" || txtlocalizacion.Text == "S11" || txtlocalizacion.Text == "S12" && GlobalVar.Compania == 110)
            {
            
                    pos29.Visible = true;
                    pos30.Visible = true;
             }
            else if(GlobalVar.Compania == 110)
            {
                    pos29.Visible = false;
                    pos30.Visible = false;
            }
            else if(GlobalVar.Compania ==686 )
            {
                pos29.Visible = true;
                pos30.Visible = true;
            }

        }

        private void cargar_rampa(object sender,EventArgs e)
        {
            grid1.DataSource = Consultar.M3Carga(txtlocalizacion.Text, txtlote.Text, Convert.ToInt32(txttarima.Text));
            grid1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.DisplayedCells);
            int filas = Consultar.M3Carga(txtlocalizacion.Text, txtlote.Text,Convert.ToInt32(txttarima.Text)).Rows.Count;
            checar_ciclos_grid(sender, e);
            object[] objArray1 = new object[] { "Localizacion #", txtlocalizacion.Text, " tiene ", filas, " items" };
            MessageBox.Show(string.Concat(objArray1), "Cargados", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void checar_ciclos_grid(object sender, EventArgs e)
        {
            string sCiclo = lblciclo.Text.ToString();
            string sDestino1 = lbldestino.Text.ToString();

            for (int i = 0; i< grid1.RowCount ; i ++ )
            {
                if (grid1.Rows[i].Cells[5].Value.ToString() != sCiclo || grid1.Rows[i].Cells[7].Value.ToString() != sDestino1 )
                {
                    grid1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    grid1.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                }
                else
                {
                    grid1.Rows[i].DefaultCellStyle.BackColor = Color.White ;
                    grid1.Rows[i].DefaultCellStyle.ForeColor = Color.Black;
                }

                    
            }
        }

       

        private void guardar_carga(object sender, EventArgs e)
        {
            if (txtcaja.Text == "")
            {
                MessageBox.Show("Debe introducir el numero de caja de la carga", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                errorsave = 1;
            }
            else
               
            {
                    if (grid1.Rows.Count >= 1)
                    {
                        errorsave = 0;
                        string hora = DateTime.Now.ToString("HH:mm");
                        string date1 = DateTime.Now.ToString("MM/dd/yy");
                        string tarima, total_tarimas, sql,id_tarima1;
                        tarima = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString();
                        total_tarimas = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString();
                        empalme = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[6].Value.ToString();
                        id_tarima1 = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[0].Value.ToString();
                    if (empalme == "" || empalme == "0")
                    {
                        cajas_ch = Microsoft.VisualBasic.Interaction.InputBox("Confime cantidad de cajas para Lote:" + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + " Tarima " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString(), "VERIFICAR");
                        CBF_Normal((object)sender, (MouseEventArgs)e);
                        Consultar.M3ModificarMapa(txtcarga.Text, posicion, Convert.ToInt32(cajas_ch), cbf_n, Convert.ToInt32(id_tarima1));
                    }
                    else
                    {
                        Consultar.M3ModificarMapas(txtcarga.Text, posicion, Convert.ToInt32(empalme));
                    }
                       
                    }
            }

        }

        private void actualizar_cbf(object sender, EventArgs e)
        {

            Consultar.M3sumaCbf(ref total, txtcarga.Text);
            double valor = 0;
            valor = (total);

            double porcentaje1;
            porcentaje1 = (valor / 3108) * 100;
            string porcentaje2 = Strings.Left(Convert.ToString(porcentaje1), 6);
            cbfporcentaje.Text = porcentaje2 + "%";
            cbfvalor.Text = Convert.ToString(valor);
            if (valor >= 3014)
            {
                cbfporcentaje.ForeColor = Color.Green;
                cbfvalor.ForeColor = Color.Green;
                rojo.Visible = false ;
                verde.Visible = true ;
                amarillo.Visible = false ;
                    

            }
            if (valor < 3014 & valor >= 2890.440)
            {
                cbfporcentaje.ForeColor = Color.Yellow;
                cbfvalor.ForeColor = Color.Yellow;
                rojo.Visible = false ;
                verde.Visible = false ;
                amarillo.Visible = true;
            }
            if (valor < 2890.44)
            {
                cbfporcentaje.ForeColor = Color.Red;
                cbfvalor.ForeColor = Color.Red;
                rojo.Visible = true ;
                verde.Visible = false ;
                amarillo.Visible = false;
            }

            label33.Visible = true;
            label36.Visible = true;
            cbfvalor.Visible = true;
            cbfporcentaje.Visible = true;

        }

        private void LimparArray()
        {
            id_carga1 = "";
            ciclo = "";
            destino1 = "";
            pos_caja = "";
            lote1 = "";
            num11 = "";
            num12 = "";
            qty1 = "";
            empal = "";
            lblcargas.Clear();
            pos_cajas.Clear();
            lott.Clear();
            lbldestinos.Clear();
            lblciclos.Clear();
            num11s.Clear();
            num12s.Clear();
            qty.Clear();
            empals.Clear();
        }


        private void cargar_enproceso(object sender, EventArgs e)
        {
            limpiar_textbox((object)sender, (EventArgs)e);
            LimparArray();

            Consultar.M3CargaenProceso(ref lblcargas, ref pos_cajas, ref lott, ref lbldestinos, ref lblciclos, ref num11s, ref num12s, ref qty, ref empals, txtcarga.Text);
            // CONSULTA GUARDADA EN ACCESS CHECAR
            int total = (lblcargas.Count - 1);

                if (total >= 0 )
                {
                    // If open case is found, loads in process will be uploaded on MAP

                    for(int i= 0; i <= total; i++)
                {
                    id_carga1 +=lblcargas[i];
                    ciclo += lblciclos[i];
                    destino1 += lbldestinos[i];
                    pos_caja += pos_cajas[i];
                    lote1 += lott[i];
                    num11 += num11s[i];
                    num12 += num12s[i];
                    qty1 += qty[i];
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
                                id_carga1 = "";
                                ciclo = "";
                                destino1 = "";
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
                                id_carga1 = "";
                                ciclo = "";
                                destino1 = "";
                                pos_caja = "";
                                lote1 = "";
                                num11 = "";
                                num12 = "";
                                qty1 = "";
                                empal = "";
                    }
                }
            } 
        }

        private void limpiar_textbox(object sender, EventArgs e)
        {

            pos1.Text = "";
            pos2.Text = "";
            pos3.Text = "";
            pos4.Text = "";
            pos5.Text = "";
            pos6.Text = "";
            pos7.Text = "";
            pos8.Text = "";
            pos9.Text = "";
            pos10.Text = "";
            pos11.Text = "";
            pos12.Text = "";
            pos13.Text = "";
            pos14.Text = "";
            pos15.Text = "";
            pos16.Text = "";
            pos17.Text = "";
            pos18.Text = "";
            pos19.Text = "";
            pos20.Text = "";
            pos21.Text = "";
            pos22.Text = "";
            pos23.Text = "";
            pos24.Text = "";
            pos25.Text = "";
            pos26.Text = "";
            pos27.Text = "";
            pos28.Text = "";
            pos29.Text = "";
            pos30.Text = "";
        }
        private void lotes_parciales(object sender, EventArgs e)
        {
            Consultar.M3ParcialesLote(ref lotfs, Convert.ToInt32(txtcarga.Text));

            int total = (lotfs.Count - 1);
            if(total > 0)

            {
                message1 = message1 + Environment.NewLine + "Tarimas sin cargar (parcial):";

                for(int i = 0; i <= total; i++)
                {
                    lote_1 += lotfs[i];
                    if((Consultar.M3Parciales(ref loct, lote_1, Convert.ToString(txtcarga.Text))) == true)
                    {
                        incompleto = "Y";
                        localizacion_1 = (loct);
                        message1 = message1 + Environment.NewLine + "Lote: " + lote_1 + ", hay tarimas en localizacion : " + localizacion_1;
                    }
                    else
                    {
                        incompleto = "N";
                    }
                    lote_1 = "";
                }
            }
        }


        private void lotes_completos(object sender, EventArgs e)
        {
            lotM3.Clear();
            conteoM3.Clear();
            totalM3.Clear();
            message1 = message1 + Environment.NewLine + Environment.NewLine + "Lotes Incompletos:";

            /*  string sql = "Select lote,total_tarimas as tarimas, count(lote) as conteo, tarimas-conteo as suma from inventory where id_carga=" + id_carga1 + " group by lote, total_tarimas having TOTAL_TARIMAS-COUNT(LOTE) > 0";*/


            if (Consultar.M3LoteCompletos(ref lotM3, ref conteoM3, ref totalM3, txtcarga.Text, GlobalVar.Compania) == true)
            {
                incompleto = "Y";
                int total = (lotM3.Count - 1);

                if (total >= 0)


                {
                    for (int i = 0; i <= total; i++)
                    {
                        lote += lotM3[i];
                        conteo += conteoM3[i];
                        totalt += totalM3[i];
                        message1 = message1 + Environment.NewLine + lote + "    Total Tarimas= " + totalt + " | Tarimas en Mapa= " + conteo;

                        lote = "";
                        conteo = "";
                        totalt = "";
                    }

                 
                }

            }
        }

        private void pos29_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos29.Text == "")
                        {
                            posicion = 29;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos29.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos24.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void pos30_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos30.Text == "")
                        {
                            posicion = 30;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos30.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos30.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void crear_mapa(object sender, EventArgs e)
        {
            pos.Clear();
            tray.Clear();
            lott.Clear();
            qty.Clear();
            tarima.Clear();
            usuario.Clear();

            if (GlobalVar.Compania == 110)
            {
                if (Consultar.M3CrearMapa(ref pos, ref tray, ref lott, ref qty, ref tarima, ref usuario, Convert.ToInt32(txtcarga.Text)))
                {
                    // CREANDO ARCHIVO PDF
                    Document doc = new Document(PageSize.LETTER);
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"\\Mexfp1\Medline\Dept. Recibo & Embarques\Mapas electronicos\Mapas\Mapassqltest\MAPA#" + txtcarga.Text + ".pdf", FileMode.Create));
                    doc.Open();
                    iTextSharp.text.Font _title = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 15, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    iTextSharp.text.Font _title2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    iTextSharp.text.Font _title3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 15, iTextSharp.text.Font.UNDERLINE, BaseColor.BLACK);
                    if (File.Exists(Path.Combine(Application.StartupPath, @"logo\logo_m1.png")))
                    {
                        iTextSharp.text.Image medlinelogo = iTextSharp.text.Image.GetInstance(Path.Combine(Application.StartupPath, @"logo\logo_m1.png"));
                        medlinelogo.BorderWidth = 0;
                        medlinelogo.Alignment = Element.ALIGN_RIGHT;
                        float percentage = 0.0f;
                        percentage = 150 / medlinelogo.Width;
                        medlinelogo.ScalePercent(percentage * 400);
                        doc.Add(medlinelogo);
                    }

                    doc.Add(new Paragraph("MAPA #" + txtcarga.Text, _title));
                    if (lbldestino.Text == "JACKSON")
                    {
                        ciclo = "MR";
                    }
                    else if (lbldestino.Text == "NPS")
                    {
                        ciclo = "NPS";
                    }
                    else if (lbldestino.Text == "SYNERGY")
                    {
                        ciclo = "SYN";
                    }
                    doc.Add(new Paragraph("Caja: " + txtcaja.Text + "  |  Ciclo: " + lblciclo.Text + "  |  Destino: " + lbldestino.Text + "  |  Rampa: " + txtrampa.Text, _title2));
                    doc.Add(new Paragraph("Notas: " + lblnotas.Text, _title2));
                    doc.Add(Chunk.NEWLINE);
                    iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    iTextSharp.text.Font _standardFont1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    PdfPTable tblPdf = new PdfPTable(6);
                    tblPdf.WidthPercentage = 100;
                    tblPdf.HorizontalAlignment = Element.ALIGN_CENTER;

                    //Ccolumnas de archivo

                    PdfPCell clposicion = new PdfPCell(new Phrase("Posicion", _standardFont1));
                    clposicion.BackgroundColor = new BaseColor(255, 253, 147);
                    clposicion.BorderWidth = 1f;
                    clposicion.HorizontalAlignment = 1;



                    PdfPCell clNombre = new PdfPCell(new Phrase("Producto", _standardFont1));
                    clNombre.BackgroundColor = new BaseColor(255, 253, 147);
                    clNombre.BorderWidth = 1f;
                    clNombre.HorizontalAlignment = 1;


                    PdfPCell cllote = new PdfPCell(new Phrase("Lote", _standardFont1));
                    cllote.BackgroundColor = new BaseColor(255, 253, 147);
                    cllote.BorderWidth = 1f;
                    cllote.HorizontalAlignment = 1;


                    PdfPCell clcajas = new PdfPCell(new Phrase("Cajas", _standardFont1));
                    clcajas.BackgroundColor = new BaseColor(255, 253, 147);
                    clcajas.BorderWidth = 1f;
                    clcajas.HorizontalAlignment = 1;


                    PdfPCell cltarimas = new PdfPCell(new Phrase("Tarimas", _standardFont1));
                    cltarimas.BackgroundColor = new BaseColor(255, 253, 147);
                    cltarimas.BorderWidth = 1f;
                    cltarimas.HorizontalAlignment = 1;


                    PdfPCell clcargador = new PdfPCell(new Phrase("# Cargador", _standardFont1));
                    clcargador.BackgroundColor = new BaseColor(255, 253, 147);
                    clcargador.BorderWidth = 1f;
                    clcargador.HorizontalAlignment = 1;



                    // CARGANDO FILAS DE MAPA

                    tblPdf.AddCell(clposicion);
                    tblPdf.AddCell(clNombre);
                    tblPdf.AddCell(cllote);
                    tblPdf.AddCell(clcajas);
                    tblPdf.AddCell(cltarimas);
                    tblPdf.AddCell(clcargador);

                    int total = (lott.Count - 1);

                    if (total > 0)
                    {
                        for (int i = 0; i <= total; i++)
                        {
                            posicion1 += pos[i];
                            producto += tray[i];
                            lote += lott[i];
                            cajas += qty[i];
                            tarimas += tarima[i];
                            cargador += usuario[i];

                            clposicion = new PdfPCell(new Phrase(posicion1, _standardFont));
                            clposicion.BorderWidth = .5f;
                            clposicion.HorizontalAlignment = 1;

                            clNombre = new PdfPCell(new Phrase(producto, _standardFont));
                            clNombre.BorderWidth = .5f;
                            clNombre.HorizontalAlignment = 1;

                            cllote = new PdfPCell(new Phrase(lote, _standardFont));
                            cllote.BorderWidth = .5f;
                            cllote.HorizontalAlignment = 1;

                            clcajas = new PdfPCell(new Phrase(cajas, _standardFont));
                            clcajas.BorderWidth = .5f;
                            clcajas.HorizontalAlignment = 1;

                            cltarimas = new PdfPCell(new Phrase(tarimas, _standardFont));
                            cltarimas.BorderWidth = .5f;
                            cltarimas.HorizontalAlignment = 1;

                            clcargador = new PdfPCell(new Phrase(cargador, _standardFont));
                            clcargador.BorderWidth = .5f;
                            clcargador.HorizontalAlignment = 1;

                            // Añadimos las celdas a la tabla
                            tblPdf.AddCell(clposicion);
                            tblPdf.AddCell(clNombre);
                            tblPdf.AddCell(cllote);
                            tblPdf.AddCell(clcajas);
                            tblPdf.AddCell(cltarimas);
                            tblPdf.AddCell(clcargador);

                            posicion1 = "";
                            producto = "";
                            lote = "";
                            cajas = "";
                            tarimas = "";
                            cargador = "";

                        }

                        doc.Add(tblPdf);
                        doc.Close();
                        writer.Close();

                    }
                }
            }
            else if(GlobalVar.Compania == 686)
            {
                if (Consultar.M3CrearMapa(ref pos, ref tray, ref lott, ref qty, ref tarima, ref usuario, Convert.ToInt32(txtcarga.Text)))
                {
                    // CREANDO ARCHIVO PDF
                    Document doc = new Document(PageSize.LETTER);
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"\\mxcprdfp1\Software_MXC\ShippingSystem\Mapas_electronicos\Mapassqltest\MAPA#" + txtcarga.Text + ".pdf", FileMode.Create));
                    doc.Open();
                    iTextSharp.text.Font _title = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 15, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    iTextSharp.text.Font _title2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    iTextSharp.text.Font _title3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 15, iTextSharp.text.Font.UNDERLINE, BaseColor.BLACK);
                    if (File.Exists(Path.Combine(Application.StartupPath, @"logo\logo_m1.png")))
                    {
                        iTextSharp.text.Image medlinelogo = iTextSharp.text.Image.GetInstance(Path.Combine(Application.StartupPath, @"logo\logo_m1.png"));
                        medlinelogo.BorderWidth = 0;
                        medlinelogo.Alignment = Element.ALIGN_RIGHT;
                        float percentage = 0.0f;
                        percentage = 150 / medlinelogo.Width;
                        medlinelogo.ScalePercent(percentage * 400);
                        doc.Add(medlinelogo);
                    }

                    doc.Add(new Paragraph("MAPA #" + txtcarga.Text, _title));
                    if (lbldestino.Text == "Ontario")
                    {
                        ciclo = "LHS";
                    }
                    else if (lbldestino.Text == "NPS")
                    {
                        ciclo = "NPS";
                    }
                    else if (lbldestino.Text == "SYNERGY")
                    {
                        ciclo = "SYN";
                    }
                    doc.Add(new Paragraph("Caja: " + txtcaja.Text + "  |  Ciclo: " + lblciclo.Text + "  |  Destino: " + lbldestino.Text + "  |  Rampa: " + txtrampa.Text, _title2));
                    doc.Add(new Paragraph("Notas: " + lblnotas.Text, _title2));
                    doc.Add(Chunk.NEWLINE);
                    iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    iTextSharp.text.Font _standardFont1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
                    PdfPTable tblPdf = new PdfPTable(6);
                    tblPdf.WidthPercentage = 100;
                    tblPdf.HorizontalAlignment = Element.ALIGN_CENTER;

                    //Ccolumnas de archivo

                    PdfPCell clposicion = new PdfPCell(new Phrase("Posicion", _standardFont1));
                    clposicion.BackgroundColor = new BaseColor(255, 253, 147);
                    clposicion.BorderWidth = 1f;
                    clposicion.HorizontalAlignment = 1;



                    PdfPCell clNombre = new PdfPCell(new Phrase("Producto", _standardFont1));
                    clNombre.BackgroundColor = new BaseColor(255, 253, 147);
                    clNombre.BorderWidth = 1f;
                    clNombre.HorizontalAlignment = 1;


                    PdfPCell cllote = new PdfPCell(new Phrase("Lote", _standardFont1));
                    cllote.BackgroundColor = new BaseColor(255, 253, 147);
                    cllote.BorderWidth = 1f;
                    cllote.HorizontalAlignment = 1;


                    PdfPCell clcajas = new PdfPCell(new Phrase("Cajas", _standardFont1));
                    clcajas.BackgroundColor = new BaseColor(255, 253, 147);
                    clcajas.BorderWidth = 1f;
                    clcajas.HorizontalAlignment = 1;


                    PdfPCell cltarimas = new PdfPCell(new Phrase("Tarimas", _standardFont1));
                    cltarimas.BackgroundColor = new BaseColor(255, 253, 147);
                    cltarimas.BorderWidth = 1f;
                    cltarimas.HorizontalAlignment = 1;


                    PdfPCell clcargador = new PdfPCell(new Phrase("# Cargador", _standardFont1));
                    clcargador.BackgroundColor = new BaseColor(255, 253, 147);
                    clcargador.BorderWidth = 1f;
                    clcargador.HorizontalAlignment = 1;



                    // CARGANDO FILAS DE MAPA

                    tblPdf.AddCell(clposicion);
                    tblPdf.AddCell(clNombre);
                    tblPdf.AddCell(cllote);
                    tblPdf.AddCell(clcajas);
                    tblPdf.AddCell(cltarimas);
                    tblPdf.AddCell(clcargador);

                    int total = (lott.Count - 1);

                    if (total > 0)
                    {
                        for (int i = 0; i <= total; i++)
                        {
                            posicion1 += pos[i];
                            producto += tray[i];
                            lote += lott[i];
                            cajas += qty[i];
                            tarimas += tarima[i];
                            cargador += usuario[i];

                            clposicion = new PdfPCell(new Phrase(posicion1, _standardFont));
                            clposicion.BorderWidth = .5f;
                            clposicion.HorizontalAlignment = 1;

                            clNombre = new PdfPCell(new Phrase(producto, _standardFont));
                            clNombre.BorderWidth = .5f;
                            clNombre.HorizontalAlignment = 1;

                            cllote = new PdfPCell(new Phrase(lote, _standardFont));
                            cllote.BorderWidth = .5f;
                            cllote.HorizontalAlignment = 1;

                            clcajas = new PdfPCell(new Phrase(cajas, _standardFont));
                            clcajas.BorderWidth = .5f;
                            clcajas.HorizontalAlignment = 1;

                            cltarimas = new PdfPCell(new Phrase(tarimas, _standardFont));
                            cltarimas.BorderWidth = .5f;
                            cltarimas.HorizontalAlignment = 1;

                            clcargador = new PdfPCell(new Phrase(cargador, _standardFont));
                            clcargador.BorderWidth = .5f;
                            clcargador.HorizontalAlignment = 1;

                            // Añadimos las celdas a la tabla
                            tblPdf.AddCell(clposicion);
                            tblPdf.AddCell(clNombre);
                            tblPdf.AddCell(cllote);
                            tblPdf.AddCell(clcajas);
                            tblPdf.AddCell(cltarimas);
                            tblPdf.AddCell(clcargador);

                            posicion1 = "";
                            producto = "";
                            lote = "";
                            cajas = "";
                            tarimas = "";
                            cargador = "";

                        }

                        doc.Add(tblPdf);
                        doc.Close();
                        writer.Close();

                    }
                }
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void send_mapa(object sender, EventArgs e)
        {
            if (GlobalVar.Compania == 110)
            {
                try
                {
                    string PathFile1 = @"\\mexfp1\Medline\Dept. Recibo & Embarques\Mapas electronicos\Merged\Mergedsqltest\MAPA_#" + txtcarga.Text + ".pdf";
                    string PathFile2 = @"\\mexfp1\medline\Dept. Recibo & Embarques\Mapas electronicos\Reporte_discrepancias\discrepanciasSqltest\" + year + "\\ Discrepancias_MAPA#" + txtcarga.Text + ".txt";
                    if (System.IO.File.Exists(PathFile1) && System.IO.File.Exists(PathFile2))
                    {
                        Attachment adjunto1 = new Attachment(PathFile1);
                        Attachment adjunto2 = new Attachment(PathFile2);
                        MailMessage mail = new MailMessage("PDMShipping@medline.com", "PDMShipping@medline.com");
                        mail.CC.Add("ADeLeon@medline.com");
                        SmtpClient client = new SmtpClient();
                        client.Port = 25;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;
                        client.Host = "mailhost.medline.com";
                        mail.Subject = "Shipping SYSTEM        MAPA-#" + txtcarga.Text + "-" + txtcaja.Text + "-" + ciclo + "   " + lblnotas.Text;
                        mail.Body = "Please find attached paperwork for shipment id# " + txtcarga.Text;
                        mail.Attachments.Add(adjunto1);
                        mail.Attachments.Add(adjunto2);
                        client.Send(mail);
                        correo = "Y";
                    }
                    else
                    {/*////////////////////////*/

                        correo = "N";

                    }

                }
                catch (Exception ex)
                {

                    toolStripProgressBar1.Visible = false;
                    MessageBox.Show(ex.Message);
                }
            }

            else if (GlobalVar.Compania == 686)
            {
                try
                {
                    if (Consultar.ConfigSMTP(ref server,ref smtp_user,ref puerto,ref mapa,ref discre ) == true)
                    {
                        //string PathFile1 = @"\\mxcprdfp1\Software_MXC\ShippingSystem\Mapas_electronicos\MergedSqlTest\MAPA_#" + txtcarga.Text + ".pdf";
                        //string PathFile2 = @"\\mxcprdfp1\Software_MXC\ShippingSystem\Mapas_electronicos\Reporte_discrepanciasSqltest\Discrepancias_MAPA#" + txtcarga.Text + ".txt";
                        string PathFile1 = mapa + "\\MAPA_#" + txtcarga.Text + ".pdf";
                        string PathFile2 = discre + "\\Discrepancias_MAPA#" + txtcarga.Text + ".txt";

                        if (File.Exists(PathFile1) && File.Exists(PathFile2))
                        {
                            Attachment adjunto1 = new Attachment(PathFile1);
                            Attachment adjunto2 = new Attachment(PathFile2);
                            MailMessage mail = new MailMessage(smtp_user, smtp_user);
                            SmtpClient client = new SmtpClient();
                            mail.CC.Add("Algonzalez@medline.com");
                            client.Port = puerto;
                            client.DeliveryMethod = SmtpDeliveryMethod.Network;
                            client.UseDefaultCredentials = false;
                            client.Host = server;//muncasarray1.medline.com
                            mail.Subject = "Shipping SYSTEM Mexicali       MAPA-#" + txtcarga.Text + "-" + txtcaja.Text + "-" + ciclo + "   " + lblnotas.Text;
                            mail.Body = "Please find attached paperwork for shipment id# " + txtcarga.Text;
                            mail.Attachments.Add(adjunto1);
                            mail.Attachments.Add(adjunto2);
                            client.Send(mail);
                            correo = "Y";
                        }
                        else
                            correo = "N";
                    }
                    else
                        correo = "N";
                }
                catch (Exception ex)
                {
                    toolStripProgressBar1.Visible = false;
                    MessageBox.Show(ex.Message);
                }
            }
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            discrepancias_Validacion(sender, e);

            if(Cancel == true)
            {
                return;
            }
            
            incompleto = "N";
            if (txtrampa.Text == "" || txtcarga.Text == "" || txtcaja.Text == "")
            {
                MessageBox.Show("Informacion de embarque ( caja/carga/rampa) no esta completa","Verificar",MessageBoxButtons.OK,MessageBoxIcon.Exclamation );
            }
            else
            {
                string mapa_vacio = "Y";
                message1 = "";
                DialogResult answer = MessageBox.Show("Confirma que desea terminar la carga?", "VERIFICAR", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    toolStripProgressBar1.Visible = true;
                    toolStripProgressBar1.Maximum = 10;
                    toolStripProgressBar1.Value = 2;
                    //verificando que mapa no este sin lotes
             
                        if ((Consultar.M3LabelLote(txtcarga.Text) == true))
                        {
                            mapa_vacio = "N";
                        }
                        else
                        {
                            mapa_vacio = "Y";
                        }


                    if (mapa_vacio == "N")
                    {
                        // verificando que la carga no tenga lotes incompletos
                        lotes_parciales((object)sender, (EventArgs)e);
                        toolStripProgressBar1.Value = 3;
                        lotes_completos((object)sender, (EventArgs)e);
                        if (incompleto == "Y")
                        {
                            toolStripProgressBar1.Visible = false;
                            MessageBox.Show("Carga no se puede completar...Lotes incompletos en MAPA:" + Environment.NewLine + message1, "No se puede embarcar!", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        }
                        else
                        {
                            try
                            {
                                //string hora = DateTime.Now.ToString("HH:mm");
                                //string date1 = DateTime.Now.ToString("MM/dd/yy");
                                Consultar.M3ModificarEmbarcado(txtcaja.Text, txtcarga.Text);
                                toolStripProgressBar1.Value = 4;

                                toolStripProgressBar1.Value = 5;
                                rojo.Visible = false;
                                amarillo.Visible = false;
                                verde.Visible = false;
                                label33.Visible = false;
                                lblnotas.Visible = false;
                                label36.Visible = false;
                                cbfporcentaje.Visible = false;
                                cbfvalor.Visible = false;
                                discrepancias_qty((object)sender, (EventArgs)e);
                                toolStripProgressBar1.Value = 6;
                                crear_mapa((object)sender, (EventArgs)e);
                                toolStripProgressBar1.Value = 8;
                                juntar_pdfs((object)sender, (EventArgs)e);
                                toolStripProgressBar1.Value = 9;
                                if (correo == "Y")
                                {
                                    send_mapa((object)sender, (EventArgs)e);
                                    if (correo == "Y")
                                    {
                                        Consultar.M3Embarcado(txtcarga.Text);
                                        limpiar_textbox((object)sender, (EventArgs)e);
                                        toolStripProgressBar1.Value = 10;
                                        txtcaja.Text = "";
                                        txtrampa.Text = "";
                                        lblciclo.Text = "";
                                        lbldestino.Text = "";
                                        txtlocalizacion.Text = "";
                                        MessageBox.Show("Embarque completado", "Completado", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                        toolStripProgressBar1.Visible = false;
                                    }
                                    else
                                        MessageBox.Show("Error al adjuntar archivo en correo", "No completado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                else
                                    MessageBox.Show("Error al adjuntar archivo en correo", "No completado", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                else
                {
                    toolStripProgressBar1.Visible = false;
                    MessageBox.Show("Mapa no tiene lotes cargados", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }      
        }

        
        private void discrepancias_qty(object sender,EventArgs e)
        {
            lote1 = "";
            cajas1 = "";

            lot.Clear();
            lblcajas.Clear();

            string texto = "Reporte de Discrepancias en MAPA # " + txtcarga.Text + Environment.NewLine + Environment.NewLine + "Lote\t\tCajas_Declaradas\tCajas_AS400\tUM";
            OdbcConnection conexion = new OdbcConnection("Dsn=QDSN_AS400SYS;uid=shipmex;pwd=mexship");

          

            Consultar.M3DiscrepanciasAs400(ref lot, ref lblcajas, Convert.ToInt32(txtcarga.Text));

            int total = (lot.Count - 1);
                                                                                                                                                                            
            if (total >= 0)
            {
                for(int i = 0; i <= total; i++)
                {
                        lote1 += lot[i];
                        cajas1 += lblcajas[i];
                    String sql2 = "Select B.WDQTCM as qty_complete ,A.WOUNTM AS um from KBM400MFG.FMWOSUM  A INNER JOIN KBM400MFG.FMWODET B ON A.WOWONO =B.WOWONO  where A.WOCO= " + GlobalVar.Compania + " and A.WOLOT='" + lote1 + "' and (RTOPNO=0021 OR RTOPNO=0030) ";
                    conexion.Open();
                    OdbcCommand comando1 = new OdbcCommand(sql2, conexion);
                    OdbcDataReader reader1 = comando1.ExecuteReader();
                    if (reader1.Read())
                    {
                        cajas_prqoh1 = reader1["qty_complete"].ToString();
                        string sUM = reader1["um"].ToString();
                        if (cajas_prqoh1 == cajas1)
                        {
                            // no hacer nada
                        }
                        else
                        {
                           texto = texto + Environment.NewLine + lote1 + "\t" + cajas1 + "\t\t\t" + cajas_prqoh1 + "\t\t\t" + sUM;

                        }
                        
                    }
                    conexion.Close();
                    lote1 = "";
                    cajas1 = "";
                   
                }
                if (GlobalVar.Compania == 110)
                {
                    string fic = @"\\mexfp1\medline\Dept. Recibo & Embarques\Mapas electronicos\Reporte_discrepancias\DiscrepanciasSqltest\" + year + " \\ Discrepancias_MAPA#" + txtcarga.Text + ".txt";
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fic);
                    sw.WriteLine(texto);
                    sw.Close();
                }
                else if(GlobalVar.Compania == 686)
                {
                    string fic = @"\\mxcprdfp1\Software_MXC\ShippingSystem\Mapas_electronicos\Reporte_discrepanciasSqltest\Discrepancias_MAPA#" + txtcarga.Text + ".txt";
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fic);
                    sw.WriteLine(texto);
                    sw.Close();
                }
                
            }
        }

        private void discrepancias_Validacion(object sender, EventArgs e)
        {
            lote1 = "";
            cajas1 = "";

            lot.Clear();
            lblcajas.Clear();

            string texto = "Reporte de Discrepancias en Mapa #" + txtcarga.Text + Environment.NewLine + Environment.NewLine + "Lote\t\tCajas_Declaradas\tCajas_AS400";
            OdbcConnection conexion = new OdbcConnection("Dsn=QDSN_AS400SYS;uid=shipmex;pwd=mexship");

            Consultar.M3DiscrepanciasAs400(ref lot, ref lblcajas, Convert.ToInt32(txtcarga.Text));

            int total = (lot.Count - 1);

            if (total >= 0)
            {
                for (int i = 0; i <= total; i++)
                {
                    if (total >= i)
                    {
                        lote1 += lot[i];
                        cajas1 += lblcajas[i];

                        String sql2 = "Select B.WDQTCM as qty_complete from KBM400MFG.FMWOSUM  A INNER JOIN KBM400MFG.FMWODET B ON A.WOWONO =B.WOWONO  where A.WOCO= " + GlobalVar.Compania + " and A.WOLOT='" + lote1 + "' and (RTOPNO=0021 OR RTOPNO=0030) and B.RTCO = " + GlobalVar.Compania + " ";
                        conexion.Open();
                        OdbcCommand comando1 = new OdbcCommand(sql2, conexion);
                        OdbcDataReader reader1 = comando1.ExecuteReader();
                        if (reader1.Read())
                        {
                            int cajas_prqoh12 = Convert.ToInt32(reader1["qty_complete"]);
                            int icajas = Convert.ToInt32(cajas1);

                            if (cajas_prqoh12 < icajas)
                            {
                                MessageBox.Show("Lote '" + lote1 + " ' Cantidad de cajas no puede ser Mayor AS400 '" + cajas_prqoh12 + "' Shipping '" + icajas + " ' ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Validacion = Microsoft.VisualBasic.Interaction.InputBox("Favor de poner Contrasena");

                                if (Validacion.Length == 0)
                                {
                                    Cancel = true;
                                    break;
                                }
                                else
                                {
                                    if (Consultar.M3password(Validacion) == false)
                                    {
                                        MessageBox.Show("Contrasena Incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        Validacion = Microsoft.VisualBasic.Interaction.InputBox("Favor de poner Contrasena");

                                    }
                                    else
                                    {
                                        Consultar.LoteIncompletos(lote1, cajas_prqoh12, icajas, Validacion, GlobalVar.Compania);
                                    }
                                }
                            }
                            else if (cajas_prqoh12 > icajas)
                            {
                                MessageBox.Show("Lote '" + lote1 + " ' Cantidad de cajas no puede ser Menor AS400 '" + cajas_prqoh12 + "' Shipping '" + icajas + " ' ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Validacion = Microsoft.VisualBasic.Interaction.InputBox("Favor de poner Contrasena");

                                if (Validacion.Length == 0)
                                {
                                    Cancel = true;
                                    break;
                                }
                                else
                                {
                                    if (Consultar.M3password(Validacion) == false)
                                    {
                                        MessageBox.Show("Contrasena Incorrecta", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        Validacion = Microsoft.VisualBasic.Interaction.InputBox("Favor de poner Contrasena");

                                    }
                                    else
                                    {
                                        Consultar.LoteIncompletos(lote1, cajas_prqoh12, icajas, Validacion, GlobalVar.Compania);
                                    }
                                }
                            }

                        }
                        conexion.Close();
                        lote = "";
                        cajas1 = "";
                    }
                }

            }
        }

        private void M3_Map_Load(object sender, EventArgs e)
        {
            /*SCROLL BAR*/
            Panel my_panel = new Panel();
            VScrollBar vScroller = new VScrollBar();
            vScroller.Dock = DockStyle.Right;
            vScroller.Width = 30;
            vScroller.Height = 200;
            vScroller.Name = "VScrollBar1";
            my_panel.Controls.Add(vScroller);


            /*************/
            toolStripStatusLabel1.Text = "Usuario=  " + GlobalVar.usuario;
            grid1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkBlue ;
            rojo.Visible = false;
            amarillo.Visible = false;
            verde.Visible = false;
           
        }

        private void CBF_Normal(object sender, MouseEventArgs e)
        {
            OdbcConnection conexion = new OdbcConnection("Dsn=QDSN_AS400SYS;uid=shipmex;pwd=mexship");
            double hight1, width1, lenght1,cbf_case;
            string tray1 = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[2].Value.ToString();
            string sql = " Select IMHGHT,IMWDTH,IMLNGT from KBM400MFG.FKITMSTR where IMPN='" + tray1  + "' and IMCO=" + GlobalVar.Compania + "";
            OdbcCommand comando = new OdbcCommand(sql, conexion);
            OdbcDataReader reader = null;
            try
            {
                conexion.Open();
                reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    hight1 = Convert.ToDouble(reader["IMHGHT"].ToString());
                    width1 = Convert.ToDouble(reader["IMWDTH"].ToString());
                    lenght1 = Convert.ToDouble(reader["IMLNGT"].ToString());
                    conexion.Close();
                    cbf_case = (hight1 * width1 * lenght1) / 1728;
                    int cajas1 = Convert.ToInt32(cajas_ch);
                    cbf_n = cajas1 * cbf_case;
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
        private void detenido(object sender, EventArgs  e)
        {
            if (grid1.RowCount == 0)
            {
            }
            else
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
        }
       

        private void pos1_MouseClick(object sender, MouseEventArgs e)

        {
            
           
            // validando que no se carguen de otro ciclo principal 
                detenido((object)sender, (EventArgs)e);
                if (cargar1 == "N")
                {
                    MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                { 
                
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    if (result == DialogResult.Yes)
                    {
                        if (pos1.Text == "")
                        {
                            posicion = 1;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    
                                    pos1.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos1.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                }    
            
        }
        }
        private void pos2_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos2.Text == "")
                        {
                            posicion = 2;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos2.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos2.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }




                }

            }
        }
        private void pos3_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos3.Text == "")
                        {
                            posicion = 3;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos3.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos3.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
           
        }
        private void pos4_MouseClick(object sender, MouseEventArgs e)
        {

            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos4.Text == "")
                        {
                            posicion = 4;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos4.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos4.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }


          
        }
        private void pos5_MouseClick(object sender, MouseEventArgs e)
        {

            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos5.Text == "")
                        {
                            posicion = 5;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos5.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos5.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

           
        }
        private void pos6_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos6.Text == "")
                        {
                            posicion = 6;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos6.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos6.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

          
        }
        private void pos7_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos7.Text == "")
                        {
                            posicion = 7;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos7.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos7.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

           
        }
        private void pos8_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos8.Text == "")
                        {
                            posicion = 8;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos8.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos8.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

           
        }
        private void pos9_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos9.Text == "")
                        {
                            posicion = 9;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos9.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos9.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

        }
        private void pos10_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos10.Text == "")
                        {
                            posicion = 10;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos10.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos10.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            
        }
        private void pos11_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos11.Text == "")
                        {
                            posicion = 11;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos11.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos11.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            

        }
        private void pos12_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos12.Text == "")
                        {
                            posicion = 12;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos12.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos12.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

           
        }
        private void pos13_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos13.Text == "")
                        {
                            posicion = 13;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos13.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos13.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

          
        }
        private void pos14_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos14.Text == "")
                        {
                            posicion = 14;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos14.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos14.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

           
        }
        private void pos15_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos15.Text == "")
                        {
                            posicion = 15;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos15.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos15.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void pos16_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos16.Text == "")
                        {
                            posicion = 16;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos16.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos16.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

         
        }
        private void pos17_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos17.Text == "")
                        {
                            posicion = 17;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos17.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos17.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void pos18_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos18.Text == "")
                        {
                            posicion = 18;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos18.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos18.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void pos19_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos19.Text == "")
                        {
                            posicion = 19;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos19.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos19.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

           
        }
        private void pos20_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos20.Text == "")
                        {
                            posicion = 20;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos20.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos20.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

           
        }
        private void pos21_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos21.Text == "")
                        {
                            posicion = 21;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos21.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos21.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

          
        }
        private void pos22_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos22.Text == "")
                        {
                            posicion = 22;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos22.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos22.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            
        }
        private void pos23_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos23.Text == "")
                        {
                            posicion = 23;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos23.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos23.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

           
        }
        private void pos24_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos24.Text == "")
                        {
                            posicion = 24;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos24.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos24.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            
        }
        private void pos25_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos25.Text == "")
                        {
                            posicion = 25;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos25.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos25.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

           
        }
        private void pos26_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos26.Text == "")
                        {
                            posicion = 26;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos26.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos26.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

           
        }
        private void pos27_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos27.Text == "")
                        {
                            posicion = 27;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos27.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos27.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
           
        }
        private void pos28_MouseClick(object sender, MouseEventArgs e)
        {
            detenido((object)sender, (EventArgs)e);
            if (cargar1 == "N")
            {
                MessageBox.Show("Lote esta marcado como NO CARGAR", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (grid1.RowCount == 0)
                {
                    MessageBox.Show("No hay lotes para agregar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    DialogResult result;
                    if (grid1.Rows[grid1.CurrentRow.Index].DefaultCellStyle.BackColor == Color.Red)
                    {
                        result = MessageBox.Show("El lote que desea agregar no tiene el mismo ciclo o destino!" + Environment.NewLine + Environment.NewLine + "Desea continuar y agregar el lote a la carga?", " VERIFICAR!", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
                    }
                    else
                    {
                        result = DialogResult.Yes;
                    }
                    // validando que no se carguen de otro ciclo principal 
                    if (result == DialogResult.Yes)
                    {
                        if (pos28.Text == "")
                        {
                            posicion = 28;
                            guardar_carga((Object)sender, (EventArgs)e);
                            if (errorsave == 0)
                            {
                                if (empalme == "" || empalme == "0")
                                {
                                    pos28.Text = grid1.Rows[grid1.CurrentCell.RowIndex].Cells[1].Value.ToString() + Environment.NewLine + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[3].Value.ToString() + " / " + grid1.Rows[grid1.CurrentCell.RowIndex].Cells[4].Value.ToString() + Environment.NewLine + cajas_ch + " cs";
                                }
                                else
                                {
                                    pos28.Text = "Empalme#" + Environment.NewLine + empalme;
                                }
                                grid1.Rows.RemoveAt(grid1.CurrentCell.RowIndex);
                                cargar_rampa((object)sender, (EventArgs)e);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Posicion ya esta cargada, remueva lote para ingresar nuevo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (txtcaja.Text == "" || txtrampa.Text=="" )
            {
                MessageBox.Show("Introduzca informacion completa ( Carga/Caja/Rampa)", "VERIFIQUE", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                numero_loc = txtlocalizacion.Text;
                id_carga1 = txtcarga.Text;
                remover_lote remover = new remover_lote();
                remover.ShowDialog();
                actualizar_cbf((object)sender, (MouseEventArgs)e);
                cargar_enproceso((object) sender, (MouseEventArgs) e);
                cargar_rampa((object)sender, (MouseEventArgs)e);
             
            }
            
        }

        

        private void nuevaCargaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crear_carga crear1 = new crear_carga();
            crear1.ShowDialog();
            Verificacion_de_caja verificar1 = new Verificacion_de_caja();
            verificar1.ShowDialog();


            if (crear_carga.grabada == "Y")
            {
                txtcarga.Text = M3_Map.numero_carga1;
                txtcaja.Text = "";
                txtrampa.Text = "";
                limpiar_textbox((object)sender, (EventArgs)e);
                lblciclo.Text = "";
                lbldestino.Text = "";
                grid1.DataSource = null;
                rojo.Visible = false;
                amarillo.Visible = false;
                verde.Visible = false;
                label33.Visible = false;
                label36.Visible = false;
                cbfporcentaje.Visible = false;
                cbfvalor.Visible = false;
            }
            if (txtcarga.Text != "")
            { 
                button1_Click_1((object) sender, (EventArgs) e);
            }

        }

        private void txtcaja_TextChanged(object sender, EventArgs e)
        {
            if (txtcaja.Text != "" && txtcarga.Text != "" && txtrampa.Text != "")
            {
                btnok.Enabled = true;
            }
            else
            {
                btnok.Enabled = false;
            }
        }

        private void txtcarga_TextChanged(object sender, EventArgs e)
        {
            if (txtcaja.Text != "" && txtcarga.Text != "" && txtrampa.Text != "")
            {
                btnok.Enabled = true;
            }
            else
            {
                btnok.Enabled = false;
            }
        }

        private void txtrampa_TextChanged(object sender, EventArgs e)
        {
            if (txtcaja.Text != "" && txtcarga.Text != "" && txtrampa.Text != "")
            {
                btnok.Enabled = true;
            }
            else
            {
                btnok.Enabled = false;
            }
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            toolStripProgressBar1.Visible = false;
            limpiar_textbox ((object) sender, (EventArgs) e);
            if (txtcarga.Text  == "")
            {
                MessageBox.Show("Introduzca el numero de carga", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
               

                lblnotas.Visible = true;
                label33.Visible = false;
                label36.Visible = false;
                cbfporcentaje.Visible = false;
                cbfvalor.Visible = false;
                rojo.Visible = false;
                verde.Visible = false;
                amarillo.Visible = false ;
                lbldestino.Text = "";
                lblciclo.Text = "";
                lblnotas.Text = "";
             
             if (Consultar.M3carganota(ref cajas_ch, ref destino1, ref ciclo, ref rampa, ref notas, ref loc,txtcarga.Text) == true)
                {
                        txtcaja.Text = (cajas_ch);
                        lblciclo.Text = (ciclo);
                        lbldestino.Text = (destino1);
                        txtrampa.Text = (rampa);
                        lblnotas.Text = (notas);
                        txtlocalizacion.Text = (loc);
                       
                        lblciclo.Visible = true;
                        lbldestino.Visible = true;
                        if (txtcaja.Text == "")
                        {
                            btnadd.Enabled = true;
                        }
                    else
                    {
                        btnadd.Enabled = false;
                    }
                }
            }
        }
           

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void grid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            reasignar = "Y";
            numero_carga1  = txtcarga .Text ;
            Verificacion_de_caja verificar = new Verificacion_de_caja();
            verificar.ShowDialog();
            if (txtcarga.Text != "")
            {
                button1_Click_1((object)sender, (EventArgs)e);
            }
        }
        private void juntar_pdfs(object sender, EventArgs e)
        {
            if (GlobalVar.Compania == 110)
            {
                string[] lstFiles = new string[3];
                lstFiles[0] = @"\\mexfp1\Medline\Dept. Recibo & Embarques\Mapas electronicos\Mapas\MapasSqltest\MAPA#" + txtcarga.Text + ".pdf";
                lstFiles[1] = @"\\mexfp1\Medline\Dept. Recibo & Embarques\Mapas electronicos\Checklist\ChecklistSqltest\Verificacion_#" + txtcarga.Text + ".pdf";


                if (File.Exists(lstFiles[0]) == true && File.Exists(lstFiles[1]) == true)
                {
                    PdfReader reader = null;
                    Document sourceDocument = null;
                    PdfCopy pdfCopyProvider = null;
                    PdfImportedPage importedPage;
                    string outputPdfPath = @"\\mexfp1\Medline\Dept. Recibo & Embarques\Mapas electronicos\Merged\MergedSqltest\MAPA_#" + txtcarga.Text + ".pdf";

                    sourceDocument = new Document();
                    pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

                    //Open the output file
                    sourceDocument.Open();

                    try
                    {
                        //Loop through the files list
                        for (int f = 0; f < lstFiles.Length - 1; f++)
                        {
                            int pages = get_pageCcount(lstFiles[f]);
                            reader = new PdfReader(lstFiles[f]);

                            //Add pages of current file

                            for (int i = 1; i <= pages; i++)
                            {
                                importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                                pdfCopyProvider.AddPage(importedPage);
                            }
                            reader.Close();
                        }
                        //At the end save the output file
                        sourceDocument.Close();
                        correo = "Y";
                    }
                    catch (Exception ex)
                    {
                        toolStripProgressBar1.Visible = false;
                        throw ex;
                    }

                }
                else
                {
                    correo = "N";
                }
            }
            else if(GlobalVar.Compania == 686)
            {

                string[] lstFiles = new string[3];
                lstFiles[0] = @"\\mxcprdfp1\Software_MXC\ShippingSystem\Mapas_electronicos\Mapassqltest\MAPA#" + txtcarga.Text + ".pdf";
                lstFiles[1] = @"\\mxcprdfp1\Software_MXC\ShippingSystem\Mapas_electronicos\ChecklistSqltest\Verificacion_#" + txtcarga.Text + ".pdf";

                if (File.Exists(lstFiles[0]) == true && File.Exists(lstFiles[1]) == true)
                {
                    PdfReader reader = null;
                    Document sourceDocument = null;
                    PdfCopy pdfCopyProvider = null;
                    PdfImportedPage importedPage;
                    string outputPdfPath = @"\\mxcprdfp1\Software_MXC\ShippingSystem\Mapas_electronicos\Mergedsqltest\MAPA_#" + txtcarga.Text + ".pdf";
                    
                    sourceDocument = new Document();
                    pdfCopyProvider = new PdfCopy(sourceDocument, new System.IO.FileStream(outputPdfPath, System.IO.FileMode.Create));

                    //Open the output file
                    sourceDocument.Open();

                    try
                    {
                        //Loop through the files list
                        for (int f = 0; f < lstFiles.Length - 1; f++)
                        {
                            int pages = get_pageCcount(lstFiles[f]);
                            reader = new PdfReader(lstFiles[f]);

                            //Add pages of current file

                            for (int i = 1; i <= pages; i++)
                            {
                                importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                                pdfCopyProvider.AddPage(importedPage);
                            }
                            reader.Close();
                        }
                        //At the end save the output file
                        sourceDocument.Close();
                        correo = "Y";
                    }
                    catch (Exception ex)
                    {
                        toolStripProgressBar1.Visible = false;
                        throw ex;
                    }

                }
                else
                {
                    correo = "N";
                }
            }
             
       }
        private int get_pageCcount(string file)
        {
        using (StreamReader sr = new StreamReader(File.OpenRead(file)))
        {
        Regex regex = new Regex(@"/Type\s*/Page[^s]");
        MatchCollection matches = regex.Matches(sr.ReadToEnd());

        return matches.Count;

            }
        }
    }
}  
    

