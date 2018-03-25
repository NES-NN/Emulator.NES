using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpNeat.Domains;
using SharpNeat.Phenomes;
using SharpNeat.Core;
using dotNES.Controllers;

namespace dotNES.Neat
{
    class SMBExperiment : SimpleNeatExperiment
    {
        private IController _controller;
        private SMB _smbState;

        /// <summary>
        /// Creates a SMB Experiment with an embededded NES Controller.
        /// </summary>
        /// <param name="controller"></param>
        public SMBExperiment(ref IController controller, ref SMB smbState): base()
        {
            _controller = controller;
            _smbState = smbState;
        }

        /// <summary>
        /// Gets the SuperMarioBros evaluator that scores individuals.
        /// </summary>
        public override IPhenomeEvaluator<IBlackBox> PhenomeEvaluator
        {
            get { return new SMBEvaluator(ref _controller, ref _smbState); }
        }

        /// <summary>
        /// Defines the number of input nodes in the neural network.
        /// </summary>
        public override int InputCount
        {
            get { return _smbState.Inputs.Length; }
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
