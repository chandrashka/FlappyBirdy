using TMPro;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    [Header("Tube")]
    [SerializeField] GameObject tube;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] TextMeshProUGUI textMeshProUGUIEndScreen;
    [SerializeField] TextMeshProUGUI bestScoreText;

    [Header("Audio")]
    [SerializeField] AudioClip scoreSound;
    [SerializeField] AudioClip hitSound;

    private int score;
    private int bestScore;
    private int gameDifficulty;
    private string[] gameDifficulties = { "Easy", "Normal", "Hard" };


    GameManager gameManager;

    private DataManager dataManager;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        dataManager = FindObjectOfType<DataManager>();
    }
    void Start()
    {
        
        gameDifficulty = dataManager.GetGameData().GameDificulty;
        bestScore = dataManager.GetGameData().BestScores[gameDifficulty];
        bestScoreText.text = "Best score: " + bestScore + "\n" + gameDifficulties[gameDifficulty];
        score = 0;
        UpdateGoogleLeaderboard();
    }

    // Update is called once per frame
    void Update()
    {
        if (score <= bestScore) 
        {
            textMeshProUGUIEndScreen.text = "You lose :(\n Your score: " + score;
            UpdateGoogleLeaderboard();
        }
        else if(score > bestScore)
        {
            dataManager.gameData.setScore(score, gameDifficulty);
            textMeshProUGUIEndScreen.text = "You did it!\n Your score: " + score;
            dataManager.SaveGame();
            bestScore = score;
            UpdateGoogleLeaderboard();
        }
    }

    private void UpdateGoogleLeaderboard()
    {
        if (gameManager.connectedToGooglePlay)
        {
            if (dataManager.gameData.GameDificulty == 0)
            {
                Social.ReportScore(dataManager.gameData.BestScores[dataManager.gameData.GameDificulty],
                         GPGSIds.leaderboard_best_scoreseasy, LeaderBoardUpdate);
            }
            else if (dataManager.gameData.GameDificulty == 1)
            {
                Social.ReportScore(dataManager.gameData.BestScores[dataManager.gameData.GameDificulty],
                         GPGSIds.leaderboard_best_scoresnormal, LeaderBoardUpdate);
            }
            if (dataManager.gameData.GameDificulty == 2)
            {
                Social.ReportScore(dataManager.gameData.BestScores[dataManager.gameData.GameDificulty],
                         GPGSIds.leaderboard_best_scoreshard, LeaderBoardUpdate);
            }
        }
    }

    private void LeaderBoardUpdate(bool obj)
    {
        if(obj) { Debug.Log("Succesfull update to leaderboard"); }
        else { Debug.Log("Not succesfull update to leaderboard"); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        score++;
        textMeshProUGUI.text = score.ToString();
        GetComponent<AudioSource>().PlayOneShot(scoreSound);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<AudioSource>().PlayOneShot(hitSound);
        gameManager.ChangeGameState(false);
        Time.timeScale = 0;
        dataManager.SaveGame();
    }
}
