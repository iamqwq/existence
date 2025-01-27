using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.Test {
    
    public class ConfigIOTest : MonoBehaviour {

        private void Start() {
            Debug.Log("---------- ConfigIOTest Start ----------");
            
            var roomAsset = Resources.Load<TextAsset>(Constants.Resources.RoomsPath + "Example");
            var room = JsonUtility.FromJson<Room>(roomAsset.text);
            Debug.Log($"Room -> Index: {room.Index}, Name: {room.Name}, Connected: {room.Connected}");
            
            // Monster config definition is different from monster entity, couldn't test here.
            
            Debug.Log("---------- ConfigIOTest End ----------");
        }

    }
}