using System.Collections;
using UnityEngine;
using Utils;

namespace EmreBeratKR.GeneticAlgorithm
{
    public class CubeEntitySimulator : MonoBehaviour
    {
        public const int MaxSpawnCountPerFrame = 100;
        
        
        private const string PrefabsPath = "";


        [Header("References")]
        [SerializeField] private Transform spawnPointTransform;
        [SerializeField] private Transform targetTransform;
        
        [Header("Values")]
        [SerializeField, Range(1, 1000)] private int capacity;
        [SerializeField] private SelectionType selectionType;

        
        public static Vector3 SpawnPosition => Instance.spawnPointTransform.position;

        
        private static CubeEntitySimulator Instance => ms_Instance ??= FindObjectOfType<CubeEntitySimulator>();


        private Vector3 TargetPosition => targetTransform.position;
        private IEntity EntityPrefab => m_EntityPrefab ??= ExtendedResources.LoadAll<IEntity>(PrefabsPath)[0];


        private static CubeEntitySimulator ms_Instance;


        private Population<IEntity> m_Population;
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


        public static float GetDistanceToTarget(Vector3 position)
        {
            var delta = Instance.TargetPosition - position;
            delta.y = 0f;
            return delta.magnitude;
        }
        

        private void StartSimulation()
        {
            SpawnInitialPopulation();
        }

        private void TickPopulation(float deltaTime)
        {
            m_Population.Tick(deltaTime);
        }
        
        private bool TryPassNextGeneration()
        {
            if (!m_Population.IsInitialized) return false;
            
            if (m_Population.HasActiveEntity) return false;

            m_Population.PassNextGeneration();
            return true;
        }

        private void SpawnInitialPopulation()
        {
            StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                m_Population = new Population<IEntity>(selectionType);
                var prefab = EntityPrefab;
                
                for (var i = 0; i < capacity; i++)
                {
                    var newEntity = (IEntity) prefab.Clone();
                    m_Population.Add(newEntity);

                    if (i % MaxSpawnCountPerFrame == 0) yield return null;
                }
                
                m_Population.Initialize();
            }
        }
    }
}
