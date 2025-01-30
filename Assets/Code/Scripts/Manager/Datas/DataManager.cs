namespace Code.Scripts.Manager.Datas {
    
    public static partial class DataManager {
        
        public static void Init() {
            ConfigManager.LoadAll();
        }
    }
}