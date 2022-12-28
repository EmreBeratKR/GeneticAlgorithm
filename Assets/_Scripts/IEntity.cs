namespace EmreBeratKR.GeneticAlgorithm
{
    public interface IEntity<out T> : ICloneable<T>, ICloneable, ITickable, IMutateable
        where T : IEntity<T>
    {
        
    }
}