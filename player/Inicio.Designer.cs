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
            this.btnEnviarIni = new System.Windows.Forms.Button();
            this.txtPassIni = new System.Windows.Forms.TextBox();
            this.lblPassIni = new System.Windows.Forms.Label();
            this.txtUserIni = new System.Windows.Forms.TextBox();
            this.lblUserIni = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnEnviarIni
            // 
            this.btnEnviarIni.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEnviarIni.Location = new System.Drawing.Point(377, 294);
            this.btnEnviarIni.Name = "btnEnviarIni";
            this.btnEnviarIni.Size = new System.Drawing.Size(76, 29);
            this.btnEnviarIni.TabIndex = 4;
            this.btnEnviarIni.Text = "Enviar";
            this.btnEnviarIni.UseVisualStyleBackColor = true;
            this.btnEnviarIni.Click += new System.EventHandler(this.btnEnviarIni_Click);
            // 
            // txtPassIni
            // 
            this.txtPassIni.Location = new System.Drawing.Point(336, 238);
            this.txtPassIni.Name = "txtPassIni";
            this.txtPassIni.PasswordChar = '*';
            this.txtPassIni.Size = new System.Drawing.Size(154, 20);
            this.txtPassIni.TabIndex = 3;
            // 
            // lblPassIni
            // 
            this.lblPassIni.AutoSize = true;
            this.lblPassIni.Location = new System.Drawing.Point(333, 208);
            this.lblPassIni.Name = "lblPassIni";
            this.lblPassIni.Size = new System.Drawing.Size(61, 13);
            this.lblPassIni.TabIndex = 2;
            this.lblPassIni.Text = "Contraseña";
            // 
            // txtUserIni
            // 
            this.txtUserIni.Location = new System.Drawing.Point(336, 162);
            this.txtUserIni.Name = "txtUserIni";
            this.txtUserIni.Size = new System.Drawing.Size(154, 20);
            this.txtUserIni.TabIndex = 1;
            // 
            // lblUserIni
            // 
            this.lblUserIni.AutoSize = true;
            this.lblUserIni.Location = new System.Drawing.Point(333, 132);
            this.lblUserIni.Name = "lblUserIni";
            this.lblUserIni.Size = new System.Drawing.Size(98, 13);
            this.lblUserIni.TabIndex = 0;
            this.lblUserIni.Text = "Nombre de Usuario";
            // 
            // Inicio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblUserIni);
            this.Controls.Add(this.txtUserIni);
            this.Controls.Add(this.lblPassIni);
            this.Controls.Add(this.txtPassIni);
            this.Controls.Add(this.btnEnviarIni);
            this.Name = "Inicio";
            this.Text = "Panel de Inicio";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEnviarIni;
        private System.Windows.Forms.TextBox txtPassIni;
        private System.Windows.Forms.Label lblPassIni;
        private System.Windows.Forms.TextBox txtUserIni;
        private System.Windows.Forms.Label lblUserIni;
    }
}

