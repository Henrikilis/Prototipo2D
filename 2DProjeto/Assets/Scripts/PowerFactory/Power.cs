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
    //public abstract float coolTime;
    public GameObject cooldown;
    public GameObject ui;
}
