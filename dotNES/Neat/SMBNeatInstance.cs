using dotNES.Controllers;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace dotNES.Neat
{
    class SMBNeatInstance
    {
        private IController controller;
        private Emulator emu;
        public SMB smb;
        private UI ui;

        private System.Threading.Thread gameThread;

        private bool gameInstanceRunning = false;
        private bool suspended = false;

        public bool GameInstanceRunning { get => gameInstanceRunning; set => gameInstanceRunning = value; }
        public bool Suspended { get => suspended; set => suspended = value; }


        public SMBNeatInstance() { }

        public SMBNeatInstance(bool withUI, string rom)
        {
            StartGameInstance(withUI, rom);
        }

        private void StartGameInstance(bool withUI, string rom)
        {
            controller = new NES001Controller();
            emu = new Emulator(rom, controller);
            smb = new SMB(ref emu);

            StartGameThread(withUI);
        }

        public void SaveState()
        {
            suspended = true;
            var dialog = new SaveFileDialog
            {
                DefaultExt = "bin",
                FileName = "emu"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(dialog.FileName, FileMode.Create, FileAccess.Write, FileShare.None);
                formatter.Serialize(stream, emu);
                stream.Close();
            }
            suspended = false;
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
                IController c = new NES001Controller();
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                emu = (Emulator)formatter.Deserialize(stream);
                emu.Controller = c; controller = c;
                stream.Close();

                smb = new SMB(ref emu);

                StartGameThread(withUI);
            }
        }

        private void StartGameThread(bool withUI)
        {
            if (withUI)
            {
                if(ui != null && ui.Visible)
                    ui.Close();
                ui = new UI(ref emu, ref controller);
                ui.Show();
            }
            else
            {
                gameInstanceRunning = false;
                gameThread?.Abort();

                gameThread = new System.Threading.Thread(() =>
                {
                    gameInstanceRunning = true;
                    while (gameInstanceRunning)
                    {
                        if (suspended)
                        {
                            System.Threading.Thread.Sleep(100);
                            continue;
                        }
                        emu.PPU.ProcessFrame();
                    }
                });
                gameThread.Start();
                gameInstanceRunning = true;


            }
        }
    }
}
