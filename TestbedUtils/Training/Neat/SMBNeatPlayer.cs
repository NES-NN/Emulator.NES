using System;
using SharpNeat.Phenomes;
using dotNES.Controllers;

namespace TestbedUtils.Training.Neat
{
    public class SMBNeatPlayer : AbstractNeatPlayer
    {
        public SMBNeatPlayer(IBlackBox brain, ref IController controller): base(brain, ref controller) {}

        public override void MakeMove(int[] inputs)
        {
            // Send inputs to the brain
            ProcessInputs(inputs);

            // Release all previous button presses
            ReleaseAllKeys();

            // Execute button presses!
            if (EvaluateOutputNode(brain.OutputSignalArray[0]))
                PressKey(7);

            if (EvaluateOutputNode(brain.OutputSignalArray[1]))
                PressKey(6);

            // Up+Down And Left+Right are not valid button combos.
            // So only press the one with the highest value.
            if (brain.OutputSignalArray[2] > brain.OutputSignalArray[3])
                PressKey(3);
            else
                PressKey(2);

            if (brain.OutputSignalArray[4] > brain.OutputSignalArray[5])
                PressKey(1);
            else
                PressKey(0);
        }
    }
}
