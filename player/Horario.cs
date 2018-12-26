using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace player
{
    class Horario
    {
        private SQLiteConnection connection;
        private string string_connection;
        private Object bloqueo = new Object();

        public Horario()
        {
            string_connection = string.Format(@"Data Source={0}; Version=3;", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Properties.Resources.FicheroSQL);
        }

        //Existencia de hora en bd
        public bool ExisteHorario()
        {
            lock (bloqueo)
            {
                bool existe = true;
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT count(*) as cont FROM horario");
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        if (datos.GetInt32(datos.GetOrdinal("cont")) == 0)
                        {
                            existe = false;
                        }
                    }
                    connection.Close();
                }
                return existe;
            }
        }
        //Existencia datos en horario auxilar
        public bool ExisteAuxiliar()
        {
            lock (bloqueo)
            {
                bool existe = true;
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT count(*) as cont FROM auxiliar");
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        if (datos.GetInt32(datos.GetOrdinal("cont")) == 0)
                        {
                            existe = false;
                        }
                    }
                    connection.Close();

                }
                return existe;
            }
        }
        //Se encarga de insertar un nuevo horario
        public void InsertoHorario(string hora_inicial, string hora_final)
        {
            lock (bloqueo)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"INSERT INTO horario ('hora_inicial', 'hora_final') VALUES ('{0}', '{1}');", hora_inicial, hora_final);
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        //Insertamos horario en mins (tabla auxiliar)
        public void InsertoHoraAux(int hora_inicial, int hora_final)
        {
            lock (bloqueo)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"INSERT INTO auxiliar ('hora_inicial', 'hora_final') VALUES ({0}, {1});", hora_inicial, hora_final);
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        //Se encarga de modificar un horario existente
        public void ModificarHorario(string hora_inicial, string hora_final)
        {
            lock (bloqueo)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"UPDATE horario SET hora_inicial='{0}', hora_final='{1}'", hora_inicial, hora_final);
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        //Borrar tabla auxiliar
        public void BorrarAuxiliar()
        {
            lock (bloqueo)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"DELETE FROM auxiliar");
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        //Recoge un horario (formato hora) de la BD
        public Tuple<string, string> RecogerHorario()
        {
            lock (bloqueo)
            {
                string h_ini = "";
                string h_fin = "";
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT hora_inicial, hora_final FROM horario");
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        //Recogemos los ficheros
                        h_ini = datos.GetString(datos.GetOrdinal("hora_inicial"));
                        h_fin = datos.GetString(datos.GetOrdinal("hora_final"));
                    }
                    connection.Close();
                }
                return new Tuple<string, string>(h_ini, h_fin);
            }
        }
        //Calcula con la tabla auxilar si comienza la reproduccion
        public bool HorarioReproduccion()
        {
            lock (bloqueo)
            {
                bool sol = false;
                //hora actual
                int now = Hour2min(DateTime.Now.ToString("HH:mm"));
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT hora_inicial, hora_final FROM auxiliar");
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        //Recogemos los ficheros
                        int h_ini = datos.GetInt32(datos.GetOrdinal("hora_inicial"));
                        int h_fin = datos.GetInt32(datos.GetOrdinal("hora_final"));
                        if (now > h_ini && now < h_fin)
                        {
                            sol = sol || true;
                        }
                    }
                    connection.Close();
                }
                return sol;
            }
        }
        //Convierte una hora (HH:mm) a minutos totales
        public int Hour2min(string hora)
        {
            int minutos = 0;
            string[] data = hora.Split(':');
            minutos = (Convert.ToInt32(data[0]) * 60) + Convert.ToInt32(data[1]);
            return minutos;
        }
        //Mira la ultima conexión de la tienda
        public int ShopLastConnect()
        {
            lock (bloqueo)
            {
                int last_con = 0;
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT last_connect FROM tienda");
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        //Recogemos los ficheros
                        last_con = datos.GetInt32(datos.GetOrdinal("last_connect"));
                    }
                    connection.Close();
                }
                return last_con;
            }
        }
        //Modificamos la ultima conexión de la tienda
        public void EditLastConnect(int timestamp)
        {
            lock (bloqueo)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"UPDATE tienda SET last_connect={0}", timestamp);
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
