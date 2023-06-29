using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [Header("Background")]
    [SerializeField] BackgroundSO[] BackgroundSO = new BackgroundSO[4];
    private GameObject[] BackgroundLayers = new GameObject[5];
    private GameObject[] BackgroundLayersNew = new GameObject[5];
    private Sprite[] sprites;
    int currentBackgroundNum;

    [Header("Coordinates")]
    private int YCoord = 0, ZCoord = 0, XCoordStart = 14, XCoordDelete = -15, XCoordFirsrLayer = 7;
    private float XCoordEnd = -9;

    [Header("Speed")]
    [SerializeField] float[] layountSpeed = new float[5];

    void Awake()
    {
        currentBackgroundNum = Random.Range(0,4);
        AddBackground();
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale != 0)
        {
            MoveBackground();
        }   
    }
    private void AddBackground()
    {
        sprites = BackgroundSO[currentBackgroundNum].GetBackground();
        for(int i = 0; i < BackgroundLayers.Length; i++)
        {
            AddLayer(i, XCoordFirsrLayer, BackgroundLayers);
        }
    }
    private void AddLayer(int layer, int XCoord, GameObject[] layers)
    {
        layers[layer] = Instantiate(new GameObject());
        layers[layer].AddComponent<SpriteRenderer>();

        SpriteRenderer spriteRenderer = layers[layer].GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[layer];
        spriteRenderer.sortingOrder = -100 + layer;

        layers[layer].transform.Translate(XCoord, YCoord, 0);
    }
    
    private void MoveBackground()
    {
        for(int i = 0; i < BackgroundLayers.Length; i++)
        {
            if (BackgroundLayers[i].transform.position.x <= XCoordDelete)
            {
                Destroy(BackgroundLayers[i]);

                BackgroundLayers[i] = BackgroundLayersNew[i];

                BackgroundLayersNew[i] = null;  
            }
            else if (BackgroundLayersNew[i]== null && BackgroundLayers[i].transform.position.x <= XCoordEnd)
            {
                AddLayer(i, XCoordStart, BackgroundLayersNew);
            }
            else 
            {
                BackgroundLayers[i].transform.Translate(-layountSpeed[i] * Time.deltaTime, YCoord, ZCoord);
                if (BackgroundLayersNew[i] != null)
                {
                    BackgroundLayersNew[i].transform.Translate(-layountSpeed[i] * Time.deltaTime, YCoord, ZCoord);
                }
            }                  
        }
    }
}
