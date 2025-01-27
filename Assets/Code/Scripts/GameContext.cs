using System.Collections.Generic;
using Code.Scripts.Entity;
using Code.Scripts.Manager;
using UnityEngine;

namespace Code.Scripts {
    
    public class GameContext {

        public static GameContext Instance { get; private set; }  = new();

        // Entity data
        // ! List<Room> Here should be Graph<Room>
        public List<Room> Rooms = new();
        
        public List<Monster> Monsters = new();
        
        // In-Game objects
        public GameObject RootObject;
        
        public List<GameObject> RoomObjects;
        
        

    }
}
