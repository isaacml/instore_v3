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
        private List<string> musica;
        private List<string> subdir;
        private string estado;
        private string id_entidad;
        private string nom_entidad;
        private string instamsg;
        private Object bloqueo = new Object();

        public Shared()
        {
            musica = new List<string>();
            subdir = new List<string>();
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
        public List<string> SubDirs
        {
            get
            {
                lock (bloqueo)
                {
                    return subdir;
                }
            }
            set
            {
                lock (bloqueo)
                {
                    subdir = value;
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
    }
}
