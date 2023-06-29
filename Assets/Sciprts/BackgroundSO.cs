using UnityEngine;

[CreateAssetMenu(menuName = "Background", fileName = "Background")]
public class BackgroundSO : ScriptableObject
{
    [SerializeField] Sprite[] backgroundSprites = new Sprite[5];
    public Sprite[] GetBackground()
    {
        return backgroundSprites;
    }
}
