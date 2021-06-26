using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingPlatform : MonoBehaviour
{
    [Header("DIREÇÃO DA PLATAFORMAR (ESQUERDA = -1/ DIREITA = 1)")]
    [SerializeField] private int direction;

    [Header("FORÇA DE ACELERAÇÃO DA PLATAFORMA")]
    [SerializeField] private float force;

    [Header("SPRITE RENDERER")]
    [SerializeField] private SpriteRenderer[] spriteRenderer;
    [SerializeField] private Sprite right, left, empty;

    private List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Rolling());
    }
    private void FixedUpdate()
    {
        foreach (Rigidbody2D rig in rigidbodies)
        {
            rig.AddForce(Vector2.right * direction * force * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();

        if(rig != null)
        {
            rigidbodies.Add(rig);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Rigidbody2D rig = collision.gameObject.GetComponent<Rigidbody2D>();

        if (rig != null)
        {
            if (rigidbodies.Contains(rig))
            {
                rigidbodies.Remove(rig);
            }
        }
    }

    IEnumerator Rolling()
    {
        while (true)
        {
            if(direction > 0)
            {
                foreach (SpriteRenderer item in spriteRenderer)
                {
                    item.sprite = empty;
                }
                yield return new WaitForSeconds(0.1f);

                foreach (SpriteRenderer item in spriteRenderer)
                {
                    item.sprite = right;
                }
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                foreach (SpriteRenderer item in spriteRenderer)
                {
                    item.sprite = empty;
                }
                yield return new WaitForSeconds(0.1f);

                foreach (SpriteRenderer item in spriteRenderer)
                {
                    item.sprite = left;
                }
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
