using UnityEngine;
using UnityEngine.UI;

public class PuzzleMesaLaboratorio : MonoBehaviour
{
    [Header("UI")]
    public GameObject painelPuzzle; // Painel UI do puzzle
    public Button frascoVerde;
    public Button frascoVermelho;
    public Button frascoAzul;

    [Header("Configuração da Sequência")]
    // Ordem correta dos frascos: Verde, Vermelho, Azul
    public string[] sequenciaCorreta = { "Verde", "Vermelho", "Azul" };
    private int indiceAtual = 0;

    private bool puzzleAtivo = false;
    private bool jogadorPerto = false;

    public PlayerController player; // Referência ao player para chamar PerderVida()

    void Start()
    {
        painelPuzzle.SetActive(false);

        frascoVerde.onClick.AddListener(() => SelecionarFrasco("Verde"));
        frascoVermelho.onClick.AddListener(() => SelecionarFrasco("Vermelho"));
        frascoAzul.onClick.AddListener(() => SelecionarFrasco("Azul"));
    }

    void Update()
    {
        if (jogadorPerto && Input.GetKeyDown(KeyCode.F) && !puzzleAtivo)
        {
            AbrirPuzzle();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = true;
            Debug.Log("Perto da mesa de laboratório! Pressione F para abrir o puzzle.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorPerto = false;
            if (puzzleAtivo)
                FecharPuzzle();
        }
    }

    void AbrirPuzzle()
    {
        painelPuzzle.SetActive(true);
        puzzleAtivo = true;
        Time.timeScale = 0f; // pausa o jogo
        indiceAtual = 0;
        Debug.Log("Puzzle aberto!");
    }

    public void SelecionarFrasco(string cor)
    {
        if (!puzzleAtivo) return;

        if (cor == sequenciaCorreta[indiceAtual])
        {
            indiceAtual++;
            Debug.Log($"Acertou {cor}! Posição {indiceAtual}/{sequenciaCorreta.Length}");

            if (indiceAtual >= sequenciaCorreta.Length)
            {
                Debug.Log("Puzzle resolvido! Parabéns.");
                FecharPuzzle();
            }
        }
        else
        {
            Debug.Log("Sequência errada! Reiniciando...");
            indiceAtual = 0;

            if (player != null)
            {
                player.PerderVida();
            }
        }
    }

    public void FecharPuzzle()
    {
        painelPuzzle.SetActive(false);
        Time.timeScale = 1f; // volta o tempo
        puzzleAtivo = false;
        indiceAtual = 0;
        Debug.Log("Puzzle fechado.");
    }
}
