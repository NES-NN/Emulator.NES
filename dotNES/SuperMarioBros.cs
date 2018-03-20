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
        private static int refreshTime = 16;

        private int[] inputs = new int[256];
        private int[,] enemy = new int[5,2];

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

        public int[] Inputs
        {
            get { updateInputs(); return inputs; }
        }

        public Dictionary<String, int> GameStats
        {
            get { updateGameStats(); return gameStats; }
        }

        public Dictionary<String, int> PlaterStats
        {
            get { updatePlaterStats(); return platerStats; }
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

        private int getTile(int dx, int dy)
        {
            int x = platerStats["x"] + dx + 8;
            int y = platerStats["y"] + dy;
            int page = (x / 256) % 2;

            int subx = (x % 256) / 16;
            int suby = (y - 32) / 16;

            uint addr = Convert.ToUInt32(0x500 + page * 13 * 16 + suby * 16 + subx);

            if (suby >= 13 || suby < 0)
                return 0;

            if (emu.CPU.AddressRead(addr) == 0)
                return 1;
            else
                return 0;
        }

        private void updateInputs()
        {
            updateEnemies();
            int i = 0, e = 0;
            for (int dy = -128; dy < 128; dy += 16)
                for (int dx = -128; dx < 128; dx += 16)
                {
                    inputs[i] = 0;

                    if (getTile(dx, dy) == 1 && platerStats["y"] + dy < 0x1B0)
                        inputs[i] = 1;

                    for (int j = e; j < 5; j++)
                        if (enemy[j, 0] - (platerStats["x"] + dx) <= 8 && enemy[j, 1] - (platerStats["y"] + dy) <= 8 &&
                            enemy[j, 0] != 0 && enemy[j, 1] != 0)
                        {
                            inputs[i] = -1;
                            e++;
                        }
                    i++;
                }
        }

        private void updateEnemies()
        {
            for (uint i = 0; i < 5; i++)
            {
                if ((Convert.ToBoolean(emu.CPU.AddressRead(0xF + i))))
                {
                    enemy[i, 0] = (int)(emu.CPU.AddressRead(0x6E + i) * 0x100 + emu.CPU.AddressRead(0x87 + i));
                    enemy[i, 1] = (int)emu.CPU.AddressRead(0xCF + i) + 12;
                }
                else
                {
                    enemy[i, 0] = 0;
                    enemy[i, 1] = 0;
                }
            }
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

            updateInputs();

            pictureBoxInputs.Image = new Bitmap(162,162);
            Graphics m = Graphics.FromImage(pictureBoxInputs.Image);
            Pen gridPen = new Pen(Color.LightGray, 0.5F);

            m.DrawRectangle(gridPen, new Rectangle(0, 0, 161, 161));

            m.FillRectangle(Brushes.Red, new Rectangle(80, 80, 10, 10));

            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i] == 0)
                    m.DrawRectangle(gridPen, new Rectangle((i % 16) * 10, (i / 16) * 10, 10, 10));
                if (inputs[i] == -1)
                    m.FillRectangle(Brushes.Green, new Rectangle((i % 16) * 10, (i / 16) * 10, 10, 10));
            }
        }

        private void SuperMarioBros_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = (refreshTime);
            timer.Tick += new EventHandler(refresh_Tick);
            timer.Start();
        }
    }
}