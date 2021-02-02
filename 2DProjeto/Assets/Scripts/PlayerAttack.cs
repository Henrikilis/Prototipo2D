using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject basicAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack(InputAction.CallbackContext context)
    {
        
        StartCoroutine(AttackCoroutine());
    }

    IEnumerator AttackCoroutine()
    {
        basicAttack.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        basicAttack.SetActive(false);
    }
}
