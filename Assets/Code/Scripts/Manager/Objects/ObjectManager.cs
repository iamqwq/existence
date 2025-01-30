using UnityEngine;

namespace Code.Scripts.Manager.Objects {
    
    public static partial class ObjectManager {

        private const string IdleMarkObjectPath = "IdleMarks";
        private const string MonsterObjectPath = "Monsters";
        
        public static GameObject Root;

        public static void Init() {
            Root = GameContext.Instance.gameObject;
        }
        
    }
}