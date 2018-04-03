using SharpNeat.Phenomes;
using dotNES.Controllers;
using System;

namespace SMBNeat
{
    class SMBNeatPlayer
    {
        private IBlackBox _brain;
        private IController _controller;

        /// <summary>
        /// Creates a new NEAT player with the specified brain and controller.
        /// </summary>
        /// <param name="brain"></param>
        /// <param name="controller"></param>
        public SMBNeatPlayer(IBlackBox brain, ref IController controller)
        {
            _brain = brain;
            _controller = controller;
        }

        /// <summary>
        /// Based on game input will trigger the player to make a move.
        /// </summary>
        /// <param name="inputs"></param>
        public void MakeMove(int[] inputs)
        {
            // Clear the network
            _brain.ResetState();

            // Convert the game state into an input array for the network
            for (int i = 0; i < _brain.InputCount; i++)
            {
                _brain.InputSignalArray[i] = inputs[i];
            }

            // Activate the network
            _brain.Activate();

            // Release all previous button presses
            ReleaseAllKeys();
                        
            // Execute button presses!
            if (EvaluateOutputNode(_brain.OutputSignalArray[0]))
                _controller.ManualPressKey(7);

            if (EvaluateOutputNode(_brain.OutputSignalArray[1]))
                _controller.ManualPressKey(6);

            // Up+Down And Left+Right are not valid button combos.
            // So only press the one with the highest value. 
            if (_brain.OutputSignalArray[2] > _brain.OutputSignalArray[3])
                _controller.ManualPressKey(3);
            else
                _controller.ManualPressKey(2);

            if (_brain.OutputSignalArray[4] > _brain.OutputSignalArray[5])
                _controller.ManualPressKey(1);
            else
                _controller.ManualPressKey(0);
        }

        /// <summary>
        /// Evaluates whether to press or release a particular key based on the output
        /// from the NeuralNetwork.
        /// </summary>
        /// <param name="output"></param>
        /// <returns>True if key press is valid</returns>  
        private bool EvaluateOutputNode(double output)
        {
            return (output > 0.5) ? true : false;
        }

        /// <summary>
        /// Updates player key presses and send them to the virtual controller.
        /// </summary>
        public void ReleaseAllKeys()
        {
            for (int i = 0; i < 7; i++)
                _controller.ManualReleaseKey(i);
        }
    }
}
