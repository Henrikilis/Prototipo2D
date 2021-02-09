using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class powerBook : MonoBehaviour
{
    [Header("Powers")]
    public Rigidbody2D rb;
    public string[] powers;
    [SerializeField]
    private int currentPower;


    [Header("DoubleJump")]    
    public float doubleJumpForce;
    private float doubleJumpCD;
    public float doubleJumpTime;
    public bool doubleJumpCDactive;

    [Header("Dash")]
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public bool dashPressed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPower = 0;
        Debug.Log(powers[currentPower]);
        dashTime = startDashTime;
    }

    void Update()
    {
        if (doubleJumpCDactive)
        {
            doubleJumpCD -= Time.deltaTime;
            if (doubleJumpCD <= 0)
            {
                doubleJumpCD = doubleJumpTime;
                doubleJumpCDactive = false;
            }
        }

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

    public void DoubleJump()
    {
        if(doubleJumpCDactive == false)
        {
            Debug.Log("doubleJump");
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
            doubleJumpCDactive = true;
        }  
    }

    public void Stomp()
    {
        Debug.Log("Stomp");

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

    }

    public void Deflect()
    {


    }

    public void Throw()
    {


    }

    public void Shield()
    {


    }

    public void Circle()
    {


    }

    public void SwapBlue()
    {


    }

    public void SwapRed()
    {


    }

    public void SlowDown()
    {


    }

    public void SpeedUp()
    {


    }

    public void SwapLeft(InputAction.CallbackContext context)
    {
        if (context.performed) {


            if (ArrayCheckMinus(currentPower))
            {
                currentPower = powers.Length - 1;
                Debug.Log(powers[currentPower]);

            }
            else
            {
                currentPower--;
                Debug.Log(powers[currentPower]);
            }
        }
    }

    public void SwapRight(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

            if (ArrayCheckPlus(currentPower))
            {
                currentPower = 0;
                Debug.Log(powers[currentPower]);
            }
            else
            {
                currentPower++;
                Debug.Log(powers[currentPower]);
            }
        }
    }

    public void Power1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(powers[currentPower] == "doublejump")
            {
                DoubleJump();

            }
            else if(powers[currentPower] == "dash")
            {

                DashF();
            }


        }


    }

    public void Power2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (powers[currentPower].ToString() == "doublejump")
            {
                Stomp();

            }
            else if (powers[currentPower] == "dash")
            {

                DashU();
            }


        }
    }

    public bool ArrayCheckMinus( int value)
    {

        if (value == 0)
        {
           
            
            return true;

        }
        

        else return false;
    }

    public bool ArrayCheckPlus( int value)
    {

          if (value == powers.Length -1)
          {

            return true;

          }

        else return false;
    }



}
