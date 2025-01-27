using System;
using System.Collections;
using System.Collections.Generic;
using Code.Scripts.Manager.Events;
using UnityEngine;

namespace Code.Scripts.Entity {
    
    public class Monster {

        public List<Room> OriginalPath { get; private set; }
        public List<Room> CurrentPath;
        public int CurrentPathIndex;
        public Room CurrentLocation;

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

            while (!CurrentLocation.Name.Equals("Monitoring Room")) {
                CurrentLocation = CurrentPath[CurrentPathIndex++];
                // EventManager.Instance.Publish();
                Debug.Log($"Now in {CurrentPath[CurrentPathIndex].Name}.");
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