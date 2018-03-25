using System.Windows.Forms;

namespace dotNES.Controllers
{
    interface IController
    {
        void Strobe(bool on);

        int ReadState();

        void PressKey(KeyEventArgs e);

        void PressKey_Manual(Keys key);

        void ReleaseKey(KeyEventArgs e);

        void ReleaseKey_Manual(Keys key);
    }
}
