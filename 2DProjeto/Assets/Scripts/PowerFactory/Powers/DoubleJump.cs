using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleJump : Power
{
    public override string Name => "DoubleJump";

    public override void Process()
    {
        Debug.Log("SUMONEI " + Name);
        player.GetComponent<PowerMovement>().DoubleJump();
    }
}
