using UnityEngine;

public class TubeMovement : MonoBehaviour
{
    [SerializeField] float[] tubesSpeed;

    private DataManager dataManager;
    private void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
    }
    void Update()
    {
        transform.Translate(-tubesSpeed[dataManager.gameData.GameDificulty] * Time.deltaTime, 0, 0);
    }
}
