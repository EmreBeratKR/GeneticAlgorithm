using UnityEngine;

namespace EmreBeratKR.GeneticAlgorithm
{
    public class EntitySimulator : MonoBehaviour
    {
        private const string PrefabsPath = "";


        [SerializeField, Range(1, 1000)] private int capacity;
        
        
        private IEntity EntityPrefab => m_EntityPrefab ??= ExtendedResources.LoadAll<IEntity>(PrefabsPath)[0];


        private readonly Population<IEntity> m_Population = new();
        private IEntity m_EntityPrefab;


        private void Start()
        {
            StartSimulation();
        }

        private void Update()
        {
            foreach (var entity in m_Population)
            {
                entity.Tick(Time.deltaTime);
            }
        }


        private void StartSimulation()
        {
            SpawnInitialEntities();
        }

        private void SpawnInitialEntities()
        {
            var prefab = EntityPrefab;
            
            for (var i = 0; i < capacity; i++)
            {
                var newEntity = (IEntity) prefab.Clone();
                newEntity.Initialize();
                m_Population.Add(newEntity);
            }
        }
    }
}
