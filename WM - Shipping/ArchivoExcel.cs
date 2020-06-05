using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Collections;
using System.Data.SqlClient;
using System.IO;

namespace WindowsFormsApplication1
{
    class ArchivoExcel
    {
        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
        Excel.Range Rangos;
        string Fecha = string.Empty;

        Datos Consultar = new Datos();

        System.Data.DataTable dtInfo;
        System.Data.DataTable dtInfoNG;

        string TituloLinea = string.Empty;
        string TituloTurno = string.Empty;

        private OleDbConnection Conexion;

        public void CrearArchivoExcel(string NombreArchivo, System.Data.DataTable dtInfo, string TituloArchivo, BackgroundWorker Proceso)
        {
            Microsoft.Office.Interop.Excel._Application xlApp = null;
            Workbook xlWorkbook = null;
            Sheets xlSheets = null;
            Worksheet xlNewSheet = null;

            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();

                System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                if (xlApp == null)
                    return;

                // Uncomment the line below if you want to see what's happening in Excel
                // xlApp.Visible = true;

                xlWorkbook = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);


                //===================== se elimino, este procedimiento permite abrir el archivo tomando como referencia el nombre del mismo =====================
                //xlWorkbook = xlApp.Workbooks.Open(RutaDeArchivo, 0, false, 5, "", "",
                //        false, XlPlatform.xlWindows, "",
                //        true, false, 0, true, false, false);
                //===============================================================================================================================================

                //xlWorkbook.CheckCompatibility = false;
                //xlWorkbook.Application.DisplayAlerts = false;

                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
                xlSheets = xlWorkbook.Sheets as Sheets;

                xlNewSheet = (Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                xlNewSheet.Name = NombreArchivo;

                xlNewSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible;
                EdicionHojaExcel(xlNewSheet, dtInfo, TituloArchivo);

                int FilaArchivo = 6;
                int i = 0;
                foreach (System.Data.DataColumn dColum in dtInfo.Columns)
                {
                    i++;
                    xlNewSheet.Cells[FilaArchivo, i] = dColum.ToString();
                }

                //============================================ Funcion que permite usar la opcion FREEZE PANES por codigo ====================================================
                // Se declara el punto de referencia tomando en cuenta la celda
                Rangos = xlNewSheet.get_Range("A7", "E7");
                Rangos.Activate();
                Rangos.Application.ActiveWindow.FreezePanes = true;
                // =========================================== Esta es la forma como se activa la funcion de FREZEE PANES ====================================================

                int Fila = 6;

                int Total = dtInfo.Rows.Count;
                int Porcentaje = 0;
                foreach (DataRow dr in dtInfo.Rows)
                {
                    Fila++;
                    for (int j = 0; j < dtInfo.Columns.Count; j++)
                    {
                        xlNewSheet.Cells[Fila, NombreColumna(j)] = dr[j].ToString();
                    }
                    Porcentaje = (Fila / Total) * 100;
                    Proceso.ReportProgress(Porcentaje);
                }

                xlApp.Visible = true;

                releaseObject(xlApp);
                releaseObject(xlWorkbook);
                releaseObject(xlSheets);
                releaseObject(xlNewSheet);
            }
            catch (Exception Error)
            {
                throw new Exception("Error: " + Error.Message);
            }
            finally
            {
                Marshal.ReleaseComObject(xlNewSheet);
                Marshal.ReleaseComObject(xlSheets);
                Marshal.ReleaseComObject(xlWorkbook);
                Marshal.ReleaseComObject(xlApp);
                xlApp = null;
            }
        }


        public void CrearArchivoExcelFedex(string NombreArchivo, System.Data.DataTable dtInfo, string TituloArchivo, BackgroundWorker Proceso, int idcarga, string cajan, string destino)
        {
            Microsoft.Office.Interop.Excel._Application xlApp = null;
            Workbook xlWorkbook = null;
            Sheets xlSheets = null;
            Worksheet xlNewSheet = null;

            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();

                System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                if (xlApp == null)
                    return;

                // Uncomment the line below if you want to see what's happening in Excel
                // xlApp.Visible = true;

                xlWorkbook = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);


