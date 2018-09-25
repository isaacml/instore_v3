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
                string publicidad = "";
                string url = HttpUtility.UrlPathEncode("http://192.168.0.102:8080/acciones.cgi?action=send_domains&dominios=Acciona.Transmediterranea.España.Andalucia.Malaga.ACC FORTUNY:.:");
                string data2 = wClient.DownloadString(url);
                string[] lista = Regex.Split(data2, @"\[publi];");
                if (lista.Length >= 1)
                {
                    bool exist_msg = lista[1].Contains(@"[mensaje];");
                    if (!exist_msg)
                    {
                        // No hay ficheros de mensaje, solo ficheros de publicidad
                        string[] publi_container = lista[1].Split(';');
                        foreach (string s_publi in publi_container)
                        {
                            string cab_msg = borrarString(s_publi, @"[mensaje]");
                            string[] only_publi = Regex.Split(cab_msg, @"\<=>");
                            //Obtenemos nombre de fichero, fecha_inicio, fecha_fin, y GAP
                            publicidad = only_publi[0] + ";" + only_publi[1] + ";" + only_publi[2] + ";" + only_publi[3];
                            publi.Add(publicidad);
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
