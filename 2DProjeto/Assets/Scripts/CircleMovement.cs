using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public powerBook pb;
    public PlayerController pc;
    public float speed;
    [SerializeField]
    private bool lockRight = false;
    [SerializeField]
    private bool lockLeft = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pb = FindObjectOfType<powerBook>();
        pc = FindObjectOfType<PlayerController>();
    }


    void Update()
    {


        if (pc.facingRight && pb.circleActive && !lockLeft)
        {
            lockRight = true;   
        }

        if (pb.circleActive && !pc.facingRight && !lockRight)
        {
            lockLeft = true;     
        }

       

        if (lockRight)
        {
            rb.velocity = new Vector2(speed * 1, rb.velocity.y);
            lockLeft = false;
        }
        
        else if (lockLeft)
        {
            //  rb.AddForce(Vector2.left * speed, ForceMode2D.Force);
            rb.velocity = new Vector2(speed * -1, rb.velocity.y);
            lockRight = false;
        }

    }

    private void OnDisable()
    {
        lockLeft = false;
        lockRight = false;
       
    }

}
