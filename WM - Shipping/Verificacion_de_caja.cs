using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data.OleDb;
using System.Net.Mail;

namespace WindowsFormsApplication1
{
    public partial class Verificacion_de_caja : Form
    {
        Datos Consultar = new Datos();
        public static string n_caja = "",pizq, pder,techo,piso,cambiar;
        public int idcarga, verificar = 0,cambiar2=0,puerto;
        public string server, smtp_user, mapa, discre,checklist,mapamerge,_NoCaja,mapaexcel,mapalayout;
        public Verificacion_de_caja()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtcaja.Text == "")
            {
                MessageBox.Show("Introducir numero de caja", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if(GlobalVar.Compania == 686)
                {
                    Consultar.ConfigSMTP(ref server, ref smtp_user, ref puerto, ref mapa, ref discre, ref checklist, ref mapamerge,ref mapaexcel,ref mapalayout);
                }

                if (verificar <= 4)
                {
                    MessageBox.Show("Es necesario verificar cada seccion de la caja", "Verificar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (si5.Checked == true && GlobalVar.Compania == 110)
                {
                    crear_pdf(sender, e);
                    Consultar.VerificacionCajaid(Convert.ToInt32(txtid.Text), GlobalVar.Compania);
                    Attachment item = new Attachment(@"\\Mexfp1\Medline\Dept. Recibo & Embarques\Mapas electronicos\Checklist\ChecklistSqltest\Verificacion_#" + txtid.Text + "_cambio.pdf");
                    MailMessage message = new MailMessage("PDMShipping@medline.com", "PDMShipping@medline.com");
                    //message.CC.Add("ctorres@medline.com");
                    SmtpClient client = new SmtpClient
                    {
                        Port = 25,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Host = "muncasarray1.medline.com"
                    };
                    message.Priority = MailPriority.High;
                    message.Subject = "Shipping SYSTEM       CAJA DANADA: " + txtcaja.Text;
                    message.Body = "Se ha encontrado una seccion danada en la caja # " + this.txtcaja.Text + "  (ver adjunto para detalles)";
                    message.Attachments.Add(item);
                    client.Send(message);
                    this.Close();
                }
                else if (si5.Checked == false && (GlobalVar.Compania == 110))
                {
                    crear_pdf(sender, e);
                    if (M3_Map.reasignar == "Y")
                    {
                        Consultar.VerificacionCaja(txtcaja.Text, Convert.ToInt32(txtid.Text), GlobalVar.Compania);
                    }
                    this.Close();
                }
                else if (si5.Checked == true && (GlobalVar.Compania == 686))
                {
                    if (!string.IsNullOrEmpty(server))
                    {
                        crear_pdf(sender, e);
                        Consultar.VerificacionCajaid(Convert.ToInt32(txtid.Text), GlobalVar.Compania);
                        //Attachment item = new Attachment(@"\\mxcprdfp1\Software_MXC\ShippingSystem\Mapas_electronicos\Checklistsqltest\Verificacion_#" + txtid.Text + "_cambio.pdf");
                        Attachment item = new Attachment(checklist + "\\Verificacion_#" + txtid.Text + "_cambio.pdf");
                        MailMessage message = new MailMessage(smtp_user,smtp_user);
                        //message.CC.Add("algonzalez@medline.com");
                        SmtpClient client2 = new SmtpClient
                        {
                            Port = puerto,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Host =server
                        };
                        message.Priority = MailPriority.High;
                        message.Subject = "Shipping SYSTEM       CAJA DANADA: " + txtcaja.Text;
                        message.Body = "Se ha encontrado una seccion danada en la caja # " + txtcaja.Text + "  (ver adjunto para detalles)";
                        message.Attachments.Add(item);
                        client2.Send(message);
                        this.Close();
                    }
                }
                else if (si5.Checked == false && (GlobalVar.Compania == 686))
                {
                    crear_pdf(sender, e);
                    if (M3_Map.reasignar == "Y")
                    {
                        Consultar.VerificacionCaja(txtcaja.Text, Convert.ToInt32(txtid.Text), GlobalVar.Compania);
                    }
                    this.Close();
                }

            }
        }

        private void crear_pdf(object sender, EventArgs e)
        {

            //****************************************************************************************************************************************
            // CREANDO ARCHIVO PDF DE REVISION DE CAJA

            string comment1="";
            if (txtcomentario.Text == "")
            {
                comment1 = "N/A";
            }
            else
            {
                comment1 = txtcomentario.Text;
            }
            string hora1 = DateTime.Now.ToString("hh:mm tt");
            string date1 = DateTime.Now.ToString("MM/dd/yy");

            
            //****************************************************************************************************************************************
            // RUTAS PARA GUARDAR PDF, SI SE CAMBIA O NO SE CAJA CAMBIA EL PATH


            Document doc = new Document(PageSize.LETTER);
            PdfWriter writer;
            if (si5.Checked == true && (GlobalVar.Compania == 110))
            {
                writer = PdfWriter.GetInstance(doc, new FileStream(@"\\Mexfp1\Medline\Dept. Recibo & Embarques\Mapas electronicos\Checklist\ChecklistSqltest\Verificacion_#" + this.txtid.Text + "_cambio.pdf", FileMode.Create));
            }
            else if (GlobalVar.Compania == 110)
            {
                writer = PdfWriter.GetInstance(doc, new FileStream(@"\\Mexfp1\Medline\Dept. Recibo & Embarques\Mapas electronicos\Checklist\ChecklistSqltest\Verificacion_#" + this.txtid.Text + ".pdf", FileMode.Create));
            }
            else if (si5.Checked == true && (GlobalVar.Compania == 686))
            {
                writer = PdfWriter.GetInstance(doc, new FileStream(checklist + "\\Verificacion_#" + this.txtid.Text + "_cambio.pdf", FileMode.Create));
            }
            else if (GlobalVar.Compania == 686)
            {
                writer = PdfWriter.GetInstance(doc, new FileStream(checklist + "\\Verificacion_#" + this.txtid.Text + ".pdf", FileMode.Create));
            }

            //****************************************************************************************************************************************
            // DECLARANDO FONTS Y PEGANDO LOGO DE ENCABEZADO

            doc.Open();
            iTextSharp.text.Font _title1 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 13, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            iTextSharp.text.Font _title2 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL , BaseColor.BLACK);
            iTextSharp.text.Font _title3 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.UNDERLINE , BaseColor.BLACK);
            iTextSharp.text.Font _title4 = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 16, iTextSharp.text.Font.BOLD, BaseColor.BLACK);
            
