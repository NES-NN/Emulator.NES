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
            //Hold max values for fitness eval.
            int X = 0, S = 0, T = 400;

            // The amount of seconds that can pass with Mario not making progress in Seconds
            int idelTime = 3;

            //How many frames the NN should wait before making another move (Game runs at 60 frames/second)
            int playerSpeed = 30;

            //Create a fresh clone of the state
            Emulator emu = LoadState(_stateFile);

            //Create a new controller for the Emulator
            emu.Controller = new NES001Controller(); ;

            //Create a new SMB Mapper object for easy access to the NN inputs.
            SMBMemoryMapper mapper = new SMBMemoryMapper(ref emu);

            //Create a new Neat Player
            SMBNeatPlayer neatPlayer = new SMBNeatPlayer(box, ref emu.Controller);

            // Until Mario becomes stuck or dies feed each input frame into the neural network and make a move!
            int levelX = mapper.FetchPlayerStats()["x"];

            //Console.WriteLine("levelX : " + SMBNeatInstance.SMB.PlayerStats["x"]);
            double fitness = 0;

            //adjust idleTime into relevent frame count.
            idelTime = idelTime * (60 / playerSpeed);
            
            while (mapper.FetchGameStats()["lives"] >= 2 && idelTime >= 0)
            {
                for (int i = 1; i <= 60; i++)
                {
                    //process 1 frame of the game
                    emu.PPU.ProcessFrame();

                    if (i % playerSpeed == 0)
                    {
                        //Evaluate inputs and make a moves
                        neatPlayer.MakeMove(mapper.FetchInputs());

                        // Check whether Mario is advancing
                        if (levelX >= mapper.FetchPlayerStats()["x"])
                            idelTime--;

                        //Update marios X progress
                        levelX  = mapper.FetchPlayerStats()["x"];

                        //Update fitness eval vars
                        X = Math.Max(X, mapper.FetchPlayerStats()["x"]);
                        S = Math.Max(S, mapper.FetchGameStats()["score"]);
                        T = Math.Min(T, mapper.FetchGameStats()["time"]);

                        //Console.Write(levelX + ", ");
                    }
                }
            }

            // Update eval count ?? what is this even doing? 
            _evalCount++;

            //get the fitness score of the run
            fitness = CalculateFinalFitness(X, S, T);

            mapper = null;

            //Console.Write("fitness : " + fitness + "\n");
            
            // Return the fitness score
            return new FitnessInfo(fitness, fitness);
        }

        private double CalculateFinalFitness(int X, int S, int T)
        {
            //Add game score and x progress then devide by time to get fitness. the +1 is to avoid a devide by zero exception.
            return (double)(X + S) / (double)(T + 1);
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
