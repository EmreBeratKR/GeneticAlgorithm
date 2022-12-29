using UnityEngine;

namespace EmreBeratKR.GeneticAlgorithm
{
    public class KillerObstacle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IKillable killable))
            {
                killable.Kill();
            }
        }
    }
}