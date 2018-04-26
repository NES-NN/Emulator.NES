using System;
using System.Collections.Generic;

namespace TestbedUtils.Memory
{
    public interface IStats
    {
        Dictionary<String, int> GetStats();
    }
}
