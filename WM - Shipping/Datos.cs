using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    class Datos
    {
        private SqlConnection Conexion;
        //Test enviroment//
        //private const string CadenaCon = ("Data Source =NLPRDLOCALDB1;Initial Catalog = ShippingSystem; User Id = nldprodtest; Password=T3stPrdN19L;");//
        //PDM Enviroment//
        //private const string CadenaCon = ("Data Source =NLPRDLOCALDB1;Initial Catalog = ShippingSystem; User Id = nldprod; Password=nldprod;");//
        //MXC Test Enviroment//
        //private const string CadenaCon = ("Data Source =MXCPRDLOCALDB01;Initial Catalog = ShippingSystemTest; User Id = mxcprd; Password=Admin10;");
        //MXC Enviroment//
        private const string CadenaCon = ("Data Source =MXCPRDLOCALDB01;Initial Catalog = ShippingSystemTest; User Id = mxcprd; Password=Admin10;");

        public static string grabada = "N", num_cargal, numero_cajal;
        public string rampa, rampa_vacial = "Y";
        public int Totalitem, loc;
        public double idcarga;

        public struct intElemento
        {

            public string strName, intId;

            public intElemento(string Name, string Id)
            {
                this.strName = Name;
                this.intId = Id;
            }   
            public string getName
            {
                get { return strName; }
            }
            public string getid
            {
                get { return intId; }
            }

        }

        public void Opendb()
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;

            try
            {
                Conexion.Open();
                Conexion.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Conexion Error DB", "Error Db", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public ArrayList NivelUsuario(string PrimerElermento)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select Acceso, Descripcion From Nivel_Acceso Where Compania = '" + GlobalVar.Compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            ArrayList ArregloLineas = new ArrayList();
            SqlDataReader dr = null;

            switch (PrimerElermento)
            {
                case "T":
                    ArregloLineas.Add(new intElemento("-- SELECCIONE--", "0"));
                    break;
                case "5":
                    ArregloLineas.Add(new intElemento("-- TODOS--", "0"));
                    break;
            }
                    try
                    {
                        Conexion.Open();
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            ArregloLineas.Add(new intElemento(dr["Descripcion"].ToString().Trim(), dr["Acceso"].ToString().Trim()));
                        }
                return ArregloLineas;
            }                       
            catch(Exception Error)
                {

                throw new Exception("Error: " + Error.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
                Conexion.Dispose();
                SqlConnection.ClearPool(Conexion);
            }
        }

        public ArrayList Compania(string PrimerElermento)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select Compania, IdCompania From Compania";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            ArrayList ArregloLineas = new ArrayList();
            SqlDataReader dr = null;

            switch (PrimerElermento)
            {
                case "T":
                    ArregloLineas.Add(new intElemento("-- SELECCIONE--", "0"));
                    break;
                case "5":
                    ArregloLineas.Add(new intElemento("-- TODOS--", "0"));
                    break;
            }
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ArregloLineas.Add(new intElemento(dr["Compania"].ToString().Trim(), dr["IdCompania"].ToString().Trim()));
                }
                return ArregloLineas;
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
                SqlConnection.ClearPool(Conexion);
            }
        }


        public void Uservalidation(string user, string password, int compania)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "SELECT id,nombre,nivel_acceso,Compania FROM Usuarios WHERE Id = '" + user + "' AND Contrasena = '" + password + "' and Activo = 1 and compania= '" + compania + "'";
            SqlCommand command = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                command.Parameters.AddWithValue("_user", user.Trim());
                command.Parameters.AddWithValue("_password", password.Trim());
                command.Parameters.AddWithValue("_compania", compania);
                Conexion.Open();
                dr = command.ExecuteReader();
                if (!dr.Read())
                {
                    MessageBox.Show("Verifique que el usuario y/o contrasena sean correctos", "VERIFICAR", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    GlobalVar.usuario = dr["Nombre"].ToString();
                    GlobalVar.n_acceso = Convert.ToInt32(dr["nivel_acceso"].ToString());
                    GlobalVar.User_Check = "Y";
                    GlobalVar.nombre_user = Convert.ToInt32(dr["id"].ToString());
                    GlobalVar.Compania = Convert.ToInt32(dr["Compania"].ToString());
                }
            }
            catch (Exception Error)
            {
                throw new Exception("Error" + Error);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }


        public DataTable CargarDetener()
        {
          
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL= "Select Lote, Razon, [User], fecha as Fecha_Hora from no_cargar";
            SqlCommand command = new SqlCommand(strSQL, Conexion);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                Conexion.Open();
                adapter.SelectCommand = command;
                adapter.Fill(dt);
                return  dt;
            }
            catch (Exception Error)
            {
                throw new Exception("Error:" + Error);
            }
            finally
            {
                Conexion.Close();
            }
           
        }

        public void IngresarDetenerLote(int Compania, string lote, string razon, string usuario)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "INSERT INTO No_Cargar (Compania,Lote, Razon, [User], Fecha) VALUES (@_Compania,@_lote, @_razon, @_usuario, getdate())";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.Parameters.AddWithValue("_lote", lote.Trim());
                cmd.Parameters.AddWithValue("_razon", razon.Trim());
                cmd.Parameters.AddWithValue("_usuario", usuario.Trim());
                cmd.ExecuteNonQuery();
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

        public void DeleteDetenerLote(string lote, int Compania)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL =  "Delete from no_cargar where lote='"+ @lote + "' and compania= '"+ @Compania+ "' " ;
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_lote", lote.Trim());
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.ExecuteNonQuery();
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

        public DataTable BuscarLote(string lote)
        {
           
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = " SELECT 'Embarques' as Area,Lote,cast(num_tarima as varchar)+ ' / '+cast(total_tarimas as varchar) as Tarima, Fecha_recibe as Fecha_Hora_entrada, Localizacion,a.Id_carga as Carga, Posicion, iif([shipped] = 'YES', 'SI','NO')  as Embarcado, NULL as Fecha_Hora_Salida, '-' as Usuario_Lleva, '-' as Usuario_Registra  from Inventario a left join cajas b on (a.id_carga = b.id_carga) where lote = '" + lote + "' and a.Compania='" + GlobalVar.Compania + "'" +
             "UNION  all SELECT 'Regresada' as area, lote,tarima, fecha_entrada  as Fecha_Hora_Entrada,  'Regresada' as Loc, '-','-','-',  FECHA_salida  as Fecha_Hora_Salida,user_lleva,nombre  from  retorno_tarimas a left join usuarios b on (a.user_registra=b.id) where lote = '" + lote + "' and a.Compania='" + GlobalVar.Compania + "'";

            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                Conexion.Open();
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception Error)
            {
                throw new Exception("Error:" + Error);
            }
            finally
            {
                Conexion.Close();
            }
           
        }

        public void CorrecionTarimas(string qty, string lote, int Compania)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Update inventario set total_tarimas = '"+ @qty + "' where lote= '"+ @lote+ "' and compania= '" + Compania + "'" ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_qty", qty.Trim());
                cmd.Parameters.AddWithValue("_lote", lote.Trim());
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error" + Environment.NewLine + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        public ArrayList LlenarRampa(string PrimerElemento)
        {
            
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select distinct compania,rampa From Distribucion Where Rampa is not null and Compania='" + GlobalVar.Compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            ArrayList ArregloLineas = new ArrayList();
            SqlDataReader dr = null;

            switch(PrimerElemento)               
            {
            case "T":
                ArregloLineas.Add(new intElemento("-- SELECCIONE --", "0"));
            break;     
            case "S":
          
                ArregloLineas.Add(new intElemento("-- T O D O S --", "0"));
            break;
            }
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ArregloLineas.Add(new intElemento(dr["Rampa"].ToString().Trim(), dr["rampa"].ToString().Trim()));
                }
            return ArregloLineas;
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
                SqlConnection.ClearPool(Conexion);
            }
          
        }

        public ArrayList LlenarEnvio(string PrimerElemento)
        {

            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select idenvio, envio From Tipo_Envio Where Compania = '" + GlobalVar.Compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            ArrayList ArregloLineas = new ArrayList();
            SqlDataReader dr = null;

            switch (PrimerElemento)
            {
                case "T":
                    ArregloLineas.Add(new intElemento("-- SELECCIONE --", "0"));
                    break;
                case "S":

                    ArregloLineas.Add(new intElemento("-- T O D O S --", "0"));
                    break;
            }
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ArregloLineas.Add(new intElemento(dr["envio"].ToString().Trim(), dr["envio"].ToString().Trim()));
                }
                return ArregloLineas;
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
                SqlConnection.ClearPool(Conexion);
            }

        }

        public ArrayList LlenarDestino(string PrimerElemento)
        {

            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "SELECT Id_Destino,Compania,Destino FROM Destino where Compania='" + GlobalVar.Compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            ArrayList ArregloLineas = new ArrayList();
            SqlDataReader dr = null;

            switch (PrimerElemento)
            {
                case "T":
                    ArregloLineas.Add(new intElemento("-- SELECCIONE --", "0"));
                    break;
                case "S":

                    ArregloLineas.Add(new intElemento("-- T O D O S --", "0"));
                    break;
            }
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ArregloLineas.Add(new intElemento(dr["Destino"].ToString().Trim(), dr["Destino"].ToString().Trim()));
                }
                return ArregloLineas;
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
                SqlConnection.ClearPool(Conexion);
            }

        }

        public ArrayList LlenarCiclo(string PrimerElemento)
        {

            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "SELECT  Compania,Ciclo FROM Ciclo where Compania='" + GlobalVar.Compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            ArrayList ArregloLineas = new ArrayList();
            SqlDataReader dr = null;

            switch (PrimerElemento)
            {
                case "T":
                    ArregloLineas.Add(new intElemento("-- SELECCIONE --", "0"));
                    break;
                case "S":

                    ArregloLineas.Add(new intElemento("-- T O D O S --", "0"));
                    break;
            }
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    ArregloLineas.Add(new intElemento(dr["Ciclo"].ToString().Trim(), dr["Ciclo"].ToString().Trim()));
                }
                return ArregloLineas;
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
                SqlConnection.ClearPool(Conexion);
            }

        }


        public void NuevoUsuario(int Empid, string nombre, string contrasena, int nivel, int Compania)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL= "INSERT INTO Usuarios(id, Nombre, Contrasena, Nivel_acceso, Compania, Activo) VALUES(@_Empid, @_nombre, @_contrasena, @_nivel, @_Compania, 1)";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                this.Conexion.Open();
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.Parameters.AddWithValue("_Empid", Empid);
                cmd.Parameters.AddWithValue("_nivel", nivel);
                cmd.Parameters.AddWithValue("_contrasena", contrasena.Trim());
                cmd.Parameters.AddWithValue("_nombre", nombre.Trim());
                cmd.ExecuteNonQuery();
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

        public void EliminarUsuario(int Empid, int Compania)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL =  "Delete from usuarios where ID= '" + @Empid + "' and compania= '" + @Compania + "'" ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.Parameters.AddWithValue("_Empid", Empid);
                cmd.ExecuteNonQuery();
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


        public void RetrabajarCarga(string carga, int Compania)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL = "Update cajas set shipped = 'NO' where id_carga= '"+ @carga + "' and compania= '" + @Compania + "'" ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                this.Conexion.Open();
                cmd.Parameters.AddWithValue("_carga", carga.Trim());
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }


        public void Removerlote(string pos, int Compania)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string StrSQL = "Update Inventario set id_carga=0, posicion=0, localizacion= '" + M3_Map.numero_loc + "', user_carga=0, verificador=0 where  posicion="+ pos +" and id_carga=" + M3_Map.id_carga1 +" and compania= '" + @Compania + "' ";
            SqlCommand cmd = new SqlCommand(StrSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_pos", pos.Trim());
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error" + Environment.NewLine + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }


        public void CloseBoxTrailer(string pos, int Compania)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string StrSQL = "Update cajas set Shipped='NUL', usuario_cierre= "+ GlobalVar.nombre_user + ", fecha_completa = getdate() where  Id_Carga=" + pos + "  and compania= '" + @Compania + "' ";
            SqlCommand cmd = new SqlCommand(StrSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_pos", pos.Trim());
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error" + Environment.NewLine + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }


        public bool CloseBoxTrailerInv(string idcarga)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select top 1 id_carga from Inventario where id_carga = '" + @idcarga + "' and Compania= '" + GlobalVar.Compania + "' ";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_idcarga", idcarga);
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Read();
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error" + exception.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }

        }

        public void VerificacionCajaid(int idcaja, int Compania)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL =  "Update cajas set caja = '' where id_carga = '"+ @idcaja + "' and compania= '" + @Compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_idcaja", idcaja);
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }


        public void VerificacionCaja(string caja, int idcaja, int Compania)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Update cajas set caja = '"+ @caja + "' where id_carga = '"+ @idcaja + "' and compania = '"+ @Compania + "' ";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_caja", caja.Trim());
                cmd.Parameters.AddWithValue("_idcaja", idcaja);
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        public DataTable HistoricoCarga(string mapa, int compania)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select id_carga as MAPA, Caja, Ciclo, Destino ,Notas,fecha_completa  as Fecha_Hora_Embarque, usuario_cierre as Usuario from cajas where shipped = 'YES' and id_carga='"+ @mapa + "' and compania= '"+ @compania + "'" ;
            SqlCommand cmd = new SqlCommand(strSQL,Conexion);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception exception)
            {
                throw new Exception("Error:" + exception);
            }
            finally
            {
                Conexion.Close();
            }
            
        }

        public DataTable HistoricoCargaFecha(string FechaInicial, string FechaFinal, int Compania)
        {
           
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL =  "Select id_carga as MAPA, Caja, Ciclo, Destino, Notas,fecha_completa  as Fecha_Hora_Embarque, usuario_cierre as Usuario from cajas where shipped = 'YES' and fecha_completa >= '"+ FechaInicial.Trim() + "'  and fecha_completa <= '"+ FechaFinal.Trim() + "' and compania= '"+ @Compania + "' " ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception Error)
            {
                throw new Exception("Error:" + Error);
            }
            finally
            {
                Conexion.Close();
            }
           
        }

        public bool VerificacionRampa(string rampa, int compania)
        {
          
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL ="Select id_carga from cajas where rampa= '"+ @rampa + "' and shipped = 'NO' AND Compania= '" + @compania + "'" ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                cmd.Parameters.AddWithValue("_rampa", rampa.Trim());
                cmd.Parameters.AddWithValue("_compania", compania);
                Conexion.Open();
                dr = cmd.ExecuteReader();
              
                if(dr.HasRows ==  true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Error)
            {
                throw new Exception("Error" + Error);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
           
        }

        public void InsertCaja(string caja, string ciclo, string destino, int rampa, string nota, int compania)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL = "INSERT INTO cajas(caja, ciclo,destino,rampa,Shipped,notas,compania) VALUES (@_caja, @_ciclo, @_destino,@_rampa,'NO',@_nota ,@_Compania)";
            SqlCommand cmd = new SqlCommand(strSQL,Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_caja", caja.Trim());
                cmd.Parameters.AddWithValue("_ciclo", ciclo.Trim());
                cmd.Parameters.AddWithValue("_destino", destino.Trim());
                cmd.Parameters.AddWithValue("_rampa", rampa);
                cmd.Parameters.AddWithValue("_nota", nota.Trim());
                cmd.Parameters.AddWithValue("_compania", compania);
                cmd.ExecuteNonQuery();
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

        public void ObtenerIdCarga(string caja, ref int idcarga, int Compania)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
           string strSQL=  "Select Max(id_carga) as num_c from cajas where Compania= '" + @Compania + "' and caja= '"+ @caja + "'" ;
            SqlCommand cmd = new SqlCommand(strSQL,Conexion);
            SqlDataReader dr = null;
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    idcarga = 0;
                }
                else
                {
                    dr.Read();
                    idcarga = (Int32)dr["num_c"];
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error" + exception.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }


        public bool Quiebre(ref string lot, ref string wos, ref string trays, ref string dest, ref string ciclos, ref string usuario, ref string fdate, ref string loc, string lote, string tarima, string tarimas, int Compania)
        {

            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select lote, WO, Tray, destino, ciclo, user_recibe, fecha_recibe, qty_cajas, localizacion from inventario where lote = '" + @lote + "' and num_tarima = '"+ @tarima + "' and (parcial = 'NO' or parcial is null)   and total_tarimas = '"+ @tarimas + "'  and (localizacion = 'E1' or localizacion = 'E2') and Compania= '"+ GlobalVar.Compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            SqlDataReader dr = null;
            try
            {
                this.Conexion.Open();
                cmd.Parameters.AddWithValue("_lote", lote.Trim());
                cmd.Parameters.AddWithValue("_tarima", tarima.Trim());
                cmd.Parameters.AddWithValue("_tarimas", tarimas.Trim());
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    return  false;
                }
                else
                {
                    dr.Read();
                    lot = dr["lote"].ToString();
                    wos = dr["WO"].ToString();
                    trays = dr["Tray"].ToString();
                    dest = dr["destino"].ToString();
                    ciclos = dr["ciclo"].ToString();
                    usuario = dr["user_recibe"].ToString();
                    fdate = dr["fecha_recibe"].ToString();
                    loc = dr["localizacion"].ToString();
                    return true;
                }
            }
            catch (Exception Error)
            {
                throw new Exception("Error" + Error.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }

        public void Quiebreupdate(string p1, string lote, string tarima, string tarimas, int Compania)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL =  "Update inventario set parcial = 'YES', qty_cajas = " + @p1 + " where num_tarima = " + @tarima + " and total_tarimas = "+ @tarimas + " and lote = '"+ @lote + "' and(localizacion = 'E1' or localizacion = 'E2') and compania= '"+ @Compania + "' " ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_p1", p1);
                cmd.Parameters.AddWithValue("_lote", lote.Trim());
                cmd.Parameters.AddWithValue("_tarima", tarima.Trim());
                cmd.Parameters.AddWithValue("_tarimas", tarimas.Trim());
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        public void InsertarRegQuiebre(int Case, string Lote, int Wo, string Tray, int Ntarimas, int TotalTarimas, int QtyCaja, string Loc, string Dest, string Ciclo, int UserRecibe, int Compania)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL = "EXEC ShipCaseparciales @_Case, @_Lote, @_Wo, @_Tray, @_Ntarimas, @_TotalTarimas, @_QtyCaja, @_Loc, @_DEst, @_Ciclo,@_UserRecibe,@_Compania";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_Case", Case);
                cmd.Parameters.AddWithValue("_Lote", Lote.Trim());
                cmd.Parameters.AddWithValue("_Wo", Wo);
                cmd.Parameters.AddWithValue("_Tray", Tray.Trim());
                cmd.Parameters.AddWithValue("_Ntarimas", Ntarimas);
                cmd.Parameters.AddWithValue("_TotalTarimas", TotalTarimas);
                cmd.Parameters.AddWithValue("_QtyCaja", QtyCaja);
                cmd.Parameters.AddWithValue("_Loc", Loc.Trim());
                cmd.Parameters.AddWithValue("_Dest", Dest.Trim());
                cmd.Parameters.AddWithValue("_Ciclo", Ciclo.Trim());
                cmd.Parameters.AddWithValue("_UserRecibe", UserRecibe);
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                throw new Exception("Error: " + Error.Message);
            }
            finally
            {
                Conexion.Close();
            }
        }


        public bool M1LabelLoteDetenido(string lote)
        {
          
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL=  "Select lote from no_cargar where lote = '"+ @lote + "' and Compania= '"+ GlobalVar.Compania + "' " ;
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            SqlDataReader dr = null;
            try
            {
                this.Conexion.Open();
                cmd.Parameters.AddWithValue("_lote", lote);
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Read();
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error" + exception.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }

        public bool M3LabelLote(string lote)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select lote from inventario where id_carga = '"+ @lote + "' and Compania= '" + GlobalVar.Compania + "' " ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                this.Conexion.Open();
                cmd.Parameters.AddWithValue("_lote", lote);
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Read();
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error" + exception.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }

        }

        public bool M1LabelLoteDuplicado(string lote, string tarima, string tarimas, int Compania)
        {
           
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL =  "Select lote from inventario where lote='" + @lote + "' and num_tarima= '"+  @tarima + "' and  total_tarimas= '"+ @tarimas +  "' and Compania= '"+ @Compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                this.Conexion.Open();
                cmd.Parameters.AddWithValue("_lote", lote);
                cmd.Parameters.AddWithValue("_tarima", tarima);
                cmd.Parameters.AddWithValue("_tarimas", tarimas);
                cmd.Parameters.AddWithValue("_Compania", Compania);
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Read();
                   return true;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error" + exception.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }

        public void ObtenerIdEmpalme(ref string identificator, int Compania)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select Max(Id_Empalme) + 1 as identificador from inventario where Compania= '" + @Compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            SqlDataReader dr = null;
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    identificator = string.Empty;
                }
                else
                {
                    dr.Read();
                    identificator = dr["Identificador"].ToString().Trim();
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error" + exception.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }

        public void M2ActualizacionInventario(int empalme, int nombre, int Qty, int id, int Compania)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL = "Update inventario set id_empalme= '"+ @empalme +"', user_empalme= '" + @nombre +"', qty_cajas= '"+ @Qty + "' where id_tarima = '" + @id + "' and Compania= '"+ @Compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                this.Conexion.Open();
                cmd.Parameters.AddWithValue("_empalme", empalme);
                cmd.Parameters.AddWithValue("_nombre", nombre);
                cmd.Parameters.AddWithValue("_Qty", Qty);
                cmd.Parameters.AddWithValue("_id", id);
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        public void M2ActualizacionMapa(int Qty, int id, string lote, int Compania)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL =  "Update Inventario set qty_cajas= '" + @Qty +"' where id_empalme= '"+ @id +"' and lote= '"+ @lote +"' and Compania= '"+ @Compania + "' ";
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_Qty", Qty);
                cmd.Parameters.AddWithValue("_id", id);
                cmd.Parameters.AddWithValue("_lote", lote.Trim());
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        public DataTable M2CargarGrid(string loc, string lote, string ntarima, string ttarimas, int compania)
        {

            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select id_tarima AS [ID_1] ,Lote as [Lote1], qty_cajas as [Cajas1],Ciclo,Destino  from inventario where  id_empalme=0 and localizacion='"+ @loc + "' and lote ='"+ @lote +"' and num_tarima ='"+ @ntarima + "' and Total_tarimas ='"+ @ttarimas + "' and compania= '"+ @compania + "'";
            SqlCommand command = new SqlCommand(strSQL, this.Conexion);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                da.SelectCommand = command;
                da.Fill(dt);
                return dt;
            }
            catch (Exception Error)
            {
                throw new Exception("Error:" + Error);
            }
            finally
            {
                Conexion.Close();
            }
         
        }

        public DataTable M2CargarGridSinLote(string loc, int compania)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL =  "Select id_tarima AS [ID_1] ,Lote as [Lote1], qty_cajas as [Cajas1],Ciclo,Destino  from inventario where  id_empalme=0 and localizacion='"+ @loc + "'  and compania= '"+ @compania + "'" ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception Error)
            {
                throw new Exception("Error:" + Error);
            }
            finally
            {
                Conexion.Close();
            }
           
        }

        public DataTable M2MapaGrid(string id, int compania)
        {

            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select  WO, Tray,Lote, cast(num_tarima as varchar)+ ' / '+cast(total_tarimas as varchar) as [# Tarima],qty_cajas, user_empalme as Empalmador, verifica_empalme as Validador,destino,ciclo from inventario where id_empalme='" + @id + "' and compania= '" + @compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception Error)
            {
                throw new Exception("Error:" + Error);
            }
            finally
            {
                Conexion.Close();
            }

        }

              public void M2ValidacionTray(ref string Tray, string lote, int Compania)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL= "Select tray from inventario where lote= '"+ @lote + "' and (localizacion = 'E1' or localizacion = 'E2' or localizacion = 'E3') and Compania= '"+ @Compania + "' " ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    Tray = string.Empty;
                }
                else
                {
                    dr.Read();
                    Tray = (string)dr["tray"];
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("No se pudo calcular el metrico CBF, contactar a administrador de sistema" + Environment.NewLine + Environment.NewLine + exception.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }



        public bool M2ValidationLote(string lote, string caja, string empalme, int Compania)
        {
   
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL= "Select lote from inventario where lote= '"+ @lote + "' and qty_cajas= '"+ @caja + "' and id_empalme= '"+ @empalme + "' and (localizacion='E1' or localizacion='E2' or localizacion = 'E3') and Compania= '" + @Compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                this.Conexion.Open();
                cmd.Parameters.AddWithValue("_lote", lote.Trim());
                cmd.Parameters.AddWithValue("_caja", caja.Trim());
                cmd.Parameters.AddWithValue("_empalme", empalme.Trim());
                cmd.Parameters.AddWithValue("_Compania", Compania);
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Read();
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error" + exception.Message);
            }
            finally
            {
                dr.Close();
                this.Conexion.Close();
            }
           
        }

        public void M2ValidationActualizacion(string loc, int nombre, int cbftotal, string lote, string caja, string empalme, int Compania)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL = "update inventario set localizacion=  '" + @loc + "', verifica_empalme= '"+ @nombre +"',cbf= '" + @cbftotal +"' where lote= '"+ @lote +"' and qty_cajas= '"+ @caja +"' and id_empalme= '"+ @empalme + "' and (localizacion='E1' or localizacion='E2' or localizacion ='E3') and Compania= '"+ @Compania + "' ";
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_loc", loc.Trim());
                cmd.Parameters.AddWithValue("_nombre", nombre);
                cmd.Parameters.AddWithValue("_cbftotal", cbftotal);
                cmd.Parameters.AddWithValue("_lote", lote.Trim());
                cmd.Parameters.AddWithValue("_caja", caja.Trim());
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        public void M2ValidacionTotal(ref int Totalitem, string empalme, int Compania)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL ="Select count(lote) as items from Inventario where id_empalme= '"+ @empalme + "' and Compania= '"+ @Compania + "' " ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                this.Conexion.Open();
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    Totalitem = 0;
                }
                else
                {
                    dr.Read();
                    Totalitem = (Int32)dr["items"];
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo calcular el metrico CBF, contactar a administrador de sistema" + Environment.NewLine + Environment.NewLine + ex.Message);
            }
            finally
            {
                dr.Close();
                this.Conexion.Close();
            }
        }

        public DataTable M4DetallePiso(string loc, int compania)
        {
        
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL =  "Select WO, Lote, cast(num_tarima as varchar)+ ' / '+cast(total_tarimas as varchar) as Tarima,Qty_Cajas,Ciclo, Destino, ID_Empalme from inventario where localizacion= '"+ @loc + "' and compania= '"+ @compania + "' order by id_empalme desc " ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception Error)
            {
                throw new Exception("Error:" + Error);
            }
            finally
            {
                Conexion.Close();
            }
            
        }
        public DataTable Regresarlote(string lote, int compania)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select id_tarima,lote,cast(num_tarima as varchar)+ ' / '+cast(total_tarimas as varchar) as Tarima,localizacion,id_carga,posicion, Fecha_recibe  from inventario where lote = '"+ @lote + "' and compania= '"+ @compania + "'" ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception Error)
            {
                throw new Exception("Error:" + Error);
            }
            finally
            {
                Conexion.Close();
            }
           
        }

        public void RegresarloteInsertar(int Compania, string lote, string tarima, string fechain, int nombre, string regresa)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            SqlCommand cmd = new SqlCommand("INSERT INTO retorno_tarimas (Compania,Lote, Tarima, Fecha_Entrada, Fecha_Salida,User_Registra,User_lleva) VALUES (@_Compania,@_lote, @_tarima,@_fechain,getdate(),@_nombre,@_regresa)", Conexion);
            try
            {
                this.Conexion.Open();
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.Parameters.AddWithValue("_lote", lote.Trim());
                cmd.Parameters.AddWithValue("_tarima", tarima.Trim());
                cmd.Parameters.AddWithValue("_fechain", fechain.Trim());
                cmd.Parameters.AddWithValue("_nombre", nombre);
                cmd.Parameters.AddWithValue("_regresa", regresa.Trim());
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw new Exception("Error: " + exception.Message);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        public void DeleteTarima(string tarima, int Compania)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL = "Delete  from inventario where id_tarima= '"+ @tarima + "' and compania= '"+ @Compania + "' " ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_tarima", tarima.Trim());
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.ExecuteNonQuery();
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


        public void M4DockDetail(ref string lblcargas, ref string lblciclos, ref string lbldestinos, ref string lblcajas, ref ArrayList pos_cajas, ref ArrayList lotes, ref ArrayList num11s, ref ArrayList num12s, ref ArrayList qty1s, ref ArrayList empals, int empalme)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL =  "Select a.id_carga,posicion,caja,lote,num_tarima,total_tarimas,qty_cajas,id_empalme,a.ciclo,a.destino from cajas a left join inventario b on a.id_carga=b.id_carga where a.rampa="+ empalme + " and a.shipped = 'NO' and a.Compania= '"+ GlobalVar.Compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            SqlDataReader dr = null;
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (true)
                    {
                        if (!dr.Read())
                        {
                            break;
                        }
                        pos_cajas.Add(dr["posicion"].ToString());
                        lotes.Add(dr["lote"].ToString());
                        num11s.Add(dr["num_tarima"].ToString());
                        num12s.Add(dr["total_tarimas"].ToString());
                        qty1s.Add(dr["qty_cajas"].ToString());
                        empals.Add(dr["id_empalme"].ToString());
                        lblcargas = dr["id_carga"].ToString().Trim();
                        lblciclos = dr["ciclo"].ToString().Trim();
                        lbldestinos = dr["destino"].ToString().Trim();
                        lblcajas = dr["caja"].ToString().Trim();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo calcular el metrico CBF, contactar a administrador de sistema" + Environment.NewLine + Environment.NewLine + ex.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }

        public void GraficoxPersona(int Tops, string FechaInicial, string FechaFinal, int Compania, ref ArrayList User1, ref ArrayList Qty2)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            SqlCommand cmd = new SqlCommand("EXEC ShipWorkUser @_Tops, @_FechaInicial, @_FechaFinal, @_Compania", Conexion);
            SqlDataReader dr = null;
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_FechaInicial", FechaInicial.Trim());
                cmd.Parameters.AddWithValue("_FechaFinal", FechaFinal.Trim());
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.Parameters.AddWithValue("_Tops", Tops);
                dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {

                        User1.Add(dr["user_carga"].ToString());
                        Qty2.Add(dr["qty"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: Grafica" + ex.Message);
            }
            finally
            {
                this.Conexion.Close();
            }
        }

        public void GraficaxSemana(ref ArrayList Semana1, ref ArrayList Qtys)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            SqlCommand cmd = new SqlCommand("Select DatePart(WEEK,fecha_completa) as Weeks, count(id_carga) as Qty from cajas where shipped = 'YES' and Compania='" + GlobalVar.Compania + "' group by  DatePart(WEEK,fecha_completa) ", Conexion);
            SqlDataReader dr = null;
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (true)
                    {
                        if (!dr.Read())
                        {
                            break;
                        }
                        Semana1.Add(dr["Weeks"].ToString());
                        Qtys.Add(dr["Qty"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Grafica Por Semana" + Environment.NewLine + Environment.NewLine + ex.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }

        public void M4LayOut(ref ArrayList ocupados, ref ArrayList rampa)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "SELECT D.RAMPA as Rampita, ISNULL(E.ID_CARGA1,0)ID_CARGA1, ISNULL(OCUPADOS,0) OCUPADOS FROM DISTRIBUCION AS D LEFT JOIN (SELECT COUNT(LUGARES) AS OCUPADOS, ID_CARGA1, RAMPA1 FROM(SELECT DISTINCT(POSICION) AS LUGARES, A.ID_CARGA AS ID_CARGA1, A.RAMPA AS RAMPA1 FROM CAJAS AS A LEFT JOIN Inventario AS B ON B.ID_CARGA = A.ID_CARGA WHERE SHIPPED = 'NO' AND b.Compania = '"+ GlobalVar.Compania + "'  GROUP BY A.ID_CARGA, RAMPA, POSICION)  AS[%$##@_Alias] GROUP BY ID_CARGA1, RAMPA1)  AS E ON D.RAMPA=E.RAMPA1 WHERE D.RAMPA IS NOT NULL AND D.Compania= '"+ GlobalVar.Compania + "'  " ;
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            SqlDataReader dr = null;
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    MessageBox.Show("No se encontro informacion para monitorear");
                }
                else
                {
                    while (true)
                    {
                        if (!dr.Read())
                        {
                            break;
                        }
                        ocupados.Add(dr["Ocupados"].ToString().Trim());
                        rampa.Add(dr["Rampita"].ToString().Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Layout informacion" + Environment.NewLine + Environment.NewLine + ex.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
                SqlConnection.ClearPool(Conexion);
            }
        }

        public void M4LayOutPiso(ref ArrayList loc, ref ArrayList tam)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            SqlCommand cmd = new SqlCommand("SELECT DISTINCT(A.LOCALIZACION), ISNULL(B.TARIMAS1,0)TARIMAS1 FROM DISTRIBUCION A LEFT JOIN (SELECT COUNT(TARIMAS) AS TARIMAS1, LOCALIZACION FROM(SELECT DISTINCT(NUM_TARIMA) AS TARIMAS, LOTE, LOCALIZACION FROM Inventario WHERE Compania = '" + GlobalVar.Compania + "') AS[%$##@_Alias] GROUP BY LOCALIZACION) B ON A.LOCALIZACION=B.LOCALIZACION", Conexion);
            SqlDataReader dr = null;
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    MessageBox.Show("No se encontro informacion para monitorear en Piso");
                }
                else
                {
                    while (true)
                    { 
                        if (!dr.Read())
                        {
                            break;
                        }
                        loc.Add(dr["Localizacion"].ToString().Trim());
                        tam.Add(dr["Tarimas1"].ToString().Trim());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Layout informacion Piso" + Environment.NewLine + Environment.NewLine + ex.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
                SqlConnection.ClearPool(Conexion);
            }
        }

        public bool ModificarLocEmpalme(string empalme, string origen)
        {
            
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select id_empalme from inventario  where id_empalme='"+ @empalme + "' and id_carga=0 and localizacion='"+ @origen + "' and compania= '" + GlobalVar.Compania + "'" ;
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            SqlDataReader dr = null;
            try
            {
                this.Conexion.Open();
                cmd.Parameters.AddWithValue("_empalme", empalme.Trim());
                cmd.Parameters.AddWithValue("_origen", origen.Trim());
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Read();
                    return true;
                }
            }
            catch (Exception Error)
            {
                throw new Exception("Error modificar empalme" + Error.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
            
        }

        public bool ModificarLocLote(string lote, string origen, int tarima, int tarimas)
        {
            
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select lote from inventario  where lote='"+ @lote +"' and id_carga=0 and localizacion='" + @origen +"' and num_tarima="+ @tarima + " and id_empalme=0  and total_tarimas= '" + @tarimas +"' and compania = '" + GlobalVar.Compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            SqlDataReader dr = null;
            try
            {
                this.Conexion.Open();
                cmd.Parameters.AddWithValue("_lote", lote.Trim());
                cmd.Parameters.AddWithValue("_origen", origen.Trim());
                cmd.Parameters.AddWithValue("_tarima", tarima);
                cmd.Parameters.AddWithValue("_tarimas", tarimas);
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Read();
                    return true;
                }
            }
            catch (Exception Error)
            {
                throw new Exception("Error Moidificar locacion lote" + Error.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
           
        }

        public void ModificarLocEmpalmeAct(string destino, string lote, string origen)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Update inventario set localizacion ='"+ @destino +"' where id_empalme=" + @lote  +" and localizacion='"+ @origen +"' and id_carga=0 and compania= '" + GlobalVar.Compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_lote", lote.Trim());
                cmd.Parameters.AddWithValue("_destino", destino);
                cmd.Parameters.AddWithValue("_origen", origen);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error" + Environment.NewLine + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }


        public void ModificarLocLoteAct(string destino, string lote, string origen, int tarima, int tarimas)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL = "Update inventario set localizacion ='"+ @destino + "' where lote='" + @lote +"' and localizacion='"+ @origen +"' and num_tarima=" + @tarima +" and total_tarimas=" + @tarimas + " and id_carga=0 and compania= '"+  GlobalVar.Compania +"'";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                this.Conexion.Open();
                cmd.Parameters.AddWithValue("_lote", lote.Trim());
                cmd.Parameters.AddWithValue("_destino", destino.Trim());
                cmd.Parameters.AddWithValue("_origen", origen.Trim());
                cmd.Parameters.AddWithValue("_tarima", tarima);
                cmd.Parameters.AddWithValue("_tarimas", tarimas);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error" + Environment.NewLine + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        public DataTable M3Carga(string loc, string lote, int tarima)
        {
            
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select Id_tarima,Lote, Tray ,num_tarima as Tarima,total_tarimas as De, ciclo as Ciclo, id_empalme as ID_Emp, Destino from inventario where localizacion='"+ @loc +"' and (lote= '" +@lote +"' or CONVERT(varchar(10),Id_Empalme)= '" + @lote + "' )  and id_carga =0 and Num_Tarima= '"+ @tarima +"'  and compania= '" + GlobalVar.Compania + "' and Localizacion not IN ('E1','E2') order by id_empalme desc, lote asc";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                da.SelectCommand = cmd;
                da.Fill(dt);
               return dt;
            }
            catch (Exception Error)
            {
                throw new Exception("Error carga con lote:" + Error);
            }
            finally
            {
                Conexion.Close();
            }
          
        }

        public void M3sumaCbf(ref double total, string carga)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL =  "Select sum(cbf) as total from inventario where id_carga="+ @carga + " and Compania= '"+ GlobalVar.Compania + "' group by id_carga " ;
            SqlCommand command = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                Conexion.Open();
                dr = command.ExecuteReader();
                if (!dr.HasRows)
                {
                    idcarga = 0;
                }
                else
                {
                    dr.Read();
                    idcarga = Convert.ToDouble(dr["total"]);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error" + exception.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }

        public bool M3lotexIdCarga(string idcarga)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select Lote from Inventario where id_carga = '"+  @idcarga + "' and Compania= '"+ GlobalVar.Compania + "' " ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                this.Conexion.Open();
                cmd.Parameters.AddWithValue("_idcarga", idcarga);
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Read();
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error" + exception.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
            
        }

        public void M3ModificarMapa(string idcarga, int posicion, int cajasch, double cbfn, int idtarima1)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Update inventario set id_carga = '"+ @idcarga + "' , posicion = "+ @posicion + ", user_carga = "+ GlobalVar.nombre_user + ", fecha_carga = getdate(),localizacion='cargado',verificador=1, qty_cajas= " + @cajasch +",cbf='" + @cbfn + "' where id_tarima= "+ @idtarima1 + " and compania = "+ GlobalVar.Compania +" ";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_idcarga", idcarga);
                cmd.Parameters.AddWithValue("_posicion", posicion);
                cmd.Parameters.AddWithValue("_cajasch", cajasch);
                cmd.Parameters.AddWithValue("_cbfn", cbfn);
                cmd.Parameters.AddWithValue("_idtarima1", idtarima1);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        public void M3ModificarMapas(string idcarga, int posicion, int empalme)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL= "Update inventario set id_carga = '"+ @idcarga + "', posicion = "+ @posicion + ", user_carga = "+ GlobalVar.nombre_user +", fecha_carga = getdate(), verificador=1, localizacion='cargado'  where  id_empalme="+ @empalme + " and compania = "+ GlobalVar.Compania + " " ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_idcarga", idcarga);
                cmd.Parameters.AddWithValue("_posicion", posicion);
                cmd.Parameters.AddWithValue("_empalme", empalme);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        public bool M3carganota(ref string cajas, ref string dest, ref string ciclos, ref string rampa, ref string nota, ref string loc, string idcarga)
        {

            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select caja, a.ciclo, a.destino , a.rampa,notas,b.localizacion from cajas a inner join distribucion b on a.rampa=b.rampa where shipped= 'NO' and id_carga='"+ @idcarga + "'  and a.Compania= '"+ GlobalVar.Compania + "'" ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                cmd.Parameters.AddWithValue("_idcarga", idcarga.Trim());
                this.Conexion.Open();
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Read();
                    cajas = dr["caja"].ToString();
                    dest = dr["destino"].ToString();
                    ciclos = dr["ciclo"].ToString();
                    rampa = dr["rampa"].ToString();
                    nota = dr["notas"].ToString();
                    loc = dr["localizacion"].ToString();
                    return true;
                }
            }
            catch (Exception Error)
            {
                throw new Exception("Carga no esta registrada o ya ha sido completada" + Error);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
           
        }

        public bool M3CrearMapa(ref ArrayList pos, ref ArrayList tray, ref ArrayList lott, ref ArrayList qty, ref ArrayList tarima, ref ArrayList usuario, int idcarga)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
             string strSQL =  "Select posicion,tray,lote,qty_cajas, cast(num_tarima as varchar)+ ' - '+cast(total_tarimas as varchar)as tarima, user_carga from inventario where id_carga="+ @idcarga + "  and Compania= "+ GlobalVar.Compania + " order by posicion asc" ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                cmd.Parameters.AddWithValue("_idcarga", idcarga);
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                 while (dr.Read())
                    {
                        pos.Add(dr["posicion"].ToString());
                        tray.Add(dr["tray"].ToString());
                        lott.Add(dr["lote"].ToString());
                        qty.Add(dr["qty_cajas"].ToString());
                        tarima.Add(dr["tarima"].ToString());
                        usuario.Add(dr["user_carga"].ToString());
                   
                    }
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception Error)
            {
                throw new Exception("No hay Carga" + Error);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
            
        }

        public void M3ModificarEmbarcado(string caja, string idcarga)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL = "Update cajas set shipped = 'YES',fecha_completa= getdate() , usuario_cierre= " + GlobalVar.nombre_user + " where caja='" + @caja + "' and shipped='NO' and id_carga="+ @idcarga + " and compania = "+ GlobalVar.Compania +"";
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_idcarga", idcarga);
                cmd.Parameters.AddWithValue("_posicion", caja);
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }


        public void M3Embarcado(string idcarga)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL = "update Inventario set localizacion = 'Embarcado', Fecha_Embarcado = getdate(), User_Embarcado = "+ GlobalVar.nombre_user + " where id_carga = "+ @idcarga  + " and compania = "+ GlobalVar.Compania + " " ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                this.Conexion.Open();
                cmd.Parameters.AddWithValue("_idcarga", idcarga);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error" + Environment.NewLine + exception.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        public bool M3Parciales(ref string loct, string lote, string idcarga)
        {
           
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL= "Select lote,localizacion from inventario where lote='"+ @lote + "' and id_carga <>"+ @idcarga + " and Compania= "+ GlobalVar.Compania + " " ;
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            SqlDataReader dr = null;
            try
            {
                cmd.Parameters.AddWithValue("_idcarga", idcarga);
                cmd.Parameters.AddWithValue("_lote", lote.Trim());
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Read();
                    loct = dr["localizacion"].ToString().Trim();
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error no  encontro localizacion" + exception.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
            
        }

        public bool ConfigSMTP(ref string server,ref string smtp_user,ref int puerto,ref string mapa,ref string discre)
        {

            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select * from Config where Compania=" + GlobalVar.Compania + " ";
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            SqlDataReader dr = null;
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Read();
                    server = dr["smtp"].ToString().Trim();
                    smtp_user = dr["usuario"].ToString().Trim();
                    puerto = int.Parse(dr["puerto"].ToString());
                    mapa = dr["mapa_file"].ToString().Trim();
                    discre = dr["discre_file"].ToString().Trim();
                    return true;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error, no se encontro configuración" + exception.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }

        }

        public bool M3ParcialesLote(ref ArrayList lote, int carga)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select distinct(lote) as Lote1 from inventario where id_carga="+ @carga+ "  and Compania= '" + GlobalVar.Compania + "'" ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                cmd.Parameters.AddWithValue("_carga", carga);
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        lote.Add(dr["Lote1"]);
                    }
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error en parciales" + exception);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
            
        }

        public void M3DiscrepanciasAs400(ref ArrayList lote, ref ArrayList cajas, int carga)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL =  "select lote,sum( qty_cajas) as cajas_t from inventario where id_carga= "+ @carga + "  and Compania= '"+ GlobalVar.Compania + "' group by lote" ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                cmd.Parameters.AddWithValue("_carga", carga);
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        lote.Add(dr["lote"]);
                        cajas.Add(dr["cajas_t"]);

                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error en parciales" + exception);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }


        public bool M3LoteCompletos(ref ArrayList lotM3, ref ArrayList conteosM3, ref ArrayList totalsM3, string Carga, int Compania)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "SELECT count(NUM_TARIMA) AS NUMTARIMAS, LOTE, convert(varchar(10),TOTAL_TARIMAS)TTARIMAS, Id_Carga,Compania  FROM INVENTARIO where Id_Carga = "+ @Carga + " and Compania = "+ @Compania + "  GROUP BY LOTE, TOTAL_TARIMAS, Id_Carga,Compania having ((TOTAL_TARIMAS) - (COUNT(NUM_TARIMA))) > 0" ;
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            SqlDataReader dr = null;
            try
            {
                cmd.Parameters.AddWithValue("_Carga", Carga.ToString().Trim());
                cmd.Parameters.AddWithValue("_Compania", Compania);
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        lotM3.Add(dr["lote"]);
                        conteosM3.Add(dr["numtarimas"]);
                        totalsM3.Add(dr["ttarimas"]);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error No se completo operacion " + exception);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
           
        }

        public void M3CargaenProceso(ref ArrayList idcargas, ref ArrayList pos, ref ArrayList lott, ref ArrayList destino, ref ArrayList ciclo, ref ArrayList ntarima, ref ArrayList tarima, ref ArrayList qty, ref ArrayList idempalme, string idcarga)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL =  "Select a.id_carga as icarga,posicion,lote,num_tarima,total_tarimas,qty_cajas,id_empalme,a.ciclo as cicly,a.destino as destiny from cajas a left join inventario b on a.id_carga = b.id_carga where a.id_carga = "+ @idcarga + " and a.shipped = 'NO' and a.Compania= "+ GlobalVar.Compania + " " ;
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            SqlDataReader dr = null;
            try
            {
                cmd.Parameters.AddWithValue("_idcarga", idcarga);
                this.Conexion.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        idcargas.Add(dr["icarga"]);
                        pos.Add(dr["posicion"]);
                        lott.Add(dr["lote"]);
                        ntarima.Add(dr["num_tarima"]);
                        tarima.Add(dr["total_tarimas"]);
                        qty.Add(dr["qty_cajas"]);
                        idempalme.Add(dr["id_empalme"]);
                        ciclo.Add(dr["cicly"]);
                        destino.Add(dr["destiny"]);
                    }
                }
            }
            catch (Exception Error)
            {
                throw new Exception("No hay Carga " + Error);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }

        public ArrayList Localizaciones(string PrimerElermento)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select idloc, Localizacion From Localizacion Where Compania = '" + GlobalVar.Compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            ArrayList ArregloLineas = new ArrayList();
            SqlDataReader dr = null;

            switch (PrimerElermento)
            {
                case "S":
                    ArregloLineas.Add(new intElemento("-- SELECCIONE--", "0"));
                    break;
                case "T":
                    ArregloLineas.Add(new intElemento("-- TODOS--", "0"));
                    break;
            }
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ArregloLineas.Add(new intElemento(dr["Localizacion"].ToString().Trim(), dr["idloc"].ToString().Trim()));
                }
                return ArregloLineas;
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
                SqlConnection.ClearPool(Conexion);
            }
        }

        public void Embarcadofechas(int Tops, string FechaInicial, string FechaFinal, int Compania, ref ArrayList User1, ref ArrayList Qty2)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            SqlCommand cmd = new SqlCommand("EXEC ShipWorkUser @_Tops, @_FechaInicial, @_FechaFinal, @_Compania", this.Conexion);
            SqlDataReader dr = null;
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_FechaInicial", FechaInicial.Trim());
                cmd.Parameters.AddWithValue("_FechaFinal", FechaFinal.Trim());
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.Parameters.AddWithValue("_Tops", Tops);
                dr = cmd.ExecuteReader();
                if (dr.HasRows == true)
                {
                    while (dr.Read())
                    {
                        User1.Add(dr["user_carga"].ToString());
                        Qty2.Add(dr["qty"].ToString());
                    }
                }
            }
            catch (Exception Error)
            {
                throw new Exception("Error: Grafica" + Error.Message);
            }
            finally
            {
                Conexion.Close();
            }
        }

        public DataTable ReporteGeneral(int Tops, string Loc, int Compania)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            SqlCommand cmd = new SqlCommand("EXEC ShipReportGeneral @_Tops, @_Loc, @_Compania", Conexion);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_Loc", Loc);
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.Parameters.AddWithValue("_Tops", Tops);
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception Error)
            {
                throw new Exception("Error: Reporte General" + Error.Message);
            }
            finally
            {
                this.Conexion.Close();
            }
           
        }

        public DataTable ShipReportEmbarcado(int Compania, string FechaInicial, string FechaFinal)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            SqlCommand cmd = new SqlCommand("EXEC ShipReportEmbarcado  @_Compania, @_FechaInicial, @_FechaFinal", Conexion);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.Parameters.AddWithValue("_FechaInicial", FechaInicial.Trim());
                cmd.Parameters.AddWithValue("_FechaFinal", FechaFinal.Trim());
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception Error)
            {
                throw new Exception("Error: Reporte Embarcado" + Error.Message);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
                SqlConnection.ClearPool(Conexion);
            }
           
        }

        public DataTable ShipReportEmbarcadoResumen(int Compania, string FechaInicial, string FechaFinal)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            SqlCommand cmd = new SqlCommand("EXEC ShipReportEmbarcadoResumen  @_Compania, @_FechaInicial, @_FechaFinal", Conexion);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.Parameters.AddWithValue("_FechaInicial", FechaInicial.Trim());
                cmd.Parameters.AddWithValue("_FechaFinal", FechaFinal.Trim());
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception Error)
            {
                throw new Exception("Error: Reporte Embarcado" + Error.Message);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
                SqlConnection.ClearPool(Conexion);
            }

        }

        public void ObtenerFormatoArchivo(ref string Fecha)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            SqlCommand cmd = new SqlCommand("SELECT CONVERT(VARCHAR(8), GETDATE(), 112) AS Fecha", Conexion);
            SqlDataReader dr = null;
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    Fecha = string.Empty;
                }
                else
                {
                    dr.Read();
                    Fecha = (string)dr["Fecha"];
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
            }
        }

        public bool M3password(string passwords)
        {
            bool flag2;
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            string strSQL =  "Select * from passwordspeciales where pass='"+ @passwords + "'  and Compania= "+  GlobalVar.Compania + " " ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                //cmd.Parameters.AddWithValue("_passwords", passwords);
                this.Conexion.Open();
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Read();
                    return true;
                }
            }
            catch (Exception Error)
            {
                throw new Exception("Error no  encontro Password" + Error.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
            
        }

        public void LoteIncompletos(string Lote, int AS400, int Shipping, string PassworUsed, int Compania)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            SqlCommand cmd = new SqlCommand("INSERT INTO LoteIncompletos(Lote,AS400,Shipping, PassworUsed, Compania) VALUES (@_Lote, @_AS400, @_Shipping, @_PassworUsed, @_Compania)", Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_Lote", Lote.Trim());
                cmd.Parameters.AddWithValue("_AS400", AS400);
                cmd.Parameters.AddWithValue("_Shipping", Shipping);
                cmd.Parameters.AddWithValue("_PassworUsed", PassworUsed.Trim());
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.ExecuteNonQuery();
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

        public DataTable ObtenerFedexID(string idfed)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL =  "Select [Id_Carga],[Part_Number],[Lot_Number],[Brach],[Total_Pull],[Shipment_Type],[Embarcado],[UserId],[Trans_Date] from uploadFedex where Compania= '"+ GlobalVar.Compania + "' and id_carga= '"+ @idfed + "'" ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception Error)
            {
                throw new Exception("Error:" + Error);
            }
            finally
            {
                Conexion.Close();
            }
            
        }

        public DataTable ExcelSubido()
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            SqlCommand cmd = new SqlCommand("Select * from uploadFedex where Compania= '" + GlobalVar.Compania + "' and Trans_Date >=CONVERT(varchar(10),getdate(),110)", Conexion);
            SqlDataAdapter da = new SqlDataAdapter();
            DataTable dt = new DataTable();
            try
            {
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception exception)
            {
                throw new Exception("Error:" + exception);
            }
            finally
            {
                Conexion.Close();
            }
            
        }

        public void IngresarFedex(int Compania, string Caja)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            SqlCommand cmd = new SqlCommand("INSERT INTO FedexCajas (Compania,Caja,Shipped) VALUES (@_Compania,@_caja,'No')", Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.Parameters.AddWithValue("_caja", Caja.Trim());
                cmd.ExecuteNonQuery();
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

        public void ObtenerIdCargaFedex(ref int idcarga)
        {
            this.Conexion = new SqlConnection();
            this.Conexion.ConnectionString = CadenaCon;
            SqlCommand cmd = new SqlCommand(" Select top 1 Id_Carga from FedexCajas where Compania= '" + GlobalVar.Compania + "' order by Id_Carga desc", Conexion);
            SqlDataReader dr = null;
            try
            {
                this.Conexion.Open();
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    idcarga = 0;
                }
                else
                {
                    dr.Read();
                    idcarga = (Int32)dr["Id_Carga"];
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error" + exception.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }

        public void IdCargaFedex(string idcargafedex)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL =  " Select  Id_Carga,envio from FedexCajas where Compania= '"+ GlobalVar.Compania + "' and id_carga = '" + @idcargafedex + "' " ;
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            SqlDataReader dr = null;
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    idcarga = 0;
                }
                else
                {
                    dr.Read();
                    idcargafedex = dr["Id_Carga"].ToString().Trim();
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error" + exception.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }

        public void FedexLote(ref int idemb, string lote, ref int Ttarimas, string tarima, ref int Qty, ref string Envio, ref string qtyreq, ref string wo, ref string trays)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "SELECT b.id_embarque,[Lote],[WO],[Tray],[Num_Tarima],[Total_tarimas],[Qty_cajas],b.Shipment_Type,b.Total_Pull FROM [ShippingSystem].[dbo].[Inventario] a, uploadFedex b  where a.Compania= '" + GlobalVar.Compania +"' and a.lote = '"+ @lote +"' and a.Num_Tarima = '"+ @tarima +"'  and b.Part_Number=a.Tray and b.Trans_Date >=CONVERT(varchar(10),getdate(),110) and  b.Lot_Number= '"+ @lote +"' and embarcado is null ";
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            SqlDataReader dr = null;
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    idemb = (Int32)dr["id_embarque"];
                    lote = dr["Lote"].ToString().Trim();
                    Ttarimas = (Int32)dr["Total_tarimas"];
                    Qty = (Int32)dr["Qty_cajas"];
                    Envio = dr["Shipment_Type"].ToString().Trim();
                    qtyreq = dr["Total_Pull"].ToString().Trim();
                    wo = dr["wo"].ToString().Trim();
                    trays = dr["tray"].ToString().Trim();
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error" + exception.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }

        public void updatexcel()
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "update uploadFedex set UserId ="+ GlobalVar.nombre_user + " ,[Trans_Date] = getdate(),Compania = "+ GlobalVar.Compania + " where compania is null" ;
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            try
            {
                Conexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }


        public void InvFedex(string idcarga, string lote, string wo, string tray, string tarima1, string tarima2, string qtycajas, string destino, string posicion, int Compania)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            SqlCommand cmd = new SqlCommand("INSERT INTO InvFedex (id_carga,Compania,lote,wo,tray,num_tarima,total_tarimas,qty_cajas,Destino,Posicion,user_recibe,Fecha_recibe) VALUES (@_idcarga,@_Compania,@_lote,@_wo,@_tray, @_tarima1,@_tarima2,@_qtycajas,@_destino,@_posicion, '" + GlobalVar.nombre_user + "', getdate())", Conexion);
            try
            {
                Conexion.Open();
                cmd.Parameters.AddWithValue("_Compania", Compania);
                cmd.Parameters.AddWithValue("_idcarga", idcarga.Trim());
                cmd.Parameters.AddWithValue("_lote", lote.Trim());
                cmd.Parameters.AddWithValue("_wo", wo.Trim());
                cmd.Parameters.AddWithValue("_tray", tray.Trim());
                cmd.Parameters.AddWithValue("_tarima1", tarima1.Trim());
                cmd.Parameters.AddWithValue("_tarima2", tarima2.Trim());
                cmd.Parameters.AddWithValue("_qtycajas", qtycajas.Trim());
                cmd.Parameters.AddWithValue("_destino", destino.Trim());
                cmd.Parameters.AddWithValue("_posicion", posicion.Trim());
                cmd.ExecuteNonQuery();
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

        public void updatefedex(string embarcado, int idemb, string idcarga)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "update uploadFedex set embarcado ="+ @embarcado +",Id_Carga= '" + @idcarga + "'   where compania = '" + GlobalVar.Compania +"'  and id_embarque = '"+ @idemb +"' ";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        public void updatefedexCaja(string idemb)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL =  "update FedexCajas set Shipped ='Yes',Fecha_completa= getdate(),Usuario_cierre = '"+ GlobalVar.nombre_user + "'   where compania = '"+ GlobalVar.Compania + "'  and id_carga = '"+ @idemb + "' " ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        public void updateinventario(int embarcado, string ntarima, string lote)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL ="update Inventario set Qty_cajas =" + @embarcado +"  where compania = '" + GlobalVar.Compania + "'  and lote = '" + @lote +"' and num_tarima= '" + @ntarima + "' ";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        public bool VerificacionPosicion(string carga, string posicion)
        {
           
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL =  "Select id_carga from InvFedex where id_carga= '"+ @carga + "' and posicion = '"+ @posicion + "' AND Compania= '"+ GlobalVar.Compania + "'" ;
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                cmd.Parameters.AddWithValue("_carga", carga.Trim());
                cmd.Parameters.AddWithValue("_posicion", posicion.Trim());
                Conexion.Open();
                dr = cmd.ExecuteReader();

                if(dr.HasRows == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error" + exception);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
           
        }

        public bool VerificacionEmbarcado(string carga)
        {
           
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select id_carga from FedexCajas where id_carga= '"+ @carga + "' and shipped = 'Yes' AND Compania= '"+ GlobalVar.Compania + "'";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                cmd.Parameters.AddWithValue("_carga", carga.Trim());
              
                Conexion.Open();
                dr = cmd.ExecuteReader();

                if (dr.HasRows == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error" + exception);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }

        }

        public DataTable ShipReportEmbarcadoFedex(string carga, string envio)
        {
            
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "Select posicion, tray as Producto ,wo  as #_Orden,lote,Qty_cajas as Cajas,num_tarima as Tarimas,user_recibe As  Cargador from invfedex where id_carga= '"+ @carga + "' and compania = '"+ GlobalVar.Compania + "' and destino = '"+ @envio + "'" ;
            SqlCommand cmd = new SqlCommand(strSQL, this.Conexion);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                this.Conexion.Open();
                cmd.Parameters.AddWithValue("_carga", carga.Trim());
                cmd.Parameters.AddWithValue("_envio", envio.Trim());
                da.SelectCommand = cmd;
                da.Fill(dt);
                return dt;
            }
            catch (Exception Error)
            {
                throw new Exception("Error: Reporte Embarcado" + Error.Message);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
                SqlConnection.ClearPool(Conexion);
            }
           
        }

        public void ObtenerIdCargafedexExcel(string caja, string destino, ref int idcarga, ref string cajan, ref string dest)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "  SELECT a.Id_carga,a.caja, b.Destino from fedexcajas a, InvFedex b where  a.Compania= '" + GlobalVar.Compania + "' and a.id_carga= '" + @caja + "' and destino= '" + @destino + "'";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            SqlDataReader dr = null;
            try
            {
                Conexion.Open();
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    idcarga = 0;
                }
                else
                {
                    dr.Read();
                    idcarga = (Int32)dr["Id_carga"];
                    cajan = dr["caja"].ToString().Trim();
                    dest = dr["Destino"].ToString().Trim();
                }
            }
            catch (Exception Error)
            {
                throw new Exception("Error" + Error.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }


        public void ObtenerIdembarqueFedex(ref int idemb,ref string pn, ref string lote, ref string brach, ref string total, ref string tipo, ref int idcarga, string idembarque)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            SqlCommand cmd = new SqlCommand(" Select distinct Id_Embarque,Part_Number,Lot_Number,Brach,Total_Pull,Shipment_Type,isnull(a.Id_Carga,0)id_Cargas from uploadFedex a, invfedex b where a.Compania= '" + GlobalVar.Compania + "' and id_embarque= '"+ idembarque + "' and (embarcado is null or embarcado = 0)  ", Conexion);
            SqlDataReader dr = null;
            try
            {
                this.Conexion.Open();
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    idcarga = 0;
                }
                else
                {
                    dr.Read();
                    idcarga = (Int32)dr["Id_Cargas"];
                    idemb = (Int32)dr["Id_embarque"];
                    pn = dr["Part_Number"].ToString();
                    lote = dr["Lot_Number"].ToString();
                    brach = dr["Brach"].ToString();
                    total = dr["Total_Pull"].ToString();
                    tipo = dr["Shipment_Type"].ToString();
                   
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error" + exception.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }

        public void ObtenerIdembarqueFedexinv(ref int idemb, ref int posi, ref string pn, ref string lote, ref string brach, ref string total, ref string tipo, ref int idcarga, ref string embarcado, string idembarque)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            SqlCommand cmd = new SqlCommand(" Select a.Id_Embarque,a.Part_Number,a.Lot_Number,a.Brach,a.Total_Pull,a.Shipment_Type,isnull(a.Id_Carga,0)id_Cargas, b.posicion,isnull(a.embarcado,0)embarcado from uploadFedex a, invfedex b where a.Compania= '" + GlobalVar.Compania + "' and a.id_embarque= '" + idembarque + "' and b.lote = a.Lot_Number  and a.Total_Pull= b.Qty_cajas", Conexion);
            SqlDataReader dr = null;
            try
            {
                this.Conexion.Open();
                dr = cmd.ExecuteReader();
                if (!dr.HasRows)
                {
                    idcarga = 0;
                    posi = 0;
                    idemb = 0;
                    pn = "";
                    lote = "";
                    brach = "";
                    total = "";
                    embarcado = "";
                    tipo = "";
                }
                else
                {
                    dr.Read();
                    idcarga = (Int32)dr["Id_Cargas"];
                    posi = (Int32)dr["posicion"];
                    idemb = (Int32)dr["Id_embarque"];
                    pn = dr["Part_Number"].ToString();
                    lote = dr["Lot_Number"].ToString();
                    brach = dr["Brach"].ToString();
                    total = dr["Total_Pull"].ToString();
                    embarcado = dr["embarcado"].ToString();
                    tipo = dr["Shipment_Type"].ToString();

                }
            }
            catch (Exception exception)
            {
                throw new Exception("Error" + exception.Message);
            }
            finally
            {
                dr.Close();
                Conexion.Close();
            }
        }


        public void updatefedexmapa(string tipo, string pn, string lote, string branch, string total, int idembarcado)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "update uploadFedex set Part_Number= '" +@pn + "', Lot_Number= '" + @lote + "',Brach= '" + @branch + "', Total_Pull= '" + @total+ "' ,  Shipment_Type ='" + @tipo + "'  where compania = '" + GlobalVar.Compania + "' and Id_Embarque= '" + @idembarcado + "' ";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }



        public void updatefedexmapaFinal(string tipo, string pn, string lote, string branch, string total, string cargado, int idembarcado)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "update uploadFedex set Part_Number= '" + @pn + "', Lot_Number= '" + @lote + "',Brach= '" + @branch + "', Total_Pull= '" + @total + "' ,  Shipment_Type ='" + @tipo + "', embarcado='"+ @cargado + "'  where compania = '" + GlobalVar.Compania + "' and Id_Embarque= '" + @idembarcado + "' and embarcado <> 0 ";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

        public void updatefedexinventarioreturn( string lote, string total, string position)
        {
            Conexion = new SqlConnection();
            Conexion.ConnectionString = CadenaCon;
            string strSQL = "update Inventario set  Qty_cajas= (select sum(" + @total + " + Qty_cajas) from Inventario where compania = '" + GlobalVar.Compania + "' and lote= '" + @lote + "' and posicion= "+ @position + ") where compania = '" + GlobalVar.Compania + "' and lote= '" + @lote + "' and posicion= " + @position + "  ";
            SqlCommand cmd = new SqlCommand(strSQL, Conexion);
            try
            {
                Conexion.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception Error)
            {
                MessageBox.Show("Error" + Environment.NewLine + Error.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conexion.Close();
                Conexion.Dispose();
            }
        }

    }
}
