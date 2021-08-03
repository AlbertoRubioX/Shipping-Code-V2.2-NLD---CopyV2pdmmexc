using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.Odbc;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.Net.Mail;


namespace WindowsFormsApplication1
{
    public partial class M1_Labeling : Form
    {
        public M1_Labeling()
        {
            InitializeComponent();

        }
        Datos Consultar = new Datos();
        public string no_cargar_1 = "Y";
        public string main_division, branch_loc, branch_loc2, fgstatus, printmessage, producto, abc, family, Mscycle, ester, wt, sql, duplicado, NPScycle, Opencycle1, Opencycle2, radiation; 
        public double mioh2, worqty,tarimas,wosm14,remainder,fgmioh,qtypartoh,unmps, branchoh, hanb4060, intransit, forecast, csxtarima,partial,partial1, pesoxcaja,peso, peso_completo,peso_parcial,book4,book5,book6 ;

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static int operation,wo_numb,qwster,qwnonster ;
           
        private void Form1_Load(object sender, EventArgs e)
        {

            toolStripStatusLabel1.Text = "Usuario=  " + GlobalVar.usuario;
        }
        public void lote_detenido(object sender, EventArgs e)
        {
            sql = "Select lote from no_cargar where lote= '" + txtlote.Text + "'";

        }