            if (File.Exists(Path.Combine(Application.StartupPath, @"logo\logo_m2.png")))
            {
                iTextSharp.text.Image medlinelogo = iTextSharp.text.Image.GetInstance(Path.Combine(Application.StartupPath, @"logo\logo_m2.png"));
                medlinelogo.BorderWidth = 0;
                medlinelogo.Alignment = Element.ALIGN_CENTER;
                float percentage = 0.0f;
                percentage = 160 / medlinelogo.Width;
                medlinelogo.ScalePercent(percentage * 400);
                doc.Add(medlinelogo);
            }
           
            doc.Add(new Paragraph(" ", _title2));  // agregando espacios en blanco
            doc.Add(new Paragraph(" ", _title2));  // agregando espacios en blanco

            
            
            //****************************************************************************************************************************************
            // AGREGANDO IMAGEN DE AYUDA VISUAL DE CAJA


            if (File.Exists(Path.Combine(Application.StartupPath, @"logo\caja2.gif")))
            {
                iTextSharp.text.Image medlinelogo = iTextSharp.text.Image.GetInstance(Path.Combine(Application.StartupPath, @"logo\caja2.gif"));
                medlinelogo.BorderWidth = 0;
                medlinelogo.Alignment = Element.ALIGN_CENTER ;
                float percentage = 0.0f;
                percentage = 100 / medlinelogo.Width;
                medlinelogo.ScalePercent(percentage * 400);
                doc.Add(medlinelogo);
            }
            doc.Add(new Paragraph(" ", _title2));  // agregando espacios en blanco
            doc.Add(new Paragraph(" ", _title2));  // agregando espacios en blanco

            //****************************************************************************************************************************************
            // GENERANDO TABLA PARA PONER INFORMACION

         
            
                    PdfPTable tblPdf1 = new PdfPTable(2);
                    tblPdf1.WidthPercentage = 100;
                    tblPdf1.HorizontalAlignment = Element.ALIGN_CENTER;

                    //Ccolumnas de archivo
                    PdfPCell cltitulo = new PdfPCell(new PdfPCell(new Phrase("Mapa# " + txtid.Text + "  /  Caja = " + txtcaja.Text  , _title4)) { Colspan = 2 });                   
                    cltitulo.BackgroundColor = new BaseColor(255, 253, 147);
                    cltitulo.BorderWidth = 1f;
                    cltitulo.HorizontalAlignment = 1;
                    tblPdf1.AddCell(cltitulo);

                    //****************************************************************************************************************************************
                    // AGREGANDO ENCABEZADOS ( 2DO ROW)

                    


