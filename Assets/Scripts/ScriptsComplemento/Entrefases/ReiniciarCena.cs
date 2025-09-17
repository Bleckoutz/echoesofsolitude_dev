using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciarCena : MonoBehaviour
{
    public void ReiniciarParaCena3()
    {
        Time.timeScale = 1f; // garante que o tempo volte ao normal
        SceneManager.LoadScene(2); // carrega a cena de índice 3
    }
}
