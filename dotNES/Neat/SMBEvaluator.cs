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
        private SMBNeatUI _ui;
        int count = 0;

        /// <summary>
        /// Creates a SMB evaluator with an embedded NES controller.
        /// </summary>
        public SMBEvaluator(SMBNeatUI ui) : base()
        {
            _ui = ui;
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
            int thisCount = count++;
            int displayID = thisCount % SMBNeatUI.Instances;

            _ui.Log("[Generation " + thisCount / 50 + " Genome " + thisCount % 50 + " Display "+ displayID + "] Starting  Evaluation\n");
            
            SMBNeatInstance _SMBNeatInstance = new SMBNeatInstance(new NES001Controller());

            _SMBNeatInstance.LoadState_Manual(false, "emu.bin");

            //Send back SMBNeatInstance to the UI so you know what the hell is going on. 
            _ui.Invoke(new Action(() => {
                while (_ui.isActive(displayID))
                {
                    WaitNMilliseconds(100);
                }
                _ui.UpdateInstance(displayID, ref _SMBNeatInstance);
            }));

            _SMBNeatInstance.SMB.UpdateStats();
            WaitNMilliseconds(500);

            // The amount of seconds that can pass with Mario not making progress
            // in quarter second incroments ie. 3 seconds = 12
            int idelTime = 12;

            SMBNeatPlayer neatPlayer = new SMBNeatPlayer(box, ref _SMBNeatInstance._controller);

            // Until Mario becomes stuck or dies feed each input frame into the neural network and make a move!
            int levelX = _SMBNeatInstance.SMB.PlayerStats["x"];

            //Console.WriteLine("levelX : " + SMBNeatInstance.SMB.PlayerStats["x"]);
            double fitness = 0;


            while (_SMBNeatInstance.SMB.GameStats["lives"] >= 2 && idelTime >= 0)
            {
                WaitNMilliseconds(250);

                neatPlayer.MakeMove(_SMBNeatInstance.SMB.Inputs);               
                _SMBNeatInstance.SMB.UpdateStats();
                
                // Check whether Mario is advancing
                if (levelX >= _SMBNeatInstance.SMB.PlayerStats["x"])
                    idelTime--;

                levelX = _SMBNeatInstance.SMB.PlayerStats["x"];
                if (levelX > fitness)
                    fitness = levelX;

            }

            // Update eval count
            _evalCount++;

            // Ensure controller gets paused
            //neatPlayer.ReleaseAllKeys();

            var endFitness = (fitness+(double)_SMBNeatInstance.SMB.GameStats["score"])/(double)(_SMBNeatInstance.SMB.GameStats["time"]+1);
            //some bug in fitness (65535?)
            if (endFitness > 170) endFitness = 0;
            _SMBNeatInstance.Stop();
            _SMBNeatInstance = null;

            _ui.UpdateInstance(displayID, ref _SMBNeatInstance);

            _ui.Log("[Generation " + thisCount / 50 + " Genome " + thisCount % 50 + " Display " + displayID + "] Finished Evaluation - fitness : " + endFitness + "\n");
            // Return the fitness score
            return new FitnessInfo(endFitness, endFitness);
        }



        private void WaitNMilliseconds(double Milliseconds)
        {
            if (Milliseconds < 1) return;
            DateTime _desired = DateTime.Now.AddMilliseconds(Milliseconds);
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
