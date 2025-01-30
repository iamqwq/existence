using System.Collections.Generic;
using UnityEngine;

namespace Code.Scripts.Manager.Objects {
    
    public static partial class ObjectManager{
        
        public static class Monsters {
            
            public static GameObject Find(string identifier) {
                var monstersParent = Root.transform.Find(MonsterObjectPath);
                if (monstersParent is null) {
                    Debug.LogWarning($"[ObjectManager] Object not found! Monsters object parent:\"{MonsterObjectPath}\" doesn't exist!");
                    return null;
                }
                var monsterObject = monstersParent.transform.Find(identifier);
                if (monsterObject is null) {
                    Debug.LogWarning($"[ObjectManager] Object not found! Monster object:\"{MonsterObjectPath + identifier}\" doesn't exist!");
                    return null;
                }
                return monsterObject.gameObject;
            }
    
            public static GameObject[] List() {
                var monstersParent = Root.transform.Find(IdleMarkObjectPath);
                if (monstersParent is null) {
                    Debug.LogWarning($"[ObjectManager] Objects not found! Monsters object parent:\"{IdleMarkObjectPath}\" doesn't exist!");
                    return null;
                }
                var list = new List<GameObject>();
                for (var i = 0; i < monstersParent.childCount; ++i) {
                    list.Add(monstersParent.gameObject.transform.gameObject);
                }
                if (list.Count == 0) {
                    Debug.LogWarning($"[ObjectManager] Monsters parent object exist, but it has no children.");
                }
                return list.ToArray();
            }        
        }
    }
}