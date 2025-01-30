using System;

namespace Code.Scripts.Data {
    
    // The ambiguity of naming here is just for the sake of simplicity in
    // configuration writing
    [Serializable]
    public class MonsterData {
        public string name;
        public string identifier;
        
        [Serializable]
        public struct Node {
            public string room;
            public float stay;
        }
        
        public Node[] path;
        public string spawn;
    }
}