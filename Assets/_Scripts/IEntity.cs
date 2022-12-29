namespace EmreBeratKR.GeneticAlgorithm
{
    public interface IEntity : IInitializeable, ICloneable, IMutateable, IRandomizeable, ITickable, IKillable
    {
        float Fitness { get; }
        bool IsAlive { get; }
        bool IsSucceed { get; }
        void Succeed();
    }
}