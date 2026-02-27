using UnityEngine;

public class Cura : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInfection player = other.GetComponent<PlayerInfection>();

            if (player != null)
            {
                player.Curar(); // chama o método de cura do jogador
            }

            Destroy(gameObject);
        }
    }
}