                    // Agregando filas = Seccion Fecha
                    PdfPCell clfecha = new PdfPCell(new PdfPCell(new Phrase("Fecha: " + date1 + "    Hora: " + hora1, _title2)) { Colspan = 2 });
                    clfecha.BorderWidth = .5f;
                    clfecha.HorizontalAlignment = Element.ALIGN_CENTER ;

                    tblPdf1.AddCell(clfecha);
                    doc.Add(new Paragraph(" ", _title2));  // agregando espacios en blanco

                    PdfPTable tblPdf2 = new PdfPTable(2);
                    tblPdf2.WidthPercentage = 100;
                    tblPdf2.HorizontalAlignment = Element.ALIGN_CENTER;

                    // seccion y danado
                    PdfPCell clseccion = new PdfPCell(new Phrase("Seccion", _title1));
                    clseccion.BorderWidth = .5f;
                    clseccion.HorizontalAlignment = 1;

                    PdfPCell cldano = new PdfPCell(new Phrase("Dañado?", _title1));
                    cldano.BorderWidth = .5f;
                    cldano.HorizontalAlignment = 1;

                   
                    tblPdf2.AddCell(clseccion );
                    tblPdf2.AddCell(cldano );

                    

                    //****************************************************************************************************************************************
                    // AGREGANDO ROWS DE REVISION


                    
                     // Agregando filas = Seccion Pared Izquierda************************
                    clseccion  = new PdfPCell(new Phrase("Pared Izquierda", _title2 ));
                    clseccion.BorderWidth = .5f;
                    clseccion.HorizontalAlignment = 1;

                    cldano = new PdfPCell(new Phrase(pizq, _title2 ));
                    cldano.BorderWidth = .5f;
                    cldano.HorizontalAlignment = 1; 
                    
                    tblPdf2.AddCell(clseccion );
                    tblPdf2.AddCell(cldano );

                    // Agregando filas = Seccion Pared Izquierda (blank) *****************
                    clseccion = new PdfPCell(new Phrase("", _title2));
                    clseccion.BorderWidth = .0f;
                    clseccion.HorizontalAlignment = 1;

                    cldano = new PdfPCell(new Phrase("", _title2));
                    cldano.BorderWidth = .0f;
                    cldano.HorizontalAlignment = 1;

                    tblPdf2.AddCell(clseccion);
                    tblPdf2.AddCell(cldano);

                     // Agregando filas = Seccion Pared Derecha
                    clseccion  = new PdfPCell(new Phrase("Pared Derecha", _title2 ));
                    clseccion.BorderWidth = .5f;
                    clseccion.HorizontalAlignment = 1;

                    cldano = new PdfPCell(new Phrase(pder , _title2 ));
                    cldano.BorderWidth = .5f;
                    cldano.HorizontalAlignment = 1; 
                    
                    tblPdf2.AddCell(clseccion );
                    tblPdf2.AddCell(cldano );


                    // Agregando filas = Seccion Techo
                    clseccion  = new PdfPCell(new Phrase("Techo / ( Roof ) ", _title2 ));
                    clseccion.BorderWidth = .5f;
                    clseccion.HorizontalAlignment = 1;

                    cldano = new PdfPCell(new Phrase(techo , _title2 ));
                    cldano.BorderWidth = .5f;
                    cldano.HorizontalAlignment = 1; 
                    
                    tblPdf2.AddCell(clseccion );
                    tblPdf2.AddCell(cldano );


                    // Agregando filas = Seccion Piso
                    clseccion  = new PdfPCell(new Phrase("Piso / ( Floor ) ", _title2 ));
                    clseccion.BorderWidth = .5f;
                    clseccion.HorizontalAlignment = 1;

                    cldano = new PdfPCell(new Phrase(piso  , _title2 ));
                    cldano.BorderWidth = .5f;
                    cldano.HorizontalAlignment = 1; 
                    
                    tblPdf2.AddCell(clseccion );
                    tblPdf2.AddCell(cldano );

                    // Agregando filas = Seccion Caja
                    PdfPCell clpie =  new PdfPCell(new PdfPCell(new Phrase("Cambiar caja ? : " + cambiar, _title1)) { Colspan = 2 });
                    clpie.BorderWidth = .5f;
                    clpie.HorizontalAlignment = 1;

                    tblPdf2.AddCell(clpie);

                   

                    //****************************************************************************************************************************************
                    // GENERANDO NUEVA TABLA PARA AGREGAR COMENTARIO Y QUIEN REALIZA
            
                    
                    doc.Add(tblPdf1);
                    doc.Add(new Paragraph(" ", _title1));  // agregando espacios en blanco
                    doc.Add(new Paragraph(" ", _title1));  // agregando espacios en blanco
                    doc.Add(tblPdf2);
                    doc.Add(new Paragraph(" ", _title1));  // agregando espacios en blanco
                    
