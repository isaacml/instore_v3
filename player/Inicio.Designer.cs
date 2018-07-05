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
            this.lblListDirMusic = new System.Windows.Forms.Label();
            this.listMusicDirs = new System.Windows.Forms.CheckedListBox();
            this.lblMusicDirs = new System.Windows.Forms.Label();
            this.musicDirs = new System.Windows.Forms.TextBox();
            this.lblMsgInst = new System.Windows.Forms.Label();
            this.sendMsgInst = new System.Windows.Forms.Button();
            this.msgIns = new System.Windows.Forms.TextBox();
            this.Controles = new System.Windows.Forms.TabPage();
            this.groupVolumen = new System.Windows.Forms.GroupBox();
            this.lblFadeContainer = new System.Windows.Forms.Label();
            this.lblMsgContainer = new System.Windows.Forms.Label();
            this.lblPubliContainer = new System.Windows.Forms.Label();
            this.lblMusicContainer = new System.Windows.Forms.Label();
            this.lblTrackFade = new System.Windows.Forms.Label();
            this.lblTrackMsg = new System.Windows.Forms.Label();
            this.lblTrackPubli = new System.Windows.Forms.Label();
            this.lblTrackMusic = new System.Windows.Forms.Label();
            this.trackBarFade = new System.Windows.Forms.TrackBar();
            this.trackBarMsg = new System.Windows.Forms.TrackBar();
            this.trackBarPubli = new System.Windows.Forms.TrackBar();
            this.trackBarMusica = new System.Windows.Forms.TrackBar();
            this.groupHorario = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.lblHasta = new System.Windows.Forms.Label();
            this.lblDesde = new System.Windows.Forms.Label();
            this.timeDesde = new System.Windows.Forms.DateTimePicker();
            this.Configuración = new System.Windows.Forms.TabPage();
            this.barra_estado = new System.Windows.Forms.StatusStrip();
            this.barStInfoServ = new System.Windows.Forms.ToolStripStatusLabel();
            this.barStStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.barStSong = new System.Windows.Forms.ToolStripStatusLabel();
            this.openMsgInst = new System.Windows.Forms.OpenFileDialog();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mostrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SalirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyBarIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.openMusicDirs = new System.Windows.Forms.FolderBrowserDialog();
            this.tabControl1.SuspendLayout();
            this.Música.SuspendLayout();
            this.Controles.SuspendLayout();
            this.groupVolumen.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFade)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMsg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPubli)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMusica)).BeginInit();
            this.groupHorario.SuspendLayout();
            this.barra_estado.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
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
            this.listMusicDirs.Items.AddRange(new object[] {
            "Jazz",
            "Regeton",
            "Rock",
            "Dance",
            "Hip Hop"});
            this.listMusicDirs.Location = new System.Drawing.Point(210, 136);
            this.listMusicDirs.Name = "listMusicDirs";
            this.listMusicDirs.Size = new System.Drawing.Size(343, 184);
            this.listMusicDirs.TabIndex = 4;
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
            this.musicDirs.Size = new System.Drawing.Size(262, 20);
            this.musicDirs.TabIndex = 2;
            this.musicDirs.Text = "C:";
            this.musicDirs.Enter += new System.EventHandler(this.musicDirs_Enter);
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
            this.sendMsgInst.Location = new System.Drawing.Point(478, 348);
            this.sendMsgInst.Name = "sendMsgInst";
            this.sendMsgInst.Size = new System.Drawing.Size(75, 23);
            this.sendMsgInst.TabIndex = 7;
            this.sendMsgInst.Text = "Enviar";
            this.sendMsgInst.UseVisualStyleBackColor = true;
            // 
            // msgIns
            // 
            this.msgIns.Location = new System.Drawing.Point(210, 351);
            this.msgIns.Name = "msgIns";
            this.msgIns.Size = new System.Drawing.Size(262, 20);
            this.msgIns.TabIndex = 6;
            this.msgIns.Enter += new System.EventHandler(this.msgIns_Enter);
            // 
            // Controles
            // 
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
            // groupVolumen
            // 
            this.groupVolumen.Controls.Add(this.lblFadeContainer);
            this.groupVolumen.Controls.Add(this.lblMsgContainer);
            this.groupVolumen.Controls.Add(this.lblPubliContainer);
            this.groupVolumen.Controls.Add(this.lblMusicContainer);
            this.groupVolumen.Controls.Add(this.lblTrackFade);
            this.groupVolumen.Controls.Add(this.lblTrackMsg);
            this.groupVolumen.Controls.Add(this.lblTrackPubli);
            this.groupVolumen.Controls.Add(this.lblTrackMusic);
            this.groupVolumen.Controls.Add(this.trackBarFade);
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
            // lblFadeContainer
            // 
            this.lblFadeContainer.AutoSize = true;
            this.lblFadeContainer.Location = new System.Drawing.Point(308, 215);
            this.lblFadeContainer.Name = "lblFadeContainer";
            this.lblFadeContainer.Size = new System.Drawing.Size(19, 13);
            this.lblFadeContainer.TabIndex = 9;
            this.lblFadeContainer.Text = "10";
            // 
            // lblMsgContainer
            // 
            this.lblMsgContainer.AutoSize = true;
            this.lblMsgContainer.Location = new System.Drawing.Point(225, 215);
            this.lblMsgContainer.Name = "lblMsgContainer";
            this.lblMsgContainer.Size = new System.Drawing.Size(19, 13);
            this.lblMsgContainer.TabIndex = 9;
            this.lblMsgContainer.Text = "10";
            // 
            // lblPubliContainer
            // 
            this.lblPubliContainer.AutoSize = true;
            this.lblPubliContainer.Location = new System.Drawing.Point(149, 215);
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
            this.lblMusicContainer.Size = new System.Drawing.Size(19, 13);
            this.lblMusicContainer.TabIndex = 7;
            this.lblMusicContainer.Text = "30";
            // 
            // lblTrackFade
            // 
            this.lblTrackFade.AutoSize = true;
            this.lblTrackFade.Location = new System.Drawing.Point(291, 28);
            this.lblTrackFade.Name = "lblTrackFade";
            this.lblTrackFade.Size = new System.Drawing.Size(51, 13);
            this.lblTrackFade.TabIndex = 0;
            this.lblTrackFade.Text = "Fade Out";
            // 
            // lblTrackMsg
            // 
            this.lblTrackMsg.AutoSize = true;
            this.lblTrackMsg.Location = new System.Drawing.Point(210, 28);
            this.lblTrackMsg.Name = "lblTrackMsg";
            this.lblTrackMsg.Size = new System.Drawing.Size(52, 13);
            this.lblTrackMsg.TabIndex = 0;
            this.lblTrackMsg.Text = "Mensajes";
            // 
            // lblTrackPubli
            // 
            this.lblTrackPubli.AutoSize = true;
            this.lblTrackPubli.Location = new System.Drawing.Point(134, 28);
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
            // trackBarFade
            // 
            this.trackBarFade.Location = new System.Drawing.Point(294, 44);
            this.trackBarFade.Maximum = 100;
            this.trackBarFade.Name = "trackBarFade";
            this.trackBarFade.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarFade.Size = new System.Drawing.Size(45, 168);
            this.trackBarFade.TabIndex = 6;
            this.trackBarFade.TickFrequency = 10;
            this.trackBarFade.Value = 10;
            this.trackBarFade.Scroll += new System.EventHandler(this.trackBarFade_Scroll);
            // 
            // trackBarMsg
            // 
            this.trackBarMsg.Location = new System.Drawing.Point(213, 44);
            this.trackBarMsg.Maximum = 100;
            this.trackBarMsg.Name = "trackBarMsg";
            this.trackBarMsg.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarMsg.Size = new System.Drawing.Size(45, 168);
            this.trackBarMsg.TabIndex = 5;
            this.trackBarMsg.TickFrequency = 10;
            this.trackBarMsg.Value = 10;
            this.trackBarMsg.Scroll += new System.EventHandler(this.trackBarMsg_Scroll);
            // 
            // trackBarPubli
            // 
            this.trackBarPubli.Location = new System.Drawing.Point(137, 44);
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
            this.trackBarMusica.Value = 30;
            this.trackBarMusica.Scroll += new System.EventHandler(this.trackBarMusica_Scroll);
            // 
            // groupHorario
            // 
            this.groupHorario.Controls.Add(this.dateTimePicker1);
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
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "HH:mm";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(264, 35);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(64, 20);
            this.dateTimePicker1.TabIndex = 2;
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
            this.timeDesde.Value = new System.DateTime(2018, 7, 5, 19, 56, 0, 0);
            // 
            // Configuración
            // 
            this.Configuración.Location = new System.Drawing.Point(4, 22);
            this.Configuración.Name = "Configuración";
            this.Configuración.Size = new System.Drawing.Size(752, 431);
            this.Configuración.TabIndex = 2;
            this.Configuración.Text = "Configuración";
            this.Configuración.ToolTipText = "Configuración";
            this.Configuración.UseVisualStyleBackColor = true;
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
            this.barra_estado.Text = "barra_estado";
            // 
            // barStInfoServ
            // 
            this.barStInfoServ.ForeColor = System.Drawing.Color.Green;
            this.barStInfoServ.Name = "barStInfoServ";
            this.barStInfoServ.Size = new System.Drawing.Size(103, 17);
            this.barStInfoServ.Text = "Estado Conectado";
            // 
            // barStStatus
            // 
            this.barStStatus.Name = "barStStatus";
            this.barStStatus.Size = new System.Drawing.Size(100, 16);
            this.barStStatus.Value = 50;
            // 
            // barStSong
            // 
            this.barStSong.Name = "barStSong";
            this.barStSong.Size = new System.Drawing.Size(113, 17);
            this.barStSong.Text = "American Love.mp3";
            // 
            // openMsgInst
            // 
            this.openMsgInst.Filter = "Archivos MP3|*.mp3";
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
            this.tabControl1.ResumeLayout(false);
            this.Música.ResumeLayout(false);
            this.Música.PerformLayout();
            this.Controles.ResumeLayout(false);
            this.groupVolumen.ResumeLayout(false);
            this.groupVolumen.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarFade)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMsg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarPubli)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarMusica)).EndInit();
            this.groupHorario.ResumeLayout(false);
            this.groupHorario.PerformLayout();
            this.barra_estado.ResumeLayout(false);
            this.barra_estado.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Música;
        private System.Windows.Forms.TabPage Controles;
        private System.Windows.Forms.TabPage Configuración;
        private System.Windows.Forms.StatusStrip barra_estado;
        private System.Windows.Forms.ToolStripStatusLabel barStInfoServ;
        private System.Windows.Forms.ToolStripProgressBar barStStatus;
        private System.Windows.Forms.ToolStripStatusLabel barStSong;
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
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox groupVolumen;
        private System.Windows.Forms.Label lblTrackMusic;
        private System.Windows.Forms.TrackBar trackBarFade;
        private System.Windows.Forms.TrackBar trackBarMsg;
        private System.Windows.Forms.TrackBar trackBarPubli;
        private System.Windows.Forms.TrackBar trackBarMusica;
        private System.Windows.Forms.Label lblTrackFade;
        private System.Windows.Forms.Label lblTrackMsg;
        private System.Windows.Forms.Label lblTrackPubli;
        private System.Windows.Forms.Label lblMusicContainer;
        private System.Windows.Forms.Label lblFadeContainer;
        private System.Windows.Forms.Label lblMsgContainer;
        private System.Windows.Forms.Label lblPubliContainer;
    }
}

