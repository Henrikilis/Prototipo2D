using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Rigidbody2D rb;
    public int maxHealth;
    public int currentHealth;
    public bool dontMove;
    public bool damageCooldown;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth == 0)
            anim.SetTrigger("Dead");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            anim.SetTrigger("Hurt");
            Debug.Log("Apanhei");
            currentHealth--;
            //Knockback
            StartCoroutine(Knockback(other));
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy") && damageCooldown == false)
        {
            anim.SetTrigger("Hurt");
            Debug.Log("Apanhei");
            currentHealth--;
            damageCooldown = true;
            //Knockback
            StartCoroutine(KnockbackT(other));
        }
    }

    IEnumerator Knockback(Collision2D other)
    {
        gameObject.layer = 11;
        dontMove = true;
        gameObject.GetComponent<PlayerController>().moveSpeed = 0;
        Vector2 direction = (transform.position - other.transform.position).normalized;
        rb.AddForce(direction * 100);
        rb.velocity = new Vector2(rb.velocity.x, 3);
        yield return new WaitForSeconds(0.5f);
        dontMove = false;
        gameObject.GetComponent<PlayerController>().moveSpeed = 3;
        gameObject.layer = 9;
    }
    IEnumerator KnockbackT(Collider2D other)
    {
        gameObject.layer = 11;
        dontMove = true;
        gameObject.GetComponent<PlayerController>().moveSpeed = 0;
        Vector2 direction = (transform.position - other.transform.position).normalized;
        rb.AddForce(direction * 100);
        rb.velocity = new Vector2(rb.velocity.x, 3);
        yield return new WaitForSeconds(0.5f);
        dontMove = false;
        gameObject.GetComponent<PlayerController>().moveSpeed = 3;
        damageCooldown = false;
        gameObject.layer = 9;
    }
}
