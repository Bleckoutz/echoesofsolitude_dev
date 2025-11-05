using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public int vidas = 3;
    public BarraDeVidaCoracoes barraDeVidaUI;
    public GameObject gameOverPanel;
    public AudioSource musicaDeFundo;

    private void Start()
    {
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        barraDeVidaUI?.AtualizarVida(vidas);
    }

    public void PerderVida()
    {
        vidas = Mathf.Max(vidas - 1, 0);
        barraDeVidaUI?.AtualizarVida(vidas);
        if (vidas <= 0) GameOver();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Inimigo"))
            PerderVida();
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        if (musicaDeFundo != null && musicaDeFundo.isPlaying)
            musicaDeFundo.Pause();
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void ReiniciarJogo()
    {
        Time.timeScale = 1f;
        if (musicaDeFundo != null)
            musicaDeFundo.UnPause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PerderVida(int quantidade = 1)
    {
        vidas -= quantidade;
        barraDeVidaUI?.AtualizarVida(vidas);

        if (vidas <= 0)
            GameOver();
    }

}
