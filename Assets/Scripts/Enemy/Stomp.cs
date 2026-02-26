using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Stomp : MonoBehaviour
{
    [Header("colidir com o player")]
    public float force;
    private bool stomp = false;

    [Header("vida do inimigo")]
    public float vida = 20f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            playerRb.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
            stomp = true;
            vida -= 20f; // Dano causado ao inimigo
            if (vida <= 0)
            Debug.Log("Inimigo levou dano! Vida restante: " + vida);
            Morrer();

            //desativar o box collider do inimigo para evitar múltiplos contatos
            BoxCollider2D boxCollider = transform.parent.GetComponent<BoxCollider2D>();
            boxCollider.enabled = false;
        }

    }

    private void Morrer()
    {
        Debug.Log("Inimigo morreu!");
        Destroy(gameObject);
    }
}
