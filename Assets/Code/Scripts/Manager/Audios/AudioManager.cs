using System.Collections.Generic;
using Code.Scripts.Entity;
using UnityEngine;

namespace Code.Scripts.Manager.Audios {
    
    public class AudioManager {
        
        public static AudioManager Instance { get; private set; } = new();

        public void Register(string name, GameObject source, Audio audio) {
            
        }
        
        private Dictionary<string, Audio> _audioDict = new();
        
        public void Play(string name, AudioSettings settings) {
            var audio = _audioDict[name];
            // audio.source.clip = 
            //     audio.randomSettings.enable ?
            //         audio.clips[Random.Range(0, audio.clips.Length)] : 
            //         audio.clips[0];
            // audio.source.Play();
        }
    }
}
