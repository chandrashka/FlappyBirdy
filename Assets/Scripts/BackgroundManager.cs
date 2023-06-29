using System.Collections.Generic;
using UnityEngine;

namespace Scripts
{
    public class BackgroundManager : MonoBehaviour
    {
        [Header("Background")]
        [SerializeField]
        private BackgroundSo[] backgroundSo = new BackgroundSo[4];
        private readonly GameObject[] _backgroundLayers = new GameObject[5];
        private readonly GameObject[] _backgroundLayersNew = new GameObject[5];
        private Sprite[] _sprites;
        int _currentBackgroundNum;

        [Header("Coordinates")]
        private const int YCoord = 0, ZCoord = 0, XCoordStart = 14, XCoordDelete = -15, XCoordFirsrLayer = 7;
        private const float XCoordEnd = -9;

        [Header("Speed")]
        [SerializeField] private float[] layoutSpeed = new float[5];

        private void Awake()
        {
            _currentBackgroundNum = Random.Range(0,4);
            AddBackground();
        }
        // Update is called once per frame
        private void Update()
        {
            if(Time.timeScale != 0)
            {
                MoveBackground();
            }   
        }
        private void AddBackground()
        {
            _sprites = backgroundSo[_currentBackgroundNum].GetBackground();
            for(int i = 0; i < _backgroundLayers.Length; i++)
            {
                AddLayer(i, XCoordFirsrLayer, _backgroundLayers);
            }
        }
        private void AddLayer(int layer, int xCoord, IList<GameObject> layers)
        {
            layers[layer] = Instantiate(new GameObject());
            layers[layer].AddComponent<SpriteRenderer>();

            SpriteRenderer spriteRenderer = layers[layer].GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = _sprites[layer];
            spriteRenderer.sortingOrder = -100 + layer;

            layers[layer].transform.Translate(xCoord, YCoord, 0);
        }
    
        private void MoveBackground()
        {
            for(int i = 0; i < _backgroundLayers.Length; i++)
            {
                if (_backgroundLayers[i].transform.position.x <= XCoordDelete)
                {
                    Destroy(_backgroundLayers[i]);

                    _backgroundLayers[i] = _backgroundLayersNew[i];

                    _backgroundLayersNew[i] = null;  
                }
                else if (_backgroundLayersNew[i]== null && _backgroundLayers[i].transform.position.x <= XCoordEnd)
                {
                    AddLayer(i, XCoordStart, _backgroundLayersNew);
                }
                else 
                {
                    _backgroundLayers[i].transform.Translate(-layoutSpeed[i] * Time.deltaTime, YCoord, ZCoord);
                    if (_backgroundLayersNew[i] != null)
                    {
                        _backgroundLayersNew[i].transform.Translate(-layoutSpeed[i] * Time.deltaTime, YCoord, ZCoord);
                    }
                }                  
            }
        }
    }
}
