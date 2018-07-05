using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace player
{
    public partial class Inicio : Form
    {
        //Evalua la salida del programa
        private bool st_salida = false; 

        public Inicio()
        {
            InitializeComponent();
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
        
    }
}
