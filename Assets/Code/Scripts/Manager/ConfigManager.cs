using Code.Scripts.Data;
using Code.Scripts.Manager.Datas;
using UnityEngine;

namespace Code.Scripts.Manager {
    
    public static class ConfigManager {

        private const string RoomConfigPath = "Data/Rooms";
        private const string MonsterConfigPath = "Data/Monsters";

        public static void LoadAll() {
            LoadRooms();
            LoadMonsters();
        }
        
        private static void LoadMonsters() {
            var configAssets = Resources.LoadAll<TextAsset>(MonsterConfigPath);
            foreach (var configAsset in configAssets) {
                var monsterConfig = JsonUtility.FromJson<MonsterData>(configAsset.text);
                DataManager.Monsters.Register(monsterConfig);
            }
        }

        private static void LoadRooms() {
            var configAssets = Resources.LoadAll<TextAsset>(RoomConfigPath);
            foreach (var configAsset in configAssets) {
                var room = JsonUtility.FromJson<RoomData>(configAsset.text);
                DataManager.Rooms.Register(room);
            }
        }
        
    }
}