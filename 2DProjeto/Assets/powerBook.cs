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
    public float doubleJumpCD;
   
   

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPower = 0;
        Debug.Log(powers[currentPower]);
    }

  

    public void DoubleJump()
    {
        
            Debug.Log("doubleJump");
            rb.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
        
    }
    public void Stomp()
    {
        Debug.Log("Stomp");

    }

    public void DashF()
    {
        Debug.Log("DashF");

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
