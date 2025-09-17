using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movimento")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Rigidbody2D rb;
    private float moveX;
    private bool isGrounded = false;
    private SpriteRenderer sprite;
    private Animator animator;

    [Header("Vida e Moedas")]
    public int vidas = 3;
    public int moedas = 0;
    public string tagDoChao = "Chao";
    public BarraDeVidaCoracoes barraDeVidaUI;
    public ContadorMoedas contadorMoedasUI;

    [Header("Bastão")]
    public GameObject bastao;
    public Transform mao;
    private bool estaSegurandoBastao = false;
    public GameObject bastaoVoadorPrefab;

    [Header("UI")]
    public GameObject gameOverPanel;

    [Header("Painel de Puzzle")]
    public GameObject painelLaboratorio;

    [Header("Audio")]
    public AudioSource musicaDeFundo; // Música de fundo que será pausada

    private bool pertoDoVendedor = false;
    private bool pertoDaMesa = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (painelLaboratorio != null) painelLaboratorio.SetActive(false);

        barraDeVidaUI?.AtualizarVida(vidas);
        contadorMoedasUI?.AtualizarMoedas(moedas);
    }

    private void Update()
    {
        moveX = Input.GetAxisRaw("Horizontal");

        if (isGrounded && Input.GetButtonDown("Jump"))
            Jump();

        if (Input.GetKeyDown(KeyCode.Q))
            Atacar();

        if (pertoDoVendedor && Input.GetKeyDown(KeyCode.E))
            ComprarVida();

        if (pertoDaMesa && Input.GetKeyDown(KeyCode.F))
            AbrirPainelLaboratorio();

        if (animator != null)
            animator.SetBool("IsRunning", moveX != 0);
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);

        if (moveX > 0) sprite.flipX = false;
        else if (moveX < 0) sprite.flipX = true;
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void Atacar()
    {
        if (!estaSegurandoBastao) return;

        Destroy(bastao);
        estaSegurandoBastao = false;

        GameObject bastaoInstanciado = Instantiate(bastaoVoadorPrefab, mao.position, Quaternion.identity);

        Animator anim = bastaoInstanciado.GetComponent<Animator>();
        if (anim != null) anim.SetBool("voando", true);

        Rigidbody2D rbBastao = bastaoInstanciado.GetComponent<Rigidbody2D>();
        if (rbBastao != null)
        {
            rbBastao.isKinematic = false;
            rbBastao.simulated = true;
            float direcao = sprite.flipX ? -1f : 1f;
            rbBastao.velocity = new Vector2(10f * direcao, 0f);
        }

        Collider2D colBastao = bastaoInstanciado.GetComponent<Collider2D>();
        if (colBastao != null)
        {
            colBastao.enabled = true;
            colBastao.isTrigger = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagDoChao))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        if (collision.gameObject.CompareTag("Inimigo"))
            PerderVida();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagDoChao))
        {
            bool noChao = false;
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    noChao = true;
                    break;
                }
            }
            isGrounded = noChao;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagDoChao))
            isGrounded = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bastao"))
        {
            if (bastao != null) Destroy(bastao);

            estaSegurandoBastao = true;
            bastao = other.gameObject;

            bastao.transform.SetParent(mao);
            bastao.transform.localPosition = Vector3.zero;
            bastao.transform.localRotation = Quaternion.identity;

            Rigidbody2D rbBastao = bastao.GetComponent<Rigidbody2D>();
            if (rbBastao != null)
            {
                rbBastao.velocity = Vector2.zero;
                rbBastao.isKinematic = true;
                rbBastao.simulated = false;
            }

            Collider2D colBastao = bastao.GetComponent<Collider2D>();
            if (colBastao != null) colBastao.enabled = false;
        }

        if (other.CompareTag("Moeda"))
        {
            moedas++;
            contadorMoedasUI?.AtualizarMoedas(moedas);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Vendedor")) pertoDoVendedor = true;
        if (other.CompareTag("MesaLaboratorio")) pertoDaMesa = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Vendedor")) pertoDoVendedor = false;
        if (other.CompareTag("MesaLaboratorio")) pertoDaMesa = false;
    }

    void ComprarVida()
    {
        if (moedas > 0 && vidas < 3)
        {
            moedas--;
            vidas++;
            barraDeVidaUI?.AtualizarVida(vidas);
            contadorMoedasUI?.AtualizarMoedas(moedas);
        }
    }

    public void PerderVida()
    {
        vidas = Mathf.Max(vidas - 1, 0);
        barraDeVidaUI?.AtualizarVida(vidas);

        if (vidas <= 0) GameOver();
    }

    void GameOver()
    {
        Time.timeScale = 0f;

        if (musicaDeFundo != null && musicaDeFundo.isPlaying)
            musicaDeFundo.Pause();

        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    void AbrirPainelLaboratorio()
    {
        if (painelLaboratorio != null)
        {
            painelLaboratorio.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;

        if (musicaDeFundo != null)
            musicaDeFundo.UnPause(); // ou Play() para reiniciar do começo

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
