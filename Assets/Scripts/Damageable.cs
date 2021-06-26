using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Damageable : MonoBehaviour
{
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int currentHealth;
    [SerializeField] protected float invicibleTime;


    public UnityEvent OnDamage, OnFinishDamage, OnDeath;

    private bool canTakeDamage = true;
    private SpriteRenderer spriteRenderer;
    private Color defaultColor;
    // Start is called before the first frame update
    protected void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (!canTakeDamage)
            return;

        canTakeDamage = false;
        currentHealth -= amount;
        OnDamage.Invoke();
        StartCoroutine(TakingDamage());
        if(currentHealth <= 0)
        {
            OnDeath.Invoke();
            Death();
        }
    }

    private float takingDamageTimeWaiForSeconds = 0.05f;
    IEnumerator TakingDamage()
    {
        float timer = 0;

        while(timer < invicibleTime)
        {
            spriteRenderer.color = Color.clear;
            yield return new WaitForSeconds(takingDamageTimeWaiForSeconds);
            spriteRenderer.color = defaultColor;
            yield return new WaitForSeconds(takingDamageTimeWaiForSeconds);
            timer += 0.1f;
        }

        spriteRenderer.color = defaultColor;
        canTakeDamage = true;
        OnFinishDamage.Invoke();

    }
    public abstract void Death();
}
