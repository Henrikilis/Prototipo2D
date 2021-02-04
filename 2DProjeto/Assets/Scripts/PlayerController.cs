using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
 

    public Transform groundPoint;
    public float radius;
    public LayerMask whatIsGround;
    public Rigidbody2D rb;

    public float moveSpeed;



    [Header("Jump")]

    [Range(1, 20)]
    public float initialJumpForce;
    [Range(0,1)]
    public float jumpForce;
    [Range(0,1)]
    public float jumpTime;
    [Range(0, 10)]
    public float fallMultiplier;
    [Range(0, 5)]
    public float lowFallMultiplier;
    // public 

    private float inputX;
    private bool isGrounded;
    private bool isJumping;
    private bool pressed = false;
    private float jumpTimeCounter;

    [Header("Roll")]
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public bool rollPressed;
    public bool facingRight;

    void Start()
    {
        dashTime = startDashTime;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, radius, whatIsGround);

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }

        // PULO

        if (pressed)
        {
            if (isGrounded)
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rb.AddForce(Vector2.up * initialJumpForce, ForceMode2D.Impulse);

            }
            if (Gamepad.current.buttonSouth.isPressed && isJumping)
            {


                if (jumpTimeCounter > 0)
                {

                    rb.velocity += Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;

                }
                else
                {
                    isJumping = false;
                    pressed = false;
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (lowFallMultiplier - 1) * Time.deltaTime;
                }

            }
            if (!Gamepad.current.buttonSouth.isPressed)
            {
                isJumping = false;
                pressed = false;
            }

        }

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

       

    }

    public void Roll(InputAction.CallbackContext context)
    {
        gameObject.GetComponent<PlayerHealth>().dontMove = true;
        rollPressed = true;
        gameObject.layer = 11;
    }

    public void Move(InputAction.CallbackContext context)
    {
        
        inputX = context.ReadValue<Vector2>().x;

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded && Gamepad.current.buttonSouth.wasPressedThisFrame)
            pressed = true;
        

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(groundPoint.position, radius);
    }
    

}
