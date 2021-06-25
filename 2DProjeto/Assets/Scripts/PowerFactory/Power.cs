using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;

public abstract class Power
{
    public abstract string Name { get; }
    public abstract void Process();
    public GameObject player = GameObject.Find("PlayerPrefab");
    public abstract void Cooldown();
}
