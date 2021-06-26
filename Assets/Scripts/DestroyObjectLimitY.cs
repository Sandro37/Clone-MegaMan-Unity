using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectLimitY : MonoBehaviour
{
    [Header("Ponto morte Y")]
    [SerializeField] private Transform CheckPontoDead;
    private GameController _GameController;

    // Start is called before the first frame update
    private void Start()
    {
        _GameController = FindObjectOfType<GameController>();

    }

    private void Update()
    {
        if (gameObject.transform.position.y <= CheckPontoDead.position.y)
        {
            Dead();
        }
    }
    public void Dead()
    {
        if (this.gameObject.CompareTag("Player"))
        {
            _GameController.restartGame();
        }

        if(this.gameObject.layer == 10)
        {
            Destroy(gameObject);
        }
    }
}
