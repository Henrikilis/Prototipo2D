using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    private float futureJump;
    public float gravity = 1f;
    

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
   
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTime = startDashTime;
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
       // MOVIMENTO BASICO
        if (gameObject.GetComponent<PlayerHealth>().dontMove == false)
         rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);

        // VERIFICAÇÃO IDLE
        anim.SetFloat("Speed", inputX);
        if (inputX == 0)
            anim.SetBool("Idle", true);
        else { anim.SetBool("Idle", false); }

        
        // FISICA DO PULO

        if (rb.velocity.y < 0)
        {
            
            rb.gravityScale = gravity * (lowFallMultiplier / 2);
        }
        else if(rb.velocity.y > 0 && !Gamepad.current.buttonSouth.isPressed)
        {
            rb.gravityScale = gravity * lowFallMultiplier ;
        }

        // PULO
        if (futureJump > Time.time && isGrounded)
        {   
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * initialJumpForce, ForceMode2D.Impulse);
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
            futureJump = Time.time + jumpDelay;
            anim.SetTrigger("Jump");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(groundPoint.position, boxSize);
    }
    

}
