using System.Collections;
using System.Collections.Generic;

namespace EmreBeratKR.GeneticAlgorithm
{
    public class Population<T> : IEnumerable<T>
        where T : IEntity
    {
        private readonly List<T> m_Entities = new();


        public T this[int index]
        {
            get => m_Entities[index];
            set => m_Entities[index] = value;
        }
        
        
        public void Add(T entity)
        {
            m_Entities.Add(entity);
        }

        public void Remove(T entity)
        {
            m_Entities.Remove(entity);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}