        private bool valida_op30()
        {
            OdbcConnection conexion = new OdbcConnection("Dsn=QDSN_AS400SYS;uid=shipmex;pwd=mexship");
            sql = @"SELECT WOCOPN FROM   KBM400MFG.FMWOSUM where WOLOT='" + lbllote.Text + "' AND  WOCO=" + GlobalVar.Compania + "";
            OdbcCommand comando = new OdbcCommand(sql, conexion);
            OdbcDataReader reader = null;
            try
            {
                conexion.Open();
                reader = comando.ExecuteReader();
                if (reader.Read())
                {
                    int op_number = Convert.ToInt32(reader["WOCOPN"]);
                    if (op_number >= 30)
                        return true;
                }
            }
            catch (OdbcException excep)
            {
                conexion.Close();
                MessageBox.Show(excep.Message);
            }
              
             
            return false;

        }
        public void btnok_Click(object sender, EventArgs e)
        {


            {
                    string lote1 = txtlote.Text;

                txttarima1.Text = "";
                txttarima2.Text = "";
                txtciclo.Text = "";
                txtlocalizacion.Text = "";

                // lote_detenido((object) sender, (EventArgs) e);
                double pesoxunit;
                int qpc_q;
                // Candena de Conexion a AS400
                OdbcConnection conexion = new OdbcConnection("Dsn=QDSN_AS400SYS;uid=shipmex;pwd=mexship");
                if (GlobalVar.Compania == 110)
                {
                   

                    sql = @"Select MRC400MFG.SF09270.SL9270,MRC400WEB.SF01120W.MOH120,KBM400MFG.FMWOSUM.WOPN,KBM400MFG.FMWOSUM.WOCOPN ,KBM400MFG.FMWOSUM.WOWONO,MRC400MFG.SF58040.BLOC1,MRC400MFG.SF16480.Q13480,MRC400WEB.SF00114.WOSM14,MRC400MFG.SF09270.IMQTOH,
                    MRC400MFG.SF09270.MO9270,MRC400MFG.SF09270.MD9270,MRC400MFG.SF09270.IMSTS,MRC400MFG.SF09270.BOOK4,MRC400MFG.SF09270.BOOK5,MRC400MFG.SF09270.MD9270,MRC400MFG.SF09270.BOOK6,MRC400MFG.SF09270.IMSHUN,MRC400MFG.SF16480.Q14480,
                    KBM400MFG.FMWOSUM.WOQTY,MRC400MFG.SF09270.BOOK4,MRC400MFG.SF09270.BOOK5,MRC400MFG.SF09270.BOOK6,MRC400MFG.SF09270.FCST,MRC400MFG.SF09270.IM1756,MRC400MFG.SF09270.IT1519,
                    MRC400MFG.SF09270.BS9270,MRC400MFG.SF09270.IMWHT,MRC400MFG.SF09270.IMSTCK,MRC400MFG.SF09270.CSPER,MRC400MFG.SF09270.TF9270,MRC400MFG.SF09270.IMABC,FSTCY1,FSTCY2,FSTCY3,FSTCY4,WTY580,STR040
                    from   MRC400WEB.SF01120W  RIGHT JOIN (MRC400MFG.SF18450  RIGHT JOIN (MRC400WEB.SF00114 right join(MRC400MFG.SF16480 right join (MRC400MFG.SF58042 RIGHT JOIN (MRC400MFG.SF58040 RIGHT JOIN (KBM400MFG.FMWOSUM LEFT join MRC400MFG.SF09270 on KBM400MFG.FMWOSUM.WOPN =MRC400MFG.SF09270.IMPN  )
                    ON MRC400MFG.SF58040.IMPN=KBM400MFG.FMWOSUM.WOPN) ON KBM400MFG.FMWOSUM.WOPN=MRC400MFG.SF58042.WOPN AND KBM400MFG.FMWOSUM.WOWONO=MRC400MFG.SF58042.WOWONO)on MRC400MFG.SF16480.IMPN=KBM400MFG.FMWOSUM.WOPN)
                    on MRC400WEB.SF00114.WOPN=KBM400MFG.FMWOSUM.WOPN) ON   MRC400MFG.SF58040.IMPN = MRC400MFG.SF18450.CP8450) ON MRC400MFG.SF18450.PP8450 = MRC400WEB.SF01120W.UNLK13 
                    where WOLOT='" + txtlote.Text + "' AND  KBM400MFG.FMWOSUM.WOCO=" + GlobalVar.Compania + "";

                }
                else if(GlobalVar.Compania == 686)
                {
                  

                    sql = @"SELECT MRC400MFG.SF09270.SL9270,MRC400WEB.SF01120W.MOH120,KBM400MFG.FMWOSUM.WOPN,KBM400MFG.FMWOSUM.WOCOPN ,KBM400MFG.FMWOSUM.WOWONO,MRC400WEB.SF58041W.BLOC1,MRC400MFG.SF16480.Q13480,MRC400WEB.SF00114.WOSM14,MRC400MFG.SF09270.IMQTOH,
                    MRC400MFG.SF09270.MO9270,MRC400MFG.SF09270.MD9270,MRC400MFG.SF09270.IMSTS,MRC400MFG.SF09270.BOOK4,MRC400MFG.SF09270.BOOK5,MRC400MFG.SF09270.MD9270,MRC400MFG.SF09270.BOOK6,MRC400MFG.SF09270.IMSHUN,MRC400MFG.SF16480.Q14480,
                    KBM400MFG.FMWOSUM.WOQTY,MRC400MFG.SF09270.BOOK4,MRC400MFG.SF09270.BOOK5,MRC400MFG.SF09270.BOOK6,MRC400MFG.SF09270.FCST,MRC400MFG.SF09270.IM1756,MRC400MFG.SF09270.IT1519,
                    MRC400MFG.SF09270.BS9270,MRC400MFG.SF09270.IMWHT,MRC400MFG.SF09270.IMSTCK,MRC400MFG.SF09270.CSPER,MRC400MFG.SF09270.TF9270,MRC400MFG.SF09270.IMABC, MRC400WEB.SF58041W.FSTCY2, MRC400WEB.SF58041W.FSTCY3 ,WTY580, MRC400WEB.SF58041W.SF580411
                    FROM   MRC400WEB.SF01120W  RIGHT JOIN (MRC400MFG.SF18450  RIGHT JOIN (MRC400WEB.SF00114 RIGHT JOIN(MRC400MFG.SF16480 RIGHT JOIN (MRC400MFG.SF58042 RIGHT JOIN (MRC400WEB.SF58041W RIGHT JOIN (KBM400MFG.FMWOSUM LEFT JOIN MRC400MFG.SF09270 ON KBM400MFG.FMWOSUM.WOPN =MRC400MFG.SF09270.IMPN  )
                    ON MRC400WEB.SF58041W.IMPN=KBM400MFG.FMWOSUM.WOPN) ON KBM400MFG.FMWOSUM.WOPN=MRC400MFG.SF58042.WOPN AND KBM400MFG.FMWOSUM.WOWONO=MRC400MFG.SF58042.WOWONO)ON MRC400MFG.SF16480.IMPN=KBM400MFG.FMWOSUM.WOPN)
                    ON MRC400WEB.SF00114.WOPN=KBM400MFG.FMWOSUM.WOPN) ON   MRC400WEB.SF58041W.IMPN = MRC400MFG.SF18450.CP8450) ON MRC400MFG.SF18450.PP8450 = MRC400WEB.SF01120W.UNLK13  
                    where WOLOT='" + txtlote.Text + "' AND  KBM400MFG.FMWOSUM.WOCO=" + GlobalVar.Compania + "";
                }

                OdbcCommand comando = new OdbcCommand(sql, conexion);
                OdbcDataReader reader = null;
                lblcmp.Visible = false;
                try
                {
                    conexion.Open();
                    reader = comando.ExecuteReader();
                    if (reader.Read())
                    {
                        //*****************************************************************Globales para su posterior uso en calculo de destino para productos
                        //*****************************************************************no esteriles *** VERIFICAR NULOS ANTES DE CONVERTIR

                        if (reader["BS9270"] is DBNull)
                        {
                            branchoh = 0;
                        }
                        else
                        {
                            branchoh = Convert.ToDouble(reader["BS9270"]);
                        }
                        if (reader["IM1756"] is DBNull)
                        {
                            hanb4060 = 0;
                        }
                        else
                        {
                            hanb4060 = Convert.ToDouble(reader["IM1756"]);
                        }
                        if (reader["IT1519"] is DBNull)
                        {
                            intransit = 0;
                        }
                        else
                        {
                            intransit = Convert.ToDouble(reader["IT1519"]);
                        }
                        if (reader["FCST"] is DBNull)
                        {
                            forecast = 0;
                        }
                        else
                        {
                            forecast = Convert.ToDouble(reader["FCST"]);
                        }
                        String wosm14_string = reader["WOSM14"].ToString();
                        if (reader["MOH120"] is DBNull)
                        {
                            mioh2 = 0;
                        }
                        else
                        {
                            mioh2 = Convert.ToDouble(reader["MOH120"]);
                        }



                        // ***************************************************************** Globales para uso general

                        main_division = reader["MD9270"].ToString();                // Using same formulas as R38 Main Division
                        if (reader["IMQTOH"] is DBNull)                             // Using same formulas as R38 Quantity of Part on Hand 
                        {
                            qtypartoh = 0;
                        }
                        else
                        {
                            qtypartoh = Convert.ToDouble(reader["IMQTOH"]);
                        }
                        // Using same formulas as R38 Qty Unavailable MPS
                        if (reader["IMSHUN"] is DBNull)
                        {
                            unmps = 0;
                        }
                        else
                        {
                            unmps = Convert.ToDouble(reader["IMSHUN"]);
                        }
                        fgstatus = reader["IMSTS"].ToString();                      // Using same formulas as R38 Status Code

                        if (reader["BOOK4"] is DBNull)
                            book4 = 0;
                        else
                            book4 = Convert.ToDouble(reader["BOOK4"]);                  // Using same formulas as R38 BOOK4

                        if (reader["BOOK5"] is DBNull)
                            book5 = 0;
                        else
                            book5 = Convert.ToDouble(reader["BOOK5"]);                  // Using same formulas as R38 BOOK5

                        if (reader["BOOK6"] is DBNull)
                            book6 = 0;
                        else
                            book6 = Convert.ToDouble(reader["BOOK6"]);                  // Using same formulas as R38 BOOK6

                        if (reader["MO9270"] is DBNull)
                            fgmioh = 0;
                        else
                            fgmioh = Convert.ToDouble(reader["MO9270"]);                // Using same formulas as R38 Months On Hand

                        producto = reader["WOPN"].ToString();                       // WO Tray
                        producto = producto.TrimEnd();
                        family = reader["TF9270"].ToString();                       // Tray Family
                        abc = reader["IMABC"].ToString();                           // ABC Code
                        if (GlobalVar.Compania == 110)
                        { 
                            NPScycle = reader["FSTCY1"].ToString();                     // NPS Cycle
                        }
                        Mscycle = reader["FSTCY2"].ToString();                      // Midwest Cycle

                        Opencycle1 = reader["FSTCY3"].ToString();                   // Open Cycle 1
                        if (GlobalVar.Compania == 110)
                        {
                            Opencycle2 = reader["FSTCY4"].ToString();                   // Open Cycle 2
                        }
                        wt = reader["WTY580"].ToString();                           // WT
                        wo_numb = Convert.ToInt32(reader["WOWONO"]);
                        if (GlobalVar.Compania == 110)
                        {
                            ester = reader["str040"].ToString();                        // Esterilizadora (de acuerdo a branches)
                        }
                        else if (GlobalVar.Compania == 686)
                        {
                            ester = reader["SF580411"].ToString();
                        }
                        if (reader["CSPER"] is DBNull)
                            csxtarima = 0;
                        else
                            csxtarima = Convert.ToDouble(reader["CSPER"]);              // Cajas por tarima
                        if (reader["WOQTY"] is DBNull)
                            worqty = 0;
                        else
                            worqty = Convert.ToDouble(reader["WOQTY"]);                 // Cantidad de cs en WO
                        if (reader["WOCOPN"] is DBNull)
                            operation = 0;
                        else
                            operation = Convert.ToInt32(reader["WOCOPN"]);
                        if (reader["IMSTCK"] is DBNull)
                            qpc_q = 0;
                        else
                            qpc_q = Convert.ToInt32(reader["IMSTCK"]);
                        // obteniendo cantidad de kits x caja
                        if (reader["IMWHT"] is DBNull)
                            pesoxunit = 0;
                        else
                            pesoxunit = Convert.ToDouble(reader["IMWHT"]);              // obteniendo el peso del kit

                        if (reader["Q13480"] is DBNull)
                            qwster = 0;
                        else
                            qwster = Convert.ToInt32(reader["Q13480"]);                 // QW Esteril

                        if (reader["Q14480"] is DBNull)
                            qwnonster = 0;
                        else
                            qwnonster = Convert.ToInt32(reader["Q14480"]);              // QW NO esteril

                        branch_loc = reader["BLOC1"].ToString();                    // Branch
                        branch_loc2 = reader["SL9270"].ToString();                    // Branch2
                        branch_loc2= branch_loc2.TrimEnd();
                        conexion.Close();
                        txtlote.Text = "";
                        if (family == "CMP")
                        {
                            worqty = worqty / qpc_q;
                            worqty = Math.Ceiling(worqty);
                            lblcmp.Visible = true;
                        }

                        // Calculando cuantas tarimas por WO

                        if (wosm14_string == "")
                        {
                            wosm14 = 0;
                        }
                        else
                        {
                            wosm14 = Convert.ToDouble(wosm14_string);
                        }
                        partial = worqty / csxtarima;
                        if (partial < 1)
                        {
                            tarimas = 1;
                        }
                        else
                        {
                            tarimas = Math.Ceiling(partial);
                        }

                        remainder = worqty % csxtarima;                               // obteniendo cajas que iran en tarima parcial
                        pesoxcaja = pesoxunit * qpc_q;                                // obteniendo el peso de caja al multiplicar [peso del kit * cantidad de kits ]
                        peso_completo = pesoxcaja * csxtarima;                        // obteniendo peso de tarimas completas mult [peso de caja * cantidad de cajas por tarima]
                        peso_parcial = remainder * pesoxcaja;                         // obteniendo el peso de tarima parcial ( cantidad de cajas ( remainder) * pesoxcaja)
                        if (operation == 40 || operation == 22)
                        {

                        }
                        else
                        {
                            //send_email((object)sender, (EventArgs)e);
                        }

                    }
                    else
                    {
                        MessageBox.Show("Lote Invalido");
                        goto jump1;

                    }
                }
                catch (OdbcException excep)
                {
                    conexion.Close();
                    MessageBox.Show(excep.Message);
                    goto jump1;
                }


                destination_revision((object)sender, (EventArgs)e);
            jump1:

                lbllote.Visible = true;
                lote_lbl.Visible = true;
                lbllote.Text = lote1;
                lblinfo.Text = "Ster:" + printmessage + Environment.NewLine + "Peso x caja:" + pesoxcaja + Environment.NewLine + "Cs x Tarima:" + csxtarima;
                txttarima2.Text = tarimas.ToString();
                if (txttarima2.Text == "1")
                {
                    txttarima1.Text = "1";
                }

                if (GlobalVar.Compania == 110)
                {
                    if (family == "CMP")
                    {
                        txtlocalizacion.Text = "S14";
                    }
                    else if (printmessage == "LAREDO" && txtciclo.Text == "LR")
                    {
                        txtlocalizacion.Text = "S3";
                    }
                    else if (printmessage == "LAREDO-HOT!" && txtciclo.Text == "LR")
                    {
                        txtlocalizacion.Text = "S1";
                    }

                    else if (printmessage == "STERIGENICS" && txtciclo.Text == "65")
                    {
                        txtlocalizacion.Text = "S13";
                    }

                    else if (printmessage == "NPS")
                    {
                        txtlocalizacion.Text = "S7";
                    }
                    else if (printmessage == "LAREDO" && txtciclo.Text == "LHM")
                    {
                        txtlocalizacion.Text = "S16";
                    }
                    else if (printmessage == "LAREDO" && txtciclo.Text == "THD")
                    {
                        txtlocalizacion.Text = "S15";
                    }
                    else if (printmessage == "JACKSON")
                    {
                        txtlocalizacion.Text = "S2";
                    }
                    else if (txtciclo.Text == "NO-ESTERIL" && branch_loc2 == "B54" && wt != "1")
                    {
                        txtlocalizacion.Text = "S5";
                    }
                    else if (txtciclo.Text == "NO-ESTERIL" && branch_loc2 == "B28" && wt != "1")
                    {
                        txtlocalizacion.Text = "S12";
                    }
                    else if (txtciclo.Text == "NO-ESTERIL" && branch_loc2 == "B33" && wt != "1")
                    {
                        txtlocalizacion.Text = "S11";
                    }
                    else if (txtciclo.Text == "NO-ESTERIL" && branch_loc2 == "B89" && wt != "1")
                    {
                        txtlocalizacion.Text = "S9";
                    }
                    else if (txtciclo.Text == "NO-ESTERIL" && branch_loc2 == "B43" && wt != "1")
                    {
                        txtlocalizacion.Text = "S8";
                    }
                    else if (family == "CUS")
                    {
                        txtlocalizacion.Text = "S3";
                    }
                    else if (txtciclo.Text == "NO-ESTERIL" && branch_loc2 == "B26" && wt != "1")
                    {
                        txtlocalizacion.Text = "S6";
                    }
                    else if (txtciclo.Text == "NOEST HOT" || txtciclo.Text == "NO-ESTERIL")
                    {
                        txtlocalizacion.Text = "S4";
                    }

                    else if (txtciclo.Text == "CDS WT")
                    {
                        txtlocalizacion.Text = "S4";
                    }
                }
                else if(GlobalVar.Compania == 686)
                {
                    if (family == "CMP")
                    {
                        txtlocalizacion.Text = "S2";
                    }
                    else if (family == "CUS")
                    {
                        txtlocalizacion.Text = "S1";
                    }
                }
                
            }
        }
            
        

