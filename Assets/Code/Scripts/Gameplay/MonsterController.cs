using System;
using System.Collections;
using System.Collections.Generic;
// using System.Runtime.Serialization;
using Code.Scripts.Data;
using Code.Scripts.Manager.Datas;
using Code.Scripts.Manager.Objects;
using UnityEngine;

namespace Code.Scripts.Gameplay {
    public class MonsterController : MonoBehaviour {

        public string identifier;
        private MonsterData _data;
        
        // for moving logic
        private List<RoomData> _originalPath;
        private List<RoomData> _currentPath;
        private int _currentPathIndex;
        private RoomData _currentLocation;

        private void Start() {
            _data = DataManager.Monsters.Find(identifier);
            _currentLocation = DataManager.Rooms.Find(_data.spawn);
            foreach (var node in _data.path) {
                _originalPath ??= new List<RoomData>();
                _originalPath.Add(DataManager.Rooms.Find(node.room));
            }
            _currentPath = _originalPath;
            StartCoroutine(MovingLogic());
        }

        private void Update() {
            
        }
        
        // Monster could be attracted to elsewhere, when reaches the temporary
        // destination, it will move to the node closest to its original path.
        // Specific Logic:
        // 0. Without considering player controls, monster will move in order
        //    of the index in the current path. (current path will be initialized
        //    to original path)
        // 1. When monster is being attracted, replace current path with
        //    the path to the temporary destination.
        // 2. When monster back to original path, call pathfinding algorithm
        //    and locate what index the returned room was in the original path.
        //    Then continue moving from this index.
        private IEnumerator MovingLogic() {
            for (; _currentPathIndex < _currentPath.Count; ++_currentPathIndex) {
                _currentLocation = _currentPath[_currentPathIndex];
                transform.position = ObjectManager.IdleMarks.Find("Room" + _currentLocation.index + "-1").transform.position;
                Debug.Log($"Now in {_currentPath[_currentPathIndex].name}.");
                yield return new WaitForSeconds(5.0f);
            }
            // Publish monster moved event, game context will handle the
            // model mount.
            // Update state machine. (play corresponding animation)
        }

        private void HandleLightsOn() {
            // CurrentPath = Graph.Method();
            // CurrentPathIndex = 0;
        }
    }
}