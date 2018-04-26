using System;
using System.Collections.Generic;

namespace TestbedUtils.Memory
{
    public interface IMemoryMapper
    {
        int[] FetchInputs();
        IStats FetchGameStats();
        IStats FetchPlayerStats();
        void Refresh();
    }
}
