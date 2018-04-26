using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNES.Controllers;

namespace TestbedUtils.Training.Players
{
    public abstract class AbstractPlayer : IPlayer
    {
        protected IController controller;

        public AbstractPlayer(ref IController controller)
        {
            this.controller = controller;
        }

        public abstract void MakeMove(int[] inputs);

        public bool EvaluateOutputNode(double output)
        {
            return (output > 0.5) ? true : false;
        }

        public void PressKey(int key)
        {
            controller.ManualPressKey(key);
        }

        public void ReleaseAllKeys()
        {
            for (int i = 0; i < 7; i++)
            {
                controller.ManualReleaseKey(i);
            }
        }
    }
}
