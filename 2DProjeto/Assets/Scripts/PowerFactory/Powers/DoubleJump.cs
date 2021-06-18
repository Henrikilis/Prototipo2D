using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleJump : Power
{
    public override string Name => "Double Jump";
    float coolTime = 5;

    public override void Process()
    {
        cooldown = GameObject.Find(Name + " Cooldown");
        ui = GameObject.Find(Name + " Button");

        Debug.Log("SUMONEI " + Name);
        //cooldown.GetComponent<Cooldown>().cooldownTime = coolTime;
        //cooldown.GetComponent<Cooldown>().timerActive = true;
        ui.GetComponent<Slider>().maxValue = coolTime;
    }
}
