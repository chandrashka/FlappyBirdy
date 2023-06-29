using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public void ShowCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(true);
    }
    public void HideCanvas(Canvas canvas)
    {
        canvas.gameObject.SetActive(false);
    }
    public void StartGameCanvases(Canvas StartScreenCanvas, Canvas GameCanvas, Canvas EndScreenCanvas,
        Canvas GameMenuCanvas, Canvas SettingsCanvas, Canvas GameDifficulty)
    {
        ShowCanvas(StartScreenCanvas);
        HideCanvas(GameCanvas);
        HideCanvas(EndScreenCanvas);
        HideCanvas(GameMenuCanvas);
        HideCanvas(SettingsCanvas);
        HideCanvas(GameDifficulty);
    }

    public void manageCanvases(bool isGameOn, bool gameBeginning,
        Canvas StartScreenCanvas,Canvas GameCanvas,Canvas EndScreenCanvas)
    {
        if (isGameOn)
        {
            ShowCanvas(GameCanvas);
            HideCanvas(EndScreenCanvas);
            HideCanvas(StartScreenCanvas);
        }
        else if (!gameBeginning)
        {
            HideCanvas(GameCanvas);
            ShowCanvas(EndScreenCanvas);
        }
    }

    internal void TurnOffCanvases(Canvas gameCanvas, Canvas endScreenCanvas,
        Canvas startScreenCanvas, Canvas gameMenuCanvas, Canvas settingsCanvas,
        Canvas gameDifficultyCanvas)
    {
        HideCanvas(gameCanvas);
        HideCanvas(endScreenCanvas);
        HideCanvas(startScreenCanvas);
        HideCanvas(gameMenuCanvas);
        HideCanvas(settingsCanvas);
        HideCanvas(gameDifficultyCanvas);
    }
}
