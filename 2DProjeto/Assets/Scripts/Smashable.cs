using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smashable : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("PlayerAttack"))
        {
            anim.SetTrigger("Dead");
            gameObject.layer = 0;
        }
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
