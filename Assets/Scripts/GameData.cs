namespace Scripts
{
    [System.Serializable]
    public class GameData 
    {
        public int[] bestScores = new int[3];
        public int birdSprite;
        public int GameDificulty;


        public void setScore(int score, int gameMode)
        {
            bestScores[gameMode] = score;
        }
        public void setBirdSprite(int birdSprite)
        {
            birdSprite = birdSprite;
        }
        public void setGameDificulty(int dificulty)
        {
            GameDificulty = dificulty;
        }
        public GameData()
        {
            bestScores[0] = 0;
            bestScores[1] = 0;
            bestScores[2] = 0;
            birdSprite = 0;
            GameDificulty = 1;
        }
    }
}
