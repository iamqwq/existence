using Code.Scripts.Manager.Events;
using UnityEngine;
using Event = Code.Scripts.Manager.Events.Event;

namespace Code.Scripts.Test {
    
    public class EventManagerTest : MonoBehaviour {

        private class TestEvent : Event {

            public class TestEventArgs : EventArgs {
                public string Message;
            }
            
            public new TestEventArgs Args;
        }

        
        private class TestEventHandlerA : IEventHandler {
            
            public void Handle(Event e) {
                 Debug.Log($"HandlerA: {((TestEvent) e).Args.Message}");
            }
        }
        
         private class TestEventHandlerB : IEventHandler {
             
             public void Handle(Event e) {
                 Debug.Log($"HandlerB: {((TestEvent) e).Args.Message}");
             }
         }
         
        private void Start() {
            Debug.Log("---------- EventManagerTest Start ----------");
            
            EventManager.Instance.Register(typeof(TestEvent), new TestEventHandlerA());
            EventManager.Instance.Register(typeof(TestEvent), new TestEventHandlerB());
            EventManager.Instance.Publish(new TestEvent { Args = new TestEvent.TestEventArgs { Message = "foo, bar"} });
            
            Debug.Log("---------- EventManagerTest End ----------");
        }

    }
}