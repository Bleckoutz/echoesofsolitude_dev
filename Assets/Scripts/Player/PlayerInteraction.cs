using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public int moedas = 0;
    public ContadorMoedas contadorMoedasUI;
    public PlayerLife playerLife;
    public GameObject painelLaboratorio;

    private bool pertoDoVendedor = false;
    private bool pertoDaMesa = false;

    private void Start()
    {
        if (painelLaboratorio != null)
            painelLaboratorio.SetActive(false);
        contadorMoedasUI?.AtualizarMoedas(moedas);
    }

    private void Update()
    {
        if (pertoDoVendedor && Input.GetKeyDown(KeyCode.E))
            ComprarVida();

        if (pertoDaMesa && Input.GetKeyDown(KeyCode.F))
            AbrirPainelLaboratorio();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
        if (moedas > 0 && playerLife.vidas < 3)
        {
            moedas--;
            playerLife.vidas++;
            playerLife.barraDeVidaUI?.AtualizarVida(playerLife.vidas);
            contadorMoedasUI?.AtualizarMoedas(moedas);
        }
    }

    void AbrirPainelLaboratorio()
    {
        if (painelLaboratorio != null)
        {
            painelLaboratorio.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
