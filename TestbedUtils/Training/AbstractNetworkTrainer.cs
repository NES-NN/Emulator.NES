using TestbedUtils.Memory;

namespace TestbedUtils.Training
{
    public abstract class AbstractNetworkTrainer : INetworkTrainer
    {
        public abstract void StartTraining();

        public abstract void StopTraining();

        protected IMemoryMapper[] memoryMapperArray;

        public delegate void UpdateMemoryMapperRef(ref IMemoryMapper memoryMapper, int i);

        public void DelegateUpdateMemoryMapperRef(ref IMemoryMapper memoryMapper, int i)
        {
            memoryMapperArray[i] = memoryMapper;
        }

        public IMemoryMapper GetMemoryMapper(int i)
        {
            return memoryMapperArray[i];
        }
    }
}
