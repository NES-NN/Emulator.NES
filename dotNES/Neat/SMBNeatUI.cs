using dotNES.Controllers;
using SharpNeat.EvolutionAlgorithms;
using SharpNeat.Genomes.Neat;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace dotNES.Neat
{
    partial class SMBNeatUI : Form
    {
        private static int Instances = 2;
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

        public void UpdateInstance (int index, ref SMBNeatInstance instance)
        {
            _smbNeatInstances[index] = instance;
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
            if (_smbNeatInstances[_currentInstance] == null)
            {
                _smbNeatInstances[_currentInstance] = new SMBNeatInstance(checkBoxShowUI.Checked, _rom);
                ButtonStartRom.Text = "Pause";
            }

            _smbNeatInstances[_currentInstance].LoadState(checkBoxShowUI.Checked);
            checkBoxShowUI.Enabled = false;
        }

        public void Log(string text)
        {
            Invoke(new Action(() =>
            {
                output.Focus();
                output.AppendText(text);
            }));
        }

        private void ButtonStartTraining_Click(object sender, EventArgs e)
        {
            //if (_smbNeatInstances[_currentInstance] == null)
            //{
            //    _smbNeatInstances[_currentInstance] = new SMBNeatInstance();
            //    ButtonStartRom.Text = "Pause";
            //}

            //_smbNeatInstances[_currentInstance].LoadState(checkBoxShowUI.Checked);
            //checkBoxShowUI.Enabled = false;
            StartTraining_Neat();
        }

        // --- NEAT Functions

        public delegate IController GetControllerDelegate();
        public delegate SMB GetSMBDelegate();
        public delegate void ResetStateDelegate();
        
        private NeatEvolutionAlgorithm<NeatGenome> _ea;

        public void StartTraining_Neat()
        {
            SMBExperiment experiment = new SMBExperiment(this);

            XmlDocument xmlConfig = new XmlDocument();
            xmlConfig.Load("smb.config.xml");
            experiment.Initialize("Super Mario Bros", xmlConfig.DocumentElement);

            _ea = experiment.CreateEvolutionAlgorithm();
            _ea.UpdateEvent += new EventHandler(ea_UpdateEvent);
            _ea.StartContinue();
        }

        private void ea_UpdateEvent(object sender, EventArgs e)
        {

            Log(string.Format("gen={0:N0} bestFitness={1:N6}", _ea.CurrentGeneration, _ea.Statistics._maxFitness) + "\n");
          
            var doc = NeatGenomeXmlIO.SaveComplete(new List<NeatGenome>() { _ea.CurrentChampGenome }, false);
            doc.Save("smb_champion.xml");
        }

        /// --Play Best 

        private void ButtonPlayBest_Click(object sender, EventArgs e)
        {
            PlayBest();
        }

        private void PlayBest()
        {
            NeatGenome genome = null;
            SMBExperiment _experiment = new SMBExperiment();

            // Load config XML.
            XmlDocument xmlConfig = new XmlDocument();
            xmlConfig.Load("smb.config.xml");
            _experiment.Initialize("Super Mario Bros", xmlConfig.DocumentElement);


            // Have the user choose the genome XML file.
            var dialog = new OpenFileDialog
            {
                DefaultExt = "xml",
                FileName = "smb_champion"
            };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                // Try to load the genome from the XML document.
                try
                {
                    using (XmlReader xr = XmlReader.Create(dialog.FileName))
                        genome = NeatGenomeXmlIO.ReadCompleteGenomeList(xr, false)[0];
                }
                catch (Exception e1)
                {
                    MessageBox.Show("Error loading genome from file!\nLoading aborted.\n" + e1.Message);
                    return;
                }
            }

            // Get a genome decoder that can convert genomes to phenomes.
            var genomeDecoder = _experiment.CreateGenomeDecoder();

            // Decode the genome into a phenome (neural network).
            var phenome = genomeDecoder.Decode(genome);


            IController _controller = new NES001Controller();

            SMBNeatInstance _SMBNeatInstance = new SMBNeatInstance(_controller);

            // Set the NEAT player's brain to the newly loaded neural network.
            SMBNeatPlayer _neatPlayer = new SMBNeatPlayer(phenome, ref _controller);


            _SMBNeatInstance.LoadState_Manual(true, "emu.bin");

            _SMBNeatInstance.SMB.UpdateStats();
            WaitNSeconds(1);

            while (_SMBNeatInstance.SMB.GameStats["lives"] >= 2)
            {
                WaitNSeconds(1);

                _neatPlayer.MakeMove(_SMBNeatInstance.SMB.Inputs);
                _SMBNeatInstance.SMB.UpdateStats();
            }
            _SMBNeatInstance.Stop();
        }
        
        private void WaitNSeconds(double seconds)
        {
            if (seconds < 1) return;
            DateTime _desired = DateTime.Now.AddSeconds(seconds);
            while (DateTime.Now < _desired)
            {
                System.Threading.Thread.Sleep(1);
                System.Windows.Forms.Application.DoEvents();
            }
        }
    }
}
