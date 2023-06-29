using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BirdSprite : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] Sprite[] Sprites;
    private SpriteRenderer Renderer;

    [Header("Animation")]
    [SerializeField] Animator animator;

    private string animatorParameter = "FlyingNum";

    private DataManager DataManager;

    private void Awake()
    {   
        Renderer = GetComponent<SpriteRenderer>();
        DataManager = FindObjectOfType<DataManager>();
    }
    private void Start()
    {
        int spriteNum = DataManager.GetGameData().BirdSprite;
        Renderer.sprite = Sprites[spriteNum];
        animator.SetInteger(animatorParameter, spriteNum);      
    }

    public void OnClick(Button button)
    {       
        int num = int.Parse(button.GetComponentInChildren<TextMeshProUGUI>().text);
        animator.SetInteger(animatorParameter, num);
        DataManager.gameData.setBirdSprite(num);
        Renderer.sprite = button.GetComponentInChildren<SpriteRenderer>().sprite;
        
        DataManager.SaveGame();
    }
}
