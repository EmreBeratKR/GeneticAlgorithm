using UnityEngine;

namespace EmreBeratKR.GeneticAlgorithm
{
    public abstract class Entity : MonoBehaviour, IEntity
    {
        public abstract float Fitness { get; }
        public abstract bool IsAlive { get; protected set; }
        public abstract bool IsSucceed { get; protected set; }


        public virtual void Initialize(Vector3 position)
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

        public virtual void Kill()
        {
            IsAlive = false;
        }

        public virtual void Succeed()
        {
            IsSucceed = true;
        }
    }
}