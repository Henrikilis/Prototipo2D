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
    public float jumpForce;

    private float inputX;
    [SerializeField]
    private bool isGrounded;

    

    void Start()
    {
    


    }

    
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundPoint.position, radius, whatIsGround);

        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);

    }

    public void Move(InputAction.CallbackContext context)
    {

        inputX = context.ReadValue<Vector2>().x;

    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(groundPoint.position, radius);
    }

}