                    doc.Add(new Paragraph("Comentarios: " + comment1 , _title2));  // agregando espacios en blanco
                    doc.Add(new Paragraph("Realizado por: " + GlobalVar.usuario, _title2));  // agregando espacios en blanco
                    doc.Add(new Paragraph(" ", _title2));  // agregando espacios en blanco
                    doc.Add(new Paragraph(" ", _title2));  // agregando espacios en blanco
                    doc.Add(new Paragraph(" ", _title2));  // agregando espacios en blanco
                    doc.Add(new Paragraph(" ", _title2));  // agregando espacios en blanco
                    doc.Add(new Paragraph(" ", _title2));  // agregando espacios en blanco
                    doc.Add(new Paragraph(" ", _title2));  // agregando espacios en blanco
                 
                    
             

            doc.Close();
         }
        private void si1_CheckedChanged(object sender, EventArgs e)
        {
            if (si1.Checked == true)
            {
                verificar ++;
                no1.Checked = false;
                pizq = "SI";
                si5.Checked = true;
            }
            else
            {
                verificar = verificar - 1;
            }
        }

        private void no1_CheckedChanged(object sender, EventArgs e)
        {
            if (no1.Checked == true)
            {
                verificar++;
                si1.Checked = false;
                pizq = "NO";
                if (si1.Checked == false && si2.Checked == false && si3.Checked == false && si4.Checked == false)
                {
                    no5.Checked = true;
                }
            }
            else
            {
                verificar = verificar - 1;
            }
        }

        private void si2_CheckedChanged(object sender, EventArgs e)
        {
            if (si2.Checked == true)
            {
                verificar++;
                no2.Checked = false;
                pder = "SI";
                si5.Checked = true;
            }
            else
            {
                verificar = verificar - 1;
            }
        }

        private void no2_CheckedChanged(object sender, EventArgs e)
        {
            if (no2.Checked == true)
            {
                verificar++;
                si2.Checked = false;
                pder = "NO";
                if (si1.Checked == false && si2.Checked == false && si3.Checked == false && si4.Checked == false)
                {
                    no5.Checked = true;
                }
            }
            else
            {
                verificar = verificar - 1;
            }
        }

        private void si3_CheckedChanged(object sender, EventArgs e)
        {
            if (si3.Checked == true)
            {
                verificar++;
                no3.Checked = false;
                techo  = "SI";
                si5.Checked = true;
            }
            else
            {
                verificar = verificar - 1;
            }
        }

        private void btncerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void no3_CheckedChanged(object sender, EventArgs e)
        {
            if (no3.Checked == true)
            {
                verificar++;
                si3.Checked = false;
                techo = "NO";
                if (si1.Checked == false && si2.Checked == false && si3.Checked == false && si4.Checked == false)
                {
                    no5.Checked = true;
                }
            }
            else
            {
                verificar = verificar - 1;
            }
        }

        private void si4_CheckedChanged(object sender, EventArgs e)
        {
            if (si4.Checked == true)
            {
                verificar++;
                no4.Checked = false;
                piso  = "SI";
                si5.Checked = true;
            }
            else
            {
                verificar = verificar - 1;
            }
        }

        private void no4_CheckedChanged(object sender, EventArgs e)
        {
            if (no4.Checked == true)
            {
                verificar++;
                si4.Checked = false;
                piso = "NO";
                if (si1.Checked == false && si2.Checked == false && si3.Checked == false && si4.Checked == false)
                {
                    no5.Checked = true;
                }
            }
            else
            {
                verificar = verificar - 1;
            }
        }

        private void si5_CheckedChanged(object sender, EventArgs e)
        {
            if (si5.Checked == true)
            {
                verificar++;
                no5.Checked = false;
                cambiar = "SI";
            }
            else
            {
                verificar = verificar - 1;
            }
        }

        private void no5_CheckedChanged(object sender, EventArgs e)
        {
            if (no5.Checked == true)
            {
                verificar++;
                si5.Checked = false;
                cambiar = "NO";
            }
            else
            {
                verificar = verificar - 1;
            }
        }

        private void Verificacion_de_caja_Load(object sender, EventArgs e)
        {
            if (idcarga > 0)
            {
                txtcaja.Text = _NoCaja;
                txtid.Text = idcarga.ToString();
            }
            else
            {
                if (M3_Map.reasignar == "Y")
                {
                    txtid.Text = M3_Map.numero_carga1;
                    txtcaja.Enabled = true;
                    txtcaja.Text = "";

                }
                else
                {
                    txtcaja.Text = crear_carga.numero_caja1;
                    txtid.Text = M3_Map.numero_carga1;
                }
            }
        }
    }
}
