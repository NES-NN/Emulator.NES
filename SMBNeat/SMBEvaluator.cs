using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpNeat.Core;
using SharpNeat.Phenomes;
using dotNES.Controllers;
using System.Threading;
using dotNES;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TestbedUtils;

namespace SMBNeat
{
    class SMBEvaluator : IPhenomeEvaluator<IBlackBox>
    {
        private ulong _evalCount;
        private bool _stopConditionSatisfied;
        private string _stateFile;

        public SMBEvaluator(string stateFile)
        {
            _stateFile = stateFile;
        }

        #region IPhenomeEvaluator<IBlackBox> Members

        /// <summary>
        /// Gets the total number of evaluations that have been performed.
        /// </summary>
        public ulong EvaluationCount
        {
            get { return _evalCount; }
        }

        /// <summary>
        /// Gets a value indicating whether some goal fitness has been achieved and that
        /// the the evolutionary algorithm/search should stop. This property's value can remain false
        /// to allow the algorithm to run indefinitely.
        /// </summary>
        public bool StopConditionSatisfied
        {
            get { return _stopConditionSatisfied; }
        }

        /// <summary>
        /// Evaluate the provided IBlackBox against the SMB Neat player and return its fitness score.
        /// </summary>
        public FitnessInfo Evaluate(IBlackBox box)
        {
            // How many frames the NN should wait before making another move (Game runs at 60 frames/second)
            int playerSpeed = 2;

            // The amount of seconds that can pass with Mario not making progress in Seconds
            int idleTime = 3;
            idleTime = idleTime * (60 / playerSpeed);

            // Create a fresh clone of the state
            Emulator emu = LoadState(_stateFile);

            // Create a new controller for the Emulator
            emu.Controller = new NES001Controller();

            // Create a new SMB Mapper object for easy access to the NN inputs.
            SMBMemoryMapper mapper = new SMBMemoryMapper(ref emu);

            // Create a new Neat Player
            SMBNeatPlayer neatPlayer = new SMBNeatPlayer(box, ref emu.Controller);

            // Until Mario becomes stuck or dies feed each input frame into the neural network and make a move!
            int levelX = GetPlayerStats(mapper).PlayerX;

            while (GetGameStats(mapper).Lives >= 2 && idleTime >= 0)
            {
                for (int i = 1; i <= 60; i++)
                {
                    // Process 1 frame of the game
                    emu.PPU.ProcessFrame();

                    if (i % playerSpeed == 0)
                    {
                        // Refresh the memory mapper
                        mapper.Refresh();
                        int currLevelX = GetPlayerStats(mapper).PlayerX;

                        // Evaluate inputs and make a moves
                        neatPlayer.MakeMove(mapper.FetchInputs());

                        // Check whether Mario is advancing
                        if (levelX >= currLevelX)
                        {
                            idleTime--;
                        }

                        // Update marios X progress
                        levelX = currLevelX;

                        //Console.WriteLine("levelX: " + levelX);
                    }
                }

                // Refresh the memory mapper before evalutating while loop
                mapper.Refresh();
            }

            // Assign fitness eval vars
            int X = GetPlayerStats(mapper).PlayerX;
            int S = GetGameStats(mapper).Score;
            int T = GetGameStats(mapper).Time;
            double fitness = 0;

            // Update eval count ?? what is this even doing? 
            _evalCount++;

            // Get the fitness score of the run
            fitness = CalculateFinalFitness(X, S, T);

            Console.WriteLine("X: " + X + "\t S: " + S + "\t T: " + T + "\t fitness: " + fitness);
            
            // Return the fitness score
            return new FitnessInfo(fitness, fitness);
        }

        private double CalculateFinalFitness(int X, int S, int T)
        {
            // Add game score and x progress then divide by time to get fitness. the +1 is to avoid a devide by zero exception.
            return (double)(X + S) / (double)(T + 1);
        }

        private SMBMemoryMapper.GameStats GetGameStats(SMBMemoryMapper mapper)
        {
            SMBMemoryMapper.GameStats gameStats = (SMBMemoryMapper.GameStats)mapper.FetchGameStats();
            return gameStats;
        }

        private SMBMemoryMapper.PlayerStats GetPlayerStats(SMBMemoryMapper mapper)
        {
            SMBMemoryMapper.PlayerStats playerStats = (SMBMemoryMapper.PlayerStats)mapper.FetchPlayerStats();
            return playerStats;
        }

        /// <summary>
        /// Reset the internal state of the evaluation scheme if any exists.
        /// </summary>
        public void Reset()
        {
        }
        #endregion

        static Emulator LoadState(string fileName)
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            Emulator state = (Emulator)formatter.Deserialize(stream);
            stream.Close();

            return state;
        }
    }
}
