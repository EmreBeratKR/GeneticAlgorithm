namespace EmreBeratKR.GeneticAlgorithm
{
    public abstract class Brain
    {
        public virtual void Randomize()
        {
            
        }

        public virtual Brain Clone()
        {
            return this;
        }

        public virtual void Mutate()
        {
            
        }

        public virtual void Tick()
        {
            
        }
    }
}