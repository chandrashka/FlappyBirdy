using UnityEngine;

namespace Scripts
{
    public class CanvasManager : MonoBehaviour
    {
        public static void ShowCanvas(Canvas canvas)
        {
            canvas.gameObject.SetActive(true);
        }
        public static void HideCanvas(Canvas canvas)
        {
            canvas.gameObject.SetActive(false);
        }
        public static void StartGameCanvases(Canvas startScreenCanvas, Canvas gameCanvas, Canvas endScreenCanvas,
            Canvas gameMenuCanvas, Canvas settingsCanvas, Canvas gameDifficulty)
        {
            ShowCanvas(startScreenCanvas);
            HideCanvas(gameCanvas);
            HideCanvas(endScreenCanvas);
            HideCanvas(gameMenuCanvas);
            HideCanvas(settingsCanvas);
            HideCanvas(gameDifficulty);
        }

        public static void ManageCanvases(bool isGameOn, bool gameBeginning,
            Canvas startScreenCanvas,Canvas gameCanvas,Canvas endScreenCanvas)
        {
            if (isGameOn)
            {
                ShowCanvas(gameCanvas);
                HideCanvas(endScreenCanvas);
                HideCanvas(startScreenCanvas);
            }
            else if (!gameBeginning)
            {
                HideCanvas(gameCanvas);
                ShowCanvas(endScreenCanvas);
            }
        }

        internal static void TurnOffCanvases(Canvas gameCanvas, Canvas endScreenCanvas,
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
}
