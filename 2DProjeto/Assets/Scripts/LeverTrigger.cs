using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    public Animator anim;
    public bool inverted;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        if(inverted)
        {
            anim.SetBool("On", true);
            GetComponent<Collider2D>().isTrigger = true;
        }
    }

    public void On()
    {
        if(!inverted)
        {
            anim.SetBool("On", true);
            GetComponent<Collider2D>().isTrigger = true;
        }
        else
        {
            anim.SetBool("On", false);
            GetComponent<Collider2D>().isTrigger = false;
        }
    }
    public void Off()
    {
        if(!inverted)
        {
            anim.SetBool("On", false);
            GetComponent<Collider2D>().isTrigger = false;
        }
        else
        {
            anim.SetBool("On", true);
            GetComponent<Collider2D>().isTrigger = true;
        }
    }
}
