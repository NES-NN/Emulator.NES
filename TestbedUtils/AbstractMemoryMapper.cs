using dotNES;
using System.Collections.Generic;

namespace TestbedUtils
{
    public abstract class AbstractMemoryMapper : IMemoryMapper
    {
        protected Emulator emulator;

        protected static int boxRadius = 6;
        protected static int inputSize = (boxRadius * 2 + 1) * (boxRadius * 2 + 1);

        protected int[] inputs = new int[inputSize + 1];

        public AbstractMemoryMapper(ref Emulator emulator)
        {
            this.emulator = emulator;
        }

        public abstract Dictionary<string, int> FetchGameStats();
        public abstract int[] FetchInputs();
        public abstract Dictionary<string, int> FetchPlayerStats();

        protected int AddressRead(uint address)
        {
            return (int)emulator.CPU.AddressRead(address);
        }

        protected int BcdToInt(uint startAddress, uint length)
        {
            int intVal = 0;
            for (uint i = startAddress + length, j = 1; i >= startAddress; i--, j *= 10)
            {
                intVal += (int)(AddressRead(i) * j);
            }
            return intVal;
        }
    }
}
