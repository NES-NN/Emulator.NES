using System.Windows.Forms;

namespace dotNES.Controllers
{
    public interface IController
    {
        void Strobe(bool on);

        int ReadState();

        void PressKey(KeyEventArgs e);

        void ManualPressKey(int key);

        void ReleaseKey(KeyEventArgs e);

        void ManualReleaseKey(int key);
    }
}
