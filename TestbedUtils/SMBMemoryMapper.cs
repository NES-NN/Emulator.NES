using System;
using System.Collections.Generic;
using dotNES;

namespace TestbedUtils
{
    public class SMBMemoryMapper : AbstractMemoryMapper
    {
        private int[,] enemy = new int[5, 2];
        private PlayerStats playerStats = new PlayerStats();
        private GameStats gameStats = new GameStats();

        public SMBMemoryMapper(ref Emulator emulator) : base(ref emulator)
        {
        }

        public override int[] FetchInputs()
        {
            UpdateInputs();
            return inputs;
        }

        public override Dictionary<string, int> FetchPlayerStats()
        {
            UpdatePlayerStats();
            return playerStats.GetStats();
        }

        public override Dictionary<string, int> FetchGameStats()
        {
            UpdateGameStats();
            return gameStats.GetStats();
        }

        private void UpdatePlayerStats()
        {
            playerStats.PlayerX = (AddressRead(0x6D) * 0x100 + AddressRead(0x86));
            playerStats.PlayerY = (AddressRead(0x03B8) + 16);
            playerStats.ScreenX = AddressRead(0x03AD);
            playerStats.ScreenY = AddressRead(0x03B8);
            playerStats.State = AddressRead(0x000E);
            playerStats.FloatState = AddressRead(0x001D);

            //0=small, 1=Big, >2=Fiery
            playerStats.PowerUp = AddressRead(0x0756); 
            playerStats.Direction = AddressRead(0x0003);
        }

        private void UpdateGameStats()
        {
            gameStats.Score = BcdToInt(0x07DE, 5);
            gameStats.Time = BcdToInt(0x07F8, 3) / 10;
            gameStats.Lives = AddressRead(0x075A);
            gameStats.Coins = AddressRead(0x075E);
            gameStats.World = AddressRead(0x075F) + 1;
            gameStats.Level = AddressRead(0x0760) + 1;

            gameStats.PowerUpVisible = AddressRead(0x001B);
            gameStats.PowerUPX1 = AddressRead(0x04C4);
            gameStats.PowerUPX2 = AddressRead(0x04C6);
            gameStats.PowerUPY1 = AddressRead(0x04C5);
            gameStats.PowerUPY2 = AddressRead(0x04C7);

        }

        private void UpdateInputs()
        {
            UpdatePlayerStats();
            UpdateEnemies();

            int i = 0, e = 0;
            for (int dy = -boxRadius * 16; dy <= boxRadius * 16; dy += 16)
            {
                for (int dx = -boxRadius * 16; dx <= boxRadius * 16; dx += 16)
                {
                    inputs[i] = 0; //empty space

                    if (GetTile(dx, dy) == 1 && playerStats.PlayerY + dy < 0x1B0)
                    {
                        inputs[i] = 1; //tile
                    }

                    if (gameStats.PowerUpVisible != 0)
                    {
                        if (GetPowerUp(dx, dy) == 2)
                        {
                            inputs[i] = 2;
                        }
                    }

                    for (int j = e; j < 5; j++)
                    {
                        if (enemy[j, 0] - (playerStats.PlayerX + dx) <= 8 && enemy[j, 1] - (playerStats.PlayerY + dy) <= 8 &&
                            enemy[j, 0] != 0 && enemy[j, 1] != 0)
                        {
                            inputs[i] = -1; //enemy
                            e++;
                        }
                    }
                    i++;
                }
            }
        }

        private void UpdateEnemies()
        {
            for (uint i = 0; i < 5; i++)
            {
                if ((Convert.ToBoolean(AddressRead(0xF + i))))
                {
                    enemy[i, 0] = (AddressRead(0x6E + i) * 0x100 + AddressRead(0x87 + i));
                    enemy[i, 1] = (AddressRead(0xCF + i) + 12);
                }
                else
                {
                    enemy[i, 0] = 0;
                    enemy[i, 1] = 0;
                }
            }
        }

        private int GetTile(int dx, int dy)
        {
            int x = playerStats.PlayerX + dx + 8;
            int y = playerStats.PlayerY + dy;

            int page = (x / 256) % 2;

            int subx = (x % 256) / 16;
            int suby = (y - 32) / 16;

            //0x0500-0x069F Current tile
            uint addr = Convert.ToUInt32(0x500 + page * 13 * 16 + suby * 16 + subx);

            if (suby >= 13 || suby < 0)
            {
                return 0;
            }

            if (AddressRead(addr) == 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private int GetPowerUp(int dx, int dy)
        {            
            if ((gameStats.PowerUPX1 - playerStats.ScreenX < dx) && (gameStats.PowerUPX2 - playerStats.ScreenX > dx))            
            {
                if ((gameStats.PowerUPY1 - playerStats.ScreenY < dy + 24) && (gameStats.PowerUPY2 - playerStats.ScreenY > dy + 24))
                {
                    return 2;
                }
            }
            return -2;
        }

        private abstract class AbstractStats
        {
            protected Dictionary<String, int> keyValuePairs;

            public abstract Dictionary<String, int> GetStats();
        }

        private class PlayerStats : AbstractStats
        {
            public int PlayerX { get; set; } = 0;
            public int PlayerY { get; set; } = 0;
            public int ScreenX { get; set; } = 0;
            public int ScreenY { get; set; } = 0;
            public int State { get; set; } = 0;
            public int FloatState { get; set; } = 0;
            public int PowerUp { get; set; } = 0;
            public int Direction { get; set; } = 0;

            public override Dictionary<String, int> GetStats()
            {
                keyValuePairs = new Dictionary<String, int>()
                {
                    { "x", PlayerX }, { "screenX", ScreenX }, { "state", State },
                    { "y", PlayerY }, { "screenY", ScreenY }, { "floatState", FloatState },
                    { "powerUP", PowerUp }, { "direction", Direction }
                };
                return keyValuePairs;
            }
        }

        private class GameStats : AbstractStats
        {
            public int Score { get; set; } = 0;
            public int Lives { get; set; } = 0;
            public int Coins { get; set; } = 0;
            public int World { get; set; } = 0;
            public int Level { get; set; } = 0;
            public int Time { get; set; } = 0;

            public int PowerUpVisible { get; set; } = 0;
            public int PowerUPX1 { get; set; } = 0;            
            public int PowerUPX2 { get; set; } = 0;
            public int PowerUPY1 { get; set; } = 0;
            public int PowerUPY2 { get; set; } = 0;

            public override Dictionary<String, int> GetStats()
            {
                keyValuePairs = new Dictionary<String, int>()
                {
                    { "score", Score }, { "lives", Lives }, { "coins", Coins },
                    { "world", World }, { "level", Level }, { "time", Time },
                    {"PowerUpVisible", PowerUpVisible }, {"PowerUPX1",PowerUPX1},
                    {"PowerUPX2",PowerUPX2 },{"PowerUPY1",PowerUPY1 },{"PowerUPY2",PowerUPY2 }
                };
                return keyValuePairs;
            }
        }
    }
}
