﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class powerBook : MonoBehaviour
{
    public Animator anim;

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

    [Header("Stomp")]

    public float stompSpeed;
    [SerializeField]
    private float stompDuration;
    private bool isStomping = false;
    [SerializeField]
    private bool endStomp = true;

    [Header("Dash")]
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public bool dashPressed;
    public bool upDashPressed;
    private float dashCD;
    public float dashCDTime;
    public bool dashCDactive;
    public Component[] dashComponent;
    public GameObject dashParticle;
   
    [Header("Shield")]
    public GameObject shield;
    public float shieldTime;

    public float shieldCDtime;
    private float shieldDuration;
    private float shieldCD;
    private bool shieldCDactive;
    public Component[] shieldComponent;

    [Header("circle")]
    public Rigidbody2D srb;
    public bool circleActive = false;
    

    [Header("Intangibility")]
    private float intTime;
    public float startIntTime;
    public bool intPressed;
    private float intCD;
    public float intCDTime;
    public bool intCDactive;
    public Component[] intComponent;
    public SpriteRenderer sr;
    public Color phasedColor;
    public Color unphasedColor;

    [Header("Dimension")]
    public bool swapSelected;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPower = 0;
        Debug.Log(powers[currentPower]);
        dashTime = startDashTime;
        shield.gameObject.SetActive(false);
        intTime = startIntTime;
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        // CONFIGURANDO REFERENCIAS
        pages[0] = GameObject.Find("Page Jump");
        pages[1] = GameObject.Find("Page Dash");
        pages[2] = GameObject.Find("Page Shield");
        pages[3] = GameObject.Find("Page Dimension");
        pages[4] = GameObject.Find("Page Time");
        doubleComponent = pages[0].GetComponentsInChildren<Slider>();
        dashComponent = pages[1].GetComponentsInChildren<Slider>();
        shieldComponent = pages[2].GetComponentsInChildren<Slider>();
        intComponent = pages[3].GetComponentsInChildren<Slider>();
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
        if (intCDactive)
        {
            intCD -= Time.deltaTime;
            foreach (Slider slider in intComponent)
                slider.value = intCDTime - intCD;
            if (intCD <= 0)
            {
                intCD = intCDTime;
                intCDactive = false;
            }
        }

        if (shieldCDactive)
        {
            shieldDuration -= Time.deltaTime;
            shieldCD -= Time.deltaTime;

            foreach (Slider slider in shieldComponent)
                slider.value = shieldCDtime - shieldCD;

            if (shieldDuration <= 0)
            {
                shield.gameObject.SetActive(false);
                circleActive = false;

            }

            if (shieldCD <= 0)
            {
                shieldCDactive = false;
                shieldDuration = shieldTime;
                shieldCD = shieldCDtime;
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
        if (gameObject.GetComponent<PlayerController>().isGrounded)
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

        // INTANGIBLE
        if(intPressed)
        {
            if (intTime <= 0)
            {
                intTime = startIntTime;
                intPressed = false;
                gameObject.layer = 9;
                sr.color = phasedColor;
            }
            else
            {
                intTime -= Time.deltaTime;
            }
        }
    }


    public void DoubleJump()
    {
        if(doubleJumpCDactive == false)
        {
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
        if (dashCDactive == false)
        {
            Debug.Log("DashUp");
            gameObject.GetComponent<PlayerHealth>().dontMove = true;
            upDashPressed = true;
            dashCDactive = true;
        }
    }

    public void Shield()
    {
        if (!shieldCDactive)
        {
            shield.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            circleActive = false;
            shield.gameObject.transform.SetParent(GetComponent<Transform>());
            shield.transform.position = GetComponentInParent<Transform>().position;
            shield.gameObject.SetActive(true);            
            shieldCDactive = true;
        }


    }

    public void Circle()
    {
        if (!shieldCDactive)
        {
            shield.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            shield.gameObject.SetActive(true);
            shield.transform.position = GetComponentInParent<Transform>().position;
            shield.gameObject.transform.SetParent(null);
            circleActive = true;
            shieldCDactive = true;
            
        }

    }

    public void Intangible()
    {
        if (intCDactive == false)
        {
            Debug.Log("Going Intangible");
            intPressed = true;
            gameObject.layer = 11;
            intCDactive = true;
            sr.color = unphasedColor;
        }
    }
    public void DimensionSwap()
    {
        if (currentPower == 3)
            swapSelected = true;
        else swapSelected = false;
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
            DimensionSwap();
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
            DimensionSwap();
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
            else if (powers[currentPower] == "shield")
            {
                Circle();
            }
            

        }


    }

    public void Power2(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (powers[currentPower] == "doublejump")
            {
                Stomp();
            }
            else if (powers[currentPower] == "dash")
            {
                DashU();
            }
            else if (powers[currentPower] == "shield")
            {
                Shield();
            }
            else if (powers[currentPower] == "swap")
            {
                Intangible();
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
