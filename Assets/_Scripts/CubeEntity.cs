using UnityEngine;

namespace EmreBeratKR.GeneticAlgorithm
{
    public class CubeEntity : Entity
    {
        [SerializeField, Min(0f)] private float baseAcceleration;
        [SerializeField, Min(0f)] private float baseSpeed;
        [SerializeField, Min(0f)] private float maxSpeed;


        private CubeEntityBrain m_Brain;
        private Vector3 m_Velocity;


        public override void Initialize()
        {
            m_Brain = CubeEntityBrain.Default;
        }

        public override void Tick(float deltaTime)
        {
            if (!m_Brain.HasDirectionLeft)
            {
                // Die
                return;
            }

            var direction = m_Brain.CurrentDirection;
            var acceleration = direction * (baseAcceleration * deltaTime);
            m_Velocity += acceleration;
            m_Velocity = m_Velocity.Clamped(0f, maxSpeed);
            transform.position += m_Velocity * (baseSpeed * deltaTime);
            
            m_Brain.Tick();
        }

        public override object Clone()
        {
            var clone = (CubeEntity) base.Clone();
            clone.m_Brain = (CubeEntityBrain) m_Brain?.Clone();
            return clone;
        }
        
        public override void Mutate()
        {
            m_Brain.Mutate();
        }

        public override void Randomize()
        {
            m_Brain.Randomize();
        }
    }
}