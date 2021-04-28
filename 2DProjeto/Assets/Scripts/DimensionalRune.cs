using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class DimensionalRune : MonoBehaviour
{
    public DimensionSwap[] swapables;
    // Start is called before the first frame update
    void Start()
    {
        swapables = GameObject.FindObjectsOfType(typeof(DimensionSwap)) as DimensionSwap[];
        StartCoroutine(Swap());
    }

    IEnumerator Swap()
    {
        yield return new WaitForSeconds(2);
        //Debug.Log("Boss Trocou");
        foreach (DimensionSwap target in swapables)
        {
            target.phased = !target.phased;
            if (target.phased)
                target.On();
            else target.Off();
        }
        StartCoroutine(Swap());
    }
}
