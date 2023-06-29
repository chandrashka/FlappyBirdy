using UnityEngine;

namespace Scripts
{
    [CreateAssetMenu(menuName = "Background", fileName = "Background")]
    public class BackgroundSo : ScriptableObject
    {
        [SerializeField] private Sprite[] backgroundSprites = new Sprite[5];
        public Sprite[] GetBackground()
        {
            return backgroundSprites;
        }
    }
}
