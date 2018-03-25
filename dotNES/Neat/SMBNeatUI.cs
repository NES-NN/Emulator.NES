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
        private SMBNeatInstance[] _smbNeatInstances = new SMBNeatInstance[Instances];
        private int _currentInstance = 0;

        private static int refreshTime = 16;
        private string _rom;

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

        public SMBNeatUI()
        {
            InitializeComponent();
            for (int i = 0; i < Instances; i++)
            {
                _smbNeatInstances[i] = null;
                InstanceList.Items.Add(i);
            }
            InstanceList.SelectedIndex = _currentInstance;
        }

        private void SMBNeat_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = (refreshTime);
            timer.Tick += new EventHandler(refresh_Tick);
            timer.Start();
        }

        private void SMBNeat_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_smbNeatInstances[_currentInstance] != null)
            {
                _smbNeatInstances[_currentInstance].GameInstanceRunning = false;
                //_gameThread?.Abort(); Need to fix this
            }
        }

        private void refresh_Tick(object sender, EventArgs e)
        {
            if (_smbNeatInstances[_currentInstance] != null)
                if (_smbNeatInstances[_currentInstance].GameInstanceRunning)
                    RefreshGameUIStats();
        }

        private void RefreshGameUIStats()
        {
            // Update Game Statistics

            Dictionary<String, int> gameStats = _smbNeatInstances[_currentInstance].SMB.GameStats;

            score.Text = Convert.ToString(gameStats["score"]);
            lives.Text = Convert.ToString(gameStats["lives"]);
            coins.Text = Convert.ToString(gameStats["coins"]);
            world.Text = Convert.ToString(gameStats["world"]);
            level.Text = Convert.ToString(gameStats["level"]);
            time.Text = Convert.ToString(gameStats["time"]);

            // Update Player Statistics

            Dictionary<String, int> playerStats = _smbNeatInstances[_currentInstance].SMB.PlayerStats;

            levelX.Text = Convert.ToString(playerStats["x"]);
            levelY.Text = Convert.ToString(playerStats["y"]);

            screenX.Text = Convert.ToString(playerStats["screenX"]);
            screenY.Text = Convert.ToString(playerStats["screenY"]);

            String value;
            state.Text = playerState.TryGetValue(
                playerStats["state"], out value) ? value : "-----";

            floatState.Text = playerFloatState.TryGetValue(
                playerStats["floatState"], out value) ? value : "-----";

            powerUP.Text = (playerStats["powerUP"] == 0) ? "Small" :            //This memory address jumps around all over
                             (playerStats["powerUP"] == 1) ? "Big" : "Fiery";   //the place, this was the safest way to do it

            direction.Text = (playerStats["direction"] == 1) ? "Right" :
                             (playerStats["direction"] == 2) ? "Left" : "";

            // Draw inputs to UI

            pictureBoxInputs.Image = new Bitmap(170, 170);
            Graphics m = Graphics.FromImage(pictureBoxInputs.Image);
            Pen gridPen = new Pen(Color.LightGray, 0.5F);

            m.DrawRectangle(gridPen, new Rectangle(0, 0, 169, 169));
            m.FillRectangle(Brushes.Red, new Rectangle(78, 78, 13, 13));

            int[] inputs = _smbNeatInstances[_currentInstance].SMB.Inputs;

            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i] == 0)
                    m.DrawRectangle(gridPen, new Rectangle((i % 13) * 13, (i / 13) * 13, 13, 13));
                if (inputs[i] == -1)
                    m.FillRectangle(Brushes.Green, new Rectangle((i % 13) * 13, (i / 13) * 13, 13, 13));
            }
        }

        // --- UI Actions

        private void InstanceList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentInstance = Convert.ToInt32(InstanceList.GetItemText(InstanceList.SelectedItem));
        }

        private void ButtonLoadRom_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = "nes"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _rom = dialog.FileName;
                labelRomName.Text = dialog.SafeFileName;
                output.Text += "Loaded Rom : " + _rom + "\n";
            }
        }

        private void ButtonStartTraining_Click(object sender, EventArgs e)
        {
            //Start Training
        }

        private void ButtonStartRom_Click(object sender, EventArgs e)
        {
            if (_smbNeatInstances[_currentInstance] != null)
                if (_smbNeatInstances[_currentInstance].GameInstanceRunning)
                {
                    _smbNeatInstances[_currentInstance].Suspended = true;
                    ButtonStartRom.Text = "Play";
                }
                else
                {
                    _smbNeatInstances[_currentInstance].Suspended = false;
                    ButtonStartRom.Text = "Pause";
                }
            else
            {
                _smbNeatInstances[_currentInstance] = new SMBNeatInstance(checkBoxShowUI.Checked, _rom);
                ButtonStartRom.Text = "Pause";
            }
            checkBoxShowUI.Enabled = false;
        }

        private void ButtonSaveState_Click(object sender, EventArgs e)
        {
            _smbNeatInstances[_currentInstance].SaveState();
        }
        
        private void ButtonLoadState_Click(object sender, EventArgs e)
        {
            _smbNeatInstances[_currentInstance] = new SMBNeatInstance();
            _smbNeatInstances[_currentInstance].LoadState(checkBoxShowUI.Checked);
            checkBoxShowUI.Enabled = false;
        }
    }
}
