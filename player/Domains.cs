using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace player
{
    class Domains
    {
        private SQLiteConnection connection;
        private Object bloqueo = new Object();
        private string string_connection;

        public Domains()
        {
            string_connection = string.Format(@"Data Source={0}; Version=3;", Path.GetFullPath("db/shop.db"));
        }
        //Envia listado de dominios
        public List<string> ListadoDominios()
        {
            return getDomains();
        }
        //Cadena de dominios para la URL
        public string CadenaDominios()
        {
            string output = "";
            //Se toman los dominios
            foreach (string d in getDomains())
            {
                //Formamos la cadena
                output += d + ":.:";
            }
            return output;
        }
        //Devuelve si existe o no un dominio
        public bool ExisteDominio(string dom)
        {
            return check_exist_domain(dom);
        }
        //Añadir dominio a la base de datos
        public void InsertarDominio(string domain)
        {
            lock (bloqueo)
            {
                //Se inserta en la BD
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"INSERT INTO dominios (dominio) VALUES ('{0}');", domain);
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        //Borrar Dominio de la BD
        public void BorrarDominio(string domain)
        {
            lock (bloqueo)
            {
                //borramos dominio de base de datos
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"DELETE FROM dominios WHERE dominio = '{0}'", domain);
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        //Recoger dominios de BD y guardar en un List<string>
        private List<string> getDomains()
        {
            lock (bloqueo)
            {
                List<string> dominios = new List<string>();
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT dominio FROM dominios");
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        // recogemos los datos
                        string dom = datos.GetString(datos.GetOrdinal("dominio"));
                        //Se guarda en listado de dominios
                        dominios.Add(dom);
                    }
                    connection.Close();
                }
                return dominios;
            }
        }
        //Mira la existencia de un dominio en BD: Devuelve TRUE (existe) o FALSE (no existe)
        private bool check_exist_domain(string domain)
        {
            lock (bloqueo)
            {
                bool exist_dom = true;
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT count(*) as cont FROM dominios WHERE dominio=('{0}');", domain);
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        // recogemos los datos
                        int cont = datos.GetInt32(datos.GetOrdinal("cont"));
                        if (cont == 0)
                        {
                            exist_dom = false;
                        }
                    }
                    connection.Close();
                }
                return exist_dom;
            }
        }
    }
}
