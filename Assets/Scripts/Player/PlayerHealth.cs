using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Damageable
{
    private int defaultLayer;
    private GameController _GameController;
    public override void Death()
    {
        _GameController.restartGame();
    }
    private void Update()
    {
        _GameController.setLifeText(currentHealth);
    }
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        _GameController = FindObjectOfType<GameController>();
        defaultLayer = gameObject.layer; 
    }

    public void SetInvicible(bool state)
    {
        if (state)
        {
            gameObject.layer = LayerMask.NameToLayer("Invencible");
        }
        else
        {
            gameObject.layer = defaultLayer; 
        }
    }
}
