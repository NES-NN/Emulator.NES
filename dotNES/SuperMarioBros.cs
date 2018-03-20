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

        private Dictionary<String, int> gameStats = new Dictionary<String, int>()
        {
            { "score", 0 }, { "lives", 0 }, { "coins", 0 },
            { "world", 0 }, { "level", 0 }, { "time", 0 }
        };

        public SuperMarioBros(ref Emulator e)
        {
            InitializeComponent();
            emu = e;
        }

        private Dictionary<String, int> GameStats
        {
            get
            {
                updateGameStats();
                return gameStats;
            }
        }

        private int bcdToInt(uint startAddress, uint length)
        {
            int intVal = 0;
            for (uint i = startAddress + length, j = 1; i >= startAddress; i--, j *= 10)
                intVal += (int)(emu.CPU.AddressRead(i) * j);
            return intVal;
        }

        private void updateGameStats()
        {
            gameStats["score"] = bcdToInt(0x07DE, 5);
            gameStats["time"] = bcdToInt(0x07F8, 3) / 10;    //Need to fix bcdToInt...  

            gameStats["lives"] = (int)emu.CPU.AddressRead(0x075A);
            gameStats["coins"] = (int)emu.CPU.AddressRead(0x075E);
            gameStats["world"] = (int)emu.CPU.AddressRead(0x075F) + 1;
            gameStats["level"] = (int)emu.CPU.AddressRead(0x0760) + 1;
        }

        private void refresh_Tick(object sender, EventArgs e)
        {
            updateGameStats();

            score.Text = Convert.ToString(gameStats["score"]);
            lives.Text = Convert.ToString(gameStats["lives"]);
            coins.Text = Convert.ToString(gameStats["coins"]);
            world.Text = Convert.ToString(gameStats["world"]);
            level.Text = Convert.ToString(gameStats["level"]);
            time.Text = Convert.ToString(gameStats["time"]);
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
