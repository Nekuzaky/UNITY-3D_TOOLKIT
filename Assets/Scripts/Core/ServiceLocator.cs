using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Core
{
    /// <summary>
    /// Service locator. Register and resolve services by interface or type.
    /// Singleton alternative when you want to swap an implementation for tests.
    /// </summary>
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _serviceDict = new Dictionary<Type, object>();

        public static void Register<T>(T service) where T : class
        {
            Debug.Assert(service != null, "[ServiceLocator.Register] service is null"); // R5
            if (service == null) return;
            _serviceDict[typeof(T)] = service;
        }

        public static T Get<T>() where T : class
        {
            return _serviceDict.TryGetValue(typeof(T), out var s) ? s as T : null;
        }

        public static bool TryGet<T>(out T service) where T : class
        {
            if (_serviceDict.TryGetValue(typeof(T), out var s) && s is T t) { service = t; return true; }
            service = null;
            return false;
        }

        public static void Unregister<T>() where T : class => _serviceDict.Remove(typeof(T));

        public static void Clear() => _serviceDict.Clear();
    }
}
