using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Animator anim;
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            On();
        }
        if (other.gameObject.CompareTag("shield"))
        {
            On();
        }
    }

    public void On()
    {
        anim.SetBool("On", true);
        target.GetComponent<LeverTrigger>().On();
    }
}
