using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Utils
{
    public static class LazyCoroutines
    {
        private const string BehaviourObjectName = "[Lazy Coroutines]";
        
        
        private static LazyCoroutineBehaviour Behaviour
        {
            get
            {
                if (!ms_Behaviour)
                {
                    ms_Behaviour = new GameObject(BehaviourObjectName)
                        .AddComponent<LazyCoroutineBehaviour>();
                    
                    Object.DontDestroyOnLoad(ms_Behaviour);
                }

                return ms_Behaviour;
            }
        }
        
        
        private static LazyCoroutineBehaviour ms_Behaviour;
        
        
        public static void StartCoroutine(IEnumerator routine)
        {
            Behaviour.StartCoroutine(routine);
        }
        
        public static void WaitForSeconds(float delay, UnityAction action)
        {
            StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                yield return new WaitForSeconds(delay);
                action?.Invoke();
            }
        }
        
        public static void WaitForSecondsRealtime(float delay, UnityAction action)
        {
            StartCoroutine(Routine());
            
            
            IEnumerator Routine()
            {
                yield return new WaitForSecondsRealtime(delay);
                action?.Invoke();
            }
        }




        private class LazyCoroutineBehaviour : MonoBehaviour
        {
            
        }
    }
}