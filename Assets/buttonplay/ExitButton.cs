using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // Esse m�todo ser� chamado pelo bot�o
    public void QuitGame()
    {
        Debug.Log("Aplicativo est� fechando...");
        Application.Quit();

        // Para o editor do Unity funcionar o teste:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
