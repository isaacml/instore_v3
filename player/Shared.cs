using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace player
{
    class Shared
    {
        private List<string> publi;
        private List<string> msg;
        private List<string> musica;
        private string estado;
        private string id_entidad;
        private string nom_entidad;
        private Object bloqueo = new Object();

        public Shared()
        {
            publi = new List<string>();
            msg = new List<string>();
            musica = new List<string>();
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
    }
}
