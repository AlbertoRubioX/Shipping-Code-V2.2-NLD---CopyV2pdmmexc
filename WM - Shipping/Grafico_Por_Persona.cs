using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.OleDb;
using System.Drawing.Printing;
using System.Collections;


namespace WindowsFormsApplication1
{
    public partial class Grafico_Por_Persona : Form
    {
        Datos Consultar = new Datos();
        ArrayList User1 = new ArrayList();
        ArrayList Qty2 = new ArrayList();
        ArrayList Semana = new ArrayList();
        ArrayList Qtys = new ArrayList();
        string semanas, cantidad,usuario,cantidad2;
        public Grafico_Por_Persona()
        {
            InitializeComponent();
        }

        private void Grafico_Por_Persona_Load(object sender, EventArgs e)
        {
           
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker2.Format = DateTimePickerFormat.Short;
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnok_Click(object sender, EventArgs e)
        {
            string FechaInicial = dateTimePicker1.Value.ToShortDateString() + " 00:00:01.000";
            string FechaFinal = dateTimePicker2.Value.ToShortDateString() + " 23:59:59.000";

            if (radioButton1.Checked == true)
            {

                if (DateTime.Parse(FechaInicial) > DateTime.Parse(FechaFinal))
                {
                    MessageBox.Show("Fecha inicial no debe ser mayor que la fecha final", "Revisar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                else
                {
                    chart1.Visible = true;
                    if (comboBox1.Text == "3")
                    {
                        Consultar.GraficoxPersona(Convert.ToInt32(comboBox1.Text), FechaInicial, FechaFinal, GlobalVar.Compania, ref User1, ref Qty2);
                    }
                    else if (comboBox1.Text == "5")
                    {
                        Consultar.GraficoxPersona(Convert.ToInt32(comboBox1.Text), FechaInicial, FechaFinal, GlobalVar.Compania, ref User1, ref Qty2);
                    }
                    else if (this.comboBox1.Text == "10")
                    {
                        Consultar.GraficoxPersona(Convert.ToInt32(comboBox1.Text), FechaInicial, FechaFinal, GlobalVar.Compania, ref User1, ref Qty2);
                    }
                    else if (comboBox1.Text == "15")
                    {
                        Consultar.GraficoxPersona(Convert.ToInt32(comboBox1.Text), FechaInicial, FechaFinal, GlobalVar.Compania, ref User1, ref Qty2);
                    }
                    else if (comboBox1.Text == "TODOS")
                    {
                        Consultar.GraficoxPersona(0, FechaInicial, FechaFinal, GlobalVar.Compania, ref User1, ref Qty2);
                    }
                    chart1.Series.Clear();
                    chart1.ChartAreas.Clear();
                    chart1.ChartAreas.Add("Area");
                    chart1.ChartAreas["Area"].AxisX.LabelStyle.Font = new Font("Arial", 14f, GraphicsUnit.Pixel);
                    chart1.ChartAreas["Area"].AxisY.Title = "Lotes";
                    chart1.ChartAreas["Area"].AxisX.Title = "Empleado";
                    chart1.ChartAreas["Area"].AxisX.MajorGrid.LineColor = Color.Transparent;
                    chart1.ChartAreas["Area"].Area3DStyle.Enable3D = true;
                    chart1.Series.Add("Lotes");
                    chart1.Series["Lotes"].IsVisibleInLegend = false;
                    chart1.Series["Lotes"].IsValueShownAsLabel = true;
                    chart1.Series["Lotes"].ChartArea = "Area";
                    chart1.Series["Lotes"].ChartType = SeriesChartType.Column;
                    lblpersona.Visible = true;
                    lblpersona.Text = "Grafica Cargas / Empleado";
                    lblpersonafecha.Visible = true;
                    lblpersonafecha.Text = "[ Del " + dateTimePicker1.Text + " al " + dateTimePicker2.Text + " ]";
                    int num = this.Qty2.Count - 1;
                    if (num > 0)
                    {
                        int num2 = 0;
                        while (true)
                        {
                            if (num2 > num)
                            {
                                this.User1.Clear();
                                this.Qty2.Clear();
                                break;
                            }
                            this.usuario = this.usuario + this.User1[num2];
                            this.cantidad2 = this.cantidad2 + this.Qty2[num2];
                            object[] yValue = new object[] { this.cantidad2 };
                            this.chart1.Series[0].Points.AddXY(this.usuario, yValue);
                            this.usuario = "";
                            this.cantidad2 = "";
                            num2++;
                        }
                    }

                }
            }

            else if (radioButton2.Checked == true)
            {
                chart1.Visible = true;
                Consultar.GraficaxSemana(ref Semana, ref Qtys);
                chart1.Series.Clear();
                chart1.ChartAreas.Clear();
                chart1.ChartAreas.Add("Area");
                chart1.ChartAreas["Area"].AxisY.Title = "Embarques";
                chart1.ChartAreas["Area"].AxisX.Title = "Semana";
                chart1.ChartAreas["Area"].AxisX.MajorGrid.LineColor = Color.Transparent;
                chart1.ChartAreas["Area"].AxisX.LabelStyle.Font = new Font("Arial", 14f, GraphicsUnit.Pixel);
                chart1.Series.Add("Embarques");
                chart1.Series["Embarques"].IsVisibleInLegend = false;
                chart1.Series["Embarques"].ChartType = SeriesChartType.Line;
                chart1.Series["Embarques"].BorderWidth = 5;
                chart1.Series["Embarques"].Color = Color.Blue;
                chart1.Series["Embarques"].ChartArea = "Area";
                chart1.Series["Embarques"].IsValueShownAsLabel = true;
                lblpersona.Visible = true;
                lblpersona.Text = "Grafica Embarques Semanales";
                lblpersonafecha.Visible = false;

                int num3 = this.Semana.Count - 1;
                if (num3 > 0)
                {
                    int num4 = 0;
                    while (true)
                    {
                        if (num4 > num3)
                        {
                            this.Semana.Clear();
                            this.Qtys.Clear();
                            break;
                        }
                        this.semanas = this.semanas + this.Semana[num4];
                        this.cantidad = this.cantidad + this.Qtys[num4];
                        double[] y = new double[] { Convert.ToDouble(this.semanas) };
                        this.chart1.Series[0].Points.Add(y);
                        object[] yValue = new object[] { this.cantidad };
                        this.chart1.Series[0].Points.AddY(yValue);
                        this.semanas = "";
                        this.cantidad = "";
                        num4++;
                    }
                }

            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                groupBox2.Visible = true;
            }
            else
            {

                
                groupBox2.Visible = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == false )
            {
                   groupBox2.Visible = true;
            }
            else
            {

                groupBox2.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PrintDocument MyPrintDocument = new PrintDocument(); 
            PrintDialog MyPrintDialog = new PrintDialog();

            if (MyPrintDialog.ShowDialog() == DialogResult.OK)
            {
                System.Drawing.Printing.PrinterSettings values;
                values = MyPrintDialog.PrinterSettings;
                MyPrintDialog.Document = MyPrintDocument;
                MyPrintDocument.PrintController = new System.Drawing.Printing.StandardPrintController();
                MyPrintDocument.Print();
            }

            MyPrintDocument.Dispose();
        }

        



      

    }
}
