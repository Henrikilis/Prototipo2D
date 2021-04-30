using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    //public AudioSource audioClip;
    public Animator anim;
    public GameObject connection;
    public int currentWeight;

    // Start is called before the first frame update
    void Start()
    {
        //audioClip = gameObject.GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //audioClip.Play();
        connection.GetComponent<LeverTrigger>().On();
        anim.SetBool("Triggered", true);
        Debug.Log("Pisei");
        currentWeight++;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        currentWeight--;
        if (currentWeight <= 0)
        {
            connection.GetComponent<LeverTrigger>().Off();
            anim.SetBool("Triggered", false);
        }
    }
}
