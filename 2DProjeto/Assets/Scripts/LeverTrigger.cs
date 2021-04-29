using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void On()
    {
        anim.SetBool("On", true);
        GetComponent<Collider2D>().isTrigger = true;
    }
}
