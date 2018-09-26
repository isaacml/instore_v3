using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;

namespace player
{
    class Shared
    {
        private List<string> publi;
        private List<string> msg;
        private Object bloqueo = new Object();

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
                        string[] publi_container = lista[1].Split(';');
                        foreach (string s_publi in publi_container)
                        {   
                            //Borra etiqueta de mensaje
                            string cab_msg = borrarString(s_publi, @"[mensaje]");
                            //Separa los datos de publicidad
                            string[] only_publi = Regex.Split(cab_msg, @"\<=>");
                            //Obtiene nombre de fichero publi + fecha_inicio + fecha_fin + GAP
                            output = only_publi[0] + ";" + only_publi[1] + ";" + only_publi[2] + ";" + only_publi[3];
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
