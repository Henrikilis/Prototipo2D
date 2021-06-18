﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePower : Power
{
    public override string Name => "Time";

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
