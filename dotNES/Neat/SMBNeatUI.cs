using dotNES.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dotNES.Neat
{
    partial class SMBNeatUI : Form
    {
        private static int Instances = 1;
        private SMBNeatInstance[] SMBNeatInstance = new SMBNeatInstance[Instances];
        private int currentInstance = 0;

        private static int refreshTime = 16;
        private string rom;

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

        private int[] inputs;

        public SMBNeatUI()
        {
            InitializeComponent();
            for (int i = 0; i < Instances; i++)
            {
                SMBNeatInstance[i] = null;
                InstanceList.Items.Add(i);
            }
            InstanceList.SelectedIndex = currentInstance;
        }

        private void RefreshGameStats()
        {
            Dictionary<String, int> gameStats = SMBNeatInstance[currentInstance].smb.GameStats;

            score.Text = Convert.ToString(gameStats["score"]);
            lives.Text = Convert.ToString(gameStats["lives"]);
            coins.Text = Convert.ToString(gameStats["coins"]);
            world.Text = Convert.ToString(gameStats["world"]);
            level.Text = Convert.ToString(gameStats["level"]);
            time.Text = Convert.ToString(gameStats["time"]);

            Dictionary<String, int> platerStats = SMBNeatInstance[currentInstance].smb.PlayerStats;

            levelX.Text = Convert.ToString(platerStats["x"]);
            levelY.Text = Convert.ToString(platerStats["y"]);

            screenX.Text = Convert.ToString(platerStats["screenX"]);
            screenY.Text = Convert.ToString(platerStats["screenY"]);

            String value;
            state.Text = playerState.TryGetValue(
                platerStats["state"], out value) ? value : "-----";

            floatState.Text = playerFloatState.TryGetValue(
                platerStats["floatState"], out value) ? value : "-----";

            powerUP.Text = (platerStats["powerUP"] == 0) ? "Small" :            //This memory address jumps around all over
                             (platerStats["powerUP"] == 1) ? "Big" : "Fiery";   //the place, this was the safest way to do it

            direction.Text = (platerStats["direction"] == 1) ? "Right" :
                             (platerStats["direction"] == 2) ? "Left" : "";

            pictureBoxInputs.Image = new Bitmap(170, 170);
            Graphics m = Graphics.FromImage(pictureBoxInputs.Image);
            Pen gridPen = new Pen(Color.LightGray, 0.5F);

            m.DrawRectangle(gridPen, new Rectangle(0, 0, 169, 169));

            m.FillRectangle(Brushes.Red, new Rectangle(78, 78, 13, 13));


            inputs = SMBNeatInstance[currentInstance].smb.Inputs;
            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i] == 0)
                    m.DrawRectangle(gridPen, new Rectangle((i % 13) * 13, (i / 13) * 13, 13, 13));
                if (inputs[i] == -1)
                    m.FillRectangle(Brushes.Green, new Rectangle((i % 13) * 13, (i / 13) * 13, 13, 13));
            }
        }

        private void refresh_Tick(object sender, EventArgs e)
        {
            if (SMBNeatInstance[currentInstance] != null)
                if(SMBNeatInstance[currentInstance].GameInstanceRunning)
                    RefreshGameStats();
        }
        
        private void SMBNeat_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = (refreshTime);
            timer.Tick += new EventHandler(refresh_Tick);
            timer.Start();
        }

        private void buttonLoadRom_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = "nes"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                rom = dialog.FileName;
                labelRomName.Text = dialog.SafeFileName;
                output.Text += "Loaded Rom : " + rom + "\n";
            }
        }

        private void SMBNeat_FormClosing(object sender, FormClosingEventArgs e)
        {
            SMBNeatInstance[currentInstance].GameInstanceRunning = false;
            //_gameThread?.Abort(); Need to fix this
        }

        private void ButtonStartTraining_Click(object sender, EventArgs e)
        {
            if (SMBNeatInstance[currentInstance] != null)
                if (SMBNeatInstance[currentInstance].GameInstanceRunning)
                {
                    SMBNeatInstance[currentInstance].Suspended = true;
                    ButtonStartTraining.Text = "Play";
                }
                else
                {
                    SMBNeatInstance[currentInstance].Suspended = false;
                    ButtonStartTraining.Text = "Pause";
                }
            else
            {
                SMBNeatInstance[currentInstance] = new SMBNeatInstance(checkBoxShowUI.Checked, rom);
                ButtonStartTraining.Text = "Pause";
            }
            checkBoxShowUI.Enabled = false;
        }

        private void ButtonSaveState_Click(object sender, EventArgs e)
        {
            SMBNeatInstance[currentInstance].SaveState();
        }

        private void InstanceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentInstance = Convert.ToInt32(InstanceList.GetItemText(InstanceList.SelectedItem));
        }

        private void ButtonLoadState_Click(object sender, EventArgs e)
        {
            SMBNeatInstance[currentInstance] = new SMBNeatInstance();
            SMBNeatInstance[currentInstance].LoadState(checkBoxShowUI.Checked);
            checkBoxShowUI.Enabled = false;
        }
    }
}
