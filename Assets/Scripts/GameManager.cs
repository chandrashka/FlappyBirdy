using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    public class GameManager : MonoBehaviour
    {
        [Header("Canvases")]
        [SerializeField] private Canvas gameCanvas;
        [SerializeField] private Canvas endScreenCanvas;
        [SerializeField] private Canvas startScreenCanvas;
        [SerializeField] private Canvas gameMenuCanvas;
        [SerializeField] private Canvas settingsCanvas;
        [SerializeField] private Canvas gameDifficultyCanvas;

        [Header("Text")]
        [SerializeField] private TextMeshProUGUI gameDifficultyText;
        [SerializeField] private TextMeshProUGUI bestScoreText;

        [Header("Button")]
        [SerializeField] private GameObject continueAfterAdButton;
        [SerializeField] private GameObject rewardAdButton;


        public bool isGameOn;
        public bool gameBeginning;
        private bool _isAdWatched;
        public bool connectedToGooglePlay;

        private int _gameCounter;
        private int _gameDifficulty;

        private readonly string[] _gameDifficulties = { "Easy", "Normal", "Hard" };

        private DataManager _dataManager;
        private CanvasManager _canvasManager;
        private TubeManager _tubeManager;

        private void Awake()
        {   
            PlayGamesPlatform.DebugLogEnabled= true;
            PlayGamesPlatform.Activate();
            LogInToGooglePlay();

            continueAfterAdButton.SetActive(false);
            _dataManager =  FindObjectOfType<DataManager>();
            _canvasManager = FindObjectOfType<CanvasManager>();
            _tubeManager = FindObjectOfType<TubeManager>();
            _gameDifficulty = _dataManager.GetGameData().GameDificulty;
        }

        private void Start()
        {
            ChangeGameState(false);
            gameBeginning = true;

            StopTime();

            CanvasManager.TurnOffCanvases(gameCanvas, endScreenCanvas,startScreenCanvas,
                gameMenuCanvas, settingsCanvas, gameDifficultyCanvas);
            CanvasManager.StartGameCanvases(startScreenCanvas, gameCanvas,
                endScreenCanvas, gameMenuCanvas, settingsCanvas, gameDifficultyCanvas);
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
            return _gameCounter;
        }
        public void StartTheGame()
        {
        
            _tubeManager.StartTheGame();
            gameBeginning = false;
            ChangeGameState(true);
            ContinueGame();
        }
        public void ChangeGameState(bool state)
        {
            isGameOn = state;
            CanvasManager.ManageCanvases(isGameOn, gameBeginning,
                startScreenCanvas, gameCanvas, endScreenCanvas);
        }

        public void Reset()
        {
            ChangeGameState(true);
            ContinueGame();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            _dataManager.SaveGame();
            _gameCounter++;
        }

        public void StopGame()
        {
            StopTime();
            CanvasManager.ShowCanvas(gameMenuCanvas);
        }

        private void ContinueGame()
        {
            if(_isAdWatched)
            {
                continueAfterAdButton.SetActive(false);
            }
        
            ContinueTime();
            CanvasManager.HideCanvas(gameMenuCanvas);
        }

        public void OpenSettings()
        {
            CanvasManager.ShowCanvas(settingsCanvas);
            CanvasManager.HideCanvas(startScreenCanvas);
        }

        public void CloseSettings()
        {
            CanvasManager.HideCanvas(settingsCanvas);
            CanvasManager.ShowCanvas(startScreenCanvas);
        }

        public void OpenGameDifficultyMenu()
        {
            CanvasManager.ShowCanvas(gameDifficultyCanvas);
            gameDifficultyText.text = "Current difficulty:\n" + _gameDifficulties[_dataManager.gameData.GameDificulty];
            CanvasManager.HideCanvas(startScreenCanvas);
        }

        public void CloseGameDifficultyMenu()
        {
            bestScoreText.text = "Best score: " +
                                 _dataManager.gameData.bestScores[_gameDifficulty] + "\n" + _gameDifficulties[_gameDifficulty];
            CanvasManager.HideCanvas(gameDifficultyCanvas);
            CanvasManager.ShowCanvas(startScreenCanvas);
        }
        public void QuitGame()
        {
            Application.Quit();
        }
     
        private static void StopTime()
        {
            Time.timeScale = 0f;
        }
        private static void ContinueTime()
        {
            Time.timeScale = 1.0f;
        }

        public void ChangeDifficulty(TextMeshProUGUI text)
        {
            if (text.text.Equals(_gameDifficulties[0])){
                _gameDifficulty = 0;
            }
            else if (text.text.Equals(_gameDifficulties[1]))
            {
                _gameDifficulty = 1;
            }
            else
            {
                _gameDifficulty = 2;
            }
            gameDifficultyText.text = "Current difficulty:\n" + text.text;
            _dataManager.gameData.setGameDificulty(_gameDifficulty);
            _dataManager.SaveGame();
        }

        internal void AfterRewardedAd()
        {
            _isAdWatched = true;
            rewardAdButton.SetActive(false);
            continueAfterAdButton.SetActive(true);

            CanvasManager.HideCanvas(endScreenCanvas);
            CanvasManager.ShowCanvas(gameCanvas);

            _tubeManager.DeleteCurrentTube();
        }
    }
}
