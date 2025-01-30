using System.Collections.Generic;
using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.Manager.Audios {
    
    public static class AudioManager {
        

        
        private static Dictionary<string, AudioData> _audioDict = new();
        
        public static void Register(string name, GameObject source, AudioData audioData) {
             
        }       
        
        public static void Play(string name, AudioSettings settings) {
            var audio = _audioDict[name];
            // audio.source.clip = 
            //     audio.randomSettings.enable ?
            //         audio.clips[Random.Range(0, audio.clips.Length)] : 
            //         audio.clips[0];
            // audio.source.Play();
        }
    }
}
