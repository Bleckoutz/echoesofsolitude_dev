using UnityEngine;

public class PlayerInfection : MonoBehaviour
{
    public PlayerControlle2 player; // referência ao script principal
    public bool isInfected = false;

    [Header("Configuração de Infecção")]
    public float infectionDuration = 30f; // tempo total até perder consciência
    private float infectionTimer = 0f;

    [Header("Velocidade")]
    public float normalSpeed = 5f;
    public float minSpeed = 1f;

    [Header("Pulo")]
    public float normalJumpForce = 10f;
    public float minJumpForce = 3f;

    [Header("Visão (escurecimento)")]
    public Transform visionMask; // pode ser um Sprite ou um objeto com Light
    public float normalVision = 1f;
    public float minVision = 0.3f;
    public GameObject escuridão;
    public GameObject luzdomundo;
    public GameObject pontodeluz;


    void Start()
    {
        if (player == null)
            player = GetComponent<PlayerControlle2>();
        if (escuridão!= null) escuridão.SetActive(false);
        if (luzdomundo != null) escuridão.SetActive(true);
        if (pontodeluz != null) pontodeluz.SetActive(false);

        // Valores iniciais do Player
        normalSpeed = player.moveSpeed;
        normalJumpForce = player.jumpForce;
    }

    void Update()
    {
        if (!isInfected) return;

        infectionTimer += Time.deltaTime;
        float progress = Mathf.Clamp01(infectionTimer / infectionDuration);

        // Reduz a velocidade do PlayerController
        player.moveSpeed = Mathf.Lerp(normalSpeed, minSpeed, progress);

        // Reduz o pulo do PlayerController
        player.jumpForce = Mathf.Lerp(normalJumpForce, minJumpForce, progress);

        // Reduz visão
        if (visionMask != null)
        {
            float visionScale = Mathf.Lerp(normalVision, minVision, progress);
            visionMask.localScale = Vector2.one * visionScale;
        }

        // Quando o tempo acaba  perde consciência
        if (infectionTimer >= infectionDuration)
            PerderConsciencia();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rato") && !isInfected)
        {
            Infectar();
        }
    }

    void Infectar()
    {
        if (escuridão != null) escuridão.SetActive(true);
        if (luzdomundo != null) luzdomundo.SetActive(false);
        if (pontodeluz != null) pontodeluz.SetActive(true);
        isInfected = true;
        infectionTimer = 0f;
        Debug.Log("⚠️ Player foi infectado!");
        
    }

    void PerderConsciencia()
    {
        Debug.Log("💀 O player perdeu a consciência!");
        player.moveSpeed = 0f;
        player.jumpForce = 0f;
        player.PerderVida(); // ou direto player.GameOver();
    }
}
