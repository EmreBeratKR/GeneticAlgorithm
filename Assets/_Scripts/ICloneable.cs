namespace EmreBeratKR.GeneticAlgorithm
{
    public interface ICloneable
    {
        
    }
    
    public interface ICloneable<out T> : ICloneable
        where T : ICloneable<T>
    {
        T Clone();
    }
}