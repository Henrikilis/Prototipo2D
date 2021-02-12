using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DimensionSwap : MonoBehaviour
{
    public bool phased;
    public SpriteRenderer sr;
    public Color phasedColor;
    public Color unphasedColor;
    public bool isCooling;
    public powerBook player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PlayerPrefab").GetComponent<powerBook>();
        sr = GetComponent<SpriteRenderer>();
        if (!phased)
        {
            Off();
        }
    }

    public void Power1(InputAction.CallbackContext context)
    {
        if (context.performed && !isCooling && player.swapSelected)
        {
            StartCoroutine(Cooldown());
            phased = !phased;
            if (phased)
                On();
            else Off();
        }
    }

    public void On()
    {
        sr.color = phasedColor;
        GetComponent<Collider2D>().isTrigger = false;
    }
    public void Off()
    {
        sr.color = unphasedColor;
        GetComponent<Collider2D>().isTrigger = true;
    }
    IEnumerator Cooldown()
    {
        isCooling = true;
        yield return new WaitForSeconds(0.5f);
        isCooling = false;
    }
}
