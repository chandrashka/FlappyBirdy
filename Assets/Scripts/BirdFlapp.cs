using UnityEngine;

namespace Scripts
{
    public class BirdFlapp : MonoBehaviour
    {
        [SerializeField] private float velocity;
        [SerializeField] private Animator animator;

        private Rigidbody2D _rigidbody2D;
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _rigidbody2D.velocity = Vector2.up * (velocity * Time.fixedDeltaTime);
            }  
        }
    }
}
