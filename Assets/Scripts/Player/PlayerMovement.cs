using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
 

    [Header("VALORES DO INPUT SYSTEM")]
    [SerializeField] private float horizontal;
    [SerializeField] private bool jumpHeld;

    [Header("Variáveis de controle")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool jumpStarted = false;
    [SerializeField] private bool isJumping = false;
    [SerializeField] private float jumpDuration = 0.15f;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpHoldForce;
    [SerializeField] private bool isOnGround;

    [Header("Variáveis de Raycast")]
    [SerializeField] private float leftOffSet;
    [SerializeField] private float rightOffSet;
    [SerializeField] private float groudOffSet;
    [SerializeField] private float groundDistance;
    [SerializeField] private LayerMask groudLayer;

    [Header("Coiote Time")]
    [SerializeField] private float coioteDuration;
    private float coioteTime;

    [Header("ANIMATOR")]
    [SerializeField] private Animator anim;


    [Header("Ladder")]
    [SerializeField] private float climbSpeed = 3;
    [SerializeField] private LayerMask ladderMask;
    [SerializeField] private float vertical;
    [SerializeField] private bool climbing;
    [SerializeField] private float checkRadius = 0.3f;
    [SerializeField] private Transform ladder;
    [SerializeField] private bool canMove = true;

    

    private float direction = 1;
    private float rightInit;
    private float leftInit;
    private Rigidbody2D rig;
    private PlayerShot playerShot;
    private Collider2D col;
    private GameController _GameController;
    private LimiteTela playerLimiteTela;


    //GETTERS E SETTERS
    public float Direction
    {
        get { return direction; }
    }
    // Start is called before the first frame update
    void Start()
    {
        playerLimiteTela = GetComponent<LimiteTela>();
        playerShot = GetComponent<PlayerShot>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        rightInit = rightOffSet;
        leftInit = leftOffSet;
        rig = GetComponent<Rigidbody2D>();

        _GameController = FindObjectOfType<GameController>();
        _GameController.PlayerTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        verificarLimite();
        flip();
        controlAnimation();
    }
    private void FixedUpdate()
    {
        physicsCheck();
        movement();
        jumpControl();
        climbLabber();
        
    }
    public void verificarLimite()
    {
        rig.position = new Vector2(Mathf.Clamp(rig.position.x, playerLimiteTela.XMin, playerLimiteTela.XMax),
            Mathf.Clamp(rig.position.y, playerLimiteTela.YMin, playerLimiteTela.Ymax));
    }
    private void climbLabber()
    {
        bool up = Physics2D.OverlapCircle(transform.position, checkRadius, ladderMask);
        bool down = Physics2D.OverlapCircle(transform.position + new Vector3(0, -1), checkRadius, ladderMask);

        if(vertical != 0 && touchingLadder())
        {
            climbing = true;
            rig.isKinematic = true;
            canMove = false;

            float xPos = ladder.position.x;
            transform.position = new Vector2(xPos, transform.position.y);
        }

        if (climbing)
        {
            if(!up && vertical >= 0)
            {
                finishClimb();
                return;
            }

            if(!down && vertical <= 0)
            {
                finishClimb();
                return;
            }

            float y = vertical * climbSpeed;

            rig.velocity = new Vector2(0, y);
        }
    }

    bool touchingLadder()
    {
        return col.IsTouchingLayers(ladderMask);
    }
    void finishClimb()
    {
        climbing = false;
        rig.isKinematic = false;
        canMove = true;
    }

    void flip()
    {
        if(horizontal > 0)
        {
            setOffSet(this.leftInit, this.rightInit);
            direction = 1;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(horizontal < 0)
        {
            setOffSet(-this.leftInit, -this.rightInit);
            direction = -1;
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        void setOffSet(float leftInit, float rightInit)
        {
            leftOffSet = leftInit;
            rightOffSet = rightInit;
        }
    }

    void controlAnimation()
    {
        if((horizontal > 0 || horizontal < 0) && isOnGround && playerShot.IsLoadingAtack)
        {
            anim.SetInteger("transition", 4);
        }
        else if(horizontal == 0 && isOnGround && playerShot.IsLoadingAtack)
        {
            anim.SetInteger("transition", 3);
        }else if (!isOnGround)
        {
            anim.SetInteger("transition", 2);
        }else if((horizontal > 0 || horizontal < 0) && isOnGround && !playerShot.IsLoadingAtack)
        {
            anim.SetInteger("transition", 1);
        }else if(horizontal == 0 && isOnGround && !playerShot.IsLoadingAtack && canMove) 
        {
            anim.SetInteger("transition", 0);
        }
    }

    void physicsCheck()
    {

        RaycastHit2D leftFoot = raycast(new Vector2(-leftOffSet, -groudOffSet), Vector2.down, groundDistance, groudLayer);
        RaycastHit2D rightFoot = raycast(new Vector2(rightOffSet, -groudOffSet), Vector2.down, groundDistance, groudLayer);

        if(leftFoot || rightFoot)
        {
            isOnGround = true;
        }else if(!(leftFoot || rightFoot))
        {
            isOnGround = false;
        }

    }
    void movement()
    {
        if (!canMove)
            return;

        rig.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, rig.velocity.y);

        if (isOnGround)
        {
            coioteTime = Time.time + coioteDuration;
        }
    }

    void jumpControl()
    {
        if (jumpStarted && (isOnGround || coioteTime > Time.time))
        {
            isJumping = true;
            jumpStarted = false;
            rig.velocity = Vector2.zero;

            rig.AddForce(jumpForce * Vector2.up * Time.fixedDeltaTime, ForceMode2D.Impulse);

            jumpTime = Time.time + jumpDuration;
            coioteTime = Time.time;
        }

        if (isJumping)
        {
            if (jumpHeld)
            {
                rig.AddForce(jumpHoldForce * Vector2.up * Time.deltaTime, ForceMode2D.Impulse);
            }

            if(jumpTime < Time.time)
            {
                isJumping = false;
            }
        }

        jumpStarted = false;
    }

    // Rayscat function
    RaycastHit2D raycast(Vector2 offset, Vector2 rayDirection, float lenght, LayerMask mask)
    {
        Vector2 pos = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, lenght, mask);

        Color color = hit ? Color.red : Color.green;

        Debug.DrawRay(pos + offset, rayDirection * lenght, color);


        return hit;
    }


    // FUNÇÕES USADAS NO INPUT SYSTEM 
    public void move(InputAction.CallbackContext callback)
    {
        horizontal = callback.ReadValue<float>();
    }

    public void climb(InputAction.CallbackContext callback)
    {
        vertical = callback.ReadValue<float>();
    }

    public void jump(InputAction.CallbackContext callback)
    {
        if (callback.started)
        {
            jumpStarted = true;
        }
        jumpHeld = callback.performed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ladder"))
        {
            ladder = collision.transform;
        }
    }
}