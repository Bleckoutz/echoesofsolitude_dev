using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BastaoPegavel : MonoBehaviour
{
    // Este script pode ficar vazio, pois a detecção e coleta do bastão será feita
    // no PlayerController via OnTriggerEnter2D.
    // Você pode usar este script para colocar configurações extras, se quiser.

    private void Reset()
    {
        // Garante que o Collider2D deste objeto seja trigger para facilitar o pickup
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.isTrigger = true;
    }
}

