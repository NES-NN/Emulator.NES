using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestbedUtils.Training.Players
{
    public interface IPlayer
    {
        bool EvaluateOutputNode(double output);
        void PressKey(int key);
        void ReleaseAllKeys();
        void MakeMove(int[] inputs);
    }
}
