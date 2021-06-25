using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : Power
{
    public override string Name => "Shield";

    public override void Process()
    {
        //cooldown = GameObject.Find(Name + " Cooldown");
        //ui = GameObject.Find(Name + " Button");
        //coolTime = 5;

        Debug.Log("SUMONEI " + Name);
        //cooldown.GetComponent<Cooldown>().cooldownTime = coolTime;
        //cooldown.GetComponent<Cooldown>().timerActive = true;
    }
    public override void Cooldown()
    {

    }
}
