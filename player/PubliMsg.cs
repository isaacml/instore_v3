﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Net;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;

namespace player
{
    class PubliMsg
    {
        private SQLiteConnection connection;
        private Object bloqueo = new Object();
        private string string_connection;
        private string dir_publi;
        private string dir_msg;
        private List<string> pfordown;
        private List<string> mfordown;

        public PubliMsg()
        {
            string_connection = string.Format(@"Data Source={0}; Version=3;", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Properties.Resources.FicheroSQL);
            dir_publi = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Properties.Resources.CarpetaPUBLI;
            dir_msg = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + Properties.Resources.CarpetaMSG;
            pfordown = new List<string>();
            mfordown = new List<string>();
        }
        //Publicidad que tiene que descargarse la tienda
        public List<string> DownloadPubli()
        {
            return pfordown;
        }
        //Mensajes que tiene que descargarse la tienda
        public List<string> DownloadMsg()
        {
            return mfordown;
        }
        //Recoge el listado de publicidad y mensajes y los guarda en la base de datos
        public bool GuardarListado(string listado)
        {
            //Se borran de BD los que no están en el listado e informamos de los cmabios
            bool changes_in_PL = existPubInList(listado);
            existMsgInList(listado);
            //Descuartizamos el listado
            string[] lista = Regex.Split(listado, @"\[publi];");
            string output;
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
                        }
                    }
                }
            }
            //NO EXISTEN FICHEROS DE PUBLICIDAD 
            else
            {
                string[] lst_msg = Regex.Split(listado, @"\[mensaje];");
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
                    }
                }
            }
            return changes_in_PL;
        }
        //Modifica el estado de publicidad/mensajes
        public void UpdateStatus(string filename, string estado, string tabla)
        {
            lock (bloqueo)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"UPDATE {0} SET existe='{1}' WHERE fichero='{2}';", tabla, estado,filename);
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
                //Se comprueba la existencia de publicidad en la carpeta PUBLI.
                bool InDir = File.Exists(dir_publi + filename);
                if (existe) //Existencia en BD
                {
                    if (InDir)
                    {
                        //Ya existe en dir y en BD, modificamos
                        if (getChangesInPubli(filename, "Y", f_ini, f_fin, gap))
                        {
                            updatePubliInBD(filename, "Y", f_ini, f_fin, gap);
                        }
                    }
                    else
                    {
                        //No existe en dir, modificamos los datos
                        if (getChangesInPubli(filename, "N", f_ini, f_fin, gap))
                        {
                            updatePubliInBD(filename, "N", f_ini, f_fin, gap);
                        }
                    }
                }
                else //No existe en BD
                {
                    if (InDir)
                    {
                        //Existe en dir se guarda en la BD con el estado(Y)
                        insertPubliInBD(filename, "Y", f_ini, f_fin, gap);
                    }
                    else
                    {
                        //No existe en dir se guarda en la BD con el estado(N)
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
                //Se comprueba la existencia de publicidad en la carpeta PUBLI.
                bool InDir = File.Exists(dir_msg + filename);
                if (existe) //Existencia en BD
                { 
                    if (InDir)
                    {
                        //Ya existe en dir y en BD, modificamos
                        if (getChangesInMsg(filename, "Y", f_ini, f_fin, playtime))
                        {
                            updateMsgInBD(filename, "Y", f_ini, f_fin, playtime); //Se modifican los datos
                        }
                    }
                    else
                    {
                        //No existe en dir, modificamos los datos
                        if (getChangesInMsg(filename, "N", f_ini, f_fin, playtime))
                        {
                            updateMsgInBD(filename, "N", f_ini, f_fin, playtime); //Se modifican los datos
                        }
                    }   
                }
                else //No existe en BD
                {
                    if (InDir)
                    {
                        //Existe en dir se guarda en la BD con el estado(Y)
                        insertMsgInBD(filename, "Y", f_ini, f_fin, playtime);
                    }
                    else
                    {
                        //No existe en dir se guarda en la BD con el estado(N)
                        insertMsgInBD(filename, "N", f_ini, f_fin, playtime);
                    }
                }
            }
        }
        //Se encarga de modificar un fichero de publicidad en la base de datos de la tienda
        private void updatePubliInBD(string filename, string existe, string f_ini, string f_fin, string gap)
        {
            lock (bloqueo)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"UPDATE publi SET existe='{0}', fecha_ini='{1}', fecha_fin='{2}', gap='{3}' WHERE fichero='{4}';",
                        existe, f_ini, f_fin, gap, filename);
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        //Se encarga de modificar un fichero de publicidad en la base de datos de la tienda
        private void updateMsgInBD(string filename, string existe,  string f_ini, string f_fin, string playtime)
        {
            lock (bloqueo)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"UPDATE mensaje SET existe='{0}', fecha_ini='{1}', fecha_fin='{2}', playtime='{3}' WHERE fichero='{4}';",
                        existe, f_ini, f_fin, playtime, filename);
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
        //Compara la publicidad en la base de datos interna (TIENDA) con la que recibe del server_externo
        //Si alguno de los datos ha cambiado se procede a la modificación
        private bool getChangesInPubli(string namefile, string existe, string f_ini, string f_fin, string gap)
        {
            lock (bloqueo)
            {
                bool change = false;
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT existe, fecha_ini, fecha_fin, gap FROM publi WHERE fichero=('{0}');", namefile);
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        //Recogemos los datos
                        string existe_bd = datos.GetString(datos.GetOrdinal("existe"));
                        string f_ini_bd = datos.GetString(datos.GetOrdinal("fecha_ini"));
                        string f_fin_bd = datos.GetString(datos.GetOrdinal("fecha_fin"));
                        int gap_bd = datos.GetInt32(datos.GetOrdinal("gap"));
                        //Comprobamos si los datos son distintos
                        if (existe_bd != existe || f_ini_bd != f_ini || f_fin_bd != f_fin || gap_bd != Convert.ToInt32(gap))
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
        private bool getChangesInMsg(string namefile, string existe, string f_ini, string f_fin, string playtime)
        {
            lock (bloqueo)
            {
                bool change = false;
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT existe, fecha_ini, fecha_fin, playtime FROM mensaje WHERE fichero=('{0}');", namefile);
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        //Recogemos los datos
                        string existe_bd = datos.GetString(datos.GetOrdinal("existe"));
                        string f_ini_bd = datos.GetString(datos.GetOrdinal("fecha_ini"));
                        string f_fin_bd = datos.GetString(datos.GetOrdinal("fecha_fin"));
                        string playtime_bd = datos.GetString(datos.GetOrdinal("playtime"));
                        //Comprobamos si los datos son distintos
                        if (existe_bd != existe || f_ini_bd != f_ini || f_fin_bd != f_fin || playtime != playtime_bd)
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
        //Recoge publicidad con el estado(N) y los guarda en un listado para la descarga
        public void PubliForDown()
        {
            lock (bloqueo)
            {
                pfordown.Clear();
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(@"SELECT fichero, fecha_ini, gap FROM publi WHERE existe='N'", connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        //Recogemos los datos
                        string fichero = datos.GetString(datos.GetOrdinal("fichero"));
                        string fecha_ini = datos.GetString(datos.GetOrdinal("fecha_ini"));
                        int gap = datos.GetInt32(datos.GetOrdinal("gap"));
                        //Guarda en listado de publicidad
                        pfordown.Add(string.Format("{0}[]{1}[]{2}", fichero, fecha_ini, gap));
                    }
                    connection.Close();
                }
            }
        }
        //Recoge mensajes con el estado(NO) y los guarda en un listado para la descarga
        public void MsgForDown()
        {
            lock (bloqueo)
            {
                mfordown.Clear();
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(@"SELECT fichero FROM mensaje WHERE existe='N'", connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        //Recogemos el nombre del fichero
                        string fichero = datos.GetString(datos.GetOrdinal("fichero"));
                        //Guarda en listado de mensajes
                        mfordown.Add(fichero);
                    }
                    connection.Close();
                }
            }
        }
        //Recoge la publicidad diaria con estado en "Y"
        //La que se encuentra dentro del rango de fechas
        public Tuple<List<string>, int> GetPublicidad()
        {
            lock (bloqueo)
            {
                int gap = 0;
                int fecha_actual = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
                List<string> publicidad = new List<string>();
                try
                {
                    publicidad.Clear();
                    using (connection = new SQLiteConnection(string_connection))
                    {
                        connection.Open();
                        SQLiteCommand cmd = new SQLiteCommand(@"SELECT fichero, fecha_ini, fecha_fin, gap FROM publi WHERE existe='Y'", connection);
                        SQLiteDataReader datos = cmd.ExecuteReader();
                        while (datos.Read())
                        {
                            //Recogemos los datos
                            string fichero = datos.GetString(datos.GetOrdinal("fichero"));
                            int f_ini = Convert.ToInt32(datos.GetString(datos.GetOrdinal("fecha_ini")));
                            int f_fin = Convert.ToInt32(datos.GetString(datos.GetOrdinal("fecha_fin")));
                            gap = datos.GetInt32(datos.GetOrdinal("gap"));
                            //Obtenemos la publicidad entre el rango de fechas
                            if (f_ini <= fecha_actual && f_fin >= fecha_actual)
                            {
                                //Guardamos en el listado de publicidad
                                publicidad.Add(fichero);
                            }
                        }
                        connection.Close();
                    }
                }
                catch
                {
                    gap = 0;
                }
                
                return new Tuple<List<string>, int>(publicidad, gap);
            }
        }
        //Recoge los ficheros de mensaje con estado en "Y"
        public List<string> GetMensajes()
        {
            lock (bloqueo)
            {
                int fecha_actual = Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"));
                List<string> mensajes = new List<string>();
                mensajes.Clear();
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(@"SELECT fichero, fecha_ini, fecha_fin, playtime FROM mensaje WHERE existe='Y'", connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        //Recogemos datos
                        string fichero = datos.GetString(datos.GetOrdinal("fichero"));
                        int f_ini = Convert.ToInt32(datos.GetString(datos.GetOrdinal("fecha_ini")));
                        int f_fin = Convert.ToInt32(datos.GetString(datos.GetOrdinal("fecha_fin")));
                        string playtime = datos.GetString(datos.GetOrdinal("playtime"));
                        //Obtenemos la publicidad entre el rango de fechas
                        if (f_ini <= fecha_actual && f_fin >= fecha_actual)
                        {
                            //Guardamos en el listado de publicidad
                            mensajes.Add(string.Format("{0}[]{1}", fichero, playtime));
                        }
                    }
                    connection.Close();
                }
                return mensajes;
            }
        }
        //Comprueba si existe la publicidad en el listado
        private bool existPubInList(string listado)
        {
            lock (bloqueo)
            {
                bool res =  false;
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(@"SELECT id, fichero FROM publi", connection);
                    SQLiteDataReader publi = cmd.ExecuteReader();
                    while (publi.Read())
                    {
                        int id = publi.GetInt32(publi.GetOrdinal("id"));
                        string file = publi.GetString(publi.GetOrdinal("fichero"));
                        //Comprobamos ficheros validos en BD
                        if (!listado.Contains(file))
                        {
                            string query = string.Format(@"DELETE FROM publi WHERE id={0}", id);
                            SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                            cmd_exc.ExecuteNonQuery();
                            //Borramos fichero de la carpeta publicidad
                            File.Delete(dir_publi + file);
                            res = true;
                        }
                    }
                    connection.Close();
                }
                return res;
            }
        }
        //Comprueba si existe mensajes en el listado
        private void existMsgInList(string listado)
        {
            lock (bloqueo)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    SQLiteCommand cmd = new SQLiteCommand(@"SELECT id, fichero FROM mensaje", connection);
                    SQLiteDataReader msg = cmd.ExecuteReader();
                    while (msg.Read())
                    {
                        int id = msg.GetInt32(msg.GetOrdinal("id"));
                        string file = msg.GetString(msg.GetOrdinal("fichero"));
                        //Comprobamos ficheros validos en BD
                        if (!listado.Contains(file))
                        {
                            string query = string.Format(@"DELETE FROM mensaje WHERE id={0}", id);
                            SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                            cmd_exc.ExecuteNonQuery();
                            //Borramos fichero de la carpeta mensajes
                            File.Delete(dir_msg + file);
                        }
                    }
                    connection.Close();
                }
            }
        }
        //Borrar de una cadena un patrón específico
        private string borrarString(string str, string trimStr)
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
