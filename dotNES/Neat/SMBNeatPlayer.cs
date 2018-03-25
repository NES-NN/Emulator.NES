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
            for (int i = 0; i < 256; i++)
            {
                _brain.InputSignalArray[i] = inputs[i];
            }

            // Activate the network
            _brain.Activate();

            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine(_brain.OutputSignalArray[i]);
            }

            // TODO: Use output array to determine what buttons to press
            _controller.PressKey_Manual(Keys.Right);
        }
    }
}
