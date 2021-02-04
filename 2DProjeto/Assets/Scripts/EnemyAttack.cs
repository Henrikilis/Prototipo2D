using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float allertPeriod;
    private float timer;
    private bool hasSpottedTarget;
    private bool targetCaught;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        hasSpottedTarget = false;
        targetCaught = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSpottedTarget)
        {
            timer += Time.deltaTime;
            if (timer >= allertPeriod)
            {
                targetCaught = true;
                timer = 0;
            }
        }
        if (targetCaught == true)
        {
            StartCoroutine(AttackCoroutine());
        }
    }

    IEnumerator AttackCoroutine()
    {
        gameObject.tag = "Enemy";
        yield return new WaitForSeconds(0.5f);
        gameObject.tag = "Untagged";
    }

    public void OnTriggerEnter2D(Collider2D hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            hasSpottedTarget = true;
            timer = 0;
            Debug.Log("Entrou");
        }
    }
    public void OnTriggerExit2D(Collider2D hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            hasSpottedTarget = false;
            targetCaught = false;
        }
    }
}
