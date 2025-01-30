using System.Collections.Generic;
using Code.Scripts.Common;
using Code.Scripts.Data;
using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.Manager.Datas {
    
    public partial class DataManager {
        
        public static class Rooms {
    
            private static readonly Dictionary<string, RoomData> RoomDict = new();
            private static readonly Graph RoomGraph = new();
    
            public static void Register(RoomData roomData) {
                if (roomData is null) {
                    Debug.LogWarning($"[DataManager] Register failed! Because the roomData being registered is null.");
                    return;
                }
                if (roomData.index is null) {
                    Debug.LogWarning($"[DataManager] Register failed! Because the index of roomData being registered is null.");
                    return;
                }
                RoomDict.Add(roomData.index, roomData);
                RoomGraph.Add(roomData);
            }
    
            public static RoomData Find(string index) {
                if (RoomDict.TryGetValue(index, out var room)) {
                    return room;
                }
                Debug.LogWarning($"[DataManager] Could not find room indexed as \"{index}\".");
                return null;
            }
            
            public static List<RoomData> Navigate(RoomData source, RoomData destination) {
                return RoomGraph.Method(source, new List<RoomData> { destination });
            }
    
            public static List<RoomData> Navigate(RoomData source, List<RoomData> path) {
                return RoomGraph.Method(source, path);
            }
            
        }    
    }
    
}