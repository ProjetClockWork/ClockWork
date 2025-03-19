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
    [SerializeField]private float limiteJump;
    [SerializeField]private float longueurCheckJump = 1.1f;

    private bool _isGrounded;

    private RaycastHit2D hit;

    void Awake()
    {
        TryGetComponent(out _transform);
        TryGetComponent(out _rgbd2D);
        TryGetComponent(out _collider2D);
        TryGetComponent(out _spriteRend);
        TryGetComponent(out _monCollider);
    }

    void Update()
    {
        Move();

        if(Input.GetKeyDown(KeyCode.Space) && _isGrounded)
            Jump();
        IsGrounded();
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
        //Debug.Log(_rgbd2D.linearVelocity);
    }

    void IsGrounded()
    {
        // On fait un raycast qui s'adatpe automatiquement à la taille et au positionnement de votre collider, et qui vise vers le bas
        hit = Physics2D.Raycast(transform.position, Vector2.down, (_monCollider.bounds.extents.y + Mathf.Abs(_monCollider.offset.y) * transform.localScale.y) * longueurCheckJump);

        // Ca c'est juste une ligne pour dessiner le raycast dans l'éditor, histoire de pouvoir l'ajuster avec la variable "longueurCheckJump"
        Debug.DrawRay(transform.position, Vector2.down * (_monCollider.bounds.extents.y + Mathf.Abs(_monCollider.offset.y) * transform.localScale.y) * longueurCheckJump, Color.red);
        // Si le raycast touche ET que c'est pas un trigger, c'est qu'il y a un sol sous nos pied, donc on peut sauter, sinon c'est qu'on est déjà en l'air et donc on peut pas sauter
        if (hit && !hit.collider.isTrigger)
        {
            _isGrounded = true;
            //animatotor.SetBool("jump", false);

            //Debug.Log(hit);
        }
        else
        {
            _isGrounded = false;
            //animatotor.SetBool("jump", true);
        }
    }
}
