namespace EmreBeratKR.GeneticAlgorithm
{
    public interface IInitializeable
    {
        bool IsInitialized { get; }
        void Initialize();
    }
}