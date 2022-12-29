using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public static class ExtendedResources
    {
        public static T Load<T>(string path)
        {
            var resource = Resources.Load<Component>(path);

            if (resource.TryGetComponent(out T target))
            {
                return target;
            }
            
            throw new InvalidCastException($"Target is NOT convertible to {typeof(T).Name}");
        }
        
        public static T[] LoadAll<T>(string path)
        {
            var resources = Resources.LoadAll<Component>(path);
            var targets = new List<T>();

            foreach (var resource in resources)
            {
                if (!resource.TryGetComponent(out T target)) continue;
                
                targets.Add(target);
            }

            return targets.ToArray();
        }
    }
}