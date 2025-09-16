using UnityEngine;

public class Moeda : MonoBehaviour
{
    public int valor = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.moedas += valor;
                Debug.Log("Moedas do player: " + player.moedas);

                if (player.contadorMoedasUI != null)
                    player.contadorMoedasUI.AtualizarMoedas(player.moedas);
            }

            Destroy(gameObject);
        }
    }
}

