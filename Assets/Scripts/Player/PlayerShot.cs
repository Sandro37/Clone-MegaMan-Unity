using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerShot : MonoBehaviour
{
    [SerializeField] private Rigidbody2D shotPrefab;
    [SerializeField] private Transform pointShot;
    [SerializeField] private float speed;
    [SerializeField] private float fireRate;
    [SerializeField] private float totalCharge;
    [SerializeField] private float totalChargeTime;
    
    [SerializeField] private float charging = 1;
    private float nextFire;
    private InputAction.CallbackContext shootPhase;
    private PlayerMovement playerMovement;
    private bool isLoadingAtack = false;

    //GETTERS E SETTERS
    public bool IsLoadingAtack
    {
        get { return isLoadingAtack; }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {

        if (shootPhase.started)
        {
            charging += Time.deltaTime * ((totalCharge - 1) / totalChargeTime);
            isLoadingAtack = true;
        }

        charging = Mathf.Clamp(charging, 1, totalCharge);
    }

    void shoot()
    {

        if (Time.time < nextFire)
        {
            return;
        }
        nextFire = Time.time + fireRate;
        Rigidbody2D newShot = Instantiate(shotPrefab, pointShot.position, Quaternion.identity);
        newShot.velocity = Vector2.right * speed * playerMovement.Direction;

        newShot.transform.localScale *= charging;

        charging = 1;

        Destroy(newShot.gameObject, 2f);

    }

    // Input System
    public void onShoot(InputAction.CallbackContext callback)
    {
        shootPhase = callback;

        if (shootPhase.canceled)
        {
            isLoadingAtack = false;
            shoot();
        }
    }
}
