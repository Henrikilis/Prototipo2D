using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public string coolName;
    public bool timerActive;
    public float cooldownTime;
    public float currentTime;
    public GameObject ui;

    void Start()
    {
        currentTime = cooldownTime;
    }

    void Update()
    {
        if (timerActive)
        {
            currentTime -= Time.deltaTime;
            //ui.GetComponent<Slider>().value = cooldownTime;
            //Debug.Log("Resfriando" + cooldownTime);
            if (currentTime <= 0)
            {
                timerActive = false;
                PowerFactory.GetPower(coolName).Cooldown();
                currentTime = cooldownTime;
                //ui.GetComponent<Slider>().value = ui.GetComponent<Slider>().maxValue;
            }
        }
    }
}
