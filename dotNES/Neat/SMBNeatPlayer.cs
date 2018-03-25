using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpNeat.Phenomes;
using dotNES.Controllers;

namespace dotNES.Neat
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

            // Execute button presses!
            EvaluateOutputNode(_brain.OutputSignalArray[0], Keys.A);
            EvaluateOutputNode(_brain.OutputSignalArray[1], Keys.S);
            EvaluateOutputNode(_brain.OutputSignalArray[2], Keys.Up);
            EvaluateOutputNode(_brain.OutputSignalArray[3], Keys.Down);
            EvaluateOutputNode(_brain.OutputSignalArray[4], Keys.Left);
            EvaluateOutputNode(_brain.OutputSignalArray[5], Keys.Right);
        }

        /// <summary>
        /// Evaluates whether to press or release a particular key based on the output
        /// from the NeuralNetwork.
        /// </summary>
        /// <param name="output"></param>
        /// <param name="key"></param>
        private void EvaluateOutputNode(double output, Keys key)
        {
            if (output > 0.5)
            {
                _controller.PressKey_Manual(key);
            }
            else
            {
                _controller.ReleaseKey_Manual(key);
            }
        }

        /// <summary>
        /// Releases all keys the player might be holding
        /// </summary>
        public void ReleaseAllKeys()
        {
            _controller.ReleaseKey_Manual(Keys.A);
            _controller.ReleaseKey_Manual(Keys.S);
            _controller.ReleaseKey_Manual(Keys.Up);
            _controller.ReleaseKey_Manual(Keys.Down);
            _controller.ReleaseKey_Manual(Keys.Left);
            _controller.ReleaseKey_Manual(Keys.Right);
        }
    }
}
