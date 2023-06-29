using TMPro;
using UnityEngine;

namespace Scripts
{
    public class UpdateScore : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI textMeshProUGUI;
        [SerializeField] private TextMeshProUGUI textMeshProUGUIEndScreen;
        [SerializeField] private TextMeshProUGUI bestScoreText;

        [Header("Audio")]
        [SerializeField] private AudioClip scoreSound;
        [SerializeField] private AudioClip hitSound;

        private int _score;
        private int _bestScore;
        private int _gameDifficulty;
        private readonly string[] _gameDifficulties = { "Easy", "Normal", "Hard" };
        
        private GameManager _gameManager;
        private DataManager _dataManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _dataManager = FindObjectOfType<DataManager>();
        }

        private void Start()
        {
            _gameDifficulty = _dataManager.GetGameData().GameDificulty;
            _bestScore = _dataManager.GetGameData().bestScores[_gameDifficulty];
            bestScoreText.text = "Best score: " + _bestScore + "\n" + _gameDifficulties[_gameDifficulty];
            _score = 0;
            UpdateGoogleLeaderboard();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_score <= _bestScore) 
            {
                textMeshProUGUIEndScreen.text = "You lose :(\n Your score: " + _score;
                UpdateGoogleLeaderboard();
            }
            else if(_score > _bestScore)
            {
                _dataManager.gameData.setScore(_score, _gameDifficulty);
                textMeshProUGUIEndScreen.text = "You did it!\n Your score: " + _score;
                _dataManager.SaveGame();
                _bestScore = _score;
                UpdateGoogleLeaderboard();
            }
        }

        private void UpdateGoogleLeaderboard()
        {
            if (_gameManager.connectedToGooglePlay)
            {
                if (_dataManager.gameData.GameDificulty == 0)
                {
                    Social.ReportScore(_dataManager.gameData.bestScores[_dataManager.gameData.GameDificulty],
                        GPGSIds.leaderboard_best_scoreseasy, LeaderBoardUpdate);
                }
                else if (_dataManager.gameData.GameDificulty == 1)
                {
                    Social.ReportScore(_dataManager.gameData.bestScores[_dataManager.gameData.GameDificulty],
                        GPGSIds.leaderboard_best_scoresnormal, LeaderBoardUpdate);
                }
                if (_dataManager.gameData.GameDificulty == 2)
                {
                    Social.ReportScore(_dataManager.gameData.bestScores[_dataManager.gameData.GameDificulty],
                        GPGSIds.leaderboard_best_scoreshard, LeaderBoardUpdate);
                }
            }
        }

        private void LeaderBoardUpdate(bool obj)
        {
            Debug.Log(obj ? "Successful update to leaderboard" : "Not successful update to leaderboard");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _score++;
            textMeshProUGUI.text = _score.ToString();
            GetComponent<AudioSource>().PlayOneShot(scoreSound);
        }
        private void OnCollisionEnter2D()
        {
            GetComponent<AudioSource>().PlayOneShot(hitSound);
            _gameManager.ChangeGameState(false);
            Time.timeScale = 0;
            _dataManager.SaveGame();
        }
    }
}
