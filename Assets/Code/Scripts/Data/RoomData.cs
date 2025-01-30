using System;

namespace Code.Scripts.Data {
    
    [Serializable]
    public class RoomData {
        // info and identity
        public string name;
        public string index;
        // data
        public string[] connected;
    }
}