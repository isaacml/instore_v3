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
        private Horario hro = new Horario();
        private Domains doms = new Domains();
        private PubliMsg publimsg = new PubliMsg();
        private Random rand = new Random();
        private bool st_salida = false;  //Evalua la salida del programa
        private double songduration = 0;
        private bool changes_in_PL = false;
        private byte[] KeyCode = new byte[] { 11, 22, 33, 44, 55, 66, 77, 88 }; //decription keys
        Queue<string> playlist = new Queue<string>();

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
            foreach (string lst in listMusicDirs.CheckedItems)
            {
                foreach (string subdir in shd.SubDirs)
                {
                    if (canAccess(subdir))
                    {
                        if (subdir.Contains(lst))
                        {
                            //Guardamos ficheros mp3/wav en el listado
                            shd.Music.AddRange(Directory.GetFiles(subdir, "*.*", SearchOption.AllDirectories)
                                      .Where(f => f.EndsWith(".mp3") || f.EndsWith(".wav") || f.EndsWith(".xxx")));
                        }
                    }
                }
            }
            changes_in_PL = true;
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
            playerMusic.StreamVolumeLevelSet(0, (float)trackBarMusica.Value, enumVolumeScales.SCALE_LINEAR);
            lblMusicContainer.Text = trackBarMusica.Value.ToString();
        }
        //Ajustes de volumen para el track bar de publicidad
        private void trackBarPubli_Scroll(object sender, EventArgs e)
        {
            lblPubliContainer.Text = trackBarPubli.Value.ToString();
        }
        //Ajustes de volumen para el track bar de mensajes
        private void trackBarMsg_Scroll(object sender, EventArgs e)
        {
            playerInsta.StreamVolumeLevelSet(0, (float)trackBarMsg.Value, enumVolumeScales.SCALE_LINEAR);
            lblMsgContainer.Text = trackBarMsg.Value.ToString();
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
            Int32 nOutputsI = playerInsta.GetOutputDevicesCount();
            Int32 nOutputsM = playerMusic.GetOutputDevicesCount();
            if (nOutputsI == 0 && nOutputsM == 0)
            {
                MessageBox.Show("No hay dispositivos de audio.", "Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Error);
                st_salida = true;
                this.Close();
            }
            else
            {
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
                //Reproductor: cada 1seg
                tPlayer.Start();
            }
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
                string ent = ((Combos)domEntidad.SelectedItem).Value;
                string alm = ((Combos)domAlmacen.SelectedItem).Value;
                string pais = ((Combos)domPais.SelectedItem).Value;
                string reg = ((Combos)domRegion.SelectedItem).Value;
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
                    //Informamos al player de la nueva publicidad
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
        //Encargado de Reproducir un mensaje instantaneo
        private void sendMsgInst_Click(object sender, EventArgs e)
        {
            byte[] bytes = null;

            if (shd.InstaMSG != null)
            {
                bytes = File.ReadAllBytes(shd.InstaMSG);

                if (playerInsta.LoadSoundFromMemory(0, bytes, bytes.Length) == enumErrorCodes.NOERROR) { }
                else
                {
                    MessageBox.Show("No puedo cargar el fichero " + shd.InstaMSG, "Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                //Establecemos el volumen de reproductor de instantaneos
                playerInsta.StreamVolumeLevelSet(0, (float)trackBarMsg.Value, enumVolumeScales.SCALE_LINEAR);
                //Bajamos el sonido del reproductor de musica                                                          
                playerMusic.StreamVolumeLevelSet(0, (float)0.0, enumVolumeScales.SCALE_LINEAR);
                //Reproducimos el instantaneo
                playerInsta.PlaySound(0);
                //bloqueamos boton: evita reproduccion masiva
                sendMsgInst.Enabled = false;
            }
        }
        //PLAYER: Musica + Publicidad
        private void tPlayer_Tick(object sender, EventArgs e)
        {
            enumPlayerStatus playerStatus = playerMusic.GetPlayerStatus(0); //estado del player Music
            enumPlayerStatus instaStatus = playerInsta.GetPlayerStatus(0); //estado del player de Instantaneos


            if (changes_in_PL)
            {
                crearPL();
                changes_in_PL = false;
            }
            //Cuando acaba la reproduccion de un instantaneo
            if (instaStatus == enumPlayerStatus.SOUND_STOPPED)
            {
                //Subimos el sonido del reproductor de musica                                                          
                playerMusic.StreamVolumeLevelSet(0, (float)trackBarMusica.Value, enumVolumeScales.SCALE_LINEAR);
                //Habilitamos el boton del player instantaneo
                sendMsgInst.Enabled = true;
            }
            //Cuando se está reproduciendo la PL
            if (playerStatus == enumPlayerStatus.SOUND_PLAYING)
            {
                double position = 0;
                double percentage = 0;
                //Calculamos el progreso de la cancion
                playerMusic.SoundPositionGet(0, ref position, false);
                percentage = position / songduration * 100.00;
                barStStatus.Value = (int)percentage; // la mostramos en el pBar
            }

            if (playerStatus == enumPlayerStatus.SOUND_STOPPED)
            {
                if (playlist.Count != 0)
                {
                    byte[] bytes = null;
                    string song = playlist.Peek();
                    prob.Items.Add("Actual: " + song);
                    bytes = File.ReadAllBytes(song);
                    //Si es un fichero encriptado, lo desencriptamos
                    if (Path.GetExtension(song) == ".xxx") bytes = decript(bytes);
                    //Cargamos el fichero en memoria
                    if (playerMusic.LoadSoundFromMemory(0, bytes, bytes.Length) == enumErrorCodes.NOERROR) { }
                    else
                    {
                        MessageBox.Show("No puedo cargar el fichero " + song, "Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //Reproducimos
                    playerMusic.PlaySound(0);
                    //Quitamos la cancion de la PL
                    playlist.Dequeue();
                    //Playlist sin canciones
                    if (playlist.Count == 0)
                    {
                        //Creamos otra
                        crearPL();
                    }
                    prob.Items.Add("Siguiente: " + playlist.Peek());
                }
            }
            if (playerStatus == enumPlayerStatus.SOUND_NONE)
            {
                if (playlist.Count != 0)
                {
                    byte[] bytes = null;
                    string song = playlist.Peek();
                    prob.Items.Add("Actual: " + song);
                    bytes = File.ReadAllBytes(song);
                    //Si es un fichero encriptado, lo desencriptamos
                    if (Path.GetExtension(song) == ".xxx") bytes = decript(bytes);
                    //Cargamos el fichero en memoria
                    if (playerMusic.LoadSoundFromMemory(0, bytes, bytes.Length) == enumErrorCodes.NOERROR) { }
                    else
                    {
                        MessageBox.Show("No puedo cargar el fichero " + song, "Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    playerMusic.PlaySound(0);
                    playlist.Dequeue();
                    prob.Items.Add("Siguiente: " + playlist.Peek());
                }
            }
        } 
        //Informacion de la canción cuando se carga
        private void playerMusic_SoundLoaded(object sender, SoundLoadedEventArgs e)
        {
            SoundInfo2 info = new SoundInfo2();
            playerMusic.SoundInfoGet(0, ref info);
            //Duracion de la canción
            playerMusic.SoundDurationGet(0, ref songduration, false);
            barStSong.Text = info.strMP3Tag1Artist + " - " + info.strMP3Tag1Title;
        }
        //GENERADOR DE PLAYLIST
        private void crearPL()
        {
            playlist.Clear();
            prob.Items.Clear();
            int pl = 1;
            //Se añade el listado de musica + publicidad
            foreach (string m in shuffle(shd.Music, rand))
            {
                playlist.Enqueue(m);
                prob.Items.Add(m);

                Tuple<List<string>, int> publi = publimsg.GetPublicidad();
                if (pl == publi.Item2)
                {
                    foreach (string p in shuffle(publi.Item1, rand))
                    {
                        playlist.Enqueue("PUBLI/" + p);
                        prob.Items.Add("PUBLI/" + p);
                        break;
                    }
                    pl = 0;
                }
                pl++;
            }
        }
        //Shuffle casero, creado con un random
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
        //Desencripta un fichero .xxx
        private byte[] decript(byte[] f_num)
        {
            for (int i = 0; i < f_num.Length; i++)
            {
                f_num[i] ^= KeyCode[i % 8];
            }
            return f_num;
        }
        //Horario: Desde...
        private void timeDesde_ValueChanged(object sender, EventArgs e)
        {
            string desde = timeDesde.Text;
            //Se comprueba si existe horario en BD
            if (hro.ExisteHorario("hora_inicial"))
            {
                //existe, modificamos
                hro.ModificarHorario("hora_inicial", desde);
            }
            else
            {
                //No existe, insertamos
                hro.InsertoHorario("hora_inicial", desde);
            }
        }
        //Horario: Hasta...
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string hasta = timeHasta.Text;
            //Se comprueba si existe horario en BD
            if (hro.ExisteHorario("hora_final"))
            {
                //existe, modificamos
                hro.ModificarHorario("hora_final", hasta);
            }
            else
            {
                //No existe, insertamos
                hro.InsertoHorario("hora_final", hasta);
            }
        }
    }
}
