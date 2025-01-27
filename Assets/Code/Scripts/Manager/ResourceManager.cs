using System.Collections.Generic;
using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.Manager {
    
    public class ResourceManager {

        public static ResourceManager Instance { get; private set; } = new();
        
        // The ambiguity of naming here is just for the sake of simplicity in
        // configuration writing
        private abstract class MonsterConfig {
            
            public string Name;

            public struct Node {
                public string Room;
                public float Stay;
            }

            public Node[] Path;

            public string Spawn;
            
            public string Destination;

        }

        // Load monster config files to specific monster objects.
        // (monster config is different from monster entity, definitions are above)
        public void LoadMonsters() {
            var configAssets = Resources.LoadAll<TextAsset>(Constants.Resources.MonstersPath);
            foreach (var configAsset in configAssets) {
                var monsterConfig = JsonUtility.FromJson<MonsterConfig>(configAsset.text);
                var monster = new Monster();
                foreach (var node in monsterConfig.Path) {
                    // ! monster.OriginalPath.Add(RoomManager.Instance.Find(node.Room));
                }
                monster.CurrentPath = monster.OriginalPath;
                // ! monster.CurrentLocation = RoomManager.Instance.Find(monsterConfig.Spawn);
                GameContext.Instance.Monsters.Add(monster);
            }

        }

        // Load room config files to specific room objects, and store corresponding 
        // game object to context.
        public void LoadRooms() {
            var configAssets = Resources.LoadAll<TextAsset>(Constants.Resources.RoomsPath);
            foreach (var configAsset in configAssets) {
                var room = JsonUtility.FromJson<Room>(configAsset.text);
                GameContext.Instance.Rooms.Add(room);
                var roomObject = GameContext.Instance.RootObject.transform.Find(Constants.GameObject.RoomsPath + room.Index).gameObject;
                GameContext.Instance.RoomObjects.Add(roomObject);
            }
        }
        
        public void LoadAudio() {
            
        }

    }
}