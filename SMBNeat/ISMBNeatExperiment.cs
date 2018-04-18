using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMBNeat
{
    public interface ISMBNeatExperiment
    {
        void StartExperment();

        void ea_UpdateEvent(object sender, EventArgs e);
    }
}
