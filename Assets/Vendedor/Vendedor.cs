using UnityEngine;

public class Vendedor : MonoBehaviour
{
    public int precoPorVida = 1;
    public int vidaMaxima = 3;

    private void OnTriggerStay2D(Collider2D other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();

        if (player != null && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Tentando comprar vida...");
            TentarVenderVida(player);
        }
    }

    void TentarVenderVida(PlayerController player)
    {
        if (player.moedas >= precoPorVida && player.vidas < vidaMaxima)
        {
            player.moedas -= precoPorVida;
            player.vidas++;

            if (player.barraDeVidaUI != null)
                player.barraDeVidaUI.AtualizarVida(player.vidas);

            Debug.Log($"Vida comprada! Vidas: {player.vidas}, Moedas: {player.moedas}");
        }
        else
        {
            if (player.moedas < precoPorVida)
                Debug.Log("Moedas insuficientes para comprar vida.");
            else if (player.vidas >= vidaMaxima)
                Debug.Log("Vida já está cheia.");
        }
    }
}
