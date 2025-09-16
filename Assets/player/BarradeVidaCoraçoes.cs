using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaCoracoes : MonoBehaviour
{
    public Image[] coracoes;       // Array com as imagens dos corações
    public Sprite coracaoCheio;
    public Sprite coracaoVazio;

    public void AtualizarVida(int vidas)
    {
        vidas = Mathf.Clamp(vidas, 0, coracoes.Length);

        for (int i = 0; i < coracoes.Length; i++)
        {
            coracoes[i].sprite = (i < vidas) ? coracaoCheio : coracaoVazio;
        }
    }
}
