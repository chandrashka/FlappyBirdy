[System.Serializable]
public class GameData 
{
    public int[] BestScores = new int[3];
    public int BirdSprite;
    public int GameDificulty;


    public void setScore(int score, int gameMode)
    {
        BestScores[gameMode] = score;
    }
    public void setBirdSprite(int birdSprite)
    {
        BirdSprite = birdSprite;
    }
    public void setGameDificulty(int dificulty)
    {
        GameDificulty = dificulty;
    }
    public GameData()
    {
        BestScores[0] = 0;
        BestScores[1] = 0;
        BestScores[2] = 0;
        BirdSprite = 0;
        GameDificulty = 1;
    }
}
