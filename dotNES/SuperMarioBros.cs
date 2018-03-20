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

        private Dictionary<int, String>
            playerState = new Dictionary<int, String>()
            {
                { 0x00, "Leftmost of screen" },
                { 0x01, "Climbing vine" },
                { 0x02, "Entering reversed-L pipe" },
                { 0x03, "Going down a pipe" },
                { 0x04, "Autowalk" },
                { 0x05, "Autowalk" },
                { 0x06, "Player dies" },
                { 0x07, "Entering area" },
                { 0x08, "Normal" },
                { 0x09, "Cannot move" },
                { 0x0A, "Dying" },
                { 0x0B, "Palette cycling, can't move" },
                { 0x0C, "transission to Fiery" }
            },
            playerFloatState = new Dictionary<int, String>()
            {
                { 0x00, "Standing on solid/else" },
                { 0x01, "Airborn by jumping" },
                { 0x02, "Airborn by walking of a ledge" },
                { 0x03, "Sliding down flagpole" }
            };

        private Dictionary<String, int> 
            gameStats = new Dictionary<String, int>()
            {
                { "score", 0 }, { "lives", 0 }, { "coins", 0 },
                { "world", 0 }, { "level", 0 }, { "time", 0 }
            },
            platerStats = new Dictionary<String, int>()
            {
                { "x", 0 }, { "screenX", 0 }, { "state", 0 },
                { "y", 0 }, { "screenY", 0 }, { "floatState", 0 },
                { "powerUP", 0 }, { "direction", 0 }
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

        private Dictionary<String, int> PlaterStats
        {
            get
            {
                updatePlaterStats();
                return platerStats;
            }
        }

        private void updatePlaterStats()
        {
            platerStats["x"] = (int)(emu.CPU.AddressRead(0x6D) * 0x100 + emu.CPU.AddressRead(0x86));
            platerStats["y"] = (int)(emu.CPU.AddressRead(0x03B8) + 16);

            platerStats["screenX"] = (int)emu.CPU.AddressRead(0x03AD);
            platerStats["screenY"] = (int)emu.CPU.AddressRead(0x03B8);

            platerStats["state"]      = (int)emu.CPU.AddressRead(0x000E);
            platerStats["floatState"] = (int)emu.CPU.AddressRead(0x001D);

            platerStats["powerUP"]    = (int)emu.CPU.AddressRead(0x0756);
            platerStats["direction"]  = (int)emu.CPU.AddressRead(0x0003);
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
            time.Text  = Convert.ToString(gameStats["time"]);

            updatePlaterStats(); String value;

            levelX.Text = Convert.ToString(platerStats["x"]);
            levelY.Text = Convert.ToString(platerStats["y"]);

            screenX.Text = Convert.ToString(platerStats["screenX"]);
            screenY.Text = Convert.ToString(platerStats["screenY"]);

            state.Text = playerState.TryGetValue(
                platerStats["state"], out value) ? value : "-----";

            floatState.Text = playerFloatState.TryGetValue(
                platerStats["floatState"], out value) ? value : "-----";

            powerUP.Text   = (platerStats["powerUP"] == 0) ? "Small" :          //This memory address jumps around all over
                             (platerStats["powerUP"] == 1) ? "Big" : "Fiery";   //the place, this was the safest way to do it

            direction.Text = (platerStats["direction"] == 1) ? "Right" :
                             (platerStats["direction"] == 2) ? "Left" : "";

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
