using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamToolkit.Core
{
    /// <summary>
    /// Strongly-typed static event bus. All cross-module communication must go
    /// through this class to avoid direct coupling. Events are structs or classes
    /// defined under Core/Events.
    /// </summary>
    public static class EventBus
    {
        private static readonly Dictionary<Type, Delegate> _subscribersDict = new Dictionary<Type, Delegate>();

        /// <summary>Subscribe to an event of type T.</summary>
        public static void Subscribe<T>(Action<T> callback) where T : struct
        {
            Debug.Assert(callback != null, "[EventBus.Subscribe] callback is null"); // R5
            if (callback == null) return;

            var type = typeof(T);
            if (_subscribersDict.TryGetValue(type, out var existing))
            {
                _subscribersDict[type] = Delegate.Combine(existing, callback);
            }
            else
            {
                _subscribersDict[type] = callback;
            }
        }

        /// <summary>Unsubscribe. Always call from OnDestroy / OnDisable.</summary>
        public static void Unsubscribe<T>(Action<T> callback) where T : struct
        {
            Debug.Assert(callback != null, "[EventBus.Unsubscribe] callback is null"); // R5
            if (callback == null) return;

            var type = typeof(T);
            if (!_subscribersDict.TryGetValue(type, out var existing)) return;

            var newDel = Delegate.Remove(existing, callback);
            if (newDel == null) _subscribersDict.Remove(type);
            else _subscribersDict[type] = newDel;
        }

        /// <summary>Publish an event. All subscribers are invoked synchronously.</summary>
        public static void Publish<T>(T evt) where T : struct
        {
            var type = typeof(T);
            if (!_subscribersDict.TryGetValue(type, out var del)) return;
            if (del is Action<T> action) action.Invoke(evt);
        }

        /// <summary>Clear all subscriptions (global scene change, tests).</summary>
        public static void Clear()
        {
            _subscribersDict.Clear();
        }

        /// <summary>Number of active subscribers for T (debug and tests).</summary>
        public static int CountSubscribers<T>() where T : struct
        {
            if (!_subscribersDict.TryGetValue(typeof(T), out var del)) return 0;
            return del.GetInvocationList().Length;
        }
    }
}
