using UnityEngine;

public class DataManager : MonoBehaviour

{
    [SerializeField] string fileName;

    public GameData gameData;
    public static DataManager instance { get; private set; }

    private FileDataHandler fileHandler;

    private void Awake()
    {
        instance = this;

        fileHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        LoadGame();
    }

 
    public GameData GetGameData()
    {
        return gameData;
    }
    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = fileHandler.Load();
        if(gameData == null)
        {
            NewGame();
        }
        
    }

    public void SaveGame()
    {
        if(gameData != null) { 
            fileHandler.Save(gameData);
        }
        
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
