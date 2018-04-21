using System;
using System.Collections.Generic;

namespace TestbedUtils
{
    public interface IMemoryMapper
    {
        int[] FetchInputs();
        IStats FetchGameStats();
        IStats FetchPlayerStats();
    }
}
