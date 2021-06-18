using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleJump : Power
{
    public override string Name => "DoubleJump";

    public override void Process()
    {
        cooldown = GameObject.Find(Name + " Cooldown");
        ui = GameObject.Find(Name + " Button");
        coolTime = 5;

        Debug.Log("SUMONEI " + Name);
        //cooldown.GetComponent<Cooldown>().cooldownTime = coolTime;
        //cooldown.GetComponent<Cooldown>().timerActive = true;
        ui.GetComponent<Slider>().maxValue = coolTime;
    }
}
