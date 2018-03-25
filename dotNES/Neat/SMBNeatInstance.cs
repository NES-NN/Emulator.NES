using System;
using System.IO;
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Generic;
using dotNES.Controllers;
using SharpNeat.EvolutionAlgorithms;
using SharpNeat.Genomes.Neat;

namespace dotNES.Neat
{
    class SMBNeatInstance
    {
        private IController _controller;
        private SMB _smbState;
        private Emulator _emulator;
        private UI _ui;
        private Thread _gameThread;

        public SMB SMB
        {
            get { return _smbState; }
        }

        private bool _gameInstanceRunning = false;
        private bool _suspended = false;

        public bool GameInstanceRunning
        {
            get => _gameInstanceRunning;
            set
            {
                if (_ui != null && _ui.Visible)
                    _ui.Close();
                _gameInstanceRunning = value;
            }
        }

        public bool Suspended { get => _suspended; set => _suspended = value; }

        public SMBNeatInstance() { }

        public SMBNeatInstance(bool withUI, string rom)
        {
            StartGameInstance(withUI, rom);
        }

        private void StartGameInstance(bool withUI, string rom)
        {
            _controller = new NES001Controller();
            _emulator = new Emulator(rom, _controller);

            StartGameThread(withUI);
            StartNeatEvolve();
        }

        public void SaveState()
        {
            _suspended = true;
            var dialog = new SaveFileDialog
            {
                DefaultExt = "bin",
                FileName = "emu"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, _emulator);
                stream.Close();
            }
            _suspended = false;
        }

        public void LoadState(bool withUI)
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = "bin",
                FileName = "emu"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadState_Manual(withUI, dialog.FileName);
            }
        }

        private void LoadState_Manual(bool withUI, string fileName)
        {
            IController c = new NES001Controller();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            _emulator = (Emulator)formatter.Deserialize(stream);
            _emulator.Controller = c; _controller = c;
            stream.Close();

            StartGameThread(withUI);
        }

        private void StartGameThread(bool withUI)
        {
            _smbState?.Stop();
            _smbState = new SMB(ref _emulator);
            _smbState.Start();

            if (withUI)
            {
                _ui?.Close();
                _ui = new UI(ref _emulator, ref _controller);
                _ui.Show();
                _gameInstanceRunning = true;
            }
            else
            {
                _gameInstanceRunning = false;
                _gameThread?.Abort();

                _gameThread = new Thread(() =>
                {
                    _gameInstanceRunning = true;
                    while (_gameInstanceRunning)
                    {
                        if (_suspended)
                        {
                            Thread.Sleep(100);
                            continue;
                        }
                        _emulator.PPU.ProcessFrame();
                    }
                });
                _gameThread.Start();
                _gameInstanceRunning = true;
            }
        }

        // --- NEAT Functions

        public delegate IController GetControllerDelegate();
        public delegate SMB GetSMBDelegate();
        public delegate void ResetStateDelegate();

        IController GetController() { return _emulator.Controller; }
        SMB GetSMB() { return _smbState; }
        void ResetState() { LoadState_Manual(false, "emu.bin"); }

        private NeatEvolutionAlgorithm<NeatGenome> _ea;

        private void StartNeatEvolve()
        {
            SMBExperiment experiment = new SMBExperiment(GetController, GetSMB, ResetState);

            XmlDocument xmlConfig = new XmlDocument();
            xmlConfig.Load("smb.config.xml");
            experiment.Initialize("Super Mario Bros", xmlConfig.DocumentElement);

            _ea = experiment.CreateEvolutionAlgorithm();
            _ea.UpdateEvent += new EventHandler(ea_UpdateEvent);
            _ea.StartContinue();
        }

        private void ea_UpdateEvent(object sender, EventArgs e)
        {
            Console.WriteLine(string.Format("gen={0:N0} bestFitness={1:N6}", _ea.CurrentGeneration, _ea.Statistics._maxFitness));
            var doc = NeatGenomeXmlIO.SaveComplete(new List<NeatGenome>() { _ea.CurrentChampGenome }, false);
            doc.Save("smb_champion.xml");
        }
    }
}
