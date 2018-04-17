using System;
using System.Collections.Generic;

namespace TestbedUtils
{
    public interface IMemoryMapper
    {
        int[] FetchInputs();
        Dictionary<String, int> FetchGameStats();
        Dictionary<String, int> FetchPlayerStats();
    }
}
