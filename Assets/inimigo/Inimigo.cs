using UnityEngine;

public class InimigoAI : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed = 3f;
    public float detectionRange = 5f;

    public Transform pointA;
    public Transform pointB;

    private Transform player;
    private bool isFollowingPlayer = false;
    private Vector3 targetPosition;

    [Header("Vida")]
    public float vida = 20f;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        targetPosition = pointA.position;

        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);
        isFollowingPlayer = distanceToPlayer <= detectionRange;

        if (isFollowingPlayer)
        {
            FollowPlayer();
            FlipSprite(player.position.x);
        }
        else
        {
            MoveBetweenPoints();
            FlipSprite(targetPosition.x);
        }
    }

    private void MoveBetweenPoints()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Troca o destino quando chegar perto do ponto
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            targetPosition = targetPosition == pointA.position ? pointB.position : pointA.position;
        }
    }

    private void FollowPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }

    private void FlipSprite(float targetX)
    {
        if (spriteRenderer == null) return;

        spriteRenderer.flipX = targetX < transform.position.x;
    }

    public void LevarDano(float dano)
    {
        vida -= dano;
        Debug.Log("Inimigo levou dano! Vida restante: " + vida);

        if (vida <= 0)
            Morrer();
    }

    private void Morrer()
    {
        Debug.Log("Inimigo morreu!");
        Destroy(gameObject);
    }
}
