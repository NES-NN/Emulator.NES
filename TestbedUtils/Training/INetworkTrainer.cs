using TestbedUtils.Memory;

namespace TestbedUtils.Training
{
    public interface INetworkTrainer
    {
        void StartTraining();
        void StopTraining();
        IMemoryMapper GetMemoryMapper(int i);
    }
}
