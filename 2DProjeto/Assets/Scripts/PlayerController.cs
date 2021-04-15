using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public Transform groundPoint;
    public LayerMask whatIsGround;
    public Rigidbody2D rb;

    public float moveSpeed;

    public Animator anim;

    [Header("Jump")]

    [Range(1, 20)]
    public float initialJumpForce;
    [Range(0, 5)]
    public float lowFallMultiplier;

    public float jumpDelay;
    [SerializeField]
    private float afterJump;
    private float afterPress = 50;
    private float futureJump;
    public float gravity = 1f;

    public bool physicsAllow;
    private float inputX;
    public bool isGrounded;

    [Header("Roll")]
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public bool rollPressed;
    public bool facingRight;

    public Vector2 boxSize;

    [Header("Camera")]
    public CinemachineVirtualCamera vcam;
    public GameObject downCam;
    public GameObject regularCam;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
        physicsAllow = true;

        vcam = GameObject.Find("vcam").gameObject.GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {

      

        // FLIP
        if(inputX > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            facingRight = true;
        }
        else if(inputX < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            facingRight = false;
        }

        // GROUND CHECK  
        isGrounded = Physics2D.OverlapBox(groundPoint.position, boxSize, 0f, whatIsGround);
        anim.SetBool("isGrounded", isGrounded);

        // ROLL
        if (direction == 0)
        {
            if (rollPressed)
            {
                if (!facingRight)
                {
                    direction = 1;
                }
                else if (facingRight)
                {
                    direction = 2;
                }
            }
        }
        else
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
                rollPressed = false;
                gameObject.GetComponent<PlayerHealth>().dontMove = false;
                gameObject.layer = 9;
            }
            else
            {
                dashTime -= Time.deltaTime;

                if (direction == 1)
                {
                    rb.velocity = Vector2.left * dashSpeed;
                    Debug.Log("ESQUERDA");
                }
                else if (direction == 2)
                {
                    rb.velocity = Vector2.right * dashSpeed;
                    Debug.Log("DIREITA");
                }
            }
        }
        
    }

 
    private void FixedUpdate()
    {

        if (!isGrounded)
        {
            afterJump = afterJump + 1;
        }
        if (isGrounded)
            afterJump = 0;

        // MOVIMENTO BASICO
        if (gameObject.GetComponent<PlayerHealth>().dontMove == false)
         rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);

        // VERIFICAÇÃO IDLE
        anim.SetFloat("Speed", inputX);
        if (inputX == 0)
            anim.SetBool("Idle", true);
        else { anim.SetBool("Idle", false); }


        // FISICA DO PULO

        if (physicsAllow)
        {
            if (rb.velocity.y < 0)
            {

                rb.gravityScale = gravity * (lowFallMultiplier / 2);
            }
            else if (rb.velocity.y > 0 && !Gamepad.current.buttonSouth.isPressed)
            {
                rb.gravityScale = gravity * lowFallMultiplier;
            }
        }
        // PULO
        if (afterPress < 5 || futureJump > Time.time && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * initialJumpForce, ForceMode2D.Impulse);
            afterPress = 50;
            futureJump = 0;

        }
    }

    public void Roll(InputAction.CallbackContext context)
    {
        if (isGrounded && context.performed) {
            gameObject.GetComponent<PlayerHealth>().dontMove = true;
            rollPressed = true;
            gameObject.layer = 11;
            anim.SetTrigger("Roll");
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        
        inputX = context.ReadValue<Vector2>().x;

    }

    public void Jump(InputAction.CallbackContext context)
    {
        //    pressed = true;
        if (context.performed)
        {
            afterPress = afterJump;
            futureJump = Time.time + jumpDelay;
            anim.SetTrigger("Jump");
        }
    }

    public void LookDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("OLHEI PARA BAIXO");
            downCam.SetActive(true);
            regularCam.SetActive(false);
        }
        else { Debug.Log("DESOLHEI");
            downCam.SetActive(false);
            regularCam.SetActive(true);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundPoint.position, boxSize);
    }
    

}
