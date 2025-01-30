using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Manager.Objects {
    
    public static partial class ObjectManager {

        public static class IdleMarks {
            
            public static GameObject Find(string identifier) {
                var marksParent = Root.transform.Find(IdleMarkObjectPath);
                if (marksParent is null) {
                    Debug.LogWarning($"[ObjectManager] Object not found! Rooms object parent:\"{IdleMarkObjectPath}\" doesn't exist!");
                    return null;
                }
                var markObject = marksParent.transform.Find(identifier);
                if (markObject is null) {
                    Debug.LogWarning($"[ObjectManager] Object not found! Room object:\"{IdleMarkObjectPath + identifier}\" doesn't exist!");
                    return null;
                }
                return markObject.gameObject;
            }

            public static GameObject[] List() {
                var marksParent = Root.transform.Find(IdleMarkObjectPath);
                if (marksParent is null) {
                    Debug.LogWarning($"[ObjectManager] Objects not found! Rooms object parent:\"{IdleMarkObjectPath}\" doesn't exist!");
                    return null;
                }
                var list = new List<GameObject>();
                for (var i = 0; i < marksParent.childCount; ++i) {
                    list.Add(marksParent.gameObject.transform.gameObject);
                }
                if (list.Count == 0) {
                    Debug.LogWarning($"[ObjectManager] Rooms parent object exist, but it has no children.");
                }
                return list.ToArray();
            }
        }
    }
}