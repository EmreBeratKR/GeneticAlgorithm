using UnityEngine;

namespace EmreBeratKR.GeneticAlgorithm
{
    public class CubeEntitySimulator : MonoBehaviour
    {
        private const string PrefabsPath = "";


        [Header("References")]
        [SerializeField] private Transform spawnPointTransform;
        [SerializeField] private Transform targetTransform;
        
        [Header("Values")]
        [SerializeField, Range(1, 1000)] private int capacity;


        private static CubeEntitySimulator Instance => ms_Instance ??= FindObjectOfType<CubeEntitySimulator>();


        private Vector3 SpawnPosition => spawnPointTransform.position;
        private Vector3 TargetPosition => targetTransform.position;
        private IEntity EntityPrefab => m_EntityPrefab ??= ExtendedResources.LoadAll<IEntity>(PrefabsPath)[0];


        private static CubeEntitySimulator ms_Instance;
        
        
        private readonly Population<IEntity> m_Population = new();
        private IEntity m_EntityPrefab;


        private void Start()
        {
            StartSimulation();
        }

        private void Update()
        {
            TickPopulation(Time.deltaTime);
            TryPassNextGeneration();
        }


        public static float GetSqrDistanceToTarget(Vector3 position)
        {
            var delta = Instance.TargetPosition - position;
            delta.y = 0f;
            return delta.sqrMagnitude;
        }
        

        private void StartSimulation()
        {
            SpawnInitialEntities();
        }

        private void TickPopulation(float deltaTime)
        {
            m_Population.Tick(deltaTime);
        }
        
        private bool TryPassNextGeneration()
        {
            if (m_Population.HasAliveEntity) return false;

            m_Population.PassNextGeneration();
            return true;
        }

        private void SpawnInitialEntities()
        {
            var prefab = EntityPrefab;
            
            for (var i = 0; i < capacity; i++)
            {
                var newEntity = (IEntity) prefab.Clone();
                newEntity.Initialize(SpawnPosition);
                m_Population.Add(newEntity);
            }
        }
    }
}
