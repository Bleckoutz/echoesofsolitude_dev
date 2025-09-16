using UnityEngine;
using UnityEngine.SceneManagement;  // Necess�rio para mudar de cena

public class PlayButtonHandler : MonoBehaviour
{
    // Este m�todo ser� chamado quando o bot�o "Play" for pressionado
    public void OnPlayButtonPressed()
    {
        // Exemplo: Carregar uma cena chamada "GameScene"
        SceneManager.LoadScene("cena 3");  // Substitua "GameScene" pelo nome real da sua cena
    }
}
