using UnityEngine;

namespace EmreBeratKR.GeneticAlgorithm
{
    public sealed class CubeEntityBrain : Brain
    {
        private const float MutationPercent = 1;
        private const int DefaultSize = 5000;


        public static CubeEntityBrain Default => new(DefaultSize);
        

        public Vector3 CurrentDirection => m_Directions[m_CurrentDirectionIndex];
        public int DirectionLeft => m_Directions.Length - m_CurrentDirectionIndex;
        public bool HasDirectionLeft => m_CurrentDirectionIndex < m_Directions.Length;


        private static bool ShouldMutate
        {
            get
            {
                var randomFloat = Random.Range(0f, 100f);
                return randomFloat > MutationPercent;
            }
        }
        
        
        private readonly Vector3[] m_Directions;


        private int m_CurrentDirectionIndex;


        public CubeEntityBrain(int size)
        {
            this.m_Directions = new Vector3[size];
            Randomize();
        }
        
        public CubeEntityBrain(CubeEntityBrain brain)
        {
            this.m_Directions = brain.m_Directions;
        }


        public override void Tick()
        {
            m_CurrentDirectionIndex += 1;
        }

        public override void Randomize()
        {
            for (var i = 0; i < m_Directions.Length; i++)
            {
                m_Directions[i] = GetRandomDirection();
            }
        }

        public override Brain Clone()
        {
            return new CubeEntityBrain(this);
        }

        public override void Mutate()
        {
            for (var i = 0; i < m_Directions.Length; i++)
            {
                if (!ShouldMutate) continue;

                m_Directions[i] = GetRandomDirection();
            }
        }


        private static Vector3 GetRandomDirection()
        {
            var randomDirection = Random.insideUnitCircle.normalized;
            return new Vector3(randomDirection.x, 0f, randomDirection.y);
        }
    }
}