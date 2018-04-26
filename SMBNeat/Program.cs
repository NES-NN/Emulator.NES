using System;
using TestbedUtils.Training;
using TestbedUtils.Training.Neat;

namespace SMBNeat
{
    class Program
    {
        static void Main(string[] args)
        {
            INetworkTrainer neatNetworkTrainer = new SMBNeatNetworkTrainer("emu.bin", "smb.config.xml", "SMB_champion.xml");
            neatNetworkTrainer.StartTraining();
            Console.ReadLine();
        }
    }
}
