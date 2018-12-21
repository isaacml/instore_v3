using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;

namespace player
{
    class Music
    {
        private SQLiteConnection connection;
        private Object bloqueo = new Object();
        private string string_connection;

        public Music()
        {
            string_connection = @"Data Source=shop.db; Version=3;";
        }

        //Guarda el directorio raiz en BD, para el caso de que se reinicie la máquina
        public void GuardarDirectorioRaiz(string dir)
        {
            lock (bloqueo)
            {
                //Evaluamos la existencia
                if (!ExisteDirectorioRaiz())
                {
                    //No existe: lo insertamos
                    using (connection = new SQLiteConnection(string_connection))
                    {
                        connection.Open();
                        string query = string.Format(@"INSERT INTO dir_root_music (dir) VALUES ('{0}');", dir);
                        SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                        cmd_exc.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                else
                {
                    //Existe: modificamos los datos de conexión
                    using (connection = new SQLiteConnection(string_connection))
                    {
                        connection.Open();
                        string query = string.Format(@"UPDATE dir_root_music SET dir='{0}'", dir);
                        SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                        cmd_exc.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }

        //Comprueba existencia de directorio raiz en BD
        public bool ExisteDirectorioRaiz()
        {
            lock (bloqueo)
            {
                bool existe = true;
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT count(*) as cont FROM dir_root_music");
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        //igual a zero: no existe
                        int cont = datos.GetInt32(datos.GetOrdinal("cont"));
                        if (cont == 0)
                        {
                            existe = false;
                        }
                    }
                    connection.Close();
                }
                return existe;
            }
        }
        //Carga el valor del server
        public string CargarDirectorioRaiz()
        {
            lock (bloqueo)
            {
                string output = "";
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT dir FROM dir_root_music");
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        output = datos.GetString(datos.GetOrdinal("dir"));

                    }
                    connection.Close();
                }
                return output;
            }
        }
        //Guarda directorios de música en BD (caso de reinicio)
        public void GuardarDirectoriosMusica(string dir)
        {
            lock (bloqueo)
            {
                //Evaluamos la existencia
                if (!ExisteDirectorioRaiz())
                {
                    //No existe ninguna carpeta: lo insertamos
                    using (connection = new SQLiteConnection(string_connection))
                    {
                        connection.Open();
                        string query = string.Format(@"INSERT INTO dir_root_music (dir) VALUES ('{0}');", dir);
                        SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                        cmd_exc.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                else
                {
                    //Existe: modificamos los datos de conexión
                    using (connection = new SQLiteConnection(string_connection))
                    {
                        connection.Open();
                        string query = string.Format(@"UPDATE dir_root_music SET dir='{0}'", dir);
                        SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                        cmd_exc.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }
        //Comprueba existencia de directorio de música en BD
        public bool ExisteDirectoriosMusica()
        {
            lock (bloqueo)
            {
                bool existe = true;
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT count(*) as cont FROM directorios");
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        //igual a zero: no existe
                        int cont = datos.GetInt32(datos.GetOrdinal("cont"));
                        if (cont == 0)
                        {
                            existe = false;
                        }
                    }
                    connection.Close();
                }
                return existe;
            }
        }
        //Insertar directorio música en BD
        public void InsertarDirectorioMusica(string directorio)
        {
            lock (bloqueo)
            {
                lock (bloqueo)
                {
                    //Se inserta en la BD
                    using (connection = new SQLiteConnection(string_connection))
                    {
                        connection.Open();
                        //Insertamos con el check 0, todavía no se ha chequeado ninguno
                        string query = string.Format(@"INSERT INTO directorios (directorio, checkpoint) VALUES ('{0}', {1});", directorio, 0);
                        SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                        cmd_exc.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }
        //Borrar directorios de música de BD
        public void BorrarDirectoriosMusica()
        {
            lock (bloqueo)
            {
                //borramos los directorios de música antigüos
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"DELETE FROM directorios");
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        //Borrar directorios de música de BD
        public List<string> ListadoDirectoriosMusica()
        {
            lock (bloqueo)
            {
                List<string> listado = new List<string>();
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT directorio FROM directorios");
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        //Recogemos nombre de directorio
                        string name = datos.GetString(datos.GetOrdinal("directorio"));
                        listado.Add(name);
                    }
                    connection.Close();
                }
                return listado;
            }
        }
        //Modifica en la basa de datos cuando se hace check en un directorio de música
        public void UpdateCheckMusica(string directorio, int check)
        {
            lock (bloqueo)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    //Cambio a Check
                    string query = string.Format(@"UPDATE directorios SET checkpoint={0} WHERE directorio='{1}'", check, directorio);
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        //Comprueba si un directorio está chequeado
        public bool IsCheck(string directorio)
        {
            lock (bloqueo)
            {
                bool res =  false;
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT checkpoint FROM directorios WHERE directorio='{0}'", directorio);
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        //Recogemos el estado del check: 1 marcado, 0 desmarcado
                        int check = datos.GetInt32(datos.GetOrdinal("checkpoint"));
                        if (check == 1)
                        {
                            res = true;
                        }
                    }
                    connection.Close();
                }
                return res;
            }
        }
    }
}
