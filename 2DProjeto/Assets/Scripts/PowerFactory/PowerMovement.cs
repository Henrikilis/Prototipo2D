using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [Header("Dash")]
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public bool dashPressed;
    public GameObject dashParticle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        dashTime = startDashTime;
    }

    public void DashF()
    {
        Debug.Log("DashF");
        gameObject.GetComponent<PlayerHealth>().dontMove = true;
        dashPressed = true;
    }

    // Update is called once per frame
    void Update()
    {
        // DASH
        if (direction == 0)
        {
            if (dashPressed)
            {
                if (gameObject.GetComponent<PlayerController>().facingRight == false)
                {
                    direction = 1;
                }
                else if (gameObject.GetComponent<PlayerController>().facingRight == true)
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
                dashPressed = false;
                gameObject.GetComponent<PlayerHealth>().dontMove = false;
                anim.SetBool("isDashing", false);
            }
            else
            {
                ObjectPooler.Instance.SpawnFromPool("Dash", new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                dashTime -= Time.deltaTime;
                anim.SetBool("isDashing", true);

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
}
