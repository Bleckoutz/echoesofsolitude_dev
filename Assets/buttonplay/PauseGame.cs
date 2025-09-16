using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private bool jogoPausado = false;
    public AudioSource musicaFundo;   // arraste a música aqui no Inspector
    public GameObject painelPause;    // arraste o painel pause aqui (com botão Exit dentro)

    public void AlternarPause()
    {
        if (jogoPausado)
        {
            // Despausar
            Time.timeScale = 1f;

            if (musicaFundo != null)
                musicaFundo.UnPause();

            if (painelPause != null)
                painelPause.SetActive(false);

            jogoPausado = false;
        }
        else
        {
            // Pausar
            Time.timeScale = 0f;

            if (musicaFundo != null)
                musicaFundo.Pause();

            if (painelPause != null)
                painelPause.SetActive(true);

            jogoPausado = true;
        }
    }
}
