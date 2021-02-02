using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;

    private float inputX; 

    void Start()
    {
    


    }

    
    void Update()
    {
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);

    }

    public void Move(InputAction.CallbackContext context)
    {

        inputX = context.ReadValue<Vector2>().x;

    }

}
