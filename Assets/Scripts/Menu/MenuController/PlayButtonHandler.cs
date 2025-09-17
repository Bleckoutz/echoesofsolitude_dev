using UnityEngine;
using UnityEngine.SceneManagement;  // Necessário para mudar de cena

public class PlayButtonHandler : MonoBehaviour
{
    // Este método será chamado quando o botão "Play" for pressionado
    public void OnPlayButtonPressed()
    {
        // Exemplo: Carregar uma cena chamada "GameScene"
        SceneManager.LoadScene("cena 3");  // Substitua "GameScene" pelo nome real da sua cena
    }
}