        private void sendtoprinter(object sender, EventArgs e)
        {
            
            string rampa1="0";
            PrintDocument p = new PrintDocument();
            p.PrintPage += delegate(object sender1, PrintPageEventArgs e1)
            {
               
                    if (txtlocalizacion.Text.Substring(0, 3) == "EMP")
                    {
                        rampa1 = "EMP";
                    }
                
                else
                {
                    rampa1 = txtlocalizacion.Text;
                }
                e1.Graphics.DrawString(printmessage /*+ Environment.NewLine +  "R:" + rampa1   + " P:" + pesoxcaja*/, new Font("Arial Black", 26), new SolidBrush(Color.Black), new RectangleF(0, 0, p.DefaultPageSettings.PrintableArea.Width, p.DefaultPageSettings.PrintableArea.Height));
                e1.Graphics.DrawString(rampa1 + "  P:" + pesoxcaja, new Font("Arial Black", 20), new SolidBrush(Color.Black), 7, 35);
                 
            };
            try
            {

//no imprimir                p.Print();
            }
            catch (Exception ex)
            {
                throw new Exception("Exception Occured While Printing", ex);
            }


        } 

        public void destination_revision(object sender, EventArgs e)
     
       {
            
            if (qwster == 0 && qwnonster == 1)
            {
                destino_noesteril((object)sender, (EventArgs)e);
            }
            else if (qwster == 1 && qwnonster == 0)
            {
                destino_esteril((object)sender, (EventArgs)e);
            }
            else
            {
            regreso1:
                printmessage = "ERROR" +  Environment.NewLine + producto;
               //testing , no imprimiendo  sendtoprinter((object)sender, (EventArgs)e);
                string input = Microsoft.VisualBasic.Interaction.InputBox("Que tipo de boleta tiene?" + Environment.NewLine + "(Seleccione Opcion 1 o 2) " + Environment.NewLine + Environment.NewLine +  "1 - Boleta Amarilla" + Environment.NewLine + "2 - Boleta Verde", "QW No Encontrado, proceso debe verificarse manualmente", "1");
                if (input == "1") 
                {
                    
                    destino_esteril((object)sender, (EventArgs)e);
                }
                else if (input == "2") 
                {
                    destino_noesteril((object)sender, (EventArgs)e);
                }
                else
                {
                    MessageBox.Show("Seleccione Valor Correcto");
                    goto regreso1;
                }
            }
           
        }

