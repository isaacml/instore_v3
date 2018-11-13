using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            string_connection = @"Data Source=shop.db; Version=3;";
        }

        //Existencia de hora en bd
        public bool ExisteHorario(string tipo)
        {
            lock (bloqueo)
            {
                bool existe = true;
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT {0} FROM horario", tipo);
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    if (datos.StepCount == 0)
                    {
                        existe = false;
                    }
                    connection.Close();
                }
                return existe;
            }
        }
        //Se encarga de insertar un nuevo horario
        public void InsertoHorario(string tipo, string hora)
        {
            lock (bloqueo)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"INSERT INTO horario ({0}) VALUES ('{1}');", tipo, hora);
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        //Se encarga de modificar un horario existente
        public void ModificarHorario(string tipo, string hora)
        {
            lock (bloqueo)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"UPDATE horario SET {0}='{1}'", tipo, hora);
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        //Recoge un horario (formato hora) de la BD
        public string RecogerHorario(string tipo)
        {
            lock (bloqueo)
            {
                string hora = "";
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT {0} FROM horario", tipo);
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        //Recogemos los ficheros
                        hora = datos.GetString(datos.GetOrdinal(tipo));
                    }
                    connection.Close();
                }
                return hora;
            }
        }
    }
}
