﻿using System.Data;
using Microsoft.Data.SqlClient;
using Entidades;
using Entidades.Enumerados;
using System.Collections.Specialized;

namespace ADO
{
    public class AccesoDatos
    {
        #region Atributos

        private static string cadena_conexion;
        private SqlConnection conexion;
        private SqlCommand? comando;
        private SqlDataReader? lector;

        #endregion

        #region Constructores

        static AccesoDatos()
        {
            AccesoDatos.cadena_conexion = Properties.Resources.miConexion;
        }

        public AccesoDatos()
        {
            // CREO UN OBJETO SQLCONECTION
            this.conexion = new SqlConnection(AccesoDatos.cadena_conexion);
        }

        #endregion

        #region Métodos

        #region Probar conexión

        public bool ProbarConexion()
        {
            bool rta = true;

            try
            {
                this.conexion.Open();
            }
            catch (Exception e)
            {
                rta = false;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }

            return rta;
        }

        #endregion

        #region Select

        public List<SistemaOperativo> ObtenerListaSistemas()
        {
            List<SistemaOperativo> lista = new List<SistemaOperativo>();

            try
            {
                this.comando = new SqlCommand();

                this.comando.CommandType = CommandType.Text;
                this.comando.CommandText = "SELECT id, nombre, version, espacioGB, estadoSoporte, tipo, edicion, virtualizacionPermitida, distribucion, interfazGrafica, integracionIcloud, compatibleConProcesadorApple FROM tabla";
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                this.lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    int tipo = (int)lector["tipo"];
                    switch (tipo)
                    {
                        case 0:
                            //windows
                            Windows windows = new Windows();
                            windows.Id = (int)lector["id"];
                            windows.Nombre = lector["nombre"].ToString();
                            windows.Version = lector["version"].ToString();
                            windows.EspacioGB = Math.Round(Convert.ToDouble(lector["espacioGB"]), 2);
                            windows.Soporte = (EEstadoSoporte)lector["estadoSoporte"];
                            windows.Edicion = (EEdicionWindows)lector["edicion"];
                            windows.VirtualizacionPermitida = (bool)lector["virtualizacionPermitida"];
                            lista.Add(windows);
                            break;
                        case 1:

                            //mac
                            MacOS mac = new MacOS();
                            mac.Id = (int)lector["id"];
                            mac.Nombre = lector["nombre"].ToString();
                            mac.Version = lector["version"].ToString();
                            mac.EspacioGB = Math.Round(Convert.ToDouble(lector["espacioGB"]), 1);
                            mac.Soporte = (EEstadoSoporte)lector["estadoSoporte"];
                            mac.CompatibleConProcesadorApple = (bool)lector["compatibleConProcesadorApple"];
                            mac.IntegracionIcloud = (bool)lector["integracionIcloud"];
                            lista.Add(mac);
                            break;
                        case 2:
                            //linux
                            Linux linux = new Linux();
                            linux.Id = (int)lector["id"];
                            linux.Nombre = lector["nombre"].ToString();
                            linux.Version = lector["version"].ToString();
                            linux.EspacioGB = Math.Round(Convert.ToDouble(lector["espacioGB"]), 1);
                            linux.Soporte = (EEstadoSoporte)lector["estadoSoporte"];
                            linux.Distribucion = (EDistribucionLinux)lector["distribucion"];
                            linux.InterfazGrafica = (bool)lector["interfazGrafica"];
                            lista.Add(linux);
                            break;

                    }
                }
                lector.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }

            return lista;
        }

        #endregion

        #region Insert

        public bool AgregarSistema(SistemaOperativo sistema)
        {
            bool rta = true;

            try
            {
                this.comando = new SqlCommand();
                string sql;

                this.comando.Parameters.AddWithValue("@nombre", sistema.Nombre);
                this.comando.Parameters.AddWithValue("@version", sistema.Version);
                this.comando.Parameters.AddWithValue("@espacioGB", sistema.EspacioGB);
                this.comando.Parameters.AddWithValue("@estadoSoporte", (int)sistema.Soporte);
                sql = "INSERT INTO tabla (nombre, version, espacioGB, estadoSoporte) VALUES (@nombre, @version, @espacioGB, @estadoSoporte)";

                if (sistema.GetType() == typeof(Windows))
                {
                    Windows windows = (Windows)sistema;
                    this.comando.Parameters.AddWithValue("@edicion", (int)windows.Edicion);
                    this.comando.Parameters.AddWithValue("@virtualizacionPermitida", windows.VirtualizacionPermitida);
                    this.comando.Parameters.AddWithValue("@tipo", (int)ETipoSistema.Windows);

                    sql = "INSERT INTO tabla (nombre, version, espacioGB, estadoSoporte, edicion, virtualizacionPermitida, tipo) VALUES (@nombre, @version, @espacioGB, @estadoSoporte, @edicion, @virtualizacionPermitida, @tipo)";

                }
                else if(sistema.GetType() == typeof(Linux))
                {
                    Linux linux = (Linux)sistema;
                    this.comando.Parameters.AddWithValue("@distribucion", (int)linux.Distribucion);
                    this.comando.Parameters.AddWithValue("@interfazGrafica", linux.InterfazGrafica);
                    this.comando.Parameters.AddWithValue("@tipo", (int)ETipoSistema.Linux);


                    sql = "INSERT INTO tabla (nombre, version, espacioGB, estadoSoporte, distribucion, interfazGrafica, tipo) VALUES (@nombre, @version, @espacioGB, @estadoSoporte, @distribucion, @interfazGrafica, @tipo)";

                }
                else if (sistema.GetType() == typeof(MacOS))
                {
                    MacOS mac = (MacOS)sistema;
                    this.comando.Parameters.AddWithValue("@integracionIcloud", mac.IntegracionIcloud);
                    this.comando.Parameters.AddWithValue("@compatibleConProcesadorApple", mac.CompatibleConProcesadorApple);
                    this.comando.Parameters.AddWithValue("@tipo", (int)ETipoSistema.MacOS);


                    sql = "INSERT INTO tabla (nombre, version, espacioGB, estadoSoporte, integracionIcloud, compatibleConProcesadorApple, tipo) VALUES (@nombre, @version, @espacioGB, @estadoSoporte, @integracionIcloud, @compatibleConProcesadorApple, @tipo)";

                }

                this.comando.CommandType = CommandType.Text;
                this.comando.CommandText = sql;
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    rta = false;
                }

            }
            catch (Exception e)
            {
                rta = false;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }

