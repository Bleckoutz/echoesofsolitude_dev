using UnityEngine;
using UnityEngine.SceneManagement;

public class BotaoExit : MonoBehaviour
{
    public string nomeCenaMenu = "Menu"; // nome da cena de menu

    public void SairParaMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nomeCenaMenu);
    }
}
