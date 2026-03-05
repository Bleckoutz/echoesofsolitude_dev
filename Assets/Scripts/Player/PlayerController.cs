using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement movimento;
    private PlayerAttack ataque;
    private PlayerLife vida;
    private PlayerInteraction interacao;

    private void Awake()
    {
        movimento = GetComponent<PlayerMovement>();
        ataque = GetComponent<PlayerAttack>();
        vida = GetComponent<PlayerLife>();
        interacao = GetComponent<PlayerInteraction>();
    }

    // 🪙 Expor moedas (vem do PlayerInteraction)
    public int moedas
    {
        get
        {
            if (interacao == null) return 0;
            return interacao.moedas;
        }
        set
        {
            if (interacao == null) return;
            interacao.moedas = value;
            interacao.contadorMoedasUI?.AtualizarMoedas(value);
        }
    }
    public int itens
    {
        get
        {
            if (interacao == null || interacao.contadorItens == null) return 0;
            return interacao.contadorItens.totalItens;
        }
        set
        {
            if (interacao == null || interacao.contadorItens == null) return;
            interacao.contadorItens.totalItens = value;
            interacao.contadorItens.textoContador.text = "Itens: " + value;
        }
    }

    //  Expor vidas (vem do PlayerLife)
    public int vidas
    {
        get
        {
            if (vida == null) return 0;
            return vida.vidas;
        }
        set
        {
            if (vida == null) return;
            vida.vidas = value;
            // Atualiza a UI da vida somente se existir
            vida.barraDeVidaUI?.AtualizarVida(value);
        }
    }

    //  Expor contador de moedas UI (tipo do seu projeto)
    public ContadorMoedas contadorMoedasUI
    {
        get => interacao != null ? interacao.contadorMoedasUI : null;
        set
        {
            if (interacao != null) interacao.contadorMoedasUI = value;
        }
    }
    public ItemColetavel contadorItensUI
    {
        get => interacao != null && interacao.contadorItens != null ? interacao.contadorItens : null;
        set
        {
            if (interacao != null && interacao.contadorItens != null) interacao.contadorItens = value;
        }
    }

    //  Expor a barra de vida do tipo correto (BarraDeVidaCoracoes)
    public BarraDeVidaCoracoes barraDeVidaUI
    {
        get => vida != null ? vida.barraDeVidaUI : null;
        set
        {
            if (vida != null) vida.barraDeVidaUI = value;
        }
    }

    //  Função para perder vida (chama o método no PlayerLife)
    public void PerderVida()
    {
        if (vida != null)
            vida.PerderVida();
    }
}
