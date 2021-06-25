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
    private bool firstTime;
    public GameObject cooldown;

    public override void Process()
    {
        cooldown = GameObject.Find(Name + " Cooldown");
        if (cooldown == null)
            Start();
        Debug.Log("Going Intangible");
        player.layer = 11;
        player.GetComponent<SpriteRenderer>().color = unphasedColor;

        cooldown.GetComponent<Cooldown>().timerActive = true;
    }

    public override void Cooldown()
    {
        player.layer = 9;
        player.GetComponent<SpriteRenderer>().color = phasedColor;
    }

    void Start()
    {
        CooldownPool.Instance.SpawnCooldown(Name, 2);
        cooldown = GameObject.Find(Name + " Cooldown");
    }
}
