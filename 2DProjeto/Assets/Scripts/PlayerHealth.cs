using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Rigidbody2D rb;
    public int maxHealth;
    public int currentHealth;
    public bool dontMove;
    public bool damageCooldown;

    public Animator anim;
    public GameObject hp;

    public GameObject checkPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        hp = GameObject.Find("HP");
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
            UIdamage();
        }
        if (other.gameObject.CompareTag("Spike"))
        {
            anim.SetTrigger("Hurt");
            Debug.Log("ME ESPETEI");
            currentHealth--;
            //Knockback
            //checkPoint = other.gameObject.GetComponent<SpikeSaw>().checkPoint;
            StartCoroutine(SpikeTeleport(other));
            UIdamage();
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
            UIdamage();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Spike"))
        {
            checkPoint = other.gameObject;
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
        gameObject.GetComponent<PlayerController>().moveSpeed = gameObject.GetComponent<PlayerController>().normalSpeed;
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
        gameObject.GetComponent<PlayerController>().moveSpeed = gameObject.GetComponent<PlayerController>().normalSpeed;
        damageCooldown = false;
        gameObject.layer = 9;
    }
    IEnumerator SpikeTeleport(Collision2D other)
    {
        gameObject.layer = 11;
        dontMove = true;
        gameObject.GetComponent<PlayerController>().moveSpeed = 0;
        Vector2 direction = (transform.position - other.transform.position).normalized;
        rb.AddForce(direction * 100);
        rb.velocity = new Vector2(rb.velocity.x, 3);
        yield return new WaitForSeconds(0.5f);
        transform.position = checkPoint.transform.position;
        gameObject.GetComponent<PlayerController>().moveSpeed = 0;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(1);
        dontMove = false;
        gameObject.GetComponent<PlayerController>().moveSpeed = gameObject.GetComponent<PlayerController>().normalSpeed;
        gameObject.layer = 9;
    }

    public void UIdamage()
    {
        hp.GetComponent<Slider>().value = currentHealth;
    }
}
