using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using AudioDjStudio;

namespace player
{
    public partial class Inicio : Form
    {
        private string file_config = "config.ini";
        private Object obj = new Object(); // para bloqueo
        private Shared shd = new Shared();
        private Connect con = new Connect();
        private Domains doms = new Domains();
        private PubliMsg publimsg = new PubliMsg();
        private Random rand = new Random();
        private bool st_salida = false;  //Evalua la salida del programa
        private double songduration = 0;
        private List<string> lst_music = new List<string>();

        public Inicio()
        {
            InitializeComponent();
        }
        //Explorador de directorios de música
        private void musicDirs_Click(object sender, EventArgs e)
        {
            if (openMusicDirs.ShowDialog() == DialogResult.OK)
            {
                listMusicDirs.Items.Clear();
                shd.SubDirs.Clear();
                //Tomamos el nombre del directorio 
                string directorio = openMusicDirs.SelectedPath;
                //Mostramos directorio
                musicDirs.Text = directorio;
                //Comprobamos si los subdirectorios son accesibles
                foreach (string d in Directory.GetDirectories(directorio))
                {
                    bool access = canAccess(d);
                    if (access == true)
                    {
                        //Guardamos los subdirectorios
                        shd.SubDirs.Add(d);
                        //Añadimos al listado de subdirectorios
                        DirectoryInfo dir = new DirectoryInfo(d);
                        listMusicDirs.Items.Add(dir.Name);
                    }
                }
            }
        }
        //Guarda en un listado los ficheros MP3 de las carpetas marcadas
        private void listMusicDirs_SelectedValueChanged(object sender, EventArgs e)
        {
            shd.Music.Clear();
            lst_music.Clear();

            int pl = 1;

            foreach (string lst in listMusicDirs.CheckedItems)
            {
                foreach (string subdir in shd.SubDirs)
                {
                    if (subdir.Contains(lst))
                    {
                        //Guardamos ficheros mp3/wav en el listado
                        shd.Music.AddRange(Directory.GetFiles(subdir, "*.*", SearchOption.AllDirectories)
                                  .Where(f => f.EndsWith(".mp3") || f.EndsWith(".wav")));
                    }
                }
            }
            //Creamos el listado de musica junto con la publicidad
            foreach (string m in shuffle(shd.Music, rand))
            {
                lst_music.Add(m);
                Tuple<List<string>, int> publi = publimsg.GetPublicidad();
                if (pl == publi.Item2)
                {
                    foreach (string p in shuffle(publi.Item1, rand))
                    {
                        lst_music.Add("PUBLI/" + p);
                        break;
                    }
                    pl = 0;
                }
                pl++;
            }
        }
        //Abre el explorador para seleccionar un msg instantaneo
        private void msgIns_Click(object sender, EventArgs e)
        {
            //Mostramos un explorador de ficheros
            if (openMsgInst.ShowDialog() == DialogResult.OK)
            {
                string msgInsta = openMsgInst.FileName;
                msgIns.Text = Path.GetFileName(msgInsta);
                shd.InstaMSG = msgInsta;
            }
        }
        //Cierre de la ventana principal del programa
        private void Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!st_salida)
            {
                //Se oculta el programa
                e.Cancel = true;
                this.Hide();
                //Mensaje de notificación
                notifyBarIcon.ShowBalloonTip(100, "HMPro", "Ejecución en 2º Plano", ToolTipIcon.Info);
            }
        }
        //Muestra el programa cuando pulsamos click derecho en el menu contextual del icono
        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }
        //Salida de programa cuando pulsamos click derecho en el menu contextual del icono
        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {   //Mensaje de confirmación para la salida
            if (MessageBox.Show("¿Seguro que quieres salir?", "Cierre del programa", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                st_salida = true;
                this.Close();
            }
        }
        //Muestra el programa cuando hacemos doble click en el icono
        private void notifyBarIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }
        //Ajustes de volumen para el track bar de musica
        private void trackBarMusica_Scroll(object sender, EventArgs e)
        {
            if (trackBarMusica.Value >= trackBarFade.Value)
            {
                lblMusicContainer.Text = trackBarMusica.Value.ToString();
            }
            else
            {
                trackBarMusica.Value = trackBarFade.Value;
            }
        }
        //Ajustes de volumen para el track bar de publicidad
        private void trackBarPubli_Scroll(object sender, EventArgs e)
        {
            if (trackBarPubli.Value >= trackBarFade.Value)
            {
                lblPubliContainer.Text = trackBarPubli.Value.ToString();
            }
            else
            {
                trackBarPubli.Value = trackBarFade.Value;
            }
        }
        //Ajustes de volumen para el track bar de mensajes
        private void trackBarMsg_Scroll(object sender, EventArgs e)
        {
            if (trackBarMsg.Value >= trackBarFade.Value)
            {
                lblMsgContainer.Text = trackBarMsg.Value.ToString();
            }
            else
            {
                trackBarMsg.Value = trackBarFade.Value;
            }
        }
        //Ajustes de volumen para el track bar de Fade Out
        private void trackBarFade_Scroll(object sender, EventArgs e)
        {
            //Se calcula el valor máximo para el fade out
            int max = 0;
            //Se obtiene el valor minimo de los otros tracks
            max = Math.Min(trackBarMusica.Value, trackBarPubli.Value);
            max = Math.Min(max, trackBarMsg.Value);
            //Se comprueba que el fade no supera ese valor
            if (max >= trackBarFade.Value)
                lblFadeContainer.Text = trackBarFade.Value.ToString();
            else
            {
                trackBarFade.Value = max;
            }
        }
        
        private void btnSendServer_Click(object sender, EventArgs e)
        {
            if ((!txtServer.Text.Contains("http://")) && (!txtServer.Text.Contains("https://")))
            {
                errorServer.SetError(txtServer, "URL no válida");
            }
            else
            {
                errorServer.SetError(txtServer, null);
                con.SaveConnection("server", txtServer.Text);
            }
        }

        private void btnSendProxy_Click(object sender, EventArgs e)
        {
            errorProxy.Clear();
            if (textProxy.Text == "")
            {
                con.SaveConnection("proxy", textProxy.Text);
            }
            else
            {
                if ((!textProxy.Text.Contains("http://")) && (!textProxy.Text.Contains("https://")))
                {
                    errorProxy.SetError(textProxy, "URL no válida");
                }
                else
                {
                    con.SaveConnection("proxy", textProxy.Text);
                }
            }
        }
        //LOAD: CARGA DE INICIO 
        private void Inicio_Load(object sender, EventArgs e)
        {
            // Verifica la presencia de tarjetas de audio
            Int32 nOutputs = playerInsta.GetOutputDevicesCount();
            if (nOutputs == 0)
            {
                MessageBox.Show("No hay dispositivos de audio.", "Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            int wait5min = (5 * 60 * 1000); // 5 min
            int wait1min = (1 * 60 * 1000); // 1 min
            int wait20hour = (20 * 60 * 60 * 1000); // 20 hours
            //Toma la entidad y el listado por primera vez
            showEntidad();
            getListDomains();
            //Muestra las URL(serv/proxy)
            txtServer.Text = con.LoadServer();
            textProxy.Text = con.LoadProxy();
            // Iniciamos el sistema de audio
            playerInsta.InitSoundSystem(1, 0, 0, 0, 0, -1);
            playerMusic.InitSoundSystem(1, 0, 0, 0, 0, -1);
            //Cada 20 horas
            Timer20HOUR.Interval = wait20hour;
            Timer20HOUR.Start();
            //Cada 5 minutos
            Timer5MIN.Interval = wait5min;
            Timer5MIN.Start();
            //Cada 1 minuto
            Timer1MIN.Interval = wait1min;
            Timer1MIN.Start();
            
        }
        private void Timer5MIN_Tick(object sender, EventArgs e)
        {
            Timer5MIN.Stop();
            //Tomamos el estado
            getStatus();
            //Solicitud (publi/msg) por dominio
            getListDomains();
            Timer5MIN.Start();
        }
        private void Timer1MIN_Tick(object sender, EventArgs e)
        {
            Timer1MIN.Stop();
            //Solicidud de ficheros publi/msg
            solicitudFicheros();
            Timer1MIN.Start();
        }
        private void Timer20HOUR_Tick(object sender, EventArgs e)
        {
            Timer20HOUR.Stop();
            //Borramos los ficheros antiguos
            publimsg.BorrarPublicidad();
            Timer20HOUR.Start();
        }
        //Solicitud de publi/msg, guardado de archivos en BD
        private void getListDomains()
        {
            listBoxDom.Items.Clear();
            //Mostramos los dominios en el panel
            foreach (string d in doms.ListadoDominios())
            {
                listBoxDom.Items.Add(d);
            }
            //Formamos la cadena de dominio
            string domains = doms.CadenaDominios();
            //Peticion de listado por dominios
            string res = serverConnection(HttpUtility.UrlPathEncode("/acciones.cgi?action=send_domains&dominios=" + domains));
            //Enviamos listado(PubliMsg.cs) para realizar el guardado
            publimsg.GuardarListado(res);
        }
        //Muestra la entidad (zona de configuración)
        private void showEntidad()
        {
            errorAddDom.Clear();
            BindingList<Combos> save_ent = new BindingList<Combos>();
            //Leemos fichero de configuración
            string[] readText = File.ReadAllLines(file_config, Encoding.UTF8);
            foreach (string s in readText)
            {
                string[] dat_ent = s.Split('=');
                //Se guarda el nombre de entidad en el obj
                shd.Entidad = dat_ent[1];
                //Guardamos el id de entidad
                string id = serverConnection(@"/transf_orgs_vs.cgi?action=entidad&nom_ent=" + shd.Entidad);
                shd.IDEntidad = id;
                save_ent.Add(new Combos(shd.IDEntidad, shd.Entidad));
            }
            domEntidad.DataSource = save_ent;
            domEntidad.DisplayMember = "Value";
            domEntidad.ValueMember = "ID";
        }
        //Cambio de Entidad(combobox de configuracion)
        private void domEntidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorAddDom.Clear();
            showEntidad();
            BindingList<Combos> save_alm = new BindingList<Combos>();
            string query = serverConnection(@"/transf_orgs_vs.cgi?action=almacen&entidad=" + shd.IDEntidad);
            if (query != "")
            {
                string[] alm = query.Split(';');
                foreach (string alms in alm)
                {
                    if (alms != "")
                    {
                        //Separamos los distintos almacenes
                        string[] almacenes = Regex.Split(alms, @"\<=>");
                        save_alm.Add(new Combos(almacenes[0], almacenes[1]));
                    }
                }
            }
            else
            {
                save_alm.Add(new Combos("", ""));
            }
            domAlmacen.DataSource = save_alm;
            domAlmacen.DisplayMember = "Value";
            domAlmacen.ValueMember = "ID";
        }
        //Cambio de Almacen(combobox de configuracion)
        private void domAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorAddDom.Clear();
            BindingList<Combos> save_pais = new BindingList<Combos>();
            string query = serverConnection(@"/transf_orgs_vs.cgi?action=pais&almacen=" + domAlmacen.SelectedValue.ToString());
            if (query != "")
            {
                string[] pais = query.Split(';');
                foreach (string p in pais)
                {
                    if (p != "")
                    {
                        //Separamos los distintos paises
                        string[] paises = Regex.Split(p, @"\<=>");
                        save_pais.Add(new Combos(paises[0], paises[1]));
                    }
                }
            }
            else
            {
                save_pais.Add(new Combos("", ""));
            }
            domPais.DataSource = save_pais;
            domPais.DisplayMember = "Value";
            domPais.ValueMember = "ID";
        }
        //Cambio de Pais(combobox de configuracion)
        private void domPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorAddDom.Clear();
            BindingList<Combos> save_region = new BindingList<Combos>();
            string query = serverConnection(@"/transf_orgs_vs.cgi?action=region&pais=" + domPais.SelectedValue.ToString());
            if (query != "")
            {
                string[] region = query.Split(';');
                foreach (string r in region)
                {
                    if (r != "")
                    {
                        //Separamos las distintas regiones
                        string[] regiones = Regex.Split(r, @"\<=>");
                        save_region.Add(new Combos(regiones[0], regiones[1]));
                    }
                }
            }
            else
            {
                save_region.Add(new Combos("", ""));
            }
            domRegion.DataSource = save_region;
            domRegion.DisplayMember = "Value";
            domRegion.ValueMember = "ID";
        }
        //Cambio de Region(combobox de configuracion)
        private void domRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorAddDom.Clear();
            BindingList<Combos> save_prov = new BindingList<Combos>();
            string query = serverConnection(@"/transf_orgs_vs.cgi?action=provincia&region=" + domRegion.SelectedValue.ToString());
            if (query != "")
            {
                string[] provincia = query.Split(';');
                foreach (string p in provincia)
                {
                    if (p != "")
                    {
                        //Separamos las distintas provincias
                        string[] provincias = Regex.Split(p, @"\<=>");
                        save_prov.Add(new Combos(provincias[0], provincias[1]));
                    }
                }
            }
            else {
                save_prov.Add(new Combos("", ""));
            }
            domProv.DataSource = save_prov;
            domProv.DisplayMember = "Value";
            domProv.ValueMember = "ID";
        }
        //Cambio de Provincia(combobox de configuracion)
        private void domProv_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorAddDom.Clear();
            BindingList<Combos> save_shop = new BindingList<Combos>();
            string query = serverConnection(@"/transf_orgs_vs.cgi?action=tienda&provincia=" + domProv.SelectedValue.ToString());
            if (query != "")
            {
                string[] tienda = query.Split(';');
                foreach (string s in tienda)
                {
                    if (s != "")
                    {
                        //Separamos las distintas tiendas
                        string[] tiendas = Regex.Split(s, @"\<=>");
                        save_shop.Add(new Combos(tiendas[0], tiendas[1]));
                    }
                }
            }
            else
            {
                save_shop.Add(new Combos("", ""));
            }
            domTienda.DataSource = save_shop;
            domTienda.DisplayMember = "Value";
            domTienda.ValueMember = "ID";
        }
        //Añadir Dominio (zona de configuracion)
        private void btnAddDom_Click(object sender, EventArgs e)
        {
            try
            {
                string ent  = ((Combos)domEntidad.SelectedItem).Value;
                string alm  = ((Combos)domAlmacen.SelectedItem).Value;
                string pais = ((Combos)domPais.SelectedItem).Value;
                string reg  = ((Combos)domRegion.SelectedItem).Value;
                string prov = ((Combos)domProv.SelectedItem).Value;
                string shop = ((Combos)domTienda.SelectedItem).Value;
                //Colocamos cada una de las organizaciones para formar el dominio
                string dominio = string.Format("{0}.{1}.{2}.{3}.{4}.{5}", ent, alm, pais, reg, prov, shop);
                //Se comprueba la existencia del dominio en la base de datos
                bool existe = doms.ExisteDominio(dominio);
                if (!existe)
                {
                    //Añadimos el dominio a la base de datos
                    doms.InsertarDominio(dominio);
                    //Se añade al listbox
                    listBoxDom.Items.Add(dominio);
                }
                else
                {
                    //El dominio ya existe
                    errorAddDom.SetError(btnAddDom, "Ese dominio ya existe");
                }
            }
            catch
            {
                //Organizaciones nulas: no tienen valor
                errorAddDom.SetError(btnAddDom, "Selecciona todas las organizaciones");
            }
        }
        //Borrar Dominio (zona de configuracion)
        private void btnBorrarDom_Click(object sender, EventArgs e)
        {
            //Borramos de la base de datos
            doms.BorrarDominio(listBoxDom.GetItemText(listBoxDom.SelectedItem));
            //Borramos dominio del listado
            listBoxDom.Items.RemoveAt(listBoxDom.SelectedIndex);
        }
        /*Evalua que tipo de conexión se va a usar
        Ejecuta el cgi y nos devuelve un query string*/
        private string serverConnection(string cgi)
        {
            string output = "";
            WebClient wCli = new WebClient();
            WebProxy wProxy = new WebProxy();
            wCli.Encoding = Encoding.UTF8; //UTF8

            //TRUE: Usamos el Proxy
            if (con.UseProxy())
            {
                try
                {
                    wProxy.Address = new Uri(con.LoadProxy());
                    wCli.Proxy = wProxy;
                    output = wCli.DownloadString(con.LoadServer() + cgi);
                    errorProxy.SetError(textProxy, "");
                }
                catch
                {
                    errorProxy.SetError(textProxy, "Proxy Incorrecto");
                }
            }
            //FALSE: Usamos el Servidor
            else
            {
                try
                {
                    output = wCli.DownloadString(con.LoadServer() + cgi);
                    errorServer.SetError(txtServer, "");
                }
                catch
                {
                    errorServer.SetError(txtServer, "Server Incorrecto");
                }
            }
            return output;
        }
        //Revisa si podemos acceder a una carpeta o no.
        private bool canAccess(string dir)
        {
            bool res;
            try
            {
                string[] enter = Directory.GetFiles(dir);
                res = true;
            }
            catch
            {
                res = false;
            }
            return res;
        }
        //Solicita los ficheros de publicidad que deben descargarse
        private void solicitudFicheros()
        {
            foreach (string getpub in publimsg.DownloadPubli())
            {
                string[] pub = Regex.Split(getpub, @"\[]");
                string nombre = pub[0]; //Nombre del Fichero
                string f_ini = pub[1]; //Fecha de Inicio
                string gap = pub[2]; //Fecha de Inicio
                //Envio de peticion (publi_msg.cgi)
                string res = serverConnection(string.Format(@"/publi_msg.cgi?action=PubliFiles&existencia=N&fichero={0}&fecha_ini={1}&gap={2}", nombre, f_ini, gap));
                if (res == "Descarga")
                {
                    //Descarga del fichero de publicidad
                    downloadFile(nombre, @"publicidad", @"PUBLI/");
                    //Modificamos el estado a YES
                    publimsg.UpdateStatus(nombre, @"publi");
                }
            }
            foreach (string m in publimsg.DownloadMsg())
            {
                string res = serverConnection(string.Format(@"/publi_msg.cgi?action=MsgFiles&existencia=N&fichero={0}", m));
                if (res == "Descarga")
                {
                    //Descarga del fichero de publicidad
                    downloadFile(m, @"mensaje", @"MSG/");
                    //Modificamos el estado a YES
                    publimsg.UpdateStatus(m, @"mensaje");
                }
            }
        }
        //Encargado de la descarga de ficheros de publicidad y mensajes
        private void downloadFile(string fichero, string tipo, string carpeta)
        {
            WebClient wCli = new WebClient();
            WebProxy wProxy = new WebProxy();
            wCli.Encoding = Encoding.UTF8; //UTF8
            string str = string.Format(@"{0}/{1}?accion={2}", con.LoadServer(), fichero, tipo);
            //TRUE: Usamos el Proxy
            if (con.UseProxy())
            {
                
                wProxy.Address = new Uri(con.LoadProxy());
                wCli.Proxy = wProxy;
            }
            //SINO: Usamos el Server
            wCli.DownloadFile(str, carpeta + fichero);
        }
        //Obtiene el estado de la tienda (Activada o Desactivada)
        private void getStatus()
        {
            shd.Status = serverConnection("/acciones.cgi?action=check_entidad&ent=" + shd.Entidad);
            //Estado de la Tienda
            if (shd.Status == "1")
            {
                barStInfoServ.ForeColor = Color.Green;
                barStInfoServ.Text = "Activada";
            }
            if (shd.Status == "0" || shd.Status == "")
            {
                barStInfoServ.ForeColor = Color.Red;
                barStInfoServ.Text = "Desactivada";
            }
        }

        private void sendMsgInst_Click(object sender, EventArgs e)
        {
            byte[] bytes = null;

            if (shd.InstaMSG != null)
            {
                bytes = File.ReadAllBytes(shd.InstaMSG);

                if (playerInsta.LoadSoundFromMemory(0, bytes, bytes.Length) == enumErrorCodes.NOERROR) // carga en fichero en el player 0
                {
                    SoundInfo2 info = new SoundInfo2();
                    playerInsta.SoundInfoGet(0, ref info);
                    barStSong.Text = info.strMP3Tag1Title;
                }
                else
                {
                    MessageBox.Show("No puedo cargar el fichero " + shd.InstaMSG, "Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                playerInsta.PlaySound(0);
                // recoge la duración de la canción cargada
                playerInsta.SoundDurationGet(0, ref songduration, false); // millisecs
                Thread.Sleep(Convert.ToInt32(songduration));
                prob.Items.Add("Acabada");
            }
        }
        public List<string> shuffle(List<string> list, Random rng)
        {
            int n = list.Count();
            while (n > 1)
            {
                n--;
                int i = rand.Next(n + 1);
                string temp = list[i];
                list[i] = list[n];
                list[n] = temp;
            }
            return list;
        }
        private void bgMusic_DoWork(object sender, DoWorkEventArgs e)
        {

            foreach (string l in lst_music)
            {
                e.Result = l;
                Thread.Sleep(Convert.ToInt32(songduration));
            }
        }

        private void iniPlayer_Click(object sender, EventArgs e)
        {
            if (!bgMusic.IsBusy)
            {
                bgMusic.RunWorkerAsync();
            }
        }

        private void bgMusic_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            byte[] bytes = null;
            string fichero = e.Result.ToString();
            prob.Items.Add(fichero);

            bytes = File.ReadAllBytes(fichero);
            if (playerMusic.LoadSoundFromMemory(0, bytes, bytes.Length) == enumErrorCodes.NOERROR) // carga en fichero en el player 0
            {
                SoundInfo2 info = new SoundInfo2();
                playerMusic.SoundDurationGet(0, ref songduration, false); // millisecs
                playerMusic.SoundInfoGet(0, ref info);
                barStSong.Text = info.strMP3Tag1Title;
            }
            else
            {
                MessageBox.Show("No puedo cargar el fichero " + fichero, "Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            playerMusic.PlaySound(0);
        }
    }
}