                //===================== se elimino, este procedimiento permite abrir el archivo tomando como referencia el nombre del mismo =====================
                //xlWorkbook = xlApp.Workbooks.Open(RutaDeArchivo, 0, false, 5, "", "",
                //        false, XlPlatform.xlWindows, "",
                //        true, false, 0, true, false, false);
                //===============================================================================================================================================

                //xlWorkbook.CheckCompatibility = false;
                //xlWorkbook.Application.DisplayAlerts = false;

                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
                xlSheets = xlWorkbook.Sheets as Sheets;

                xlNewSheet = (Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                xlNewSheet.Name = NombreArchivo;

                xlNewSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible;
                EdicionHojaExcelFedex(xlNewSheet, dtInfo, TituloArchivo, idcarga, cajan, destino);

                int FilaArchivo = 6;
                int i = 0;
                foreach (System.Data.DataColumn dColum in dtInfo.Columns)
                {
                    i++;
                    xlNewSheet.Cells[FilaArchivo, i] = dColum.ToString();
                }

                //============================================ Funcion que permite usar la opcion FREEZE PANES por codigo ====================================================
                // Se declara el punto de referencia tomando en cuenta la celda
                Rangos = xlNewSheet.get_Range("A7", "E7");
                Rangos.Activate();
                Rangos.Application.ActiveWindow.FreezePanes = true;
                // =========================================== Esta es la forma como se activa la funcion de FREZEE PANES ====================================================

                int Fila = 6;

                int Total = dtInfo.Rows.Count;
                int Porcentaje = 0;
                foreach (DataRow dr in dtInfo.Rows)
                {
                    Fila++;
                    for (int j = 0; j < dtInfo.Columns.Count; j++)
                    {
                        xlNewSheet.Cells[Fila, NombreColumna(j)] = dr[j].ToString();
                    }
                    Porcentaje = (Fila / Total) * 100;
                    Proceso.ReportProgress(Porcentaje);
                }

                xlApp.Visible = true;

                releaseObject(xlApp);
                releaseObject(xlWorkbook);
                releaseObject(xlSheets);
                releaseObject(xlNewSheet);
            }
            catch (Exception Error)
            {
                throw new Exception("Error: " + Error.Message);
            }
            finally
            {
                Marshal.ReleaseComObject(xlNewSheet);
                Marshal.ReleaseComObject(xlSheets);
                Marshal.ReleaseComObject(xlWorkbook);
                Marshal.ReleaseComObject(xlApp);
                xlApp = null;
            }
        }
        public void CrearArchivoExcelQueryTables(string NombreArchivo, System.Data.DataTable dtInfo, string TituloArchivo, BackgroundWorker Proceso, string FechaInicial,
            string FechaFinal, string SerialQR, string Nameplate, string IdModel, string Query)
        {
            const string OLEDBConnection = "OLEDB;Provider=SQLOLEDB.1;Data Source=172.20.96.13;UID=sa;PWD=;Initial Catalog=AirBag";
            Microsoft.Office.Interop.Excel._Application xlApp = null;
            Workbook xlWorkbook = null;
            Sheets xlSheets = null;
            Worksheet xlNewSheet = null;

            try
            {
                xlApp = new Microsoft.Office.Interop.Excel.Application();

                System.Globalization.CultureInfo oldCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

                if (xlApp == null)
                    return;

                // Uncomment the line below if you want to see what's happening in Excel
                // xlApp.Visible = true;

                xlWorkbook = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);


                //===================== se elimino, este procedimiento permite abrir el archivo tomando como referencia el nombre del mismo =====================
                //xlWorkbook = xlApp.Workbooks.Open(RutaDeArchivo, 0, false, 5, "", "",
                //        false, XlPlatform.xlWindows, "",
                //        true, false, 0, true, false, false);
                //===============================================================================================================================================

                //xlWorkbook.CheckCompatibility = false;
                //xlWorkbook.Application.DisplayAlerts = false;

                System.Threading.Thread.CurrentThread.CurrentCulture = oldCI;
                xlSheets = xlWorkbook.Sheets as Sheets;

                xlNewSheet = (Worksheet)xlSheets.Add(xlSheets[1], Type.Missing, Type.Missing, Type.Missing);
                xlNewSheet.Name = NombreArchivo;

                xlNewSheet.Visible = Microsoft.Office.Interop.Excel.XlSheetVisibility.xlSheetVisible;
                EdicionHojaExcel(xlNewSheet, dtInfo, TituloArchivo);


                //============================================ Funcion que permite usar la opcion FREEZE PANES por codigo ====================================================
                // Se declara el punto de referencia tomando en cuenta la celda
                Rangos = xlNewSheet.get_Range("A7", "E7");
                Rangos.Activate();
                Rangos.Application.ActiveWindow.FreezePanes = true;
                // =========================================== Esta es la forma como se activa la funcion de FREZEE PANES ====================================================

                xlNewSheet.QueryTables.Add(OLEDBConnection, xlNewSheet.get_Range("A6", "E6"), Query.Trim()).Refresh();


                xlApp.Visible = true;

                releaseObject(xlApp);
                releaseObject(xlWorkbook);
                releaseObject(xlSheets);
                releaseObject(xlNewSheet);
            }
            catch (Exception Error)
            {
                throw new Exception("Error: " + Error.Message);
            }
            finally
            {
                Marshal.ReleaseComObject(xlNewSheet);
                Marshal.ReleaseComObject(xlSheets);
                Marshal.ReleaseComObject(xlWorkbook);
                Marshal.ReleaseComObject(xlApp);
                xlApp = null;
            }
        }

        private void EdicionHojaExcel(Worksheet Hoja, System.Data.DataTable dt, string TituloArchivo)
        {
            Hoja.Shapes.AddPicture(System.Windows.Forms.Application.StartupPath + @"\medline.jpg", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 58, 4, 120, 55);

            Hoja.get_Range("E2", "M2").Merge(false);
            Rangos = Hoja.get_Range("E2", "M2");
            //Rangos.FormulaR1C1 = "REPORTE DE PRODUCCION DE LINEA AIR BAG";
            Rangos.FormulaR1C1 = TituloArchivo.Trim();
            Rangos.HorizontalAlignment = 3;
            Rangos.VerticalAlignment = 3;
            Rangos.Font.Size = 20;
            Rangos.Font.Bold = true;

            //Permite darle un formato en el tamaño y longitd de cada Celda
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Rangos = Hoja.get_Range(NombreColumna(i) + (i + 7), NombreColumna(i) + (i + 7));
            }

            Rangos = Hoja.get_Range("A6", (NombreColumna(dt.Columns.Count - 1) + 6));
            Rangos.Cells.Interior.ColorIndex = 35;
            Rangos.EntireColumn.ColumnWidth = 12;
            Rangos.EntireRow.RowHeight = 20;
        }


        private void EdicionHojaExcelFedex(Worksheet Hoja, System.Data.DataTable dt, string TituloArchivo, int idcarga, string cajan, string destino)
        {
           // Hoja.Shapes.AddPicture(System.Windows.Forms.Application.StartupPath + @"\medline.jpg", Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 58, 4, 120, 55);

            Hoja.get_Range("A1", "B1").Merge(false);
            Rangos = Hoja.get_Range("A1", "B1");
            Rangos.FormulaR1C1 = "Mapa:   " + idcarga;
            Rangos.HorizontalAlignment = 1;
            Rangos.VerticalAlignment = 3;
            Rangos.Font.Size = 12;
            Rangos.Font.Bold = true;

            Hoja.get_Range("A2", "B2").Merge(false);
            Rangos = Hoja.get_Range("A2", "B2");
            Rangos.FormulaR1C1 = "Caja:   " + cajan;
            Rangos.HorizontalAlignment = 1;
            Rangos.VerticalAlignment = 3;
            Rangos.Font.Size = 12;
            Rangos.Font.Bold = true;

            Hoja.get_Range("A3", "B3").Merge(false);
            Rangos = Hoja.get_Range("A3", "B3");
            Rangos.FormulaR1C1 = "Notas:   " + destino;
            Rangos.HorizontalAlignment = 1;
            Rangos.VerticalAlignment = 3;
            Rangos.Font.Size = 12;
            Rangos.Font.Bold = true;

            Hoja.get_Range("C1", "G1").Merge(false);
            Rangos = Hoja.get_Range("C1", "G1");
            Rangos.FormulaR1C1 = TituloArchivo.Trim();
            Rangos.HorizontalAlignment = 3;
            Rangos.VerticalAlignment = 3;
            Rangos.Font.Size = 0x10;
            Rangos.Font.Bold = true;

            //Permite darle un formato en el tamaño y longitd de cada Celda
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                Rangos = Hoja.get_Range(NombreColumna(i) + (i + 7), NombreColumna(i) + (i + 7));
            }

            Rangos = Hoja.get_Range("A6", (NombreColumna(dt.Columns.Count - 1) + 6));
            Rangos.Cells.Interior.ColorIndex = 35;
            Rangos.EntireColumn.ColumnWidth = 12;
            Rangos.EntireRow.RowHeight = 20;
        }


        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception Error)
            {
                obj = null;
                MessageBox.Show("Error: " + Error.Message);
            }
            finally
            {
                GC.Collect();
            }
        }

        //public void ObtenerInformacionArchivoExcel(string FechaHoraInicial, string FechaHoraFinal, bool EslineaAirBag, int Turno)
        //{
        //    if (EslineaAirBag)
        //    {
        //        dtInfo = Consultar.ObtenerUnidadesPorModeloAirBag(FechaHoraInicial.Trim(), FechaHoraFinal.Trim());
        //        dtInfoNG = Consultar.ObtenerUnidadesPorModeloNGAirBag(FechaHoraInicial.Trim(), FechaHoraFinal.Trim());
        //        TituloLinea = "AirBag";
        //        TituloTurno = Turno != 1 ? "6to / 7mo" : "1er";
        //    }
        //}

        public void LeerArchivoExcel(string RutaArchivo, DataGridView dgView, string HojaExcel)
        {
            string cadenaconexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + RutaArchivo.Trim() + ";Extended Properties='Excel 12.0;HDR=YES';";
            Conexion = new OleDbConnection();
            Conexion.ConnectionString = cadenaconexion;

            string Consulta = "select * from [" + HojaExcel.Trim() + "]";
            OleDbCommand cmd = new OleDbCommand(Consulta, Conexion);
            OleDbDataAdapter da = new OleDbDataAdapter();
            OleDbDataReader dr = null;
            System.Data.DataTable dt = new System.Data.DataTable();



            try
            {
                //==================================== SE CREA EN MEMORIA LA COLUMNA DEL DATATABLE ==============================
                //dt.Columns.Add("Columna: 1", typeof(string));
                //===============================================================================================================


                //================================ Se usa esta rutina para poder saber el Total de Columnas que se agregaran al DataGridView =============================
                Conexion.Open();
                dr = cmd.ExecuteReader();

                dgView.ColumnCount = dr.FieldCount;

                for (int Columnas = 0; Columnas < dr.FieldCount; Columnas++)
                {
                    dgView.Columns[Columnas].Name = "Columna: " + (Columnas + 1);
                }

                dr.Close();
                //========================================================================================================================================================

                da.SelectCommand = cmd;
                da.Fill(dt);

                dgView.RowCount = dt.Rows.Count;


                int j = 0;
                foreach (DataRow dtr in dt.Rows)
                {
                    for (int x = 0; x < dt.Columns.Count; x++)
                    {
                        dgView.Rows[j].Cells["Columna: " + (x + 1)].Value = dtr[x].ToString();
                    }
                    j++;
                }
            }
            catch (Exception Error)
            {
                throw new Exception("Error: " + Error.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        public void ObtenerHojasDeExcel(ComboBox Combo, string RutaArchivo)
        {
            string cadenaconexion = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + RutaArchivo.Trim() + ";Extended Properties='Excel 12.0;HDR=YES';";
            Conexion = new OleDbConnection();
            Conexion.ConnectionString = cadenaconexion;

            System.Data.DataTable dt = new System.Data.DataTable();
            try
            {
                Conexion.Open();
                dt = Conexion.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                Combo.Items.Clear();
                Combo.Items.Add("-- SELECCIONE UNA HOJA DE EXCEL --");

                foreach (DataRow dr in dt.Rows)
                {
                    Combo.Items.Add((string)dr["TABLE_NAME"]);
                }

                Combo.SelectedIndex = 0;
            }
            catch (Exception Error)
            {
                throw new Exception("Error: " + Error.Message);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        #region NombreDeColumnasArchivoExcel
        public string NombreColumna(int NumeroColumna)
        {
            string[] letras = new string[104];
            letras[0] = "A";
            letras[1] = "B";
            letras[2] = "C";
            letras[3] = "D";
            letras[4] = "E";
            letras[5] = "F";
            letras[6] = "G";
            letras[7] = "H";
            letras[8] = "I";
            letras[9] = "J";
            letras[10] = "K";
            letras[11] = "L";
            letras[12] = "M";
            letras[13] = "N";
            letras[14] = "O";
            letras[15] = "P";
            letras[16] = "Q";
            letras[17] = "R";
            letras[18] = "S";
            letras[19] = "T";
            letras[20] = "U";
            letras[21] = "V";
            letras[22] = "W";
            letras[23] = "X";
            letras[24] = "Y";
            letras[25] = "Z";
            letras[26] = "AA";
            letras[27] = "AB";
            letras[28] = "AC";
            letras[29] = "AD";
            letras[30] = "AE";
            letras[31] = "AF";
            letras[32] = "AG";
            letras[33] = "AH";
            letras[34] = "AI";
            letras[35] = "AJ";
            letras[36] = "AK";
            letras[37] = "AL";
            letras[38] = "AM";
            letras[39] = "AN";
            letras[40] = "AO";
            letras[41] = "AP";
            letras[42] = "AQ";
            letras[43] = "AR";
            letras[44] = "AS";
            letras[45] = "AT";
            letras[46] = "AU";
            letras[47] = "AV";
            letras[48] = "AW";
            letras[49] = "AX";
            letras[50] = "AY";
            letras[51] = "AZ";
            letras[52] = "BA";
            letras[53] = "BC";
            letras[54] = "BD";
            letras[55] = "BE";
            letras[56] = "BF";
            letras[57] = "BG";
            letras[58] = "BH";
            letras[59] = "BI";
            letras[60] = "BJ";
            letras[61] = "BK";
            letras[62] = "BL";
            letras[63] = "BM";
            letras[64] = "BN";
            letras[65] = "BO";
            letras[66] = "BP";
            letras[67] = "BQ";
            letras[68] = "BR";
            letras[69] = "BS";
            letras[70] = "BT";
            letras[71] = "BU";
            letras[72] = "BV";
            letras[73] = "BW";
            letras[74] = "BX";
            letras[75] = "BY";
            letras[76] = "BZ";
            letras[77] = "CA";
            letras[78] = "CB";
            letras[79] = "CC";
            letras[80] = "CD";
            letras[81] = "CE";
            letras[82] = "CF";
            letras[83] = "CG";
            letras[84] = "CH";
            letras[85] = "CI";
            letras[86] = "CJ";
            letras[87] = "CK";
            letras[88] = "CL";
            letras[89] = "CM";
            letras[90] = "CN";
            letras[91] = "CO";
            letras[92] = "CP";
            letras[93] = "CQ";
            letras[94] = "CR";
            letras[95] = "CS";
            letras[96] = "CT";
            letras[97] = "CU";
            letras[98] = "CV";
            letras[99] = "CW";
            letras[100] = "CX";
            letras[101] = "CY";
            letras[102] = "CZ";
            letras[103] = "DA";

            return letras[NumeroColumna];
        }
        #endregion 
    }
}

