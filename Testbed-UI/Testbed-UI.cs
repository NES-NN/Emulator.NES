using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Testbed_UI
{
    public partial class testbedUI : Form
    {
        public testbedUI()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void playBestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayBestUI playBestUI = new PlayBestUI();

            playBestUI.Show();
        }

        private void testConfigurationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Get windows to display what the NN is doing
            PlayBestUI playBestUI = new PlayBestUI();
            SMBStats sMBStats = new SMBStats();

            //Adjust it name
            playBestUI.Text = "Trainging - NN name here";

            playBestUI.Show();
            sMBStats.Show();
        }
    }
}
