using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinsLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Você chegou no fim do mapa. WINS");
        }
    }
}
