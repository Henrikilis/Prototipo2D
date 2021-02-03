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
    public float tapJumpForce;
    public float jumpForce;
    public float superJumpForce;
    // public 

    private float inputX;
    [SerializeField]
    private bool isGrounded;

    

    void Start()
    {
    


    }
    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, radius, whatIsGround);
    }

    private void FixedUpdate()
    {      
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
    }

    public void Move(InputAction.CallbackContext context)
    {

        inputX = context.ReadValue<Vector2>().x;

    }

    public void TapJump(InputAction.CallbackContext context)
    {
        if (isGrounded && context.performed)
        {
            rb.velocity = new Vector2(rb.velocity.x, tapJumpForce);
            Debug.Log("tapJumping");
        }


    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded && context.performed)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            Debug.Log("jumping");
        }

    }

    public void SuperJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rb.velocity = new Vector2(rb.velocity.x, superJumpForce);
            Debug.Log("Superjumping");

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(groundPoint.position, radius);
    }

}
