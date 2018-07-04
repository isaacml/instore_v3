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
        private bool enable_salir = false;

        public Inicio()
        {
            InitializeComponent();
        }

        private void msgIns_Enter(object sender, EventArgs e)
        {
            if (openMsgInst.ShowDialog() == DialogResult.OK)
            {
                msgIns.Text = openMsgInst.FileName;
            }
        }

        private void Inicio_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!enable_salir)
            {
                e.Cancel = true;
                this.Hide();
                notifyBarIcon.ShowBalloonTip(100, "HMPro", "Ejecución en 2º Plano", ToolTipIcon.Info);
            }
        }

        private void mostrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void SalirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void notifyBarIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }
    }
}
