using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;
using System.Data.SQLite;
using System.IO;

namespace player
{
    class Shared
    {
        SQLiteConnection connection;
        private List<string> publi;
        private List<string> msg;
        private Object bloqueo = new Object();
        private string string_connection = @"Data Source=shop.db; Version=3;";

        public Shared()
        {
            publi = new List<string>();
            msg = new List<string>();
        }

        public void recogerListado(WebClient wClient)
        {
            lock (bloqueo)
            {
                publi.Clear();
                msg.Clear();

                string output = "";
                string url = HttpUtility.UrlPathEncode("http://192.168.0.102:8080/acciones.cgi?action=send_domains&dominios=Acciona.Transmediterranea.España.Andalucia.Malaga.ACC FORTUNY:.:");
                string res = wClient.DownloadString(url);

                string[] lista = Regex.Split(res, @"\[publi];");
                //EXISTEN FICHEROS DE PUBLICIDAD 
                if (lista.Length > 1)
                {   //Se comprueba si hay ficheros de mensajes
                    bool exist_msg = lista[1].Contains(@"[mensaje];");
                    if (!exist_msg)
                    {
                        // No hay ficheros de mensaje, solo ficheros de publicidad
                        string[] publi_container = Regex.Split(lista[1], @"\;");
                        foreach (string s_publi in publi_container)
                        {
                            string cl_publi = borrarString(s_publi, @"[mensaje]");
                            //Separa los datos de publicidad
                            string[] only_publi = Regex.Split(cl_publi, @"\<=>");
                            //Obtiene nombre de fichero publi + fecha_inicio + fecha_fin + GAP
                            output = only_publi[0] + ";" + only_publi[1] + ";" + only_publi[2] + ";" + only_publi[3];
                            //Se comprueba la existencia de la publicidad en la base de datos
                            bool exist = existFileInBD(only_publi[0], "publi");
                            //Guarda la publicidad en la tabla publi de la tienda
                            savePubliInBD(exist, only_publi[0], only_publi[1], only_publi[2], only_publi[3]);
                            //Guarda en list de publicidad
                            publi.Add(output);
                        }
                    }
                    else
                    {
                        // Hay ficheros de publicidad y de mensaje
                        string[] lista_mensaje = Regex.Split(lista[1], @"\[mensaje];");
                        if (lista_mensaje.Length > 1)
                        {
                            //PUBLICIDAD
                            string[] publi_container = lista_mensaje[0].Split(';');
                            foreach (string s_publi in publi_container)
                            {
                                //Separa los datos de publicidad
                                string[] only_publi = Regex.Split(s_publi, @"\<=>");
                                //Obtiene nombre de fichero publi + fecha_inicio + fecha_fin + GAP
                                output = only_publi[0] + ";" + only_publi[1] + ";" + only_publi[2] + ";" + only_publi[3];
                                //Se comprueba la existencia de la publicidad en la base de datos
                                bool exist = existFileInBD(only_publi[0], "publi");
                                //Guarda la publicidad en la tabla publi de la tienda
                                savePubliInBD(exist, only_publi[0], only_publi[1], only_publi[2], only_publi[3]);
                                //Guarda en list de publicidad
                                publi.Add(output);
                            }
                            //MENSAJES
                            string[] msg_container = lista_mensaje[1].Split(';');
                            foreach (string s_msg in msg_container)
                            {
                                //Separa los datos de mensaje
                                string[] sep_msg = Regex.Split(s_msg, @"\<=>");
                                //Obtiene nombre de fichero mensaje + fecha_inicio + fecha_fin + Hora
                                output = sep_msg[0] + ";" + sep_msg[1] + ";" + sep_msg[2] + ";" + sep_msg[3];
                                //Se comprueba la existencia del mensaje en la base de datos
                                bool exist = existFileInBD(sep_msg[0], "mensaje");
                                //Guarda el mensaje en la tabla msg de la tienda
                                saveMsgInBD(exist, sep_msg[0], sep_msg[1], sep_msg[2], sep_msg[3]);
                                //Guarda en el list de mensaje
                                msg.Add(output);
                            }
                        }
                    }
                }
                //NO EXISTEN FICHEROS DE PUBLICIDAD 
                else
                {
                    string[] lst_msg= Regex.Split(res, @"\[mensaje];");
                    if (lst_msg.Length > 1)
                    {   // Solo ficheros de mensajes
                        string[] msg_container = lst_msg[1].Split(';');
                        foreach (string s_msg in msg_container)
                        {
                            //Separa los datos de mensaje
                            string[] sep_msg = Regex.Split(s_msg, @"\<=>");
                            //Obtiene nombre de fichero mensaje + fecha_inicio + fecha_fin + Hora
                            output = sep_msg[0] + ";" + sep_msg[1] + ";" + sep_msg[2] + ";" + sep_msg[3];
                            //Se comprueba la existencia del mensaje en la base de datos
                            bool exist = existFileInBD(sep_msg[0], "mensaje");
                            //Guarda el mensaje en la tabla msg de la tienda
                            saveMsgInBD(exist, sep_msg[0], sep_msg[1], sep_msg[2], sep_msg[3]);
                            //Guarda en el list de mensaje
                            msg.Add(output);
                        }
                    }
                }
            }
        }
        //Devuelve el estado de la tienda (Activada/Desactivada)
        public string estadoTienda(WebClient wClient)
        {
            lock (bloqueo)
            {
                string data = wClient.DownloadString("http://192.168.0.102:8080/acciones.cgi?action=check_entidad&ent=Acciona");
                return data;
            }
        }
        //Devuelve el listado completo de publicidad
        public List<string> getPubli()
        {
            lock (bloqueo)
            {
                return publi;
            }
        }
        //Devuelve el listado completo de los mensajes
        public List<string> getMsg()
        {
            lock (bloqueo)
            {
                return msg;
            }
        }
        //Mira la existencia de un fichero en una tabla (publi/msg): Devuelve TRUE (existe) o FALSE (no existe)
        private bool existFileInBD(string namefile, string table)
        {
            lock (bloqueo)
            {
                bool existe = true;
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT count(*) as cont FROM {0} WHERE fichero=('{1}');", table, namefile);
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        // recogemos los datos
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
        //Compara la publicidad en la base de datos interna (TIENDA) con la que recibe del server_externo
        //Si alguno de los datos ha cambiado se procede a la modificación
        private bool getChangesInPubli(string namefile, string f_ini, string f_fin, string gap)
        {
            lock (bloqueo)
            {
                bool change = false;
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT fecha_ini, fecha_fin, gap FROM publi WHERE fichero=('{0}');", namefile);
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        //Recogemos los datos
                        string f_ini_bd = datos.GetOrdinal("fecha_ini").ToString();
                        string f_fin_bd = datos.GetOrdinal("fecha_fin").ToString();
                        int gap_bd = datos.GetInt32(datos.GetOrdinal("gap"));
                        //Comprobamos si los datos son distintos
                        if (f_ini_bd != f_ini || f_fin_bd != f_fin || gap_bd != Convert.ToInt32(gap))
                        {
                            change = true; //Se realiza el cambio
                        }
                    }
                    connection.Close();
                }
                return change;
            }
        }
        //Compara el mensaje en la base de datos interna (TIENDA) con el que recibe del server_externo
        //Si alguno de los campos ha cambiado se procede a la modificación
        private bool getChangesInMsg(string namefile, string f_ini, string f_fin, string playtime)
        {
            lock (bloqueo)
            {
                bool change = false;
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT fecha_ini, fecha_fin, playtime FROM mensaje WHERE fichero=('{0}');", namefile);
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        //Recogemos los datos
                        string f_ini_bd = datos.GetOrdinal("fecha_ini").ToString();
                        string f_fin_bd = datos.GetOrdinal("fecha_fin").ToString();
                        string playtime_bd = datos.GetOrdinal("playtime").ToString();
                        //Comprobamos si los datos son distintos
                        if (f_ini_bd != f_ini || f_fin_bd != f_fin || playtime != playtime_bd)
                        {
                            change = true; //Se realiza el cambio
                        }
                    }
                    connection.Close();
                }
                return change;
            }
        }
        //Se encarga de insertar un fichero de publicidad en la base de datos de la tienda
        private void insertPubliInBD(string filename, string stat, string f_ini, string f_fin, string gap)
        {
            lock (bloqueo)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"INSERT INTO publi (fichero, existe, fecha_ini, fecha_fin, gap) VALUES ('{0}','{1}','{2}','{3}','{4}');",
                        filename, stat, f_ini, f_fin, gap);
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        //Se encarga de insertar un fichero de mensaje en la base de datos de la tienda
        private void insertMsgInBD(string filename, string stat, string f_ini, string f_fin, string playtime)
        {
            lock (bloqueo)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"INSERT INTO mensaje (fichero, existe, fecha_ini, fecha_fin, playtime) VALUES ('{0}','{1}','{2}','{3}','{4}');",
                        filename, stat, f_ini, f_fin, playtime);
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        //Se encarga de modificar un fichero de publicidad en la base de datos de la tienda
        private void updatePubliInBD(string filename, string f_ini, string f_fin, string gap)
        {
            lock (bloqueo)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"UPDATE publi SET fecha_ini='{0}', fecha_fin='{1}', gap='{2}' WHERE fichero='{3}';",
                        f_ini, f_fin, gap, filename);
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        //Se encarga de modificar un fichero de publicidad en la base de datos de la tienda
        private void updateMsgInBD(string filename, string f_ini, string f_fin, string playtime)
        {
            lock (bloqueo)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"UPDATE mensaje SET fecha_ini='{0}', fecha_fin='{1}', playtime='{2}' WHERE fichero='{3}';",
                        f_ini, f_fin, playtime, filename);
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        //Gestiona el guardado de publicidad en la base de datos de la tienda (insertado/modificado)
        private void savePubliInBD(bool existe, string filename, string f_ini, string f_fin, string gap)
        {
            lock (bloqueo)
            {
                if (existe)
                {
                    //Ya existe, comprobamos que los datos han cambiado
                    if (getChangesInPubli(filename, f_ini, f_fin, gap))
                    {
                        updatePubliInBD(filename, f_ini, f_fin, gap); //Se modifican los datos
                    }
                }
                else //No existe
                {
                    //Se comprueba si la tienda tiene el fichero de publicidad en su carpeta PUBLI.
                    bool InDir = File.Exists("PUBLI/" + filename);
                    if (InDir)
                    {
                        // LO TIENE, se guarda en la BD con el estado en Y
                        insertPubliInBD(filename, "Y", f_ini, f_fin, gap);
                    }
                    else
                    {
                        // NO LO TIENE, se guarda en BD con el estado en N
                        insertPubliInBD(filename, "N", f_ini, f_fin, gap);
                    }
                }
            }
        }
        //Gestiona el guardado de mensajes en la base de datos de la tienda (insertado/modificado)
        private void saveMsgInBD(bool existe, string filename, string f_ini, string f_fin, string playtime)
        {
            lock (bloqueo)
            {
                if (existe)
                {
                    //Ya existe, comprobamos que los datos han cambiado
                    if (getChangesInMsg(filename, f_ini, f_fin, playtime))
                    {
                        updateMsgInBD(filename, f_ini, f_fin, playtime); //Se modifican los datos
                    }
                }
                else //No existe
                {
                    //Se comprueba si la tienda tiene el fichero de mensaje en su carpeta MSG.
                    bool InDir = File.Exists("MSG/" + filename);
                    if (InDir)
                    {
                        // LO TIENE, se guarda en la BD con el estado en Y
                        insertMsgInBD(filename, "Y", f_ini, f_fin, playtime);
                    }
                    else
                    {
                        // NO LO TIENE, se guarda en BD con el estado en N
                        insertMsgInBD(filename, "N", f_ini, f_fin, playtime);
                    }
                }
            }
        }
        //Borrar de una cadena un patrón específico
        public string borrarString(string str, string trimStr)
        {
            lock (bloqueo)
            {
                if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(trimStr)) return str;

                while (str.EndsWith(trimStr))
                {
                    str = str.Remove(str.LastIndexOf(trimStr));
                }
                return str;
            }
        }
    }
}
