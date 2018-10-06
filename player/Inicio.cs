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
using System.Data.SQLite;
using System.IO;

namespace player
{
    public partial class Inicio : Form
    {
        SQLiteConnection connection;
        private bool st_salida = false;  //Evalua la salida del programa
        private Object obj = new Object(); // para bloqueo
        private Shared shd = new Shared();
        private string string_connection = @"Data Source=shop.db; Version=3;";

        public Inicio()
        {
            InitializeComponent();
            wClient.Encoding = Encoding.UTF8; //UTF8
        }
        //Entrada en el campo de texto (msgIns)
        private void msgIns_Enter(object sender, EventArgs e)
        {   //Mostramos un explorador de ficheros
            if (openMsgInst.ShowDialog() == DialogResult.OK)
            {
                msgIns.Text = openMsgInst.FileName;
            }
        }
        private void musicDirs_Enter(object sender, EventArgs e)
        {
            if (openMusicDirs.ShowDialog() == DialogResult.OK)
            {
                musicDirs.Text = openMusicDirs.SelectedPath;
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
            if ((txtServer.Text.Contains("http://") == false) && (txtServer.Text.Contains("https://") == false))
            {
                errorServer.SetError(txtServer, "URL no válida");
            }
            else
            {
                errorServer.SetError(txtServer, null);
            }
        }

        private void btnSendProxy_Click(object sender, EventArgs e)
        {
            if ((textProxy.Text.Contains("http://") == false) && (textProxy.Text.Contains("https://") == false))
            {
                errorProxy.SetError(textProxy, "URL no válida");
            }
            else
            {
                errorProxy.SetError(textProxy, null);
            }
            if (textProxy.Text == "")
            {
                errorProxy.SetError(textProxy, null);
            }
        }
        //LOAD: CARGA DE INICIO 
        private void Inicio_Load(object sender, EventArgs e)
        {
            showEntidad();
            uploadDomains();
            //Task.Factory.StartNew(timeListado);
            //int wait5min = (5 * 60 * 1000); // 5 min
            timeEstado.Interval = 300000;
            timeEstado.Start();
            timeListado.Interval = 190000;
            timeListado.Start();

        }
        private void timeEstado_Tick(object sender, EventArgs e)
        {
            listBoxDom.Items.Add("Ejecutado ESTADO");
            shd.Status = wClient.DownloadString("http://192.168.0.102:8080/acciones.cgi?action=check_entidad&ent=Acciona");
            //Estado de la Tienda
            if (shd.Status == "1")
            {
                barStInfoServ.ForeColor = Color.Green;
                barStInfoServ.Text = "Activada";
            }
            if (shd.Status == "0")
            {
                barStInfoServ.ForeColor = Color.Red;
                barStInfoServ.Text = "Desactivada";
            }
        }

        private void timeListado_Tick(object sender, EventArgs e)
        {
            listBoxDom.Items.Add("Ejecutado LISTADO");
            string res = wClient.DownloadString(HttpUtility.UrlPathEncode("http://192.168.0.102:8080/acciones.cgi?action=send_domains&dominios=Acciona.Transmediterranea.España.Andalucia.Malaga.ACC FORTUNY:.:"));
            string[] lista = Regex.Split(res, @"\[publi];");
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
                string[] lst_msg = Regex.Split(res, @"\[mensaje];");
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
        }
        //Muestra la entidad (zona de configuración)
        private void showEntidad()
        {
            errorAddDom.Clear();
            //Leemos fichero de configuración
            string[] readText = File.ReadAllLines("config.ini", Encoding.UTF8);
            foreach (string s in readText)
            {
                string[] dat_ent = s.Split('=');
                //Mostramos el nombre de entidad
                domEntidad.Items.Add(dat_ent[1]);
                //Guardamos el id de entidad
                string id = wClient.DownloadString(@"http://192.168.0.102:8080/transf_orgs_vs.cgi?action=entidad&nom_ent=" + dat_ent[1]);
                shd.IDEntidad = id;
            }
        }
        //Cambio de Entidad(combobox de configuracion)
        private void domEntidad_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorAddDom.Clear();
            BindingList<Combos> save_alm = new BindingList<Combos>();
            string query = wClient.DownloadString(@"http://192.168.0.102:8080/transf_orgs_vs.cgi?action=almacen&entidad=" + shd.IDEntidad);
            string[]alm = query.Split(';');
            foreach (string alms in alm)
            {
                if (alms != "")
                {
                    //Separamos los distintos almacenes
                    string[] almacenes = Regex.Split(alms, @"\<=>");
                    save_alm.Add(new Combos(almacenes[0], almacenes[1]));
                }
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
            string query = wClient.DownloadString(@"http://192.168.0.102:8080/transf_orgs_vs.cgi?action=pais&almacen=" + domAlmacen.SelectedValue.ToString());
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
            domPais.DataSource = save_pais;
            domPais.DisplayMember = "Value";
            domPais.ValueMember = "ID";
        }
        //Cambio de Pais(combobox de configuracion)
        private void domPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorAddDom.Clear();
            BindingList<Combos> save_region = new BindingList<Combos>();
            string query = wClient.DownloadString(@"http://192.168.0.102:8080/transf_orgs_vs.cgi?action=region&pais=" + domPais.SelectedValue.ToString());
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
            domRegion.DataSource = save_region;
            domRegion.DisplayMember = "Value";
            domRegion.ValueMember = "ID";
        }
        //Cambio de Region(combobox de configuracion)
        private void domRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorAddDom.Clear();
            BindingList<Combos> save_prov = new BindingList<Combos>();
            string query = wClient.DownloadString(@"http://192.168.0.102:8080/transf_orgs_vs.cgi?action=provincia&region=" + domRegion.SelectedValue.ToString());
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
            domProv.DataSource = save_prov;
            domProv.DisplayMember = "Value";
            domProv.ValueMember = "ID";
        }
        //Cambio de Provincia(combobox de configuracion)
        private void domProv_SelectedIndexChanged(object sender, EventArgs e)
        {
            errorAddDom.Clear();
            BindingList<Combos> save_shop = new BindingList<Combos>();
            string query = wClient.DownloadString(@"http://192.168.0.102:8080/transf_orgs_vs.cgi?action=tienda&provincia=" + domProv.SelectedValue.ToString());
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

            domTienda.DataSource = save_shop;
            domTienda.DisplayMember = "Value";
            domTienda.ValueMember = "ID";
        }
        //Añadir Dominio (zona de configuracion)
        private void btnAddDom_Click(object sender, EventArgs e)
        {
            try
            {
                string ent  = domEntidad.SelectedItem.ToString();
                string alm  = ((Combos)domAlmacen.SelectedItem).Value;
                string pais = ((Combos)domPais.SelectedItem).Value;
                string reg  = ((Combos)domRegion.SelectedItem).Value;
                string prov = ((Combos)domProv.SelectedItem).Value;
                string shop = ((Combos)domTienda.SelectedItem).Value;
                //Colocamos cada una de las organizaciones para formar el dominio
                string dominio = string.Format("{0} - {1} - {2} - {3} - {4} - {5}", ent, alm, pais, reg, prov, shop);
                //Se comprueba la existencia del dominio en la base de datos
                bool existe = existDomainBD(dominio);
                if (!existe)
                {
                    lock (obj)
                    {
                        //Se inserta en la BD
                        using (connection = new SQLiteConnection(string_connection))
                        {
                            connection.Open();
                            string query = string.Format(@"INSERT INTO dominios (dominio) VALUES ('{0}');", dominio);
                            SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                            cmd_exc.ExecuteNonQuery();
                            connection.Close();
                        }
                        //Se añade al listbox
                        listBoxDom.Items.Add(dominio);
                    }
                }
                else
                {
                    //El dominio ya existe
                    errorAddDom.SetError(btnAddDom, "Fail to Add: this domain already exists");
                }
            }
            catch
            {
                //Organizaciones nulas: no tienen valor
                errorAddDom.SetError(btnAddDom, "Fail to Add: select all organizations");
            }
        }
        //Borrar Dominio (zona de configuracion)
        private void btnBorrarDom_Click(object sender, EventArgs e)
        {
            lock (obj)
            {
                //borramos dominio de base de datos
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"DELETE FROM dominios WHERE dominio = '{0}'", listBoxDom.GetItemText(listBoxDom.SelectedItem));
                    SQLiteCommand cmd_exc = new SQLiteCommand(query, connection);
                    cmd_exc.ExecuteNonQuery();
                    connection.Close();
                }
                //Borramos dominio del listado
                listBoxDom.Items.RemoveAt(listBoxDom.SelectedIndex);
            }
        }
        //Encargado de cargar los dominios de la BD en el listbox
        private void uploadDomains()
        {
            lock (obj)
            {
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT dominio FROM dominios");
                    SQLiteCommand cmd = new SQLiteCommand(query, connection);
                    SQLiteDataReader datos = cmd.ExecuteReader();
                    while (datos.Read())
                    {
                        // recogemos los datos
                        string dom = datos.GetString(datos.GetOrdinal("dominio"));
                        listBoxDom.Items.Add(dom);
                    }
                    connection.Close();
                }
            }
        }
        //Mira la existencia de un dominio en BD: Devuelve TRUE (existe) o FALSE (no existe)
        private bool existDomainBD(string dom)
        {
            lock (obj)
            {
                bool existe = true;
                using (connection = new SQLiteConnection(string_connection))
                {
                    connection.Open();
                    string query = string.Format(@"SELECT count(*) as cont FROM dominios WHERE dominio=('{0}');", dom);
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
        //Mira la existencia de un fichero en una tabla (publi/msg): Devuelve TRUE (existe) o FALSE (no existe)
        private bool existFileInBD(string namefile, string table)
        {
            lock (obj)
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
            lock (obj)
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
                        string f_ini_bd = datos.GetString(datos.GetOrdinal("fecha_ini"));
                        string f_fin_bd = datos.GetString(datos.GetOrdinal("fecha_fin"));
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
            lock (obj)
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
                        string f_ini_bd = datos.GetString(datos.GetOrdinal("fecha_ini"));
                        string f_fin_bd = datos.GetString(datos.GetOrdinal("fecha_fin"));
                        string playtime_bd = datos.GetString(datos.GetOrdinal("playtime"));
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
            lock (obj)
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
            lock (obj)
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
            lock (obj)
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
            lock (obj)
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
            lock (obj)
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
            lock (obj)
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
