using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        StartCoroutine(timePress());
        
    }

    IEnumerator timePress()
    {
        if(anim != null)
        {
            anim.enabled = false;
            float range = Random.Range(0.1f, 1.0f);
            yield return new WaitForSeconds(range);
            anim.enabled = true;
        }
    }
}
