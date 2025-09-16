using UnityEngine;



public class BastaoVoador : MonoBehaviour
{
    public float dano = 10f;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        if (animator != null)
            animator.SetBool("voando", true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Inimigo"))
        {
            other.SendMessage("LevarDano", dano, SendMessageOptions.DontRequireReceiver);

            if (animator != null)
                animator.SetBool("voando", false);

            Destroy(gameObject, 0.1f);
        }
    }
}
