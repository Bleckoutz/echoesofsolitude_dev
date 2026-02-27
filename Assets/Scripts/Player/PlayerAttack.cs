using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bastao;
    public Transform mao;
    public GameObject bastaoVoadorPrefab;

    private bool estaSegurandoBastao = false;
    private PlayerMovement movimento;


    [Header("configuração de cura)")]
    public float tempoEntreCuras = 5f;
    public float quantidadeCura = 1f;
    private void Start()
    {
        movimento = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Atacar();
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
            float direcao = movimento.EstaViradoParaEsquerda() ? -1f : 1f;
            rbBastao.velocity = new Vector2(10f * direcao, 0f);
        }

        Collider2D colBastao = bastaoInstanciado.GetComponent<Collider2D>();
        if (colBastao != null)
        {
            colBastao.enabled = true;
            colBastao.isTrigger = true;
        }
    }
}
