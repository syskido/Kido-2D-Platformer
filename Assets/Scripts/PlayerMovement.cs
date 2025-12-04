using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private LayerMask groundLayer; // LayerMask ���
    [SerializeField] private LayerMask wallLayer; // LayerMask ���
    private Rigidbody2D body;
    private Animator anim; // Animation �� Animator�� ����
    private BoxCollider2D boxCollider; // BoxCollider2D ���

    private void Awake()
    {
        //Grab references for rigidbody and animator from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // Animation �� Animator�� ����
        boxCollider = GetComponent<BoxCollider2D>(); // BoxCollider2D ���
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y); // linearVelocity �� velocity�� ����

        // Flip player when moving left/right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        if (Input.GetKey(KeyCode.Space) && isGrounded())
            Jump();

        anim.SetBool("run", horizontalInput != 0); // Animator���� SetBool ���
        anim.SetBool("grounded", isGrounded()); // Animator���� SetBool ���

        print(onWall());
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        anim.SetTrigger("jump"); // Animator���� SetTrigger ���
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
}
