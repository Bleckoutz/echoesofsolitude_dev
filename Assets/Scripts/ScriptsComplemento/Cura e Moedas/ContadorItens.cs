using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemColetavel : MonoBehaviour
{
    // Texto do contador
    public TextMeshProUGUI textoContador;
    private static int totalItens = 0;

    void Start()
    {
        
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex == 0)
        {
            totalItens = 0;
        }

        // Se não arrastar no inspetor, tenta achar sozinho
        if (textoContador == null)
        {
            textoContador = GameObject.FindWithTag("ContadorTexto")?.GetComponent<TextMeshProUGUI>();
        }

        // Atualiza o texto logo no início
        if (textoContador != null)
            textoContador.text = "Itens: " + totalItens;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            totalItens++;

            if (textoContador != null)
                textoContador.text = "Itens: " + totalItens;
            else
                Debug.LogWarning("Texto do contador não foi encontrado!");

            Destroy(gameObject); // item desaparece
        }
    }
}
