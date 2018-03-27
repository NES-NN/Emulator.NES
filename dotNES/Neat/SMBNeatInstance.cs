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
using System.Diagnostics;

namespace dotNES.Neat
{
    class SMBNeatInstance
    {
        public IController _controller;
        private SMB _SMB;
        private Emulator _emulator, _savedState;
        public UI _ui;
        private Thread _gameThread;
        private string _stateFileName = "emu.bin";
        private int activeSpeed = 1;

        public SMB SMB
        {
            get { return _SMB; }
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

        public SMBNeatInstance(IController controller)
        {
            _controller = controller;
        }

        public SMBNeatInstance(bool withUI, string rom)
        {
            StartGameInstance(withUI, rom);
        }

        private void StartGameInstance(bool withUI, string rom)
        {
            _controller = new NES001Controller();
            _emulator = new Emulator(rom, _controller);

            StartGameThread(withUI);
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
                // Store state fileName
                _stateFileName = dialog.FileName;

                // Load the state
                LoadState_Manual(withUI, _stateFileName);
            }
        }

        public void LoadState_Manual(bool withUI, string fileName)
        {
            //IController c = new NES001Controller();
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            _emulator = (Emulator)formatter.Deserialize(stream);
            _emulator.Controller = _controller; // = c; _controller = c;
            stream.Close();
            _savedState = _emulator;
            
            StartGameThread(withUI);
        }

        public IController RestartInstance()
        {
            IController c = new NES001Controller();
            _suspended = true;
            _emulator = _savedState;
            _emulator.Controller = c; _controller = c;

            return c;
        }

        internal void Stop()
        {
            _ui?.Close();
            _gameInstanceRunning = false;
            Thread.Sleep(1000);
            _gameThread?.Abort();
        }

        private void StartGameThread(bool withUI)
        {
            _SMB?.Stop();
            _SMB = new SMB(ref _emulator);
            _SMB.Start();

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

                    Stopwatch s = new Stopwatch();
                    Stopwatch s0 = new Stopwatch();
                    while (_gameInstanceRunning)
                    {
                        if (_suspended)
                        {
                            Thread.Sleep(100);
                            continue;
                        }
                        s.Restart();
                        for (int i = 0; i < 60 && !_suspended; i++)
                        {
                            s0.Restart();
                            _emulator.PPU.ProcessFrame();
                            s0.Stop();
                            Thread.Sleep(Math.Max((int)(980 / 60.0 - s0.ElapsedMilliseconds), 0) / activeSpeed);
                        }
                        s.Stop();
                        Console.WriteLine($"60 frames in {s.ElapsedMilliseconds}ms");
                    }
                });
                _gameThread.Start();
                _gameInstanceRunning = true;
            }
        }
    }
}
