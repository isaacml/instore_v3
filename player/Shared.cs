using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Data.SQLite;
using System.IO;

namespace player
{
    class Shared
    {
        private SQLiteConnection connection;
        private Object bloqueo = new Object();
        private List<string> musica;
        private string string_connection;
        private string estado;
        private string id_entidad;
        private string nom_entidad;
        private string instamsg;
        private int vol_musica;
        private int vol_publi;
        private int vol_msg;
        

        public Shared()
        {
            musica = new List<string>();
            string_connection = @"Data Source=shop.db; Version=3;";
        }

        //Estado de la tienda
        public string Status
        {
            get
            {
                lock (bloqueo)
                {
                    return estado;
                }
            }
            set
            {
                lock (bloqueo)
                {
                    estado = value;
                }
            }
        }
        //Identificador de la entidad
        public string IDEntidad
        {
            get
            {
                lock (bloqueo)
                {
                    return id_entidad;
                }
            }
            set
            {
                lock (bloqueo)
                {
                    id_entidad = value;
                }
            }
        }
        //Nombre de entidad
        public string Entidad
        {
            get
            {
                lock (bloqueo)
                {
                    return nom_entidad;
                }
            }
            set
            {
                lock (bloqueo)
                {
                    nom_entidad = value;
                }
            }
        }
        //Listado de Musica
        public List<string> Music
        {
            get
            {
                lock (bloqueo)
                {
                    return musica;
                }
            }
            set
            {
                lock (bloqueo)
                {
                    musica = value;
                }
            }
        }
        //Listado Sub-directorios
        public string InstaMSG
        {
            get
            {
                lock (bloqueo)
                {
                    return instamsg;
                }
            }
            set
            {
                lock (bloqueo)
                {
                    instamsg = value;
                }
            }
        }
        //Zona de Volumenes
        public int VolMusica //Volumen de la música
        {
            get
            {
                lock (bloqueo)
                {
                    using (connection = new SQLiteConnection(string_connection))
                    {
                        connection.Open();
                        string query = string.Format(@"SELECT musica FROM volumen");
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        SQLiteDataReader datos = cmd.ExecuteReader();
                        while (datos.Read())
                        {
                            vol_musica = datos.GetInt32(datos.GetOrdinal("musica"));
                        }
                        connection.Close();
                    }

                    return vol_musica;
                }
            }
            set
            {
                lock (bloqueo)
                {
                    using (connection = new SQLiteConnection(string_connection))
                    {
                        connection.Open();
                        string query = string.Format(@"UPDATE volumen SET musica={0}", value);
                        SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                        cmd_exc.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }
        public int VolPublicidad //Volumen de la publicidad
        {
            get
            {
                lock (bloqueo)
                {
                    using (connection = new SQLiteConnection(string_connection))
                    {
                        connection.Open();
                        string query = string.Format(@"SELECT publi FROM volumen");
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        SQLiteDataReader datos = cmd.ExecuteReader();
                        while (datos.Read())
                        {
                            vol_publi = datos.GetInt32(datos.GetOrdinal("publi"));
                        }
                        connection.Close();
                    }

                    return vol_publi;
                }
            }
            set
            {
                lock (bloqueo)
                {
                    using (connection = new SQLiteConnection(string_connection))
                    {
                        connection.Open();
                        string query = string.Format(@"UPDATE volumen SET publi={0}", value);
                        SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                        cmd_exc.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }
        public int VolMensajes //Volumen de los mensajes
        {
            get
            {
                lock (bloqueo)
                {
                    using (connection = new SQLiteConnection(string_connection))
                    {
                        connection.Open();
                        string query = string.Format(@"SELECT msg FROM volumen");
                        SQLiteCommand cmd = new SQLiteCommand(query, connection);
                        SQLiteDataReader datos = cmd.ExecuteReader();
                        while (datos.Read())
                        {
                            vol_msg = datos.GetInt32(datos.GetOrdinal("msg"));
                        }
                        connection.Close();
                    }

                    return vol_msg;
                }
            }
            set
            {
                lock (bloqueo)
                {
                    using (connection = new SQLiteConnection(string_connection))
                    {
                        connection.Open();
                        string query = string.Format(@"UPDATE volumen SET msg={0}", value);
                        SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                        cmd_exc.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
        }
    }
}
