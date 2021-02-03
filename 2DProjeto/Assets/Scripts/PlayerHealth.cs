using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public Rigidbody2D rb;
    public int maxHealth;
    public int currentHealth;
    public bool takingDamage;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth == 0)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Apanhei");
            currentHealth--;
            //Knockback
            StartCoroutine(Knockback(other));
        }
    }

    IEnumerator Knockback(Collision2D other)
    {
        takingDamage = true;
        gameObject.GetComponent<PlayerController>().moveSpeed = 0;
        Vector2 direction = (transform.position - other.transform.position).normalized;
        rb.AddForce(direction * 100);
        rb.velocity = new Vector2(rb.velocity.x, 3);
        yield return new WaitForSeconds(0.5f);
        takingDamage = false;
        gameObject.GetComponent<PlayerController>().moveSpeed = 3;
    }
}