        public  void destino_noesteril(object sender, EventArgs e)
        {
            // Tomando calculos del reporte MPower 38 "Green Tag Prioritization" para definir prioridades
            Double MOH;
            if (forecast == 0)
                {
                    MOH = 999.9;
                }
            else
                {
                    MOH = (branchoh - hanb4060 - intransit) / forecast; 
                }
            if (MOH == 999.9 && wt.TrimEnd () == "1")
                {
                    printmessage = "NOEST HOT";
                }
            else if (MOH <= 0.75)
                {
                    printmessage = "NOEST HOT";
                }
            else
                {
                   printmessage  = "NO-ESTERIL";
                }
            
                txtciclo.Text = printmessage ;
            
            
        }

        public void destino_esteril(object sender, EventArgs e)
        {
            // mimic same R38 criterias
            Double OHqty;
            OHqty = qtypartoh - unmps;

            double RevMIOH;
            // Getting RevMIOH according to R38 criterias
            if (forecast > 0 && family != "CMP")
            {
                RevMIOH = (wosm14 + OHqty) / forecast;
            }
            else if ((fgstatus == "3" && book6 != 0) || (fgstatus == "3" && book5 != 0) || (fgstatus == "3" && book4 != 0))
            {
                RevMIOH = OHqty / ((book6 + book5 + book4) / 3);
            }
            else
            {
                RevMIOH = fgmioh;
            }

            // Calculating destination R38 Formulas taken from DB

            string MidwestCycle = Mscycle;
            if (MidwestCycle == null || MidwestCycle == "")
            {
                MidwestCycle = "";
            }
            else
            {
                MidwestCycle = Mscycle.Substring(0, 2);
            }

            if (GlobalVar.Compania == 110)
            {

                if ((family != "CMP" && RevMIOH <= 0.8 && abc != "N" && MidwestCycle == "MR") || (family == "CMP" && mioh2 <= 1.25 && abc != "N" && MidwestCycle == "MR") || (MidwestCycle == "MR" && abc == "N" && wt.TrimEnd() == "1") || (MidwestCycle == "MR" && main_division == "20" && wt.TrimEnd() == "1"))
                {
                    printmessage = "LAREDO-HOT!";
                }
                else if (Opencycle1.Contains("ELP") == true && ester == "STERIGENICS    ")
                {
                    printmessage = "STERIGENICS";
                }

                else if (family != "CMP" && fgstatus == "3" && RevMIOH <= 0.8 && MidwestCycle == "MR")
                {
                    printmessage = "LAREDO-HOT!";
                }
                else if ((ester.TrimEnd() == "JACKSON" && RevMIOH <= 1.00 && abc != "N" && family != "CMP") || (ester.TrimEnd() == "NPS LRD" && RevMIOH <= 1.25 && abc != "N" && family != "CMP") || (ester.TrimEnd() == "SYNERGY" && RevMIOH <= 1.25 && abc != "N" && family != "CMP") || (wt.Trim() == "1" && MidwestCycle != ""))
                {
                    printmessage = "LAREDO";
                }
                else if (main_division == "20" && (producto == "DYNDBARD1" || producto == "DYNDBARD3" || producto == "DYNDBARD6" || producto == "DYNDBARD7" || producto == "DYNDBARD8" || producto == "DYNDBARD9" || producto == "DYNDBARD10" || producto == "DYNDBARD11" || producto == "DYNDBARD12" || producto == "DYNDBARD13" || producto == "DYNDBARD14" || producto == "DYNDBARD15" || producto == "DYNDBARD16" || producto == "DYNDBARD17" || producto == "DYNDCORAM1" || producto == "DYNDCORAM2" || producto == "DYNDCORAM3" || producto == "DYNDCORAM4" || producto == "DC5090LF" || producto == "DC5040LF" || producto == "DC5030LF"))
                {
                    printmessage = "LAREDO";
                }
                else if ((ester.TrimEnd() == "NPS WKG" && RevMIOH <= 0.8 && MidwestCycle == "MR" && family != "CMP") || (ester.TrimEnd() == "NPS WKG" && MidwestCycle == "MR" && family != "CMP" && wt.TrimEnd() == "1" && RevMIOH <= 0.8))
                {
                    printmessage = "LAREDO-HOT!";
                }
                else if ((ester.TrimEnd() == "NPS WKG" && family == "CMP") || (ester.TrimEnd() == "NPS WKG" && family != "CMP" && RevMIOH <= 1.25))
                {
                    printmessage = "LAREDO";
                }
                else if (ester.TrimEnd() == "NPS WKG" && family != "CMP" && RevMIOH > 1.25)
                {
                    printmessage = "NPS";
                }
                else if (fgstatus != "3" && forecast <= 5 && abc != "N")
                {
                    printmessage = "LAREDO";
                }
                else if (ester.TrimEnd() == "NPS LRD")
                {
                    printmessage = "NPS";
                }
                else if (Mscycle == "" && Opencycle1 == "" && Opencycle2 == "" && NPScycle == "")
                {
                    printmessage = "CHECK";
                }
                else
                {
                    printmessage = ester.TrimEnd();
                }
            }

            else if (GlobalVar.Compania ==686)
            {
                if ((family != "CMP" && RevMIOH <= 0.8 && abc != "N" && MidwestCycle == "MR") || (family == "CMP" && mioh2 <= 1.25 && abc != "N" && MidwestCycle == "MR") || (MidwestCycle == "MR" && abc == "N" && wt.TrimEnd() == "1") || (MidwestCycle == "MR" && main_division == "20" && wt.TrimEnd() == "1"))
                {
                    printmessage = "STERIGENICS ONTARIO";
                }
                else if (ester.TrimEnd() == "STERIGENICS ONTARIO" && MidwestCycle == "MR")
                {
                    printmessage = "STERIGENICS ONTARIO";
                }
                else if (Mscycle == "" && Opencycle1 == "")
                {
                    printmessage = "CHECK";
                }
                else
                {
                    printmessage = ester.TrimEnd();
                }
            }

            definir_ciclo((object)sender, (EventArgs)e);

        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void revisar_destino(object sender, EventArgs e)
            
        {
            string localizacion="0";
            string hora = DateTime.Now.ToString("hh:mm:ss tt");
            string date1 = DateTime.Now.ToString("MM/dd/yy");

            localizacion  = txtlocalizacion.Text;

            Consultar.InsertarRegQuiebre(7, lbllote.Text, Convert.ToInt32(wo_numb), producto, Convert.ToInt32(txttarima1.Text), Convert.ToInt32(txttarima2.Text), 0, localizacion, printmessage, txtciclo.Text, GlobalVar.nombre_user, GlobalVar.Compania);

                txtlocalizacion.Text = "";
                txtlote.Text = "";
                txttarima1 .Text = "";
                txttarima2.Text = "";
                lblinfo.Text = "";
                txtciclo.Text = "";
                lblcmp.Visible = false;
                lbllote.Text = "Lote";
                lbllote.Visible = false;
                lote_lbl.Visible = false;
                txtlote.Focus();

        }

        private void txtlote_KeyPress(object sender, KeyPressEventArgs e)
      {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                txttarima1.Focus();
            
            }
        }

