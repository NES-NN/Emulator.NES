using System;
using System.Collections.Generic;

namespace TestbedUtils
{
    public abstract class AbstractStats : IStats
    {
        protected Dictionary<String, int> keyValuePairs;

        public abstract Dictionary<String, int> GetStats();
    }
}
