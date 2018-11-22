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
        private double songduration = 0;
        private byte[] KeyCode = new byte[] { 11, 22, 33, 44, 55, 66, 77, 88 }; //decription keys
        private string file_config = "config.ini";
        private string publi_folder = @"PUBLI/";
        private string msg_folder = @"MSG/";
        private Object obj = new Object(); // para bloqueo
        private Shared shd = new Shared();
        private Connect con = new Connect();
        private Horario hro = new Horario();
        private Domains doms = new Domains();
        private PubliMsg publimsg = new PubliMsg();
        private Random rand = new Random();
        Queue<string> playlist = new Queue<string>();
        //Evalua la salida del programa
        private bool st_salida = false;  
        //Evalua si hay cambio en la playlist
        private bool changes_in_PL = false;
        //Evalua el estado de la tienda: activada o bloqueada
        private bool estado_tienda = false;
        //Determina si es un fichero de publicidad
        private bool is_publi_file;

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
            //Establecemos el volumen para los msg insta       
            playerInsta.StreamVolumeLevelSet(0, (float)trackBarMsg.Value, enumVolumeScales.SCALE_LINEAR);
            //Establecemos el volumen para los msg auto                                                      
            playerMsgAuto.StreamVolumeLevelSet(0, (float)trackBarMsg.Value, enumVolumeScales.SCALE_LINEAR);
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
                getStatus();
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
            Int32 nOutputsA = playerMsgAuto.GetOutputDevicesCount();
            if (nOutputsI == 0 && nOutputsM == 0 && nOutputsA == 0)
            {
                MessageBox.Show("No hay dispositivos de audio.", "Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Error);
                st_salida = true;
                this.Close();
            }
            else
            {
                //Cargamos horario por primera vez
                if (hro.ExisteHorario())
                {
                    Tuple<string, string> horario = hro.RecogerHorario();
                    timeDesde.Value = Convert.ToDateTime(horario.Item1);
                    timeHasta.Value = Convert.ToDateTime(horario.Item2);
                }
                //Muestra las URL(serv/proxy)
                txtServer.Text = con.LoadServer();
                textProxy.Text = con.LoadProxy();
                //Toma la entidad por primera vez
                showEntidad();
                //Muestra el estado por primera vez
                getStatus();
                //Toma el listado por primera vez
                if (estado_tienda)
                {
                    getListDomains();
                }
                //tiempos
                int wait5min = (5 * 60 * 1000); // 5 min
                int wait1min = (1 * 60 * 1000); // 1 min
                // Iniciamos el sistema de audio
                playerInsta.InitSoundSystem(1, 0, 0, 0, 0, -1);
                playerMusic.InitSoundSystem(1, 0, 0, 0, 0, -1);
                playerMsgAuto.InitSoundSystem(1, 0, 0, 0, 0, -1);
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
            //Muestra el estado
            getStatus();
            if (estado_tienda)
            {
                //Solicitud (publi/msg) por dominio
                getListDomains();
            }
            Timer5MIN.Start();
        }
        private void Timer1MIN_Tick(object sender, EventArgs e)
        {
            Timer1MIN.Stop();
            if (estado_tienda)
            {
                //estado del player de Instantaneos
                enumPlayerStatus instaStatus = playerInsta.GetPlayerStatus(0); 
                //Comprobamos que el player de instantaneos está parado
                if (instaStatus == enumPlayerStatus.SOUND_STOPPED || instaStatus == enumPlayerStatus.SOUND_NONE)
                {
                    //Busca un mensaje para reproducir
                    mensajes_automaticos();
                }
                //Solicidud de ficheros publi/msg
                solicitudFicheros();
            }
            Timer1MIN.Start();
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
            //Peticion de listado: por cadena de dominios
            string res = serverConnection(HttpUtility.UrlPathEncode("/acciones.cgi?action=send_domains&dominios=" + doms.CadenaDominios()));
            if (res != "")
            {
                //Enviamos listado(PubliMsg.cs) para realizar el guardado
                //Se comprueba si hay cambio en la PL
                if (publimsg.GuardarListado(res))
                {
                    changes_in_PL = true;
                }
            }
        }
        //Muestra la entidad (zona de configuración)
        private void showEntidad()
        {
            errorAddDom.Clear();
            BindingList<Combos> save_ent = new BindingList<Combos>();
            //Leemos la primera linea del fichero de configuración
            string entidad = File.ReadLines(file_config).First();
            //Obtenemos solo el nombre de la entidad
            string[] dat_ent = entidad.Split('=');
            //Se guarda el nombre de entidad en el obj
            shd.Entidad = dat_ent[1];
            //Guardamos el id de entidad
            string id = serverConnection(@"/transf_orgs_vs.cgi?action=entidad&nom_ent=" + shd.Entidad);
            if (id != "")
            {
                shd.IDEntidad = id;
                save_ent.Add(new Combos(shd.IDEntidad, shd.Entidad));
            }
            else
            {
                shd.IDEntidad = "";
                save_ent.Add(new Combos("", shd.Entidad));
            }
            domEntidad.DataSource = save_ent;
            domEntidad.DisplayMember = "Value";
            domEntidad.ValueMember = "ID";
        }
        //Cambio de Entidad(combobox de configuracion)
        private void domEntidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorAddDom.Clear();
            //Tomamos la entidad
            showEntidad();
            BindingList<Combos> save_alm = new BindingList<Combos>();
            //Si el identificador es distinto de vacio
            if (shd.IDEntidad != "")
            {
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
            }
            else //No hay entidad
            {
                //Reseteamos los selects a vacios
                save_alm.Add(new Combos("", ""));
                domPais.DataSource = save_alm;
                domRegion.DataSource = save_alm;
                domProv.DataSource = save_alm;
                domTienda.DataSource = save_alm;
            }
            domAlmacen.DataSource = save_alm;
            domAlmacen.DisplayMember = "Value";
            domAlmacen.ValueMember = "ID";
        }
        //Cambio de Almacen(combobox de configuracion)
        private void domAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (domAlmacen.SelectedValue.ToString() != "")
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

        //Descarga los ficheros con estado N
        private void solicitudFicheros()
        {
            //Ejecutamos la solicitud de publicidad
            publimsg.PubliForDown();
            //Ejecutamos la solicitud de mensajes
            publimsg.MsgForDown();
            //Miramos los ficheros para la descarga
            if (publimsg.DownloadPubli().Count != 0)
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
                        downloadFile(nombre, @"publicidad", publi_folder);
                        //Modificamos el estado a YES
                        publimsg.UpdateStatus(nombre, "Y", @"publi");
                        //Cambios en la PL
                        changes_in_PL = true;
                    }
                }
            }
            if (publimsg.DownloadMsg().Count != 0)
            {
                foreach (string getmsg in publimsg.DownloadMsg())
                {
                    string res = serverConnection(string.Format(@"/publi_msg.cgi?action=MsgFiles&existencia=N&fichero={0}", getmsg));
                    if (res == "Descarga")
                    {
                        //Descarga del fichero de publicidad
                        downloadFile(getmsg, @"mensaje", msg_folder);
                        //Modificamos el estado a YES
                        publimsg.UpdateStatus(getmsg, "Y", @"mensaje");
                    }
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
                estado_tienda = true;
            }
            if (shd.Status == "0" || shd.Status == "")
            {
                barStInfoServ.ForeColor = Color.Red;
                barStInfoServ.Text = "Desactivada";
                estado_tienda = false;
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
                //Bajamos el sonido del reproductor de mensajes automaticoss                                                       
                playerMsgAuto.StreamVolumeLevelSet(0, (float)0.0, enumVolumeScales.SCALE_LINEAR);
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
            enumPlayerStatus autoStatus = playerMsgAuto.GetPlayerStatus(0); //estado del player de MsgAuto

            //Comprueba si hay cambio en la PL de reproduccion
            if (changes_in_PL)
            {
                crearPL();
                changes_in_PL = false;
            }
            //Cuando la reproduccion de un msg instantaneo y msg auto están parados
            if ((instaStatus == enumPlayerStatus.SOUND_STOPPED && autoStatus == enumPlayerStatus.SOUND_NONE) || (instaStatus == enumPlayerStatus.SOUND_STOPPED && autoStatus == enumPlayerStatus.SOUND_STOPPED) || (instaStatus == enumPlayerStatus.SOUND_NONE && autoStatus == enumPlayerStatus.SOUND_STOPPED))
            {
                //Es un fichero de publicidad
                if (is_publi_file)
                {
                    //Tomamos el volumen del trackbar de publicidad
                    playerMusic.StreamVolumeLevelSet(0, (float)trackBarPubli.Value, enumVolumeScales.SCALE_LINEAR);
                }
                else //Es una cancion normal
                {
                    //Tomamos el volumen del trackbar de música
                    playerMusic.StreamVolumeLevelSet(0, (float)trackBarMusica.Value, enumVolumeScales.SCALE_LINEAR);
                }
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
            //Comprueba que el horario está entre el rango de fechas
            if (hro.HorarioReproduccion())
            {
                //Cuando el reproductor de musica para, se vuelve a lanzar la siguiente cancion
                if (playerStatus == enumPlayerStatus.SOUND_STOPPED)
                {
                    if (playlist.Count != 0)
                    {
                        byte[] bytes = null;
                        string song = playlist.Peek(); //Siguiente cancion
                        //mira si el siguiente fichero es de publi
                        is_publi_file = isPubli(song);
                        //Leemos los bytes de la cancion
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
                    }
                }
                if (playerStatus == enumPlayerStatus.SOUND_NONE)
                {
                    if (playlist.Count != 0)
                    {
                        byte[] bytes = null;
                        string song = playlist.Peek();
                        //mira si el fichero es de publi
                        is_publi_file = isPubli(song);
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
                    }
                }
            }
            else
            {
                barStStatus.Value = 0;
                barStSong.Text = "";
            }
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
                        playlist.Enqueue(publi_folder + p);
                        prob.Items.Add(publi_folder + p);
                        break;
                    }
                    pl = 0;
                }
                pl++;
            }
        }
        //Shuffle casero, creado con un random
        private List<string> shuffle(List<string> list, Random rng)
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
        //Comprueba si los mensajes automaticos están dentro del horario
        private void mensajes_automaticos()
        {
            string actual = DateTime.Now.ToString("HH:mm");
            byte[] bytes = null;

            foreach (string m in publimsg.GetMensajes())
            {
                //Separa los datos de mensaje
                string[] data = Regex.Split(m, @"\[]");
                string filename = data[0];
                string horario = data[1];
                if (horario == actual)
                {
                    bytes = File.ReadAllBytes(msg_folder+filename);
                    //Cargamos el fichero en memoria
                    if (playerMsgAuto.LoadSoundFromMemory(0, bytes, bytes.Length) == enumErrorCodes.NOERROR) { }
                    else
                    {
                        MessageBox.Show("No puedo cargar el fichero " + filename, "Error Grave", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    //Reproducimos el mensaje automatico
                    playerMsgAuto.PlaySound(0);
                    break;
                }
            }
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
            //TABLA HORARIO
            string desde = timeDesde.Value.ToString("HH:mm");
            string hasta = timeHasta.Value.ToString("HH:mm");
            //Se comprueba si existe horario en BD
            if (hro.ExisteHorario())
            {
                //existe, modificamos
                hro.ModificarHorario(desde, hasta);
            }
            else
            {
                //No existe, insertamos
                hro.InsertoHorario(desde, hasta);
            }
            //TABLA AUXILIAR
            int d = hro.Hour2min(desde);
            int h = hro.Hour2min(hasta);
            //Desde.. Hasta.. (para nuestro programa)
            if (d > h)
            {
                //Hay datos en aux
                if (hro.ExisteAuxiliar())
                {
                    //los borramos
                    hro.BorrarAuxiliar();
                }
                //Insertamos los nuevos datos aux
                hro.InsertoHoraAux(d, 1439); //Desde
                hro.InsertoHoraAux(0, h); //Hasta
            }
            else
            {
                //Hay datos en aux
                if (hro.ExisteAuxiliar())
                {
                    //los borramos
                    hro.BorrarAuxiliar();
                }
                //Insertamos los nuevos datos aux
                hro.InsertoHoraAux(d, h);
            }
        }
        //Horario: Hasta...
        private void timeHasta_ValueChanged(object sender, EventArgs e)
        {
            //TABLA HORARIO
            string desde = timeDesde.Value.ToString("HH:mm");
            string hasta = timeHasta.Value.ToString("HH:mm");
            //Se comprueba si existe horario en BD
            if (hro.ExisteHorario())
            {
                //existe, modificamos
                hro.ModificarHorario(desde, hasta);
            }
            else
            {
                //No existe, insertamos
                hro.InsertoHorario(desde, hasta);
            }
            //TABLA AUXILIAR
            int d = hro.Hour2min(desde);
            int h = hro.Hour2min(hasta);
            //Desde.. Hasta.. (para nuestro programa)
            if (d > h)
            {
                //Hay datos en aux
                if (hro.ExisteAuxiliar())
                {
                    //los borramos
                    hro.BorrarAuxiliar();
                }
                //Insertamos los nuevos datos aux
                hro.InsertoHoraAux(d, 1439); //Desde
                hro.InsertoHoraAux(0, h); //Hasta
            }
            else
            {
                //Hay datos en aux
                if (hro.ExisteAuxiliar())
                {
                    //los borramos
                    hro.BorrarAuxiliar();
                }
                //Insertamos los nuevos datos aux
                hro.InsertoHoraAux(d, h);
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
        //Cuando se carga un mensaje automatico
        private void playerMsgAuto_SoundLoaded(object sender, SoundLoadedEventArgs e)
        {
            //Establecemos el volumen para los msg auto                                                      
            playerMsgAuto.StreamVolumeLevelSet(0, (float)trackBarMsg.Value, enumVolumeScales.SCALE_LINEAR);
            //Bajamos el sonido del reproductor de musica                                                          
            playerMusic.StreamVolumeLevelSet(0, (float)0.0, enumVolumeScales.SCALE_LINEAR);
        }
        //Determina si un fichero es o no de publicidad
        private bool isPubli(string song)
        {
            bool output = false;
            //Si la cancion que toca es publicidad
            if (song.Contains(publi_folder))
            {
                output = true;
            }
            //Sino false
            return output;
        }
    }
}
