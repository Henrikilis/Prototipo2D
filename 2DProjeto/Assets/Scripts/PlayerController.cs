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

    [Range(1,20)]
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

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if(inputX > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if(inputX < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }

        isGrounded = Physics2D.OverlapCircle(groundPoint.position, radius, whatIsGround);

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;

        }

        if (pressed)
        {
            if (isGrounded && Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;

            }
            if (Gamepad.current.buttonSouth.isPressed && isJumping)
            {
                
                //  Debug.Log("SuperJump!");
                if (jumpTimeCounter > 0)
                {
                    Debug.Log(jumpTimeCounter);
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;

                }
                else
                {
                    isJumping = false;
                    rb.velocity += Vector2.up * Physics2D.gravity.y * (lowFallMultiplier - 1) * Time.deltaTime;
                }

            }
            if (Gamepad.current.buttonSouth.wasReleasedThisFrame)
            {
                isJumping = false;
            }

        }

    }

    private void FixedUpdate()
    {        
    if (gameObject.GetComponent<PlayerHealth>().takingDamage == false)
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
    }

    
    public void Move(InputAction.CallbackContext context)
    {
        
        inputX = context.ReadValue<Vector2>().x;

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded && Gamepad.current.buttonSouth.wasPressedThisFrame)
            pressed = true;
        if (isGrounded && Gamepad.current.buttonSouth.wasReleasedThisFrame)
            pressed = false;

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(groundPoint.position, radius);
    }
    

}
