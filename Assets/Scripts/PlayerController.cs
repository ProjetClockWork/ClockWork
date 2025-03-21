using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rgbd2D;
    private Collider2D _collider2D;
    private SpriteRenderer _spriteRend;
    private Transform _transform;
    private Collider2D _monCollider;

    [SerializeField]private float moveSpeed; //private mais visible dans unity
    [SerializeField]private float jumpPower;
    [SerializeField]private float longueurCheckJump = 1.1f;

    [SerializeField]private float airDampening;
    [SerializeField]private float groundDampening;

    private bool _isGrounded;

    private RaycastHit2D hit;
    private RaycastHit2D hitRight;
    private RaycastHit2D hitLeft;


    public enum STATE
    {
        NONE,
        NORMAL,
        REWIND
    }
    private STATE state;
 
    void Awake()
    {
        TryGetComponent(out _transform);
        TryGetComponent(out _rgbd2D);
        TryGetComponent(out _collider2D);
        TryGetComponent(out _spriteRend);
        TryGetComponent(out _monCollider);
    }

    void Start()
    {
        StateToNormal();
    }

    void Update()
    {
        switch (state)
        {
            case STATE.NORMAL:
                if(Input.GetKeyDown(KeyCode.R))
                {
                    gameObject.SendMessage("StartRewind");
                    StateToRewind();
                }
                else
                {
                    Move();
                    IsGrounded();
                    Jump();
                    Dash();
                    AirGroundDampening();
                }

                break;
            case STATE.REWIND:
                break;
            default:
                break;
        }
    }

    void Move()
    {
        if (Input.GetButton("Horizontal"))
        {
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                _spriteRend.flipX = Input.GetAxis("Horizontal") < 0;
            }

            hitRight = Physics2D.Raycast(transform.position, Vector2.right, (_monCollider.bounds.extents.x + Mathf.Abs(_monCollider.offset.x) * transform.localScale.x) * longueurCheckJump);
            hitLeft = Physics2D.Raycast(transform.position, Vector2.left, (_monCollider.bounds.extents.x + Mathf.Abs(_monCollider.offset.x) * transform.localScale.x) * longueurCheckJump);

            if (hitRight && !hitRight.collider.isTrigger && Input.GetAxisRaw("Horizontal") == 1)
            {
                Debug.DrawRay(transform.position, Vector2.right * (_monCollider.bounds.extents.x + Mathf.Abs(_monCollider.offset.x) * transform.localScale.x) * longueurCheckJump, Color.red);
            }
            else if (hitLeft && !hitLeft.collider.isTrigger && Input.GetAxisRaw("Horizontal") == -1)
            {
                Debug.DrawRay(transform.position, Vector2.left * (_monCollider.bounds.extents.x + Mathf.Abs(_monCollider.offset.x) * transform.localScale.x) * longueurCheckJump, Color.red);
            }
            else 
            { 
                _rgbd2D.linearVelocityX = Input.GetAxisRaw("Horizontal") * moveSpeed; 
            }
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            _rgbd2D.linearVelocityY = jumpPower;
    }

    void IsGrounded()
    {
        hit = Physics2D.Raycast(transform.position, Vector2.down, (_monCollider.bounds.extents.y + Mathf.Abs(_monCollider.offset.y) * transform.localScale.y) * longueurCheckJump);

        Debug.DrawRay(transform.position, Vector2.down * (_monCollider.bounds.extents.y + Mathf.Abs(_monCollider.offset.y) * transform.localScale.y) * longueurCheckJump, Color.red);        

        if (hit && !hit.collider.isTrigger)
        {
            _isGrounded = true;
            //animatotor.SetBool("jump", false);
        }
        else
        {
            _isGrounded = false;
            //animatotor.SetBool("jump", true);
        }
    }

    void AirGroundDampening()
    {
        if (_isGrounded)
        {
            _rgbd2D.linearDamping = groundDampening;
        }
        else 
        {
            _rgbd2D.linearDamping = airDampening;
        }
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            _rgbd2D.linearVelocity *= 5;
        }
    }

    public void StateToNormal()
    {
        state = STATE.NORMAL;
    }

    public void StateToRewind()
    {
        state = STATE.REWIND;
    }
}
