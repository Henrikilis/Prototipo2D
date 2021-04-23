using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableDestroy : MonoBehaviour
{
    public float time;
    public bool die;

    void OnEnable()
    {
        StartCoroutine(Disable());
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(time);
        if (die)
            Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
