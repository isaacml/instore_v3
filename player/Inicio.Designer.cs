namespace player
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
            this.lblMsgInst = new System.Windows.Forms.Label();
            this.sendMsgInst = new System.Windows.Forms.Button();
            this.msgIns = new System.Windows.Forms.TextBox();
            this.Controles = new System.Windows.Forms.TabPage();
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
            this.tabControl1.SuspendLayout();
            this.Música.SuspendLayout();
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
            // lblMsgInst
            // 
            this.lblMsgInst.AutoSize = true;
            this.lblMsgInst.Location = new System.Drawing.Point(141, 341);
            this.lblMsgInst.Name = "lblMsgInst";
            this.lblMsgInst.Size = new System.Drawing.Size(102, 13);
            this.lblMsgInst.TabIndex = 2;
            this.lblMsgInst.Text = "Reproducir Mensaje";
            // 
            // sendMsgInst
            // 
            this.sendMsgInst.Location = new System.Drawing.Point(510, 354);
            this.sendMsgInst.Name = "sendMsgInst";
            this.sendMsgInst.Size = new System.Drawing.Size(75, 23);
            this.sendMsgInst.TabIndex = 1;
            this.sendMsgInst.Text = "Enviar";
            this.sendMsgInst.UseVisualStyleBackColor = true;
            // 
            // msgIns
            // 
            this.msgIns.Location = new System.Drawing.Point(144, 357);
            this.msgIns.Name = "msgIns";
            this.msgIns.Size = new System.Drawing.Size(360, 20);
            this.msgIns.TabIndex = 0;
            this.msgIns.Enter += new System.EventHandler(this.msgIns_Enter);
            // 
            // Controles
            // 
            this.Controles.Location = new System.Drawing.Point(4, 22);
            this.Controles.Name = "Controles";
            this.Controles.Padding = new System.Windows.Forms.Padding(3);
            this.Controles.Size = new System.Drawing.Size(752, 431);
            this.Controles.TabIndex = 1;
            this.Controles.Text = "Controles";
            this.Controles.ToolTipText = "Tienda";
            this.Controles.UseVisualStyleBackColor = true;
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
    }
}

