using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Bastãodano : MonoBehaviour
{
    [Header("Bastão")]
    public GameObject bastao;
    public Transform mao;
    private bool estaSegurandoBastao = false;
    public GameObject bastaoVoadorPrefab;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            Atacar();
       
    }

    void Atacar()
    {
        if (!estaSegurandoBastao) return;

        Destroy(bastao);
        estaSegurandoBastao = false;

        GameObject bastaoInstanciado = Instantiate(bastaoVoadorPrefab, mao.position, Quaternion.identity);

        Animator anim = bastaoInstanciado.GetComponent<Animator>();
        if (anim != null) anim.SetBool("voando", true);

        Rigidbody2D rbBastao = bastaoInstanciado.GetComponent<Rigidbody2D>();
        if (rbBastao != null)
        {
            rbBastao.isKinematic = false;
            rbBastao.simulated = true;
            float direcao = sprite.flipX ? -1f : 1f;
            rbBastao.velocity = new Vector2(10f * direcao, 0f);
        }

        Collider2D colBastao = bastaoInstanciado.GetComponent<Collider2D>();
        if (colBastao != null)
        {
            colBastao.enabled = true;
            colBastao.isTrigger = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bastao"))
        {
            if (bastao != null) Destroy(bastao);

            estaSegurandoBastao = true;
            bastao = other.gameObject;

            bastao.transform.SetParent(mao);
            bastao.transform.localPosition = Vector3.zero;
            bastao.transform.localRotation = Quaternion.identity;

            Rigidbody2D rbBastao = bastao.GetComponent<Rigidbody2D>();
            if (rbBastao != null)
            {
                rbBastao.velocity = Vector2.zero;
                rbBastao.isKinematic = true;
                rbBastao.simulated = false;
            }

            Collider2D colBastao = bastao.GetComponent<Collider2D>();
            if (colBastao != null) colBastao.enabled = false;
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
