using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Canvases")]
    [SerializeField] Canvas GameCanvas;
    [SerializeField] Canvas EndScreenCanvas;
    [SerializeField] Canvas StartScreenCanvas;
    [SerializeField] Canvas GameMenuCanvas;
    [SerializeField] Canvas SettingsCanvas;
    [SerializeField] Canvas GameDifficultyCanvas;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI gameDifficultyText;
    [SerializeField] TextMeshProUGUI bestScoreText;

    [Header("Button")]
    [SerializeField] GameObject continueAfterAdButton;
    [SerializeField] GameObject rewardAdButton;


    public bool isGameOn;
    public bool gameBeginning;
    private bool isAdWatched = false;
    public bool connectedToGooglePlay;

    private int gameCounter;
    private int gameDifficulty;

    private string[] gameDifficulties = { "Easy", "Normal", "Hard" };

    private DataManager dataManager;
    private CanvasManager canvasManager;
    private TubeManager tubeManager;

    private void Awake()
    {   
        PlayGamesPlatform.DebugLogEnabled= true;
        PlayGamesPlatform.Activate();
        LogInToGooglePlay();

        continueAfterAdButton.SetActive(false);
        dataManager =  FindObjectOfType<DataManager>();
        canvasManager = FindObjectOfType<CanvasManager>();
        tubeManager = FindObjectOfType<TubeManager>();
        gameDifficulty = dataManager.GetGameData().GameDificulty;

        
    }
    void Start()
    {
        ChangeGameState(false);
        gameBeginning = true;

        StopTime();

        canvasManager.TurnOffCanvases(GameCanvas, EndScreenCanvas,StartScreenCanvas,
            GameMenuCanvas, SettingsCanvas, GameDifficultyCanvas);
        canvasManager.StartGameCanvases(StartScreenCanvas, GameCanvas,
            EndScreenCanvas, GameMenuCanvas, SettingsCanvas, GameDifficultyCanvas);
    }

    private void LogInToGooglePlay()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthetincation);
    }
    private void ProcessAuthetincation(SignInStatus status)
    {
        if(status == SignInStatus.Success)
        {
            connectedToGooglePlay= true;
        }
        else connectedToGooglePlay = false;
    }
    public void ShowLeaderboad()
    {
        if(!connectedToGooglePlay) {
            LogInToGooglePlay();
        }
        Social.ShowLeaderboardUI();   
    }

    public int GetGameCounter()
    {
        return gameCounter;
    }
    public void StartTheGame()
    {
        
        tubeManager.StartTheGame();
        gameBeginning = false;
        ChangeGameState(true);
        ContinueGame();
    }
    public void ChangeGameState(bool state)
    {
        isGameOn = state;
        canvasManager.manageCanvases(isGameOn, gameBeginning,
            StartScreenCanvas, GameCanvas, EndScreenCanvas);
    }

    public void Reset()
    {
        ChangeGameState(true);
        ContinueGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        dataManager.SaveGame();
        gameCounter++;
    }

    public void StopGame()
    {
        StopTime();
        canvasManager.ShowCanvas(GameMenuCanvas);
    }
    public void ContinueGame()
    {
        if(isAdWatched)
        {
            continueAfterAdButton.SetActive(false);
        }
        
        ContinueTime();
        canvasManager.HideCanvas(GameMenuCanvas);
    }

    public void OpenSettings()
    {
        canvasManager.ShowCanvas(SettingsCanvas);
        canvasManager.HideCanvas(StartScreenCanvas);
    }

    public void CloseSettings()
    {
        canvasManager.HideCanvas(SettingsCanvas);
        canvasManager.ShowCanvas(StartScreenCanvas);
    }

    public void OpenGameDifficultyMenu()
    {
        canvasManager.ShowCanvas(GameDifficultyCanvas);
        gameDifficultyText.text = "Current difficulty:\n" + gameDifficulties[dataManager.gameData.GameDificulty];
        canvasManager.HideCanvas(StartScreenCanvas);
    }

    public void CloseGameDifficultyMenu()
    {
        bestScoreText.text = "Best score: " +
            dataManager.gameData.BestScores[gameDifficulty] + "\n" + gameDifficulties[gameDifficulty];
        canvasManager.HideCanvas(GameDifficultyCanvas);
        canvasManager.ShowCanvas(StartScreenCanvas);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
     
    private void StopTime()
    {
        Time.timeScale = 0f;
    }
    private void ContinueTime()
    {
        Time.timeScale = 1.0f;
    }

    public void ChangeDifficulty(TextMeshProUGUI text)
    {
        if (text.text.Equals(gameDifficulties[0])){
            gameDifficulty = 0;
        }
        else if (text.text.Equals(gameDifficulties[1]))
        {
            gameDifficulty = 1;
        }
        else
        {
            gameDifficulty = 2;
        }
        gameDifficultyText.text = "Current difficulty:\n" + text.text;
        dataManager.gameData.setGameDificulty(gameDifficulty);
        dataManager.SaveGame();
    }

    internal void AfterRewardedAd()
    {
        isAdWatched = true;
        rewardAdButton.SetActive(false);
        continueAfterAdButton.SetActive(true);

        canvasManager.HideCanvas(EndScreenCanvas);
        canvasManager.ShowCanvas(GameCanvas);

        tubeManager.deleteCurrentTube();
    }
}
