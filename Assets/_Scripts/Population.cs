using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace EmreBeratKR.GeneticAlgorithm
{
    public class Population<T> : IEnumerable<T>
        where T : IEntity
    {
        private readonly SelectionType m_SelectionType;

        
        private List<T> m_Entities;

        
        public T this[int index]
        {
            get => m_Entities[index];
            set => m_Entities[index] = value;
        }
        public T BestEntity
        {
            get
            {
                var currentBestEntity = m_Entities[0];
                var currentBestFitness = currentBestEntity.Fitness;

                for (var i = 1; i < m_Entities.Count; i++)
                {
                    var entity = m_Entities[i];
                    var fitness = entity.Fitness;
                    
                    if (fitness <= currentBestFitness) continue;

                    currentBestEntity = entity;
                    currentBestFitness = fitness;
                }

                return currentBestEntity;
            }
        }
        public float AverageFitness => TotalFitness / EntityCount;
        public int EntityCount => m_Entities.Count;
        public int GenerationNumber { get; private set; } = 1;

        public bool HasActiveEntity
        {
            get
            {
                foreach (var entity in m_Entities)
                {
                    if (entity.IsAlive && !entity.IsSucceed) return true;
                }

                return false;
            }
        }
        public bool IsInitialized { get; private set; }


        private float TotalFitness
        {
            get
            {
                var result = 0f;

                foreach (var entity in m_Entities)
                {
                    result += entity.Fitness;
                }

                return result;
            }
        }
        

        public Population(SelectionType selectionType)
        {
            this.m_SelectionType = selectionType;
            m_Entities = new List<T>();
        }
        

        public void Initialize()
        {
            foreach (var entity in m_Entities)
            {
                entity.Initialize();
            }

            IsInitialized = true;
        }
        
        public void Add(T entity)
        {
            m_Entities.Add(entity);
        }

        public void Remove(T entity)
        {
            m_Entities.Remove(entity);
        }

        public void Tick(float deltaTime)
        {
            foreach (var entity in m_Entities)
            {
                entity.Tick(deltaTime);
            }
        }

        public void PassNextGeneration()
        {
            IsInitialized = false;
            
            Debug.Log($"{GenerationNumber} -> {GenerationNumber + 1}");
            GenerationNumber += 1;
            ApplySelection();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
        
        private void Clear()
        {
            foreach (var entity in m_Entities)
            {
                entity.Destroy();
            }
            
            m_Entities.Clear();
        }
        
        private void ApplySelection()
        {
            Debug.Log(TotalFitness);
            
            if (m_SelectionType == SelectionType.Natural)
            {
                ApplyNaturalSelection();
                return;
            }
            
            ApplyArtificialSelection();
        }

        private void ApplyNaturalSelection()
        {
            LazyCoroutines.StartCoroutine(Routine());

            IEnumerator Routine()
            {
                var newEntities = new List<T>();

                for (var i = 0; i < m_Entities.Count; i++)
                {
                    var parent = GetParentByNaturalSelection();
                    var newEntity = (T) parent.Clone();
                    newEntity.Mutate();
                    newEntities.Add(newEntity);

                    if (i % CubeEntitySimulator.MaxSpawnCountPerFrame == 0) yield return null;
                }
            
                Clear();
                m_Entities = newEntities;
            
                Initialize();
            }
        }

        private void ApplyArtificialSelection()
        {
            
        }

        private T GetParentByNaturalSelection()
        {
            var randomNumber = Random.Range(0f, TotalFitness);
            var fitnessPointer = 0f;

            foreach (var entity in m_Entities)
            {
                fitnessPointer += entity.Fitness;
                
                if (randomNumber > fitnessPointer) continue;
                
                return entity;
            }

            return BestEntity;
        }
    }
    
    
    public enum SelectionType
    {
        Natural,
        Artificial
    }
}