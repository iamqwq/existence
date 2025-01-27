using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Manager.Events {

    public class EventManager {

        public static EventManager Instance { get; private set; } = new();

        private Dictionary<Type, List<IEventHandler>> _handlerDict = new();

        public void Register(Type e, IEventHandler handler) {
            if (!_handlerDict.ContainsKey(e)) {
                _handlerDict.Add(e, new List<IEventHandler>());
            }
            _handlerDict[e].Add(handler);
        }

        public void Publish(Event e) {
            if (_handlerDict[e.GetType()] is null) {
                Debug.LogWarning(
                    $"[EventManager] No response for published event \"{e}\"! " +
                    $"Because no listener was registered for this event."
                );
                return;
            }
            foreach (var handler in _handlerDict[e.GetType()]) {
                handler.Handle(e);
            }
            // Debug.LogWarning(
            //     "[EventManager] Publish failed! Because args type could not be matched. " +
            //     $"Event {e} expected {_handlerDict[e].GetType()} but parameter was {args.GetType()}"
            // );
        }
        
    }
}
