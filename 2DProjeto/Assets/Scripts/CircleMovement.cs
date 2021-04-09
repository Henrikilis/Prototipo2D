using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public powerBook pb;
    public PlayerController pc;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pb = FindObjectOfType<powerBook>();
        pc = FindObjectOfType<PlayerController>();
    }

  
    void Update()
    {

        if (pb.circleActive)
        {
            if (pc.facingRight)
                rb.AddForce(Vector2.right * 2, ForceMode2D.Force);
            else
                rb.AddForce(Vector2.left * 2, ForceMode2D.Force);
        }
    }
}
