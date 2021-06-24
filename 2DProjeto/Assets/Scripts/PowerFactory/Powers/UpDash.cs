using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpDash : Power
{
    public override string Name => "UpDash";

    public override void Process()
    {
        //cooldown = GameObject.Find(Name + " Cooldown");
        //ui = GameObject.Find(Name + " Button");
        //coolTime = 5;
        player.GetComponent<PowerMovement>().DashU();
        Debug.Log("SUMONEI " + Name);
        //cooldown.GetComponent<Cooldown>().cooldownTime = coolTime;
        //cooldown.GetComponent<Cooldown>().timerActive = true;
        //ui.GetComponent<Slider>().maxValue = coolTime;
    }
}
