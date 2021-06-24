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
    public bool upDashPressed;
    public GameObject dashParticle;

    [Header("Double Jump")]
    public float doubleJumpForce;
    public float doubleJumpTime;
    [Header("Stomp")]
    public float stompSpeed;
    [SerializeField]
    private float stompDuration;
    private bool isStomping = false;
    [SerializeField]
    private bool endStomp = true;

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
    public void DashU()
    {
        Debug.Log("DashUp");
        gameObject.GetComponent<PlayerHealth>().dontMove = true;
        upDashPressed = true;
    }

    public void DoubleJump()
    {
        gameObject.GetComponent<PlayerController>().physicsAllow = false;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
    }
    public void Stomp()
    {
        if (!GetComponent<PlayerController>().isGrounded)
        {
            endStomp = false;
            stompDuration = 0;
            isStomping = true;
            gameObject.GetComponent<PlayerHealth>().dontMove = true;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(Vector2.down * stompSpeed, ForceMode2D.Impulse);
        }
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

        // UP DASH
        if (upDashPressed)
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rb.velocity = Vector2.zero;
                upDashPressed = false;
                gameObject.GetComponent<PlayerHealth>().dontMove = false;
                anim.SetBool("isDashing", false);
            }
            else
            {
                ObjectPooler.Instance.SpawnFromPool("Dash", new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.identity);
                anim.SetBool("isDashing", true);
                gameObject.GetComponent<PlayerController>().physicsAllow = false;
                dashTime -= Time.deltaTime;
                rb.velocity = Vector2.up * dashSpeed;
            }
        }

        // STOMP
        if (rb.velocity.y < 0 && isStomping)
        {
            stompDuration += Time.deltaTime;

        }
        else
        {
            isStomping = false;
        }
        if (!isStomping && !endStomp && stompDuration > 0)
        {
            stompDuration -= Time.deltaTime;

            if (stompDuration <= 0)
            {
                stompDuration = 0;
                gameObject.GetComponent<PlayerHealth>().dontMove = false;
                endStomp = true;
            }
        }
    }
}
