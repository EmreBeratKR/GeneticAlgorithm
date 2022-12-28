using UnityEngine;

namespace EmreBeratKR.GeneticAlgorithm
{
    public abstract class Entity : MonoBehaviour, IEntity
    {
        public virtual void Initialize()
        {
            
        }
        
        public virtual object Clone()
        {
            return Instantiate(this);
        }

        public virtual void Mutate()
        {
            
        }

        public virtual void Randomize()
        {
            
        }
        
        public virtual void Tick(float deltaTime)
        {
            
        }
    }
}