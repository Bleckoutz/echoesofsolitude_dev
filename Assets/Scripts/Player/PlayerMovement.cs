using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpForce = 10f;
    private Rigidbody2D rb;
    private float moveX;
    private bool isGrounded = false;
    private SpriteRenderer sprite;
    private Animator animator;

    [Header("UI")]
    public GameObject gameOverPanel;

    [Header("Painel de Puzzle")]
    public GameObject painelLaboratorio;
    void Start()
    {
        // Instância os componentes
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        // Seta como inativo os paineis (game over e laboratorio)
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (painelLaboratorio != null) painelLaboratorio.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        // Movimenta o player na horizontal
        rb.velocity = new Vector2(moveX * moveSpeed, rb.velocity.y);
        // Alterna a sprite quando movimenta para trás
        if (moveX > 0) sprite.flipX = false;
        else if (moveX < 0) sprite.flipX = true;
    }
}
