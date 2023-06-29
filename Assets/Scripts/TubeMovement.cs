using UnityEngine;

namespace Scripts
{
    public class TubeMovement : MonoBehaviour
    {
        [SerializeField] private float[] tubesSpeed;

        private DataManager _dataManager;
        private void Awake()
        {
            _dataManager = FindObjectOfType<DataManager>();
        }

        private void Update()
        {
            transform.Translate(-tubesSpeed[_dataManager.gameData.GameDificulty] * Time.deltaTime, 0, 0);
        }
    }
}
