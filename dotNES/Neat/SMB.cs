using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNES.Neat
{
    class SMB
    {
        private Emulator emu;

        private static int BoxRadius = 6;
        private static int InputSize = (BoxRadius * 2 + 1) * (BoxRadius * 2 + 1);

        private int[] inputs = new int[InputSize + 1];
        private int[,] enemy = new int[5, 2];

        private Dictionary<String, int>
            gameStats = new Dictionary<String, int>()
            {
                { "score", 0 }, { "lives", 0 }, { "coins", 0 },
                { "world", 0 }, { "level", 0 }, { "time", 0 }
            },
            playerStats = new Dictionary<String, int>()
            {
                { "x", 0 }, { "screenX", 0 }, { "state", 0 },
                { "y", 0 }, { "screenY", 0 }, { "floatState", 0 },
                { "powerUP", 0 }, { "direction", 0 }
            };

        public SMB(ref Emulator e)
        {
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

        public Dictionary<String, int> PlayerStats
        {
            get { updatePlayerStats(); return playerStats; }
        }

        private void updatePlayerStats()
        {
            playerStats["x"] = (int)(emu.CPU.AddressRead(0x6D) * 0x100 + emu.CPU.AddressRead(0x86));
            playerStats["y"] = (int)(emu.CPU.AddressRead(0x03B8) + 16);

            playerStats["screenX"] = (int)emu.CPU.AddressRead(0x03AD);
            playerStats["screenY"] = (int)emu.CPU.AddressRead(0x03B8);

            playerStats["state"] = (int)emu.CPU.AddressRead(0x000E);
            playerStats["floatState"] = (int)emu.CPU.AddressRead(0x001D);

            playerStats["powerUP"] = (int)emu.CPU.AddressRead(0x0756);
            playerStats["direction"] = (int)emu.CPU.AddressRead(0x0003);
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

        private void updateInputs()
        {
            updateEnemies();
            int i = 0, e = 0;
            for (int dy = -BoxRadius * 16; dy <= BoxRadius * 16; dy += 16)
                for (int dx = -BoxRadius * 16; dx <= BoxRadius * 16; dx += 16)
                {
                    inputs[i] = 0;

                    if (getTile(dx, dy) == 1 && playerStats["y"] + dy < 0x1B0)
                        inputs[i] = 1;

                    for (int j = e; j < 5; j++)
                        if (enemy[j, 0] - (playerStats["x"] + dx) <= 8 && enemy[j, 1] - (playerStats["y"] + dy) <= 8 &&
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

        private int getTile(int dx, int dy)
        {
            int x = playerStats["x"] + dx + 8;
            int y = playerStats["y"] + dy;
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

        private int bcdToInt(uint startAddress, uint length)
        {
            int intVal = 0;
            for (uint i = startAddress + length, j = 1; i >= startAddress; i--, j *= 10)
                intVal += (int)(emu.CPU.AddressRead(i) * j);
            return intVal;
        }
    }
}
