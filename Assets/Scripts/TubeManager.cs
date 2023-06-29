using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scripts
{
    public class TubeManager : MonoBehaviour
    {
        [FormerlySerializedAs("BottomTubePrefab")]
        [Header("Tubes Prefabs")]
        [SerializeField] private GameObject bottomTubePrefab;
        [SerializeField] private GameObject topTubePrefab;
        
        private DataManager _dataManager;

        [Header("Tube parameters")]
        [SerializeField] private float[] tubesSpace;

        private int _gameDifficulty;
        public bool isGameStarted;

        private const int NewTubeX = 4;
        private const int FirstTubeX = 4;
        private const int BottomTubeYMin = -3;

        private int _tubeNum;
        private readonly GameObject[][] _tubes = new GameObject[3][];
        private bool _isFirstTube = true;

        private void Awake()
        {
            _dataManager = FindObjectOfType<DataManager>();
            _gameDifficulty = _dataManager.gameData.GameDificulty;
        }
        public void StartTheGame()
        {
            isGameStarted = true;
        }

        private void Update()
        {
            if (isGameStarted)
            {
                UpdateTubes();
            }       
        }

        private void UpdateTubes()
        {
            _gameDifficulty = _dataManager.gameData.GameDificulty;
            if (_tubes[0] == null)
            {
                _tubes[0] = CreateTube();
            }
            if (_tubes[0] != null && _tubes[0][0].transform.position.x <= 0 && _tubeNum == 1)
            {
                _tubes[1] = CreateTube();
            }
            else if (_tubes[1] != null && _tubes[1][0].transform.position.x <= 0 && _tubeNum == 2)
            {
                _tubes[2] = CreateTube();
            }
            else if (_tubes[0][0].transform.position.x <= -7)
            {
                DestroyTube(_tubes[0]);
                _tubes[0] = _tubes[1];
                _tubes[1] = _tubes[2];
                _tubes[2] = null;
            }
        }

        private GameObject[] CreateTube()
        {
            var bottomNewTubeY = Random.Range(BottomTubeYMin, 3);
            var topNewTubeY = bottomNewTubeY + tubesSpace[_gameDifficulty];
        

            var bottomNewTube = Instantiate(bottomTubePrefab);
            var topNewTube = Instantiate(topTubePrefab);
            GameObject[] tube = {bottomNewTube, topNewTube};

            if (_isFirstTube)
            {
                tube[0].transform.Translate(FirstTubeX, bottomNewTubeY, 0);
                tube[1].transform.Translate(FirstTubeX, bottomNewTubeY + tubesSpace[_gameDifficulty], 0);
                _isFirstTube = false;
            }
            else
            {
                tube[0].transform.Translate(NewTubeX, bottomNewTubeY, 0);
                tube[1].transform.Translate(NewTubeX, topNewTubeY, 0);
            }
            _tubeNum++;

            return tube;
        }

        public void DeleteCurrentTube()
        {
            if (_tubes[1] == null)
            {
                DestroyTube(_tubes[0]);
                _tubes[0] = null;
            }
            else if (_tubes[2] == null)
            {
                DestroyTube(_tubes[1]);
                _tubes[1] = null;
            }
            else
            {
                DestroyTube(_tubes[2]);
                _tubes[2] = null;
            }
        }

        private void DestroyTube(IReadOnlyList<GameObject> tube)
        {
            Destroy(tube[0]);
            Destroy(tube[1]);
            _tubeNum--;
        }
    }
}
