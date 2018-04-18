using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpNeat.Domains;
using SharpNeat.Phenomes;
using SharpNeat.Core;

using dotNES;

namespace SMBNeat
{
    class SMBExperiment : SimpleNeatExperiment
    {
        private string[] _state;

        public SMBExperiment(string[] state)
        {
            _state = state;
        }

        /// <summary>
        /// Gets the SuperMarioBros evaluator that scores individuals.
        /// </summary>
        public override IPhenomeEvaluator<IBlackBox> PhenomeEvaluator
        {
            get { return new SMBEvaluator(_state); }
        }

        /// <summary>
        /// Defines the number of input nodes in the neural network.
        /// </summary>
        public override int InputCount
        {
            get { return 170; }
        }

        /// <summary>
        /// Defines the number of output nodes in the neural network.
        /// This equates to the number of buttons on the controller.
        /// </summary>
        public override int OutputCount
        {
            get { return 6; }
        }

        /// <summary>
        /// Defines whether all networks should be evaluated every
        /// generation, or only new (child) networks.
        /// </summary>
        public override bool EvaluateParents
        {
            get { return true; }
        }
    }
}
