using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stomp : Power
{
    public override string Name => "Stomp";

    public override void Process()
    {
        Debug.Log("SUMONEI " + Name);
        player.GetComponent<PowerMovement>().Stomp();
    }
}
