using SharpNeat.Phenomes;
using SharpNeat.Core;

namespace TestbedUtils.Training.Neat
{
    public class SMBNeatExperiment : AbstractNeatExperiment
    {
        private string state;
        private AbstractNetworkTrainer.UpdateMemoryMapperRef handler;

        public SMBNeatExperiment(string state, AbstractNetworkTrainer.UpdateMemoryMapperRef handler)
        {
            this.state = state;
            this.handler = handler;
        }

        /// <summary>
        /// Gets the SuperMarioBros evaluator that scores individuals.
        /// </summary>
        public override IPhenomeEvaluator<IBlackBox> PhenomeEvaluator
        {
            get { return new SMBEvaluator(state, handler); }
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
