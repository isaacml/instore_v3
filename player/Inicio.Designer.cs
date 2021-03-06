﻿namespace player
{
    partial class Inicio
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inicio));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Música = new System.Windows.Forms.TabPage();
            this.playerInsta = new AudioDjStudio.AudioDjStudio();
            this.lblListDirMusic = new System.Windows.Forms.Label();
            this.listMusicDirs = new System.Windows.Forms.CheckedListBox();
            this.lblMusicDirs = new System.Windows.Forms.Label();
            this.musicDirs = new System.Windows.Forms.TextBox();
            this.lblMsgInst = new System.Windows.Forms.Label();
            this.sendMsgInst = new System.Windows.Forms.Button();
            this.msgIns = new System.Windows.Forms.TextBox();
            this.Controles = new System.Windows.Forms.TabPage();
            this.playerMsgAuto = new AudioDjStudio.AudioDjStudio();
            this.playerMusic = new AudioDjStudio.AudioDjStudio();
            this.groupVolumen = new System.Windows.Forms.GroupBox();
            this.lblMsgContainer = new System.Windows.Forms.Label();
            this.lblPubliContainer = new System.Windows.Forms.Label();
            this.lblMusicContainer = new System.Windows.Forms.Label();
            this.lblTrackMsg = new System.Windows.Forms.Label();
            this.lblTrackPubli = new System.Windows.Forms.Label();
            this.lblTrackMusic = new System.Windows.Forms.Label();
            this.trackBarMsg = new System.Windows.Forms.TrackBar();
            this.trackBarPubli = new System.Windows.Forms.TrackBar();
            this.trackBarMusica = new System.Windows.Forms.TrackBar();
            this.groupHorario = new System.Windows.Forms.GroupBox();
            this.timeHasta = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.lblDesde = new System.Windows.Forms.Label();
            this.timeDesde = new System.Windows.Forms.DateTimePicker();
            this.Configuración = new System.Windows.Forms.TabPage();
            this.lblProxy = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.btnSendProxy = new System.Windows.Forms.Button();
            this.btnSendServer = new System.Windows.Forms.Button();
            this.textProxy = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.groupBoxDom = new System.Windows.Forms.GroupBox();
            this.btnBorrarDom = new System.Windows.Forms.Button();
            this.listBoxDom = new System.Windows.Forms.ListBox();
            this.btnAddDom = new System.Windows.Forms.Button();
            this.domTienda = new System.Windows.Forms.ComboBox();
            this.domProv = new System.Windows.Forms.ComboBox();
            this.domRegion = new System.Windows.Forms.ComboBox();
            this.domPais = new System.Windows.Forms.ComboBox();
            this.domAlmacen = new System.Windows.Forms.ComboBox();
            this.domEntidad = new System.Windows.Forms.ComboBox();
            this.openMsgInst = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mostrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SalirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyBarIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.openMusicDirs = new System.Windows.Forms.FolderBrowserDialog();
            this.errorServer = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProxy = new System.Windows.Forms.ErrorProvider(this.components);
            this.Timer5MIN = new System.Windows.Forms.Timer(this.components);
            this.errorAddDom = new System.Windows.Forms.ErrorProvider(this.components);
            this.Timer1MIN = new System.Windows.Forms.Timer(this.components);
            this.tPlayer = new System.Windows.Forms.Timer(this.components);
            this.barra_estado = new System.Windows.Forms.StatusStrip();
            this.barStInfoServ = new System.Windows.Forms.ToolStripStatusLabel();
            this.barStStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.barStSong = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1.SuspendLayout();
            this.Música.SuspendLayout();
            this.Controles.SuspendLayout();
            this.groupVolumen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMsg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPubli)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMusica)).BeginInit();
            this.groupHorario.SuspendLayout();
            this.Configuración.SuspendLayout();
            this.groupBoxDom.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorServer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProxy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorAddDom)).BeginInit();
            this.barra_estado.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Música);
            this.tabControl1.Controls.Add(this.Controles);
            this.tabControl1.Controls.Add(this.Configuración);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(760, 457);
            this.tabControl1.TabIndex = 0;
            // 
            // Música
            // 
            this.Música.Controls.Add(this.playerInsta);
            this.Música.Controls.Add(this.lblListDirMusic);
            this.Música.Controls.Add(this.listMusicDirs);
            this.Música.Controls.Add(this.lblMusicDirs);
            this.Música.Controls.Add(this.musicDirs);
            this.Música.Controls.Add(this.lblMsgInst);
            this.Música.Controls.Add(this.sendMsgInst);
            this.Música.Controls.Add(this.msgIns);
            this.Música.Location = new System.Drawing.Point(4, 22);
            this.Música.Name = "Música";
            this.Música.Padding = new System.Windows.Forms.Padding(3);
            this.Música.Size = new System.Drawing.Size(752, 431);
            this.Música.TabIndex = 0;
            this.Música.Text = "Música";
            this.Música.ToolTipText = "Música";
            this.Música.UseVisualStyleBackColor = true;
            // 
            // playerInsta
            // 
            this.playerInsta.FaderSettings = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            this.playerInsta.LastError = AudioDjStudio.enumErrorCodes.ERR_NOERROR;
            this.playerInsta.Location = new System.Drawing.Point(573, 323);
            this.playerInsta.Name = "playerInsta";
            this.playerInsta.Size = new System.Drawing.Size(48, 48);
            this.playerInsta.TabIndex = 10;
            // 
            // lblListDirMusic
            // 
            this.lblListDirMusic.AutoSize = true;
            this.lblListDirMusic.Location = new System.Drawing.Point(207, 120);
            this.lblListDirMusic.Name = "lblListDirMusic";
            this.lblListDirMusic.Size = new System.Drawing.Size(109, 13);
            this.lblListDirMusic.TabIndex = 3;
            this.lblListDirMusic.Text = "Listado de Directorios";
            // 
            // listMusicDirs
            // 
            this.listMusicDirs.FormattingEnabled = true;
            this.listMusicDirs.Location = new System.Drawing.Point(210, 136);
            this.listMusicDirs.Name = "listMusicDirs";
            this.listMusicDirs.Size = new System.Drawing.Size(343, 184);
            this.listMusicDirs.TabIndex = 4;
            this.listMusicDirs.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.listMusicDirs_ItemCheck);
            // 
            // lblMusicDirs
            // 
            this.lblMusicDirs.AutoSize = true;
            this.lblMusicDirs.Location = new System.Drawing.Point(207, 53);
            this.lblMusicDirs.Name = "lblMusicDirs";
            this.lblMusicDirs.Size = new System.Drawing.Size(111, 13);
            this.lblMusicDirs.TabIndex = 1;
            this.lblMusicDirs.Text = "Seleccionar Directorio";
            // 
            // musicDirs
            // 
            this.musicDirs.Location = new System.Drawing.Point(210, 69);
            this.musicDirs.Name = "musicDirs";
            this.musicDirs.ReadOnly = true;
            this.musicDirs.Size = new System.Drawing.Size(262, 20);
            this.musicDirs.TabIndex = 2;
            this.musicDirs.Text = "C:";
            this.musicDirs.Click += new System.EventHandler(this.musicDirs_Click);
            // 
            // lblMsgInst
            // 
            this.lblMsgInst.AutoSize = true;
            this.lblMsgInst.Location = new System.Drawing.Point(207, 335);
            this.lblMsgInst.Name = "lblMsgInst";
            this.lblMsgInst.Size = new System.Drawing.Size(102, 13);
            this.lblMsgInst.TabIndex = 5;
            this.lblMsgInst.Text = "Reproducir Mensaje";
            // 
            // sendMsgInst
            // 
            this.sendMsgInst.BackColor = System.Drawing.Color.PaleGreen;
            this.sendMsgInst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendMsgInst.Location = new System.Drawing.Point(478, 348);
            this.sendMsgInst.Name = "sendMsgInst";
            this.sendMsgInst.Size = new System.Drawing.Size(75, 23);
            this.sendMsgInst.TabIndex = 7;
            this.sendMsgInst.Text = "Play";
            this.sendMsgInst.UseVisualStyleBackColor = false;
            this.sendMsgInst.Click += new System.EventHandler(this.sendMsgInst_Click);
            // 
            // msgIns
            // 
            this.msgIns.Location = new System.Drawing.Point(210, 351);
            this.msgIns.Name = "msgIns";
            this.msgIns.ReadOnly = true;
            this.msgIns.Size = new System.Drawing.Size(262, 20);
            this.msgIns.TabIndex = 6;
            this.msgIns.Click += new System.EventHandler(this.msgIns_Click);
            // 
            // Controles
            // 
            this.Controles.Controls.Add(this.playerMsgAuto);
            this.Controles.Controls.Add(this.playerMusic);
            this.Controles.Controls.Add(this.groupVolumen);
            this.Controles.Controls.Add(this.groupHorario);
            this.Controles.Location = new System.Drawing.Point(4, 22);
            this.Controles.Name = "Controles";
            this.Controles.Padding = new System.Windows.Forms.Padding(3);
            this.Controles.Size = new System.Drawing.Size(752, 431);
            this.Controles.TabIndex = 1;
            this.Controles.Text = "Controles";
            this.Controles.ToolTipText = "Tienda";
            this.Controles.UseVisualStyleBackColor = true;
            // 
            // playerMsgAuto
            // 
            this.playerMsgAuto.FaderSettings = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            this.playerMsgAuto.LastError = AudioDjStudio.enumErrorCodes.ERR_NOERROR;
            this.playerMsgAuto.Location = new System.Drawing.Point(586, 291);
            this.playerMsgAuto.Name = "playerMsgAuto";
            this.playerMsgAuto.Size = new System.Drawing.Size(48, 48);
            this.playerMsgAuto.TabIndex = 3;
            this.playerMsgAuto.SoundLoaded += new AudioDjStudio.AudioDjStudio.SoundLoadedEventHandler(this.playerMsgAuto_SoundLoaded);
            // 
            // playerMusic
            // 
            this.playerMusic.FaderSettings = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA";
            this.playerMusic.LastError = AudioDjStudio.enumErrorCodes.ERR_NOERROR;
            this.playerMusic.Location = new System.Drawing.Point(585, 352);
            this.playerMusic.Name = "playerMusic";
            this.playerMusic.Size = new System.Drawing.Size(48, 48);
            this.playerMusic.TabIndex = 2;
            this.playerMusic.SoundLoaded += new AudioDjStudio.AudioDjStudio.SoundLoadedEventHandler(this.playerMusic_SoundLoaded);
            // 
            // groupVolumen
            // 
            this.groupVolumen.Controls.Add(this.lblMsgContainer);
            this.groupVolumen.Controls.Add(this.lblPubliContainer);
            this.groupVolumen.Controls.Add(this.lblMusicContainer);
            this.groupVolumen.Controls.Add(this.lblTrackMsg);
            this.groupVolumen.Controls.Add(this.lblTrackPubli);
            this.groupVolumen.Controls.Add(this.lblTrackMusic);
            this.groupVolumen.Controls.Add(this.trackBarMsg);
            this.groupVolumen.Controls.Add(this.trackBarPubli);
            this.groupVolumen.Controls.Add(this.trackBarMusica);
            this.groupVolumen.Location = new System.Drawing.Point(190, 135);
            this.groupVolumen.Name = "groupVolumen";
            this.groupVolumen.Size = new System.Drawing.Size(389, 265);
            this.groupVolumen.TabIndex = 1;
            this.groupVolumen.TabStop = false;
            this.groupVolumen.Text = "Volumen";
            // 
            // lblMsgContainer
            // 
            this.lblMsgContainer.AutoSize = true;
            this.lblMsgContainer.Location = new System.Drawing.Point(279, 215);
            this.lblMsgContainer.Name = "lblMsgContainer";
            this.lblMsgContainer.Size = new System.Drawing.Size(19, 13);
            this.lblMsgContainer.TabIndex = 9;
            this.lblMsgContainer.Text = "50";
            // 
            // lblPubliContainer
            // 
            this.lblPubliContainer.AutoSize = true;
            this.lblPubliContainer.Location = new System.Drawing.Point(179, 215);
            this.lblPubliContainer.Name = "lblPubliContainer";
            this.lblPubliContainer.Size = new System.Drawing.Size(19, 13);
            this.lblPubliContainer.TabIndex = 8;
            this.lblPubliContainer.Text = "80";
            // 
            // lblMusicContainer
            // 
            this.lblMusicContainer.AutoSize = true;
            this.lblMusicContainer.Location = new System.Drawing.Point(68, 215);
            this.lblMusicContainer.Name = "lblMusicContainer";
            this.lblMusicContainer.Size = new System.Drawing.Size(25, 13);
            this.lblMusicContainer.TabIndex = 7;
            this.lblMusicContainer.Text = "100";
            // 
            // lblTrackMsg
            // 
            this.lblTrackMsg.AutoSize = true;
            this.lblTrackMsg.Location = new System.Drawing.Point(261, 28);
            this.lblTrackMsg.Name = "lblTrackMsg";
            this.lblTrackMsg.Size = new System.Drawing.Size(52, 13);
            this.lblTrackMsg.TabIndex = 0;
            this.lblTrackMsg.Text = "Mensajes";
            // 
            // lblTrackPubli
            // 
            this.lblTrackPubli.AutoSize = true;
            this.lblTrackPubli.Location = new System.Drawing.Point(161, 28);
            this.lblTrackPubli.Name = "lblTrackPubli";
            this.lblTrackPubli.Size = new System.Drawing.Size(56, 13);
            this.lblTrackPubli.TabIndex = 0;
            this.lblTrackPubli.Text = "Publicidad";
            // 
            // lblTrackMusic
            // 
            this.lblTrackMusic.AutoSize = true;
            this.lblTrackMusic.Location = new System.Drawing.Point(61, 28);
            this.lblTrackMusic.Name = "lblTrackMusic";
            this.lblTrackMusic.Size = new System.Drawing.Size(41, 13);
            this.lblTrackMusic.TabIndex = 0;
            this.lblTrackMusic.Text = "Música";
            // 
            // trackBarMsg
            // 
            this.trackBarMsg.Location = new System.Drawing.Point(264, 44);
            this.trackBarMsg.Maximum = 100;
            this.trackBarMsg.Name = "trackBarMsg";
            this.trackBarMsg.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarMsg.Size = new System.Drawing.Size(45, 168);
            this.trackBarMsg.TabIndex = 5;
            this.trackBarMsg.TickFrequency = 10;
            this.trackBarMsg.Value = 50;
            this.trackBarMsg.Scroll += new System.EventHandler(this.trackBarMsg_Scroll);
            // 
            // trackBarPubli
            // 
            this.trackBarPubli.Location = new System.Drawing.Point(164, 44);
            this.trackBarPubli.Maximum = 100;
            this.trackBarPubli.Name = "trackBarPubli";
            this.trackBarPubli.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarPubli.Size = new System.Drawing.Size(45, 168);
            this.trackBarPubli.TabIndex = 4;
            this.trackBarPubli.TickFrequency = 10;
            this.trackBarPubli.Value = 80;
            this.trackBarPubli.Scroll += new System.EventHandler(this.trackBarPubli_Scroll);
            // 
            // trackBarMusica
            // 
            this.trackBarMusica.BackColor = System.Drawing.SystemColors.Control;
            this.trackBarMusica.Location = new System.Drawing.Point(57, 44);
            this.trackBarMusica.Maximum = 100;
            this.trackBarMusica.Name = "trackBarMusica";
            this.trackBarMusica.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarMusica.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackBarMusica.Size = new System.Drawing.Size(45, 168);
            this.trackBarMusica.TabIndex = 3;
            this.trackBarMusica.TickFrequency = 10;
            this.trackBarMusica.Value = 100;
            this.trackBarMusica.Scroll += new System.EventHandler(this.trackBarMusica_Scroll);
            // 
            // groupHorario
            // 
            this.groupHorario.Controls.Add(this.timeHasta);
            this.groupHorario.Controls.Add(this.lblHasta);
            this.groupHorario.Controls.Add(this.lblDesde);
            this.groupHorario.Controls.Add(this.timeDesde);
            this.groupHorario.Location = new System.Drawing.Point(190, 36);
            this.groupHorario.Name = "groupHorario";
            this.groupHorario.Size = new System.Drawing.Size(389, 80);
            this.groupHorario.TabIndex = 0;
            this.groupHorario.TabStop = false;
            this.groupHorario.Text = "Horario";
            // 
            // timeHasta
            // 
            this.timeHasta.CustomFormat = "HH:mm";
            this.timeHasta.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeHasta.Location = new System.Drawing.Point(264, 35);
            this.timeHasta.Name = "timeHasta";
            this.timeHasta.ShowUpDown = true;
            this.timeHasta.Size = new System.Drawing.Size(64, 20);
            this.timeHasta.TabIndex = 2;
            this.timeHasta.Value = new System.DateTime(2018, 11, 8, 12, 0, 0, 0);
            this.timeHasta.ValueChanged += new System.EventHandler(this.timeHasta_ValueChanged);
            // 
            // lblHasta
            // 
            this.lblHasta.AutoSize = true;
            this.lblHasta.Location = new System.Drawing.Point(220, 41);
            this.lblHasta.Name = "lblHasta";
            this.lblHasta.Size = new System.Drawing.Size(38, 13);
            this.lblHasta.TabIndex = 0;
            this.lblHasta.Text = "Hasta:";
            // 
            // lblDesde
            // 
            this.lblDesde.AutoSize = true;
            this.lblDesde.Location = new System.Drawing.Point(54, 41);
            this.lblDesde.Name = "lblDesde";
            this.lblDesde.Size = new System.Drawing.Size(41, 13);
            this.lblDesde.TabIndex = 0;
            this.lblDesde.Text = "Desde:";
            // 
            // timeDesde
            // 
            this.timeDesde.CustomFormat = "HH:mm";
            this.timeDesde.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.timeDesde.Location = new System.Drawing.Point(101, 35);
            this.timeDesde.Name = "timeDesde";
            this.timeDesde.ShowUpDown = true;
            this.timeDesde.Size = new System.Drawing.Size(64, 20);
            this.timeDesde.TabIndex = 1;
            this.timeDesde.Value = new System.DateTime(2018, 11, 8, 10, 0, 0, 0);
            this.timeDesde.ValueChanged += new System.EventHandler(this.timeDesde_ValueChanged);
            // 
            // Configuración
            // 
            this.Configuración.Controls.Add(this.lblProxy);
            this.Configuración.Controls.Add(this.lblServer);
            this.Configuración.Controls.Add(this.btnSendProxy);
            this.Configuración.Controls.Add(this.btnSendServer);
            this.Configuración.Controls.Add(this.textProxy);
            this.Configuración.Controls.Add(this.txtServer);
            this.Configuración.Controls.Add(this.groupBoxDom);
            this.Configuración.Location = new System.Drawing.Point(4, 22);
            this.Configuración.Name = "Configuración";
            this.Configuración.Size = new System.Drawing.Size(752, 431);
            this.Configuración.TabIndex = 2;
            this.Configuración.Text = "Configuración";
            this.Configuración.ToolTipText = "Configuración";
            this.Configuración.UseVisualStyleBackColor = true;
            // 
            // lblProxy
            // 
            this.lblProxy.AutoSize = true;
            this.lblProxy.Location = new System.Drawing.Point(159, 375);
            this.lblProxy.Name = "lblProxy";
            this.lblProxy.Size = new System.Drawing.Size(61, 13);
            this.lblProxy.TabIndex = 6;
            this.lblProxy.Text = "URL Proxy:";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(158, 334);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(74, 13);
            this.lblServer.TabIndex = 5;
            this.lblServer.Text = "URL Servidor:";
            // 
            // btnSendProxy
            // 
            this.btnSendProxy.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSendProxy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendProxy.Location = new System.Drawing.Point(585, 367);
            this.btnSendProxy.Name = "btnSendProxy";
            this.btnSendProxy.Size = new System.Drawing.Size(75, 23);
            this.btnSendProxy.TabIndex = 4;
            this.btnSendProxy.Text = "Enviar";
            this.btnSendProxy.UseVisualStyleBackColor = false;
            this.btnSendProxy.Click += new System.EventHandler(this.btnSendProxy_Click);
            // 
            // btnSendServer
            // 
            this.btnSendServer.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnSendServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSendServer.Location = new System.Drawing.Point(585, 328);
            this.btnSendServer.Name = "btnSendServer";
            this.btnSendServer.Size = new System.Drawing.Size(75, 23);
            this.btnSendServer.TabIndex = 3;
            this.btnSendServer.Text = "Enviar";
            this.btnSendServer.UseVisualStyleBackColor = false;
            this.btnSendServer.Click += new System.EventHandler(this.btnSendServer_Click);
            // 
            // textProxy
            // 
            this.textProxy.Location = new System.Drawing.Point(255, 370);
            this.textProxy.Name = "textProxy";
            this.textProxy.Size = new System.Drawing.Size(300, 20);
            this.textProxy.TabIndex = 2;
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(255, 331);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(300, 20);
            this.txtServer.TabIndex = 1;
            this.txtServer.Text = "http://dss.vidanio.com:8080";
            // 
            // groupBoxDom
            // 
            this.groupBoxDom.Controls.Add(this.btnBorrarDom);
            this.groupBoxDom.Controls.Add(this.listBoxDom);
            this.groupBoxDom.Controls.Add(this.btnAddDom);
            this.groupBoxDom.Controls.Add(this.domTienda);
            this.groupBoxDom.Controls.Add(this.domProv);
            this.groupBoxDom.Controls.Add(this.domRegion);
            this.groupBoxDom.Controls.Add(this.domPais);
            this.groupBoxDom.Controls.Add(this.domAlmacen);
            this.groupBoxDom.Controls.Add(this.domEntidad);
            this.groupBoxDom.Location = new System.Drawing.Point(47, 21);
            this.groupBoxDom.Name = "groupBoxDom";
            this.groupBoxDom.Size = new System.Drawing.Size(685, 292);
            this.groupBoxDom.TabIndex = 0;
            this.groupBoxDom.TabStop = false;
            this.groupBoxDom.Text = "Configuración de Dominios";
            // 
            // btnBorrarDom
            // 
            this.btnBorrarDom.BackColor = System.Drawing.Color.Firebrick;
            this.btnBorrarDom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBorrarDom.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnBorrarDom.Location = new System.Drawing.Point(286, 256);
            this.btnBorrarDom.Name = "btnBorrarDom";
            this.btnBorrarDom.Size = new System.Drawing.Size(121, 23);
            this.btnBorrarDom.TabIndex = 8;
            this.btnBorrarDom.Text = "Borrar";
            this.btnBorrarDom.UseVisualStyleBackColor = false;
            this.btnBorrarDom.Click += new System.EventHandler(this.btnBorrarDom_Click);
            // 
            // listBoxDom
            // 
            this.listBoxDom.FormattingEnabled = true;
            this.listBoxDom.Location = new System.Drawing.Point(57, 155);
            this.listBoxDom.Name = "listBoxDom";
            this.listBoxDom.Size = new System.Drawing.Size(574, 95);
            this.listBoxDom.TabIndex = 7;
            // 
            // btnAddDom
            // 
            this.btnAddDom.BackColor = System.Drawing.Color.PaleGreen;
            this.btnAddDom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddDom.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnAddDom.Location = new System.Drawing.Point(286, 116);
            this.btnAddDom.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddDom.Name = "btnAddDom";
            this.btnAddDom.Size = new System.Drawing.Size(121, 23);
            this.btnAddDom.TabIndex = 6;
            this.btnAddDom.Text = "Añadir";
            this.btnAddDom.UseVisualStyleBackColor = false;
            this.btnAddDom.Click += new System.EventHandler(this.btnAddDom_Click);
            // 
            // domTienda
            // 
            this.domTienda.FormattingEnabled = true;
            this.domTienda.Location = new System.Drawing.Point(454, 83);
            this.domTienda.Name = "domTienda";
            this.domTienda.Size = new System.Drawing.Size(121, 21);
            this.domTienda.TabIndex = 5;
            this.domTienda.Text = "<Tienda>";
            // 
            // domProv
            // 
            this.domProv.FormattingEnabled = true;
            this.domProv.Location = new System.Drawing.Point(286, 83);
            this.domProv.Name = "domProv";
            this.domProv.Size = new System.Drawing.Size(121, 21);
            this.domProv.TabIndex = 4;
            this.domProv.Text = "<Provincia>";
            this.domProv.SelectedIndexChanged += new System.EventHandler(this.domProv_SelectedIndexChanged);
            // 
            // domRegion
            // 
            this.domRegion.FormattingEnabled = true;
            this.domRegion.Location = new System.Drawing.Point(107, 84);
            this.domRegion.Name = "domRegion";
            this.domRegion.Size = new System.Drawing.Size(121, 21);
            this.domRegion.TabIndex = 3;
            this.domRegion.Text = "<Región>";
            this.domRegion.SelectedIndexChanged += new System.EventHandler(this.domRegion_SelectedIndexChanged);
            // 
            // domPais
            // 
            this.domPais.FormattingEnabled = true;
            this.domPais.Location = new System.Drawing.Point(454, 44);
            this.domPais.Name = "domPais";
            this.domPais.Size = new System.Drawing.Size(121, 21);
            this.domPais.TabIndex = 2;
            this.domPais.Text = "<País>";
            this.domPais.SelectedIndexChanged += new System.EventHandler(this.domPais_SelectedIndexChanged);
            // 
            // domAlmacen
            // 
            this.domAlmacen.FormattingEnabled = true;
            this.domAlmacen.Location = new System.Drawing.Point(286, 44);
            this.domAlmacen.Name = "domAlmacen";
            this.domAlmacen.Size = new System.Drawing.Size(121, 21);
            this.domAlmacen.TabIndex = 1;
            this.domAlmacen.Text = "<Almacen>";
            this.domAlmacen.SelectedIndexChanged += new System.EventHandler(this.domAlmacen_SelectedIndexChanged);
            // 
            // domEntidad
            // 
            this.domEntidad.FormattingEnabled = true;
            this.domEntidad.Location = new System.Drawing.Point(107, 44);
            this.domEntidad.Name = "domEntidad";
            this.domEntidad.Size = new System.Drawing.Size(121, 21);
            this.domEntidad.TabIndex = 0;
            this.domEntidad.Text = "<Entidad>";
            this.domEntidad.SelectedIndexChanged += new System.EventHandler(this.domEntidad_SelectedIndexChanged);
            // 
            // openMsgInst
            // 
            this.openMsgInst.Filter = "Archivos MP3|*.mp3|Archivos WMA|*.wma";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mostrarToolStripMenuItem,
            this.SalirToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(116, 48);
            // 
            // mostrarToolStripMenuItem
            // 
            this.mostrarToolStripMenuItem.Name = "mostrarToolStripMenuItem";
            this.mostrarToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.mostrarToolStripMenuItem.Text = "Mostrar";
            this.mostrarToolStripMenuItem.Click += new System.EventHandler(this.mostrarToolStripMenuItem_Click);
            // 
            // SalirToolStripMenuItem
            // 
            this.SalirToolStripMenuItem.Name = "SalirToolStripMenuItem";
            this.SalirToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.SalirToolStripMenuItem.Text = "Salir";
            this.SalirToolStripMenuItem.Click += new System.EventHandler(this.SalirToolStripMenuItem_Click);
            // 
            // notifyBarIcon
            // 
            this.notifyBarIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyBarIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyBarIcon.Icon")));
            this.notifyBarIcon.Text = "HMPro Player";
            this.notifyBarIcon.Visible = true;
            this.notifyBarIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyBarIcon_MouseDoubleClick);
            // 
            // openMusicDirs
            // 
            this.openMusicDirs.SelectedPath = "C:";
            // 
            // errorServer
            // 
            this.errorServer.ContainerControl = this;
            // 
            // errorProxy
            // 
            this.errorProxy.ContainerControl = this;
            // 
            // Timer5MIN
            // 
            this.Timer5MIN.Tick += new System.EventHandler(this.Timer5MIN_Tick);
            // 
            // errorAddDom
            // 
            this.errorAddDom.ContainerControl = this;
            // 
            // Timer1MIN
            // 
            this.Timer1MIN.Tick += new System.EventHandler(this.Timer1MIN_Tick);
            // 
            // tPlayer
            // 
            this.tPlayer.Interval = 1000;
            this.tPlayer.Tick += new System.EventHandler(this.tPlayer_Tick);
            // 
            // barra_estado
            // 
            this.barra_estado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.barStInfoServ,
            this.barStStatus,
            this.barStSong});
            this.barra_estado.Location = new System.Drawing.Point(0, 435);
            this.barra_estado.Name = "barra_estado";
            this.barra_estado.Size = new System.Drawing.Size(760, 22);
            this.barra_estado.TabIndex = 1;
            this.barra_estado.Text = "statusStrip1";
            // 
            // barStInfoServ
            // 
            this.barStInfoServ.ForeColor = System.Drawing.Color.Green;
            this.barStInfoServ.Name = "barStInfoServ";
            this.barStInfoServ.Size = new System.Drawing.Size(42, 17);
            this.barStInfoServ.Text = "Estado";
            // 
            // barStStatus
            // 
            this.barStStatus.Name = "barStStatus";
            this.barStStatus.Size = new System.Drawing.Size(175, 16);
            // 
            // barStSong
            // 
            this.barStSong.Name = "barStSong";
            this.barStSong.Size = new System.Drawing.Size(75, 17);
            this.barStSong.Text = "playing.song";
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 457);
            this.Controls.Add(this.barra_estado);
            this.Controls.Add(this.tabControl1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Inicio";
            this.Text = "HMPro";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Inicio_FormClosing);
            this.Load += new System.EventHandler(this.Inicio_Load);
            this.tabControl1.ResumeLayout(false);
            this.Música.ResumeLayout(false);
            this.Música.PerformLayout();
            this.Controles.ResumeLayout(false);
            this.groupVolumen.ResumeLayout(false);
            this.groupVolumen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMsg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPubli)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMusica)).EndInit();
            this.groupHorario.ResumeLayout(false);
            this.groupHorario.PerformLayout();
            this.Configuración.ResumeLayout(false);
            this.Configuración.PerformLayout();
            this.groupBoxDom.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorServer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProxy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorAddDom)).EndInit();
            this.barra_estado.ResumeLayout(false);
            this.barra_estado.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Música;
        private System.Windows.Forms.TabPage Controles;
        private System.Windows.Forms.TabPage Configuración;
        private System.Windows.Forms.OpenFileDialog openMsgInst;
        private System.Windows.Forms.TextBox msgIns;
        private System.Windows.Forms.Button sendMsgInst;
        private System.Windows.Forms.Label lblMsgInst;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mostrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem SalirToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyBarIcon;
        private System.Windows.Forms.FolderBrowserDialog openMusicDirs;
        private System.Windows.Forms.Label lblMusicDirs;
        private System.Windows.Forms.TextBox musicDirs;
        private System.Windows.Forms.Label lblListDirMusic;
        private System.Windows.Forms.CheckedListBox listMusicDirs;
        private System.Windows.Forms.GroupBox groupHorario;
        private System.Windows.Forms.DateTimePicker timeDesde;
        private System.Windows.Forms.Label lblHasta;
        private System.Windows.Forms.Label lblDesde;
        private System.Windows.Forms.DateTimePicker timeHasta;
        private System.Windows.Forms.GroupBox groupVolumen;
        private System.Windows.Forms.Label lblTrackMusic;
        private System.Windows.Forms.TrackBar trackBarMsg;
        private System.Windows.Forms.TrackBar trackBarPubli;
        private System.Windows.Forms.TrackBar trackBarMusica;
        private System.Windows.Forms.Label lblTrackMsg;
        private System.Windows.Forms.Label lblTrackPubli;
        private System.Windows.Forms.Label lblMusicContainer;
        private System.Windows.Forms.Label lblMsgContainer;
        private System.Windows.Forms.Label lblPubliContainer;
        private System.Windows.Forms.GroupBox groupBoxDom;
        private System.Windows.Forms.Button btnAddDom;
        private System.Windows.Forms.ComboBox domTienda;
        private System.Windows.Forms.ComboBox domProv;
        private System.Windows.Forms.ComboBox domRegion;
        private System.Windows.Forms.ComboBox domPais;
        private System.Windows.Forms.ComboBox domAlmacen;
        private System.Windows.Forms.ComboBox domEntidad;
        private System.Windows.Forms.Button btnBorrarDom;
        private System.Windows.Forms.ListBox listBoxDom;
        private System.Windows.Forms.Label lblProxy;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Button btnSendProxy;
        private System.Windows.Forms.Button btnSendServer;
        private System.Windows.Forms.TextBox textProxy;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.ErrorProvider errorServer;
        private System.Windows.Forms.ErrorProvider errorProxy;
        private System.Windows.Forms.Timer Timer5MIN;
        private System.Windows.Forms.ErrorProvider errorAddDom;
        private System.Windows.Forms.Timer Timer1MIN;
        private AudioDjStudio.AudioDjStudio playerMusic;
        private System.Windows.Forms.Timer tPlayer;
        private AudioDjStudio.AudioDjStudio playerInsta;
        private AudioDjStudio.AudioDjStudio playerMsgAuto;
        private System.Windows.Forms.StatusStrip barra_estado;
        private System.Windows.Forms.ToolStripStatusLabel barStInfoServ;
        private System.Windows.Forms.ToolStripProgressBar barStStatus;
        private System.Windows.Forms.ToolStripStatusLabel barStSong;
    }
}