        private void txttarima1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
               
                button2_Click((object)sender, (EventArgs)e);
            }

        }

        private void txtlocalizacion_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {

                button2_Click((object)sender, (EventArgs)e);
            }
        }

        private void no_cargar(object sender, EventArgs e)
        {
                
                if (Consultar.M1LabelLoteDetenido(txtlote.Text) == true)
                {
                    no_cargar_1 = "N";
                }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txttarima1.Text) <= Convert.ToInt32(txttarima2.Text))
            {

                if (lbllote.Text == "Lote :" || txttarima1.Text == "" || txttarima2.Text == "" || txtciclo.Text == "" || txtlocalizacion.Text == "")
                {
                    MessageBox.Show("Favor de llenar la informacion comp", "NO CARGAR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {

                    no_cargar((object)sender, (EventArgs)e);
                    if (no_cargar_1 == "N")
                    {

                        MessageBox.Show("Lote esta marcado como 'NO CARGAR', notificar a supervisor", "NO CARGAR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    if (family == "CMP")
                    {
                        if (txtlocalizacion.Text == "S14" || txtlocalizacion.Text == "E1" && GlobalVar.Compania == 110)
                        {
                            send_to_printer((object)sender, (EventArgs)e);
                        }
                        else if (txtlocalizacion.Text == "S1" || txtlocalizacion.Text == "E1" && GlobalVar.Compania == 686)
                        {
                            send_to_printer((object)sender, (EventArgs)e);
                        }
                        else
                        {
                            MessageBox.Show("Producto CMP debe ser dirigido a localizacion 'S10' o 'E1'", "PRODUCTO CMP", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        send_to_printer((object)sender, (EventArgs)e);
                    }
                }
            }
            else
            {
                MessageBox.Show("Cantidad de tarimas no puede ser Mayor", "Tarima", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        private void send_to_printer(object sender, EventArgs e)
        {
            if (GlobalVar.Compania == 110)
            {
                if (txtlocalizacion.Text == "S1" || txtlocalizacion.Text == "S2" || txtlocalizacion.Text == "S3" || txtlocalizacion.Text == "S4" || txtlocalizacion.Text == "S5" || txtlocalizacion.Text == "S6" || txtlocalizacion.Text == "S7" || txtlocalizacion.Text == "S8" || txtlocalizacion.Text == "S9" || txtlocalizacion.Text == "S10" || txtlocalizacion.Text == "S11" || txtlocalizacion.Text == "S12" || txtlocalizacion.Text == "S13" || txtlocalizacion.Text == "S14" || txtlocalizacion.Text == "S15" || txtlocalizacion.Text == "S16" || txtlocalizacion.Text == "E1" || txtlocalizacion.Text == "E2")
                {
                    try
                    {
                        no_duplicado(txtlote.Text, txttarima1.Text, txttarima2.Text, 0);
                        if (duplicado == "N")
                        {
                            //no imprimiendo  sendtoprinter((object)sender, (EventArgs)e);
                            revisar_destino((object)sender, (EventArgs)e);


                        }
                        else
                        {
                            MessageBox.Show(" Tarima " + txttarima1.Text + "/" + txttarima2.Text + " del lote " + lbllote.Text + " ya esta registrada");
                        }

                    }
                    catch (OdbcException excep1)
                    {

                        MessageBox.Show(excep1.Message);
                    }

                }
                else
                {
                    MessageBox.Show("El valor en el campo localizacion no es correcto", "Revisar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if(GlobalVar.Compania == 686)
            {
                if (txtlocalizacion.Text == "S1" || txtlocalizacion.Text == "S2"  || txtlocalizacion.Text == "E1")
                {
                    try
                    {
                        no_duplicado(txtlote.Text, txttarima1.Text, txttarima2.Text, 0);
                        if (duplicado == "N")
                        {
                            //valida as400 Operacion 30 [v1.0.0.3]
                            if(txttarima1.Text == txttarima2.Text)
                            {
                                if(valida_op30())
                                    revisar_destino((object)sender, (EventArgs)e);
                                else
                                    MessageBox.Show("Lote sin transacción 30 en as400 ", "Imposible registrar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                //no imprimiendo  sendtoprinter((object)sender, (EventArgs)e);
                                revisar_destino((object)sender, (EventArgs)e);
                            }
                            
                        }
                        else
                        {
                            MessageBox.Show(" Tarima " + txttarima1.Text + "/" + txttarima2.Text + " del lote " + lbllote.Text + " ya esta registrada");
                        }

                    }
                    catch (OdbcException excep1)
                    {

                        MessageBox.Show(excep1.Message);
                    }

                }
                else
                {
                    MessageBox.Show("El valor en el campo localizacion no es correcto", "Revisar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

    }

        private void txtciclo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {
                txtlocalizacion.Focus();

            }

        }

        private void no_duplicado(string lotev, string  tarimav, string  dev,int duplicate)
        {
            if(Consultar.M1LabelLoteDuplicado(lbllote.Text, txttarima1.Text, txttarima2.Text, GlobalVar.Compania) ==  true)                
                {
                    duplicado = "Y";
                }
                else
                {
                    duplicado = "N";
                }          
        }

        private void send_email(object sender, EventArgs e)
        {
            if (GlobalVar.Compania == 110)
            {
                try
                {
                    MailMessage mail = new MailMessage("PDMShipping@medline.com", "PDMShipping@medline.com");
                    mail.CC.Add("FBRodriguez@medline.com");
                    SmtpClient client = new SmtpClient();
                    client.Port = 25;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Host = "muncasarray1.medline.com";
                    mail.Subject = "[Shipping System] Missing PRQOH/NS Capture " + txtlote.Text + "";
                    mail.Body = "  Please review WO# " + wo_numb + " Lot # " + txtlote.Text + Environment.NewLine + "  WO is entering Shipping Area but current operation is not PRQOH/NS";
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            else if(GlobalVar.Compania == 686)
            {
                try
                {
                    MailMessage mail = new MailMessage("MXLShipping@medline.com", "MXLShipping@medline.com");
                    mail.CC.Add("FBRodriguez@medline.com");
                    SmtpClient client = new SmtpClient();
                    client.Port = 25;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Host = "muncasarray1.medline.com";
                    mail.Subject = "[Shipping System] Missing PRQOH/NS Capture " + txtlote.Text + "";
                    mail.Body = "  Please review WO# " + wo_numb + " Lot # " + txtlote.Text + Environment.NewLine + "  WO is entering Shipping Area but current operation is not PRQOH/NS";
                    client.Send(mail);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }


        }

        private void definir_ciclo(object sender, EventArgs e)
        {
            if (GlobalVar.Compania == 110)
            {
                if (Mscycle == "" && NPScycle == "" && Opencycle1 == "" && Opencycle2 == "")
                {
                    txtciclo.Text = "Check";
                }
                else
                {
                    if (Mscycle.Contains("JHD") == true && ester == "JACKSON        ")
                    {
                        txtciclo.Text = "JHD";
                    }

                    else if (Opencycle1.Contains("ELP") == true && ester == "STERIGENICS    ")
                    {
                        txtciclo.Text = "65";
                    }

                    else if (Mscycle.Substring(0, 2) == "MR" || Opencycle1.Contains("MR") == true || Opencycle2.Contains("MR") == true)
                    {
                        txtciclo.Text = "LR";
                    }
                    else if (Mscycle.Contains("THD") == true || Opencycle1.Contains("THD") == true || Opencycle2.Contains("THD") == true)
                    {
                        txtciclo.Text = "THD";
                    }
                    else if (Mscycle.Contains("LHM") == true || Opencycle1.Contains("LHM") == true || Opencycle2.Contains("LHM") == true)
                    {
                        txtciclo.Text = "LHM";
                    }
                    else
                    {
                        txtciclo.Text = "Check";
                    }
                }

            }
            else if (GlobalVar.Compania == 686)
            {
                if (Mscycle == "" && Opencycle1 == "")
                {
                    txtciclo.Text = "Check";
                }
                else
                {
                    if (Mscycle.Substring(0, 2) == "MR" && Opencycle1.Substring(3, 2) == "65")
                    {
                        txtciclo.Text = "65";
                    }

                    else if (Mscycle.Substring(0, 2) == "MR" && Opencycle1.Substring(0, 2) == "54")
                    {
                        txtciclo.Text = "54";
                    }
                    else if (Mscycle.Substring(0, 2) == "MR" && Opencycle1.Substring(10, 2) == "65")
                    {
                        txtciclo.Text = "65";
                    }
                    else if (Mscycle.Substring(0, 2) == "MR" || Opencycle1.Contains("54") == true && Opencycle1.Contains("65") == true)
                    {
                        txtciclo.Text = "65";
                    }
                    else
                    {
                        txtciclo.Text = "Check";
                    }
                }
            }
        }

        private void txtlote_Leave(object sender, EventArgs e)
        {
            String lote1;
            if (txtlote.TextLength >= 9)
            {
                lote1 = txtlote.Text;
                if (lote1.Substring(8, 1) == "A" || lote1.Substring(8, 1) == "a" || lote1.Substring(8, 1) == "z" || lote1.Substring(8, 1) == "Z" || lote1.Substring(8, 1) == "b" || lote1.Substring(8, 1) == "B" || lote1.Substring(8, 1) == "c" || lote1.Substring(8, 1) == "C" || lote1.Substring(8, 1) == "x" || lote1.Substring(8, 1) == "X" || lote1.Substring(8, 1) == "y" || lote1.Substring(8, 1) == "Y")
                {

                    txtlote.Text = lote1.Substring(0, 9);
                    btnok_Click((object)sender, (EventArgs)e);


                }
                else
                {

                    txtlote.Text = lote1.Substring(0, 8);
                    btnok_Click((object)sender, (EventArgs)e);
                    
                }

            }
            else if (txtlote.TextLength  == 8)
            {
                btnok_Click((object)sender, (EventArgs)e);
            }
        }


        private void txttarima2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (Char)Keys.Enter)
            {

                button2_Click((object)sender, (EventArgs)e);
            }
        }

     }
         
}
