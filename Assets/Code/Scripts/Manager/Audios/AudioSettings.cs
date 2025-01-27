namespace Code.Scripts.Manager.Audios {

    public struct AudioSettings {
            
        public struct RandomSettings {
            public bool Enable;
        }
        
        public struct FadeSettings {
            public bool Enable;
            public float FadeInDuration;
            public float FadeOutDuration;
        }
    }
}