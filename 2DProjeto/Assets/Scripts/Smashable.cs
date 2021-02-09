using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smashable : MonoBehaviour
{
    public Animator anim;
    public float stompJumpForce;

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
            gameObject.layer = 13;
        }

        if(other.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, 0);
            other.GetComponent<Rigidbody2D>().AddForce(Vector2.up * stompJumpForce, ForceMode2D.Impulse);
            anim.SetTrigger("Dead");
            gameObject.layer = 13;

        }

    }


    

    public void Death()
    {
        Destroy(gameObject);
    }
}
