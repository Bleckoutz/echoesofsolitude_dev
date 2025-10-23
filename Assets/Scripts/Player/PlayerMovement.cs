using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public string tagDoChao = "chao";

    public Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator animator;
    private bool isGrounded = false;
    private float moveX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();

        if (animator != null)
            animator.SetBool("IsRunning", moveX != 0);
        //faz a transição do pulo quando não estiver no chão
        if (animator != null)
            animator.SetBool("IsJumping", !isGrounded);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if (moveX > 0) sprite.flipX = false;
        else if (moveX < 0) sprite.flipX = true;
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
                if (contact.normal.y > 0.5f) { isGrounded = true; break; }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            bool noChao = false;
            foreach (ContactPoint2D contact in collision.contacts)
                if (contact.normal.y > 0.5f) { noChao = true; break; }
            isGrounded = noChao;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
            isGrounded = false;
    }

    public bool EstaViradoParaEsquerda() => sprite.flipX;
}
