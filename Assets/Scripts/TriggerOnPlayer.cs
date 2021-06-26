using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOnPlayer : MonoBehaviour
{

    public UnityEvent OnTrigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnTrigger.Invoke(); 
        }    
    }
}