            return rta;
        }

        #endregion

        #region Update

        /// <summary>
        /// Modifica un elemento de la DB por ID, si el id no existe o por algun otra razon no modificó el elemento, retorna false
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool ModificarSistema(SistemaOperativo sistema)
        {
            bool rta = true;
            try
            {
                this.comando = new SqlCommand();

                this.comando.Parameters.AddWithValue("@nombre", sistema.Nombre);
                this.comando.Parameters.AddWithValue("@version", sistema.Version);
                this.comando.Parameters.AddWithValue("@espacioGB", sistema.EspacioGB);
                this.comando.Parameters.AddWithValue("@estadoSoporte", (int)sistema.Soporte);
                this.comando.Parameters.AddWithValue("@id", sistema.Id);

                string sql = "UPDATE tabla ";
                sql += "SET nombre = @nombre, version = @version, espacioGB = @espacioGB, estadoSoporte = @estadoSoporte ";
               
                if (sistema.GetType() == typeof(Windows))
                {
                    Windows windows = (Windows)sistema;
                    this.comando.Parameters.AddWithValue("@edicion", (int)windows.Edicion);
                    this.comando.Parameters.AddWithValue("@virtualizacionPermitida", windows.VirtualizacionPermitida);
                    this.comando.Parameters.AddWithValue("@tipo", (int)ETipoSistema.Windows);

                    sql += ", edicion = @edicion, virtualizacionPermitida = @virtualizacionPermitida, tipo = @tipo";

                }
                else if (sistema.GetType() == typeof(Linux))
                {
                    Linux linux = (Linux)sistema;
                    this.comando.Parameters.AddWithValue("@distribucion", (int)linux.Distribucion);
                    this.comando.Parameters.AddWithValue("@interfazGrafica", linux.InterfazGrafica);
                    this.comando.Parameters.AddWithValue("@tipo", (int)ETipoSistema.Linux);

                    sql += ", distribucion = @distribucion, interfazGrafica = @interfazGrafica, tipo = @tipo";

                }
                else if (sistema.GetType() == typeof(MacOS))
                {
                    MacOS mac = (MacOS)sistema;
                    this.comando.Parameters.AddWithValue("@integracionIcloud", mac.IntegracionIcloud);
                    this.comando.Parameters.AddWithValue("@compatibleConProcesadorApple", mac.CompatibleConProcesadorApple);
                    this.comando.Parameters.AddWithValue("@tipo", (int)ETipoSistema.MacOS);

                    sql += ", integracionIcloud = @integracionIcloud, compatibleConProcesadorApple = @compatibleConProcesadorApple, tipo = @tipo";

                }

                sql += " WHERE id = @id";

                this.comando.CommandType = CommandType.Text;
                this.comando.CommandText = sql;
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    rta = false;
                }

            }
            catch (Exception)
            {
                rta = false;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }

            return rta;
        }

        #endregion

        #region Delete

        public bool EliminarSistema(int id)
        {
            bool rta = true;

            try
            {
                this.comando = new SqlCommand();

                this.comando.Parameters.AddWithValue("@id", id);

                string sql = "DELETE FROM tabla ";
                sql += "WHERE id = @id";

                this.comando.CommandType = CommandType.Text;
                this.comando.CommandText = sql;
                this.comando.Connection = this.conexion;

                this.conexion.Open();

                int filasAfectadas = this.comando.ExecuteNonQuery();

                if (filasAfectadas == 0)
                {
                    rta = false;
                }

            }
            catch (Exception)
            {
                rta = false;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }

            return rta;
        }

        #endregion


        #region UObtenerID
        /// <summary>
        /// Devuelve el ultimo ID generado en la tabla de la base de datos, si no se creo ningun objeto
        /// devuleve un 0, Si tuvo excepciones devulve -1
        /// </summary>
        /// <returns></returns>
        public int ObtenerUltimoIdGenerado()
        {
            int ultimoId;

            try
            {
                string sql = "SELECT IDENT_CURRENT('tabla')";

                this.comando = new SqlCommand(sql, this.conexion);

                this.conexion.Open();
                object result = comando.ExecuteScalar();
                if (result != DBNull.Value) // representa un valor nulo de la base de datos 
                {
                    ultimoId = Convert.ToInt32(result);
                }
                else
                {
                    ultimoId = 0;
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                ultimoId = -1;
            }
            finally
            {
                if (this.conexion.State == ConnectionState.Open)
                {
                    this.conexion.Close();
                }
            }

            return ultimoId;
        }

        #endregion
        #endregion
    }
}
