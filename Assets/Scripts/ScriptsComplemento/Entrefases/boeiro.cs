using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class boeiro : MonoBehaviour
{
    // Este método será chamado quando um objeto com a tag "player" encostar no objeto com este script
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayGames();
        }
    }

    public void PlayGames()
    {
        SceneManager.LoadScene("boeiro");
    }

    void Update()
    {
        // Deixado em branco por enquanto
    }
}
