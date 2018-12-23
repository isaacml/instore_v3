using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Net;
using System.IO;

namespace player
{
    class Connect
    {
        private SQLiteConnection connection;
        private Object bloqueo = new Object();
        private string string_connection;
        WebClient wCli;

        public Connect()
        {
            wCli = new WebClient();
            string_connection = string.Format(@"Data Source={0}; Version=3;", Path.GetFullPath("db/shop.db"));
        }

        //Guarda una conexion en BD (Server o Proxy)
        public void SaveConnection(string columna, string valor)
        {
            lock (bloqueo)
            {
                bool existe = true;
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT count(*) as cont FROM conection");
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
                //Evaluamos la existencia
                if (!existe)
                {
                    //No existe: lo insertamos
                    using (connection = new SQLiteConnection(string_connection))
                    {
                        connection.Open();
                        string query = string.Format(@"INSERT INTO conection ({0}) VALUES ('{1}');", columna, valor);
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
                        string query = string.Format(@"UPDATE conection SET {0}='{1}'", columna, valor);
                        SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                        cmd_exc.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }
        //Carga el valor del server
        public string LoadServer()
        {
            lock (bloqueo)
            {
                string output = "";
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT server FROM conection");
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        output = datos.GetString(datos.GetOrdinal("server"));
                        
                    }
                    connection.Close();
                }
                return output;
            }
        }
        //Carga el valor del proxy
        public string LoadProxy()
        {
            lock (bloqueo)
            {
                string output = "";
                try
                {
                    using (connection = new SQLiteConnection(string_connection))
                    {
                        connection.Open();
                        string query = string.Format(@"SELECT proxy FROM conection");
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        SQLiteDataReader datos = cmd.ExecuteReader();
                        while (datos.Read())
                        {
                            output = datos.GetString(datos.GetOrdinal("proxy"));

                        }
                        connection.Close();
                    }
                }
                catch
                {
                    output = "";
                }
                return output;
            }
        }
        //Determina si usamos o no el proxy
        public bool UseProxy()
        {
            bool output;
            string proxy = LoadProxy();

            if (proxy == "")
            {
                output = false;
            }
            else {
                output = true;
            }
            return output;
        }
        //Determina si podemos conectar con el servidor o no
        public bool CanConnectWithServer(string server)
        {
            bool con;
            try
            { 
                using (wCli.OpenRead(server))
                {
                    con = true;
                }
            }
            catch
            {
                con = false;
            }
            return con;
        }
    }
}
