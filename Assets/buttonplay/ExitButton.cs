using UnityEngine;

public class ExitButton : MonoBehaviour
{
    // Esse método será chamado pelo botão
    public void QuitGame()
    {
        Debug.Log("Aplicativo está fechando...");
        Application.Quit();

        // Para o editor do Unity funcionar o teste:
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
