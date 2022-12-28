using UnityEngine;

namespace EmreBeratKR.GeneticAlgorithm
{
    public abstract class Entity : MonoBehaviour, IEntity<Entity>
    {
        public virtual Entity Clone()
        {
            return this;
        }

        public virtual void Tick(float deltaTime)
        {
            
        }

        public virtual void Mutate()
        {
            
        }
    }
}