﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class powerBook : MonoBehaviour
{
    [Header("Powers")]
    public Rigidbody2D rb;
    public string[] powers;
    [SerializeField]
    private int currentPower;

    [Header("UI")]
    public GameObject[] pages;

    [Header("DoubleJump")]    
    public float doubleJumpForce;
    private float doubleJumpCD;
    public float doubleJumpTime;
    public bool doubleJumpCDactive;
    public Component[] doubleComponent;

    [Header("Dash")]
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public bool dashPressed;
    private float dashCD;
    public float dashCDTime;
    public bool dashCDactive;
    public Component[] dashComponent;

    [Header("Stomp")]

    public float stompSpeed;
    [SerializeField]
    private float stompDuration;
    private bool isStomping = false;
    [SerializeField]
    private bool endStomp = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPower = 0;
        Debug.Log(powers[currentPower]);
        dashTime = startDashTime;

        // CONFIGURANDO REFERENCIAS
        pages[0] = GameObject.Find("Page Jump");
        pages[1] = GameObject.Find("Page Dash");
        pages[2] = GameObject.Find("Page Shield");
        pages[3] = GameObject.Find("Page Dimension");
        pages[4] = GameObject.Find("Page Time");
        doubleComponent = pages[0].GetComponentsInChildren<Slider>();
        dashComponent = pages[1].GetComponentsInChildren<Slider>();
        pages[1].SetActive(false);
        pages[2].SetActive(false);
        pages[3].SetActive(false);
        pages[4].SetActive(false);
    }

    void Update()
    {
        // COOLDOWNS
        if (doubleJumpCDactive)
        {
            doubleJumpCD -= Time.deltaTime;
            foreach (Slider slider in doubleComponent)
                slider.value = doubleJumpTime - doubleJumpCD;
            if (doubleJumpCD <= 0)
            {
                doubleJumpCD = doubleJumpTime;
                doubleJumpCDactive = false;
            }
        }
        if (dashCDactive)
        {
            dashCD -= Time.deltaTime;
            foreach (Slider slider in dashComponent)
                slider.value = dashCDTime - dashCD;
            if (dashCD <= 0)
            {
                dashCD = dashCDTime;
                dashCDactive = false;
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
        
        
        if(!isStomping && !endStomp && stompDuration > 0)
        {
            stompDuration -= Time.deltaTime;

            if(stompDuration  <= 0)
            {
                stompDuration = 0;
                gameObject.GetComponent<PlayerHealth>().dontMove = false;
                endStomp = true;
            }
        }


        //Double Jump
        if (rb.velocity.y < 0 && doubleJumpCDactive)
        {
            gameObject.GetComponent<PlayerController>().physicsAllow = true;

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
            gameObject.GetComponent<PlayerController>().physicsAllow = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
            doubleJumpCDactive = true;
        }  
    }

    public void Stomp()
    {
       
        if (!GetComponent<PlayerController>().isGrounded && doubleJumpCDactive == false)
        {
            endStomp = false;
            stompDuration = 0;
            isStomping = true;
            gameObject.GetComponent<PlayerHealth>().dontMove = true;
            rb.velocity = new Vector2(0, 0);
            rb.AddForce(Vector2.down * stompSpeed, ForceMode2D.Impulse);
            
            doubleJumpCDactive = true;

        }

    }

    public void DashF()
    {
        if(dashCDactive == false)
        {
            Debug.Log("DashF");
            gameObject.GetComponent<PlayerHealth>().dontMove = true;
            dashPressed = true;
            dashCDactive = true;
        }
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

            pages[currentPower].SetActive(false);
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
            pages[currentPower].SetActive(true);
        }
    }

    public void SwapRight(InputAction.CallbackContext context)
    {
        
        if (context.performed)
        {
            pages[currentPower].SetActive(false);
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
            pages[currentPower].SetActive(true);
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
