using Code.Scripts.Manager.Datas;
using Code.Scripts.Manager.Objects;
using UnityEngine;

namespace Code.Scripts {
    
    public class GameContext : MonoBehaviour {

        public static GameContext Instance { get; private set; }

        private void Awake() {
            Instance = this;
            DataManager.Init();
            ObjectManager.Init();
        }
    }
}
