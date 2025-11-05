using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerInfection : MonoBehaviour
{
    [Header("Luzes Globais")]
    public Light2D lightSaudavel;   // Branca
    public Light2D lightInfectado;  // Preta

    [Header("Luz da Visão do Player")]
    public Light2D playerVisionLight;

    [Header("Configuração da Infecção")]
    public float tempoParaMorrer = 6f; // tempo até a visão acabar
    private float tempoAtual;
    private bool infectado = false;
    private float raioInicial;


    private void Start()
    {
        lightSaudavel.enabled = true;
        lightInfectado.enabled = false;
        tempoAtual = tempoParaMorrer;
        raioInicial = playerVisionLight.pointLightOuterRadius;

    }

    private void Update()
    {
        if (infectado)
        {
            tempoAtual -= Time.deltaTime;
            float porcentagem = tempoAtual / tempoParaMorrer;

            // Mantém dentro do limite 0 a 1
            porcentagem = Mathf.Clamp01(porcentagem);

            // Diminui o raio da luz do player gradualmente
            float novoRaio = Mathf.Lerp(0f, raioInicial, porcentagem);
            playerVisionLight.pointLightOuterRadius = novoRaio;

            if (tempoAtual <= 0)
            {
                Morrer();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!infectado && other.CompareTag("Rato"))
        {
            Infectar();
        }
    }

    void Infectar()
    {
        infectado = true;
        lightSaudavel.enabled = false;
        lightInfectado.enabled = true;

        Debug.Log("INFECTADO! Visão reduzindo...");
    }

    void Morrer()
    {
        Debug.Log("Player morreu pela infecção!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
