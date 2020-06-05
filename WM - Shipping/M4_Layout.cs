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
    public partial class M4_Layout : Form
    {

        Datos Consultar = new Datos();
        public static int rampa_d;
        public static string piso_d,carga_porcent;
        ArrayList ocup = new ArrayList();
        ArrayList rampas = new ArrayList();
        ArrayList loc = new ArrayList();
        ArrayList tam = new ArrayList();
        
        public M4_Layout()
        {
            InitializeComponent();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void M4_Layout_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Usuario=  " +   GlobalVar.usuario ;
            btnactualizar_Click((object)sender, (EventArgs)e);
        }
        private void limpiar_docks(object sender, EventArgs e)
        {
            R24.Value = 0;
            R25.Value = 0;
            R26.Value = 0;
            R27.Value = 0;
            R28.Value = 0;
            R29.Value = 0;
            R30.Value = 0;
            R31.Value = 0;
            R32.Value = 0;
            R33.Value = 0;
            R34.Value = 0;
            R35.Value = 0;
            R36.Value = 0;
            R37.Value = 0;
            R38.Value = 0;
            R39.Value = 0;
            R40.Value = 0;
            R41.Value = 0;
            R42.Value = 0;
            R43.Value = 0;
            R391.Value = 0;

        }
        private void limpiar_piso(object sender, EventArgs e)
        {
            e1.Text = "0";
            e2.Text = "0";
            s1.Text = "0";
            s2.Text = "0";
            s3.Text = "0";
            s4.Text = "0";
            s5.Text = "0";
            s6.Text = "0";
            s7.Text = "0";
            s8.Text = "0";
            s9.Text = "0";
            s10.Text = "0";
            s11.Text = "0";
            s12.Text = "0";
            s13.Text = "0";
            s14.Text = "0";
            s15.Text = "0";
            s16.Text = "0";
            F1.Text = "0";
            F2.Text = "0";
            F3.Text = "0";
        }
        private void cargar_docks(object sender, EventArgs e)
        {
            string rampa1 = "";
            string ocupados0 = "";
            int ocupados1 = 0;
            limpiar_docks((object)sender, (EventArgs)e);
            Consultar.M4LayOut(ref ocup, ref rampas);

            int total = (ocup.Count - 1);

            for (int  i=0; i <= total; i++)
            {

                ocupados0 += ocup[i];
                rampa1 += rampas[i];
                {
                    if (ocupados0 != "")
                    {
                        ocupados1 = Convert.ToInt32(ocupados0);
                    }
                    else
                    {
                        ocupados1 = 0;
                    }
                    switch (rampa1)
                    {
                        case "39":
                            R391.Value = ocupados1;
                            break;
                        case "38":
                            R24.Value = ocupados1;
                            break;
                        case "37":
                            R25.Value = ocupados1;
                            break;
                        case "36":
                            R26.Value = ocupados1;
                            break;
                        case "35":
                            R27.Value = ocupados1;
                            break;
                        case "34":
                            R28.Value = ocupados1;
                            break;
                        case "33":
                            R29.Value = ocupados1;
                            break;
                        case "32":
                            R30.Value = ocupados1;
                            break;
                        case "31":
                            R31.Value = ocupados1;
                            break;
                        case "30":
                            R32.Value = ocupados1;
                            break;
                        case "29":
                            R33.Value = ocupados1;
                            break;
                        case "28":
                            R34.Value = ocupados1;
                            break;
                        case "27":
                            R35.Value = ocupados1;
                            break;
                        case "26":
                            R36.Value = ocupados1;
                            break;
                        case "25":
                            R37.Value = ocupados1;
                            break;
                        case "24":
                            R38.Value = ocupados1;
                            break;
                        case "21":
                            R39.Value = ocupados1;
                            break;
                        case "20":
                            R40.Value = ocupados1;
                            break;
                        case "19":
                            R41.Value = ocupados1;
                            break;
                        case "18":
                            R42.Value = ocupados1;
                            break;
                        case "17":
                            R43.Value = ocupados1;
                            break;
                        case "16":
                            R44.Value = ocupados1;
                            break;
                        case "15":
                            R45.Value = ocupados1;
                            break;
                        case "14":
                            R46.Value = ocupados1;
                            break;
                    }
                    ocupados0 = "";
                    rampa1 = "";
                }
            }
        }
                   
        private void cargar_piso(object sender, EventArgs e)
        {
           
            string localizacion="",tarimas="";
            limpiar_piso((object)sender, (EventArgs)e);

            Consultar.M4LayOutPiso(ref loc, ref tam);

            int total = (loc.Count - 1);

            for (int i = 0; i <= total; i++)
            {

                localizacion += loc[i];
                tarimas += tam[i];

                        if (tarimas == "")
                        {
                            tarimas = "0";
                        }
                        switch (localizacion)
                        {
                            case "E1":
                                e1.Text = tarimas;
                                break;
                            case "E2":
                                e2.Text = tarimas;
                                break;
                            case "S1":
                                s1.Text = tarimas;
                                break;
                            case "S2":
                                s2.Text = tarimas;
                                break;
                            case "S3":
                                s3.Text = tarimas;
                                break;
                            case "S4":
                                s4.Text = tarimas;
                                break;
                            case "S5":
                                s5.Text = tarimas;
                                break;
                            case "S6":
                                s6.Text = tarimas;
                                break;
                            case "S7":
                                s7.Text = tarimas;
                                break;
                            case "S8":
                                s8.Text = tarimas;
                                break;
                            case "S9":
                                s9.Text = tarimas;
                                break;
                            case "S10":
                                s10.Text = tarimas;
                                break;
                            case "S11":
                                s11.Text = tarimas;
                                break;
                            case "S12":
                                s12.Text = tarimas;
                                break;
                            case "S13":
                                s13.Text = tarimas;
                                break;
                            case "S14":
                                s14.Text = tarimas;
                                break;
                            case "S15":
                                s15.Text = tarimas;
                                break;
                            case "S16":
                                s16.Text = tarimas;
                                break;
                            case "F1":
                                F1.Text = tarimas;
                                break;
                            case "F2":
                                F2.Text = tarimas;
                                break;
                            case "F3":
                                F3.Text = tarimas;
                                break;

                }
                localizacion = "";
                tarimas = "";
                    }
            string hora = DateTime.Now.ToString("HH:mm tt");
            string date1 = DateTime.Now.ToString("MM/dd/yy");
            lbldate.Text = date1 + "  @ " + hora;
        }
        private void total_embarques(object sender, EventArgs e)
        {
           /* string sql="select count( id_tarima) as TotalShipping from inventory where localizacion <> 'cargado'";
            OleDbCommand comando = new OleDbCommand(sql, con);
            try
            {
                con.Open();
                OleDbDataReader reader = comando.ExecuteReader();
                if (reader.Read()) 
                { 
                    lbltotal.Text = reader["TotalShipping"].ToString();
                }
                else
                {
                    lbltotal.Text = "0";
                }
                double  total1 = Convert.ToDouble (lbltotal.Text );
                total1 = (total1 / 604) * 100;
                lblporcentaje.Text = total1.ToString("F2") + " %";
                
               
            }
            catch ( Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }*/
            double  total1 = Convert.ToInt16(s1.Text) + Convert.ToInt16(s2.Text) + Convert.ToInt16(s3.Text) + Convert.ToInt16(s4.Text) + Convert.ToInt16(s5.Text) + Convert.ToInt16(s6.Text) + Convert.ToInt16(s7.Text) + Convert.ToInt16(s8.Text) + Convert.ToInt16(s9.Text) + Convert.ToInt16(s10.Text) + +Convert.ToInt16(s11.Text) + Convert.ToInt16(s12.Text) + Convert.ToInt16(s13.Text) + Convert.ToInt16(s14.Text) + Convert.ToInt16(s15.Text) + Convert.ToInt16(s16.Text);
            lbltotal.Text = total1.ToString();
            double porcentaje1 = total1 / 604;
            lblporcentaje.Text = porcentaje1.ToString("F2") + " %";

        }
        private void btnactualizar_Click(object sender, EventArgs e)
        {
            cargar_docks((object)sender, (EventArgs)e);
            cargar_piso((object)sender, (EventArgs)e);
            total_embarques((object)sender, (EventArgs)e);
            double valor,trailer=28;
            valor = R24.Value + R25.Value + R26.Value + R27.Value + R28.Value + R29.Value + R30.Value + R31.Value + R32.Value + R33.Value + R34.Value + R35.Value + R36.Value + R37.Value + R38.Value + R39.Value + R40.Value + R41.Value + R42.Value + R43.Value + R44.Value + R45.Value + R46.Value;
            lblskids.Text  = valor.ToString ();
            double avg_trailer = valor / trailer ;
            lblavg.Text  = avg_trailer.ToString ("F2") ;
        }

        private void R24_Click(object sender, EventArgs e)
        {
            if(R24.Value >= 1)
            {
                rampa_d = 38;
                M4_DockDetail detalle = new M4_DockDetail();
                carga_porcent = R24.Value + "/28";
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados","Verificar",MessageBoxButtons.OK , MessageBoxIcon.Exclamation );
            }
           
        }

        private void R46_Click(object sender, EventArgs e)
        {
            if (R46.Value >= 1)
            {
                carga_porcent = R46.Value + "/28";
                rampa_d = 14;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            } 
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R25_Click(object sender, EventArgs e)
        {
            if (R25.Value >= 1)
            {
                carga_porcent = R25.Value + "/28";
                rampa_d = 37;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

     
        

     

        private void R26_Click(object sender, EventArgs e)
        {
            if (R26.Value >= 1)
            {
                carga_porcent = R26.Value + "/28";
                rampa_d = 36;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R27_Click(object sender, EventArgs e)
        {
            if (R27.Value >= 1)
            {
                carga_porcent = R27.Value + "/28";
                rampa_d = 35;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R28_Click(object sender, EventArgs e)
        {
            if (R28.Value >= 1)
            {
                carga_porcent = R28.Value + "/28";
                rampa_d = 34;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R29_Click(object sender, EventArgs e)
        {
            if (R29.Value >= 1)
            {
                carga_porcent = R29.Value + "/28";
                rampa_d = 33;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R30_Click(object sender, EventArgs e)
        {
            if (R30.Value >= 1)
            {
                carga_porcent = R30.Value + "/28";
                rampa_d = 32;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R31_Click(object sender, EventArgs e)
        {
            if (R31.Value >= 1)
            {
                carga_porcent = R31.Value + "/28";
                rampa_d = 31;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R32_Click(object sender, EventArgs e)
        {
            if (R32.Value >= 1)
            {
                carga_porcent = R32.Value + "/28";
                rampa_d = 30;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R33_Click(object sender, EventArgs e)
        {
            if (R33.Value >= 1)
            {
                carga_porcent = R33.Value + "/28";
                rampa_d = 29;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R34_Click(object sender, EventArgs e)
        {
            if (R34.Value >= 1)
            {
                carga_porcent = R34.Value + "/28";
                rampa_d = 28;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R35_Click(object sender, EventArgs e)
        {
            if (R35.Value >= 1)
            {
                carga_porcent = R35.Value + "/28";
                rampa_d = 27;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R36_Click(object sender, EventArgs e)
        {
            if (R36.Value >= 1)
            {
                carga_porcent = R36.Value + "/28";
                rampa_d = 26;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R37_Click(object sender, EventArgs e)
        {
            if (R37.Value >= 1)
            {
                carga_porcent = R37.Value + "/28";
                rampa_d = 25;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R38_Click(object sender, EventArgs e)
        {
            if (R38.Value >= 1)
            {
                carga_porcent = R38.Value + "/28";
                rampa_d = 24;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R39_Click(object sender, EventArgs e)
        {
            if (R39.Value >= 1)
            {
                carga_porcent = R39.Value + "/28";
                rampa_d = 21;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R40_Click(object sender, EventArgs e)
        {
            if (R40.Value >= 1)
            {
                carga_porcent = R40.Value + "/28";
                rampa_d = 20;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R41_Click(object sender, EventArgs e)
        {
            if (R41.Value >= 1)
            {
                carga_porcent = R41.Value + "/28";
                rampa_d = 19;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R42_Click(object sender, EventArgs e)
        {
            if (R42.Value >= 1)
            {
                carga_porcent = R42.Value + "/28";
                rampa_d = 18;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R43_Click(object sender, EventArgs e)
        {
            if (R43.Value >= 1)
            {
                carga_porcent = R43.Value + "/28";
                rampa_d = 17;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R44_Click(object sender, EventArgs e)
        {
            if (R44.Value >= 1)
            {
                carga_porcent = R44.Value + "/28";
                rampa_d = 16;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R45_Click(object sender, EventArgs e)
        {
            if (R45.Value >= 1)
            {
                carga_porcent = R45.Value + "/28";
                rampa_d = 15;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void label41_Click_1(object sender, EventArgs e)
        {
            piso_d = "e1";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void lbls2_Click(object sender, EventArgs e)
        {
            piso_d = "S1";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void lbls3_Click(object sender, EventArgs e)
        {
            piso_d = "S2";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void lbls4_Click(object sender, EventArgs e)
        {
            piso_d = "S14";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void lbls5_Click(object sender, EventArgs e)
        {
            piso_d = "S6";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void lbls6_Click(object sender, EventArgs e)
        {
            piso_d = "S12";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void lbls7_Click(object sender, EventArgs e)
        {
            piso_d = "S13";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void lbls8_Click(object sender, EventArgs e)
        {
            piso_d = "S8";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void lbls9_Click(object sender, EventArgs e)
        {
            piso_d = "S9";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void lbls10_Click(object sender, EventArgs e)
        {
            piso_d = "S16";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void lbls11_Click(object sender, EventArgs e)
        {
            piso_d = "S11";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void lbls12_Click(object sender, EventArgs e)
        {
            piso_d = "S11";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void lbls13_Click(object sender, EventArgs e)
        {
            piso_d = "S10";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void lbls14_Click(object sender, EventArgs e)
        {
            piso_d = "S10";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void lbls15_Click(object sender, EventArgs e)
        {
            piso_d = "E2";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void label48_Click(object sender, EventArgs e)
        {
            piso_d = "E3";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void label48_Click_1(object sender, EventArgs e)
        {
            piso_d = "e2";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void label35_Click(object sender, EventArgs e)
        {
            piso_d = "S3";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void label51_Click(object sender, EventArgs e)
        {
            piso_d = "S4";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void label69_Click(object sender, EventArgs e)
        {
            piso_d = "S5";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void label64_Click(object sender, EventArgs e)
        {
            piso_d = "S6";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void label55_Click(object sender, EventArgs e)
        {
            piso_d = "S7";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void label79_Click(object sender, EventArgs e)
        {
            piso_d = "S8";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void label73_Click(object sender, EventArgs e)
        {
            piso_d = "S9";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void label84_Click(object sender, EventArgs e)
        {
            piso_d = "S15";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void R391_Click(object sender, EventArgs e)
        {
            if (R391.Value >= 1)
            {
                carga_porcent = R38.Value + "/28";
                rampa_d = 39;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void label82_Click(object sender, EventArgs e)
        {
            piso_d = "F1";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void label89_Click(object sender, EventArgs e)
        {
            piso_d = "F2";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void label90_Click(object sender, EventArgs e)
        {
            piso_d = "F3";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void e1_Click(object sender, EventArgs e)
        {
            piso_d = "e1";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        

    }
}
