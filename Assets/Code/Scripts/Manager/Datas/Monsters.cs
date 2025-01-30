using System.Collections.Generic;
using Code.Scripts.Data;
using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.Manager.Datas {

    public partial class DataManager {
        
        public static class Monsters {
                
            private static readonly Dictionary<string, MonsterData> MonsterDict = new();
            // private static readonly List<MonsterData> Monsters = new();
    
            public static void Register(MonsterData monsterData) {
                if (monsterData is null) {
                    Debug.LogWarning($"[Monster Manager] Register failed! Because the monster being registered is null.");
                    return;
                }
                if (monsterData.identifier is null) {
                    Debug.LogWarning($"[Monster Manager] Register failed! Because the identifier of monster being registered is null.");
                    return;
                }
                Debug.Log(monsterData.identifier);
                MonsterDict.Add(monsterData.identifier, monsterData);
                // Monsters.Add(monsterData);
            }
            
            public static MonsterData Find(string identifier) {
                 if (MonsterDict.TryGetValue(identifier, out var monster)) {
                     return monster;
                 }
                 Debug.LogWarning($"[Monster Manager] Could not find monster identified as \"{identifier}\".");
                 return null;           
            }
                
        }    
    }
    
}