using UnityEngine;

namespace EmreBeratKR.GeneticAlgorithm
{
    public abstract class Entity : MonoBehaviour, IEntity
    {
        public abstract float Fitness { get; }
        public abstract bool IsAlive { get; protected set; }
        public abstract bool IsSucceed { get; protected set; }
        public abstract bool IsInitialized { get; protected set; }


        public virtual void Initialize()
        {
            IsInitialized = true;
        }
        
        public virtual object Clone()
        {
            var clone = Instantiate(this);
            clone.name = name;
            clone.IsInitialized = false;
            return clone;
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

        public virtual void Kill()
        {
            IsAlive = false;
        }

        public virtual void Destroy()
        {
            Destroy(gameObject);
        }

        public virtual void Succeed()
        {
            IsSucceed = true;
        }
    }
}