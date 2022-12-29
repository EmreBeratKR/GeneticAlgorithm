using UnityEngine;

namespace EmreBeratKR.GeneticAlgorithm
{
    public class EntityTarget : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IEntity entity))
            {
                entity.Succeed();
            }
        }
    }
}