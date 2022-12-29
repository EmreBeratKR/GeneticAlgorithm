using UnityEngine;

namespace EmreBeratKR.GeneticAlgorithm
{
    public class CubeEntity : Entity
    {
        [Header("References")]
        [SerializeField] private new Renderer renderer;
        [SerializeField] private Color aliveColor;
        [SerializeField] private Color killedColor;
        [SerializeField] private Color succeedColor;
        
        [Header("Values")]
        [SerializeField, Min(0f)] private float baseAcceleration;
        [SerializeField, Min(0f)] private float baseSpeed;
        [SerializeField, Min(0f)] private float maxSpeed;


        public override float Fitness
        {
            get
            {
                var sqrDistanceToTarget = CubeEntitySimulator.GetSqrDistanceToTarget(transform.position);
                var tickLeft = m_Brain.DirectionLeft;
                var rawFitness = sqrDistanceToTarget + tickLeft;
                return MathfExtra.Sigmoid(rawFitness);
            }
        }

        public override bool IsAlive { get; protected set; }
        public override bool IsSucceed { get; protected set; }


        private Color Color
        {
            set => renderer.material.color = value;
        }
        

        private CubeEntityBrain m_Brain;
        private Vector3 m_Velocity;


        public override void Initialize(Vector3 position)
        {
            Color = aliveColor;
            m_Brain = CubeEntityBrain.Default;
            IsAlive = true;
            IsSucceed = false;
            transform.position = position;
        }

        public override void Tick(float deltaTime)
        {
            if (IsSucceed) return;
            
            if (!IsAlive) return;
            
            if (!m_Brain.HasDirectionLeft)
            {
                Kill();
                return;
            }

            var direction = m_Brain.CurrentDirection;
            var acceleration = direction * (baseAcceleration * deltaTime);
            m_Velocity += acceleration;
            m_Velocity = m_Velocity.Clamped(0f, maxSpeed);
            transform.position += m_Velocity * (baseSpeed * deltaTime);

            transform.forward = m_Velocity;
            
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

        public override void Kill()
        {
            base.Kill();
            Color = killedColor;
        }

        public override void Succeed()
        {
            base.Succeed();
            Color = succeedColor;
        }
    }
}