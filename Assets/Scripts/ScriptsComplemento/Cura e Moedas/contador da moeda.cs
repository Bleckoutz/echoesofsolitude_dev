using UnityEngine;
using TMPro;

public class ContadorMoedas : MonoBehaviour
{
    public TextMeshProUGUI textoMoedas;

    public void AtualizarMoedas(int valor)
    {
        if (textoMoedas != null)
            textoMoedas.text = valor.ToString();
    }
}
