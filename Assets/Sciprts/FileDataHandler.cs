using System.IO;
using UnityEngine;

public class FileDataHandler 
{

    private string dataDirPath = "";
    private string dataFileName = "";


    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        
        GameData loadedData = null;
        if(File.Exists(fullPath))
        {
            try 
            {
                string dataToLoad = "";
                using(FileStream fs = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader sr = new StreamReader(fs))
                    {
                        dataToLoad = sr.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            } 
            catch
            {
                Debug.Log("File save error");
            }
        }
        return loadedData;
    }

    public void Save(GameData data) 
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(data, true);

            using(FileStream fs = new FileStream(fullPath, FileMode.Create))
            {
                using(StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(dataToStore);
                }
            }
        }
        catch 
        {
            Debug.Log("File load error");
        }
    }
}
