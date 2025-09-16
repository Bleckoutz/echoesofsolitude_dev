using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BastaoPegavel : MonoBehaviour
{
    // Este script pode ficar vazio, pois a detec��o e coleta do bast�o ser� feita
    // no PlayerController via OnTriggerEnter2D.
    // Voc� pode usar este script para colocar configura��es extras, se quiser.

    private void Reset()
    {
        // Garante que o Collider2D deste objeto seja trigger para facilitar o pickup
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.isTrigger = true;
    }
}

