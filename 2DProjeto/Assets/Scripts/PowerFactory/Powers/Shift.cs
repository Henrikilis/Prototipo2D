using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shift : Power
{
    public override string Name => "Shift";

    [Header("Intangibility")]
    public Color phasedColor = new Color(1f,1f,1f,1f);
    public Color unphasedColor = new Color(0f, 0f, 1f, 0.5f);

    public override void Process()
    {
        Debug.Log("Going Intangible");
        player.layer = 11;
        player.GetComponent<SpriteRenderer>().color = unphasedColor;
        Intangibility();
    }

    IEnumerator Intangibility()
    {
        yield return new WaitForSeconds(2);
        Debug.Log("Done");
        player.layer = 9;
        player.GetComponent<SpriteRenderer>().color = phasedColor;
    }
}
