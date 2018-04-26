using SharpNeat.Phenomes;
using TestbedUtils.Training.Players;
using dotNES.Controllers;

namespace TestbedUtils.Training.Neat
{
    public abstract class AbstractNeatPlayer : AbstractPlayer
    {
        protected IBlackBox brain;

        public AbstractNeatPlayer(IBlackBox brain, ref IController controller) : base(ref controller)
        {
            this.brain = brain;
        }

        protected void ProcessInputs(int[] inputs)
        {
            brain.ResetState();

            // Convert the game state into an input array for the network
            for (int i = 0; i < brain.InputCount; i++)
            {
                brain.InputSignalArray[i] = inputs[i];
            }

            brain.Activate();
        }
    }
}
