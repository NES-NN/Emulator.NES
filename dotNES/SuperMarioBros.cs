using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotNES
{
    partial class SuperMarioBros : Form
    {
        private Emulator emu;

        public SuperMarioBros(ref Emulator e)
        {
            InitializeComponent();
            emu = e;
        }

        private void refresh_Tick(object sender, EventArgs e)
        {

        }

        private void SuperMarioBros_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = (500);
            timer.Tick += new EventHandler(refresh_Tick);
            timer.Start();
        }
    }
}
