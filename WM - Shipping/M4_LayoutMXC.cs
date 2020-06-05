using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace WindowsFormsApplication1
{
    public partial class M4_LayoutMXC : Form
    {
        Datos Consultar = new Datos();
        public static int rampa_d;
        public static string piso_d, carga_porcent;
        ArrayList ocup = new ArrayList();
        ArrayList rampas = new ArrayList();
        ArrayList loc = new ArrayList();
        ArrayList tam = new ArrayList();
        public M4_LayoutMXC()
        {
            InitializeComponent();
        }

        private void lbls10_Click(object sender, EventArgs e)
        {
            piso_d = "S2";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void label41_Click(object sender, EventArgs e)
        {
            piso_d = "S1";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void M4_LayoutMXC_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "Usuario=  " + GlobalVar.usuario;
            btnactualizar_Click((object)sender, (EventArgs)e);
        }

        private void limpiar_docks(object sender, EventArgs e)
        {
            R24.Value = 0;
            R25.Value = 0;
            R26.Value = 0;
            R27.Value = 0;
            R19.Value = 0;
            R18.Value = 0;
            R17.Value = 0;
            R16.Value = 0;
            R15.Value = 0;
            R14.Value = 0;
            R13.Value = 0;
            R12.Value = 0;
            R11.Value = 0;
            R10.Value = 0;
            R9.Value = 0;
            R8.Value = 0;
            R7.Value = 0;
            R6.Value = 0;
            R5.Value = 0;
            R4.Value = 0;
            R3.Value = 0;
            R2.Value = 0;
            R1.Value = 0;

        }
        private void limpiar_piso(object sender, EventArgs e)
        {
            s1.Text = "0";
            s2.Text = "0";
            e1.Text = "0";
            e2.Text = "0";           
            Aduana.Text = "0";


        }

        private void btnactualizar_Click(object sender, EventArgs e)
        {
            cargar_docks((object)sender, (EventArgs)e);
            cargar_piso((object)sender, (EventArgs)e);
            total_embarques((object)sender, (EventArgs)e);
            double valor, trailer = 28;
            valor = R24.Value + R25.Value + R26.Value + R27.Value + R19.Value + R18.Value + R17.Value + R16.Value + R15.Value + R14.Value + R13.Value + R12.Value + R11.Value + R10.Value + R9.Value + R8.Value + R7.Value + R6.Value + R5.Value + R4.Value + R3.Value + R2.Value + R1.Value;
            lblskids.Text = valor.ToString();
            double avg_trailer = valor / trailer;
            lblavg.Text = avg_trailer.ToString("F2");
        }

        private void total_embarques(object sender, EventArgs e)
        {
            double total1 = Convert.ToInt16(s1.Text) + Convert.ToInt16(e1.Text) + Convert.ToInt16(s2.Text);

            lbltotal.Text = total1.ToString();
            double porcentaje1 = total1 / 604;
            lblporcentaje.Text = porcentaje1.ToString("F2") + " %";

        }

        private void cargar_docks(object sender, EventArgs e)
        {
            string rampa1 = "";
            string ocupados0 = "";
            int ocupados1 = 0;
            limpiar_docks((object)sender, (EventArgs)e);
            Consultar.M4LayOut(ref ocup, ref rampas);

            int total = (ocup.Count - 1);

            for (int i = 0; i <= total; i++)
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
                        case "19":
                            R19.Value = ocupados1;
                            break;
                        case "18":
                            R18.Value = ocupados1;
                            break;
                        case "17":
                            R17.Value = ocupados1;
                            break;
                        case "16":
                            R16.Value = ocupados1;
                            break;
                        case "15":
                            R15.Value = ocupados1;
                            break;
                        case "14":
                            R14.Value = ocupados1;
                            break;
                        case "13":
                            R13.Value = ocupados1;
                            break;
                        case "12":
                            R12.Value = ocupados1;
                            break;
                        case "11":
                            R11.Value = ocupados1;
                            break;
                        case "10":
                            R10.Value = ocupados1;
                            break;
                        case "9":
                            R9.Value = ocupados1;
                            break;
                        case "8":
                            R8.Value = ocupados1;
                            break;
                        case "7":
                            R7.Value = ocupados1;
                            break;
                        case "6":
                            R6.Value = ocupados1;
                            break;
                        case "5":
                            R5.Value = ocupados1;
                            break;
                        case "4":
                            R4.Value = ocupados1;
                            break;
                        case "3":
                            R3.Value = ocupados1;
                            break;
                        case "2":
                            R2.Value = ocupados1;
                            break;
                        case "1":
                            R1.Value = ocupados1;
                            break;
                        
                    }
                    ocupados0 = "";
                    rampa1 = "";
                }
            }
        }


        private void cargar_piso(object sender, EventArgs e)
        {
            string localizacion = "", tarimas = "";
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
                    case "ADUANA":
                        Aduana.Text = tarimas;
                        break;
                    case "S1":
                        s1.Text = tarimas;
                        break;
                    case "S2":
                        s2.Text = tarimas;
                        break;


                }
                localizacion = "";
                tarimas = "";
            }
            string hora = DateTime.Now.ToString("HH:mm tt");
            string date1 = DateTime.Now.ToString("MM/dd/yy");
            lbldate.Text = date1 + "  @ " + hora;
        }

        private void R1_Click(object sender, EventArgs e)
        {
            if (R1.Value >= 1)
            {
                carga_porcent = R1.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R2_Click(object sender, EventArgs e)
        {
            if (R2.Value >= 1)
            {
                carga_porcent = R2.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R3_Click(object sender, EventArgs e)
        {
            if (R3.Value >= 1)
            {
                carga_porcent = R3.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R4_Click(object sender, EventArgs e)
        {
            if (R4.Value >= 1)
            {
                carga_porcent = R4.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R5_Click(object sender, EventArgs e)
        {
            if (R5.Value >= 1)
            {
                carga_porcent = R5.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R6_Click(object sender, EventArgs e)
        {
            if (R6.Value >= 1)
            {
                carga_porcent = R6.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R7_Click(object sender, EventArgs e)
        {
            if (R7.Value >= 1)
            {
                carga_porcent = R7.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R8_Click(object sender, EventArgs e)
        {
            if (R8.Value >= 1)
            {
                carga_porcent = R8.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R9_Click(object sender, EventArgs e)
        {
            if (R9.Value >= 1)
            {
                carga_porcent = R9.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R10_Click(object sender, EventArgs e)
        {
            if (R10.Value >= 1)
            {
                carga_porcent = R10.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R11_Click(object sender, EventArgs e)
        {
            if (R11.Value >= 1)
            {
                carga_porcent = R11.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R12_Click(object sender, EventArgs e)
        {
            if (R12.Value >= 1)
            {
                carga_porcent = R12.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R13_Click(object sender, EventArgs e)
        {
            if (R13.Value >= 1)
            {
                carga_porcent = R13.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R14_Click(object sender, EventArgs e)
        {
            if (R14.Value >= 1)
            {
                carga_porcent = R14.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R15_Click(object sender, EventArgs e)
        {
            if (R15.Value >= 1)
            {
                carga_porcent = R15.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R16_Click(object sender, EventArgs e)
        {
            if (R16.Value >= 1)
            {
                carga_porcent = R16.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R17_Click(object sender, EventArgs e)
        {
            if (R17.Value >= 1)
            {
                carga_porcent = R17.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R18_Click(object sender, EventArgs e)
        {
            if (R18.Value >= 1)
            {
                carga_porcent = R18.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void R19_Click(object sender, EventArgs e)
        {
            if (R19.Value >= 1)
            {
                carga_porcent = R19.Value + "/30";
                rampa_d = 1;
                M4_DockDetail detalle = new M4_DockDetail();
                detalle.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rampa no tiene lotes cargados", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void label29_Click(object sender, EventArgs e)
        {
            piso_d = "Aduana";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void label32_Click(object sender, EventArgs e)
        {
            piso_d = "E2";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lbls2_Click(object sender, EventArgs e)
        {
            piso_d = "E1";
            M4_DetallePiso piso1 = new M4_DetallePiso();
            piso1.ShowDialog();
        }
    }
}
