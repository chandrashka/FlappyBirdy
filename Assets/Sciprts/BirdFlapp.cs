using UnityEngine;

public class BirdFlapp : MonoBehaviour
{
    [SerializeField] float velocity;
    [SerializeField] Animator animator;

    Rigidbody2D Rigidbody2D;
    private void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody2D.velocity = Vector2.up * velocity * Time.fixedDeltaTime;
        }  
    }
}
