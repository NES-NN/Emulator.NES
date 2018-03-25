using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpNeat.Core;
using SharpNeat.Phenomes;
using dotNES.Controllers;

namespace dotNES.Neat
{
    class SMBEvaluator : IPhenomeEvaluator<IBlackBox>
    {
        private ulong _evalCount;
        private bool _stopConditionSatisfied;

        private IController _controller;
        private SMB _smbState;

        /// <summary>
        /// Creates a SMB evaluator with an embedded NES controller.
        /// </summary>
        /// <param name="controller"></param>
        public SMBEvaluator(ref IController controller, ref SMB smbState): base()
        {
            _controller = controller;
            _smbState = smbState;
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
        /// Evaluate the provided IBlackBox against the random SuperMarioBros player and return its fitness score.
        /// </summary>
        public FitnessInfo Evaluate(IBlackBox box)
        {
            double fitness = 0;

            SMBNeatPlayer neatPlayer = new SMBNeatPlayer(box, ref _controller);

            int[] inputs = _smbState.Inputs;

            neatPlayer.MakeMove(inputs);

            // TODO: Based on move will need to extract fitness score

            // Return the fitness score
            return new FitnessInfo(fitness, fitness);
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
