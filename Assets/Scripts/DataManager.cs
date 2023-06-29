using UnityEngine;

namespace Scripts
{
    public class DataManager : MonoBehaviour

    {
        [SerializeField] private string fileName;

        public GameData gameData;
        public static DataManager Instance { get; private set; }

        private FileDataHandler _fileHandler;

        private void Awake()
        {
            Instance = this;

            _fileHandler = new FileDataHandler(Application.persistentDataPath, fileName);

            LoadGame();
        }

 
        public GameData GetGameData()
        {
            return gameData;
        }

        private void NewGame()
        {
            gameData = new GameData();
        }

        private void LoadGame()
        {
            gameData = _fileHandler.Load();
            if(gameData == null)
            {
                NewGame();
            }
        }

        public void SaveGame()
        {
            if(gameData != null) { 
                _fileHandler.Save(gameData);
            }
        
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }
    }
}
