using UnityEngine;

public class BarreiraController : MonoBehaviour
{
    public string tagDosInimigos = "Inimigo";

    private void Update()
    {
        // Verifica a quantidade de inimigos com a tag
        GameObject[] inimigos = GameObject.FindGameObjectsWithTag(tagDosInimigos);

        // Se n�o existir mais nenhum, destr�i a barreira
        if (inimigos.Length == 0)
        {
            Destroy(gameObject);
        }
    }
}
