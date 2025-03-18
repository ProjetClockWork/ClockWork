using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rgbd2D;
    private Collider2D _collider2D;
    private SpriteRenderer _spriteRend;
    private Transform _transform;

    [SerializeField]private float moveSpeed; //private mais visible dans unity
    [SerializeField]private float jumpPower;
    [SerializeField]private float limiteJump;

    private bool _isGrounded = true;

    void Awake()
    {
        TryGetComponent(out _transform);
        TryGetComponent(out _rgbd2D);
        TryGetComponent(out _collider2D);
        TryGetComponent(out _spriteRend);
    }

    void Update()
    {
        Move();

        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            Jump();
    }

    void Move()
    {
        if (Input.GetButton("Horizontal"))
        {
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                _spriteRend.flipX = Input.GetAxis("Horizontal") < 0;
            }
            _transform.position += Vector3.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        }
    }

    void Jump()
    {
        _rgbd2D.linearVelocityY = jumpPower;
        Debug.Log(_rgbd2D.linearVelocity);
    }

    void IsGrounded()
    {

    }
}
