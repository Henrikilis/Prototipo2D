using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash : Power
{
    public override string Name => "Dash";

    public override void Process()
    {
        Debug.Log("SUMONEI " + Name);
        player.GetComponent<PowerMovement>().DashF();
    }
}
