using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private bool destroyOnDamage;

    void DoDamage(Damageable damageable)
    {
        damageable.TakeDamage(damage);

        if (destroyOnDamage)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable damageable = collision.gameObject.GetComponent<Damageable>();

        if(damageable != null)
        {
            DoDamage(damageable);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            DoDamage(damageable);
        }
    }
}
