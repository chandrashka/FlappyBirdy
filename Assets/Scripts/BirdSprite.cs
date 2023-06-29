using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts
{
    public class BirdSprite : MonoBehaviour
    {
        [Header("Sprites")]
        [SerializeField] private Sprite[] sprites;
        private SpriteRenderer _renderer;

        [Header("Animation")]
        [SerializeField] Animator animator;

        private const string AnimatorParameter = "FlyingNum";

        private DataManager _dataManager;
        private static readonly int FlyingNum = Animator.StringToHash(AnimatorParameter);

        private void Awake()
        {   
            _renderer = GetComponent<SpriteRenderer>();
            _dataManager = FindObjectOfType<DataManager>();
        }
        private void Start()
        {
            var spriteNum = _dataManager.GetGameData().birdSprite;
            _renderer.sprite = sprites[spriteNum];
            animator.SetInteger(FlyingNum, spriteNum);      
        }

        public void OnClick(Button button)
        {       
            var num = int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text);
            animator.SetInteger(FlyingNum, num);
            _dataManager.gameData.setBirdSprite(num);
            _renderer.sprite = button.GetComponentInChildren<SpriteRenderer>().sprite;
        
            _dataManager.SaveGame();
        }
    }
}
