using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpNeat.Core;
using SharpNeat.Phenomes;
using dotNES.Controllers;

using static dotNES.Neat.SMBNeatInstance;
using System.Threading;

namespace dotNES.Neat
{
    class SMBEvaluator : IPhenomeEvaluator<IBlackBox>
    {
        private ulong _evalCount;
        private bool _stopConditionSatisfied;

        /// <summary>
        /// Creates a SMB evaluator with an embedded NES controller.
        /// </summary>
        public SMBEvaluator() : base()
        {
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
            IController _controller = new NES001Controller(); ;
            SMBNeatInstance SMBNeatInstance = new SMBNeatInstance(_controller);

            SMBNeatInstance.LoadState_Manual(true, "emu.bin");


            SMBNeatInstance.SMB.UpdateStats();
            WaitNSeconds(1);
            double fitness = 0;

            // The amount of frames that can pass with Mario not making progress
            int errorAllowance = 10;

            SMBNeatPlayer neatPlayer = new SMBNeatPlayer(box, ref SMBNeatInstance._controller);

            // Until Mario becomes stuck or dies feed each input frame into the neural network and make a move!
            int levelX = SMBNeatInstance.SMB.PlayerStats["x"];

            //Console.WriteLine("levelX : " + SMBNeatInstance.SMB.PlayerStats["x"]);


            while (SMBNeatInstance.SMB.GameStats["lives"] >= 2 && errorAllowance >= 0)
            {
                WaitNSeconds(1); 

                neatPlayer.MakeMove(SMBNeatInstance.SMB.Inputs);
                SMBNeatInstance.SMB.UpdateStats();
                

                //Console.WriteLine("levelX : " + levelX + " SMBNeatInstance.SMB.PlayerStats[\"x\"] : " + SMBNeatInstance.SMB.PlayerStats["x"]);
                
                // Check whether Mario is advancing
                if (levelX >= SMBNeatInstance.SMB.PlayerStats["x"])
                    errorAllowance--;

                levelX = SMBNeatInstance.SMB.PlayerStats["x"];

                // Update eval count
                _evalCount++;

                // Update fitness score
                fitness++;            }

            // Ensure controller gets paused
            neatPlayer.ReleaseAllKeys();

            SMBNeatInstance._ui.Close();

            // Return the fitness score
            return new FitnessInfo(fitness, fitness);
        }



        private void WaitNSeconds(double seconds)
        {
            if (seconds < 1) return;
            DateTime _desired = DateTime.Now.AddSeconds(seconds);
            while (DateTime.Now < _desired)
            {
                Thread.Sleep(1);
                System.Windows.Forms.Application.DoEvents();
            }
        }

        /// <summary>
        /// Reset the internal state of the evaluation scheme if any exists.
        /// </summary>
        public void Reset()
        {
        }
        #endregion
    }
}
