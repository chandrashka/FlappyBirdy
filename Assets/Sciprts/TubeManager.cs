using Newtonsoft.Json.Bson;
using UnityEngine;

public class TubeManager : MonoBehaviour
{
    [Header("Tubes Prefabs")]
    [SerializeField] GameObject BottomTubePrefab;
    [SerializeField] GameObject TopTubePrefab;


    private DataManager dataManager;

    [Header("Tube parametrs")]
    [SerializeField] float[] tubesSpace;

    private int gameDifficulty;
    public bool isGameStarted = false;

    int NewTubeX =  4;
    private readonly int FirstTubeX = 4;
    private readonly int BottomTubeYMin = -3;
    
    private int tubeNum;
    private GameObject[][] tubes = new GameObject[3][];
    private bool IsFirstTube = true;

    private void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
        gameDifficulty = dataManager.gameData.GameDificulty;
    }
    public void StartTheGame()
    {
        isGameStarted = true;
    }
    void Update()
    {
        if (isGameStarted)
        {
            UpdateTubes();
        }       
    }

    private void UpdateTubes()
    {
        gameDifficulty = dataManager.gameData.GameDificulty;
        if (tubes[0] == null)
        {
            tubes[0] = createTube();
        }
        if (tubes[0] != null && tubes[0][0].transform.position.x <= 0 && tubeNum == 1)
        {
            tubes[1] = createTube();
        }
        else if (tubes[1] != null && tubes[1][0].transform.position.x <= 0 && tubeNum == 2)
        {
            tubes[2] = createTube();
        }
        else if (tubes[0][0].transform.position.x <= -7)
        {
            destroyTube(tubes[0]);
            tubes[0] = tubes[1];
            tubes[1] = tubes[2];
            tubes[2] = null;
        }
    }

    private GameObject[] createTube()
    {
        
        int BottomNewTubeY = Random.Range(BottomTubeYMin, 3);
        float TopNewTubeY = BottomNewTubeY + tubesSpace[gameDifficulty];
        

        GameObject BottomNewTube = Instantiate(BottomTubePrefab);
        GameObject TopNewTube = Instantiate(TopTubePrefab);
        GameObject[] tube = {BottomNewTube, TopNewTube};

        if (IsFirstTube)
        {
            Debug.Log(TopNewTubeY);
            tube[0].transform.Translate(FirstTubeX, BottomNewTubeY, 0);
            tube[1].transform.Translate(FirstTubeX, BottomNewTubeY + tubesSpace[gameDifficulty], 0);
            IsFirstTube = false;
        }
        else
        {
            Debug.Log(TopNewTubeY);
            tube[0].transform.Translate(NewTubeX, BottomNewTubeY, 0);
            tube[1].transform.Translate(NewTubeX, TopNewTubeY, 0);
        }
        tubeNum++;

        return tube;
    }

    public void deleteCurrentTube()
    {
        if (tubes[1] == null)
        {
            destroyTube(tubes[0]);
            tubes[0] = null;
        }
        else if (tubes[2] == null)
        {
            destroyTube(tubes[1]);
            tubes[1] = null;
        }
        else
        {
            destroyTube(tubes[2]);
            tubes[2] = null;
        }
    }

    private void destroyTube(GameObject[] tube)
    {
        Destroy(tube[0]);
        Destroy(tube[1]);
        tubeNum--;
    }
}
