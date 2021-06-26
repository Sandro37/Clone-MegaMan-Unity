using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PierrotbotDamage : MonoBehaviour
{
    [SerializeField] private int damage;

    void DoDamage(Damageable damageable)
    {
        damageable.TakeDamage(damage);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Damageable damageable = collision.gameObject.GetComponent<Damageable>();

        if (damageable != null)
        {
            DoDamage(damageable);
        }
    }
}
