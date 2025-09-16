using UnityEngine;

public class PlayerInfection : MonoBehaviour
{
    public PlayerController player; // referência ao script principal
    public bool isInfected = false;

    [Header("Configuração de Infecção")]
    public float infectionDuration = 30f; // tempo total até perder consciência
    private float infectionTimer = 0f;

    [Header("Velocidade")]
    public float normalSpeed = 5f;
    public float minSpeed = 1f;

    [Header("Visão (escurecimento)")]
    public SpriteRenderer visionMask;
    public float normalVision = 1f;
    public float minVision = 0.3f;

    void Start()
    {
        if (player == null)
            player = GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!isInfected) return;

        infectionTimer += Time.deltaTime;
        float progress = Mathf.Clamp01(infectionTimer / infectionDuration);

        // Reduz a velocidade do PlayerController
        player.moveSpeed = Mathf.Lerp(normalSpeed, minSpeed, progress);

        // Reduz visão (se você tiver um efeito gráfico)
        if (visionMask != null)
        {
            float visionScale = Mathf.Lerp(normalVision, minVision, progress);
            visionMask.transform.localScale = Vector3.one * visionScale;
        }

        // Quando o tempo acaba → perde consciência
        if (infectionTimer >= infectionDuration)
            PerdeConsciencia();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rato") && !isInfected)
        {
            isInfected = true;
            infectionTimer = 0f;
            Debug.Log("Player foi infectado!");
        }
    }

    void PerdeConsciencia()
    {
        Debug.Log("O player perdeu a consciência!");
        player.moveSpeed = 0f;
        // Aqui você pode chamar o GameOver do PlayerController, por exemplo:
        player.PerderVida(); // ou direto player.GameOver();
    }
}
