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
        private string estado;
        private string entidad;
        private Object bloqueo = new Object();

        public Shared()
        {
            publi = new List<string>();
            msg = new List<string>();
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
        //Guarda el identificador de la entidad
        public string IDEntidad
        {
            get
            {
                lock (bloqueo)
                {
                    return entidad;
                }
            }
            set
            {
                lock (bloqueo)
                {
                    entidad = value;
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
