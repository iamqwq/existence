using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Manager.Events {

    public static class EventManager {

        private static readonly Dictionary<Type, List<IEventHandler>> HandlerDict = new();

        public static void Register(Type e, IEventHandler handler) {
            if (!HandlerDict.ContainsKey(e)) {
                HandlerDict.Add(e, new List<IEventHandler>());
            }
            HandlerDict[e].Add(handler);
        }

        public static void Publish(Event e) {
            if (!HandlerDict.ContainsKey(e.GetType())) {
                Debug.LogWarning(
                    $"[EventManager] No response for published event \"{e}\"! " +
                    $"Because no listener was registered for this event."
                );
                return;
            }
            foreach (var handler in HandlerDict[e.GetType()]) {
                handler.Handle(e);
            }
        }
        
    }
}
