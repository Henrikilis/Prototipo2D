using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class PowerUp 
{
    [SerializeField]
    public string _name;

    [SerializeField]
    public int _dropChance;

    [SerializeField]
    public float _duration;

    [SerializeField]
    public Sprite _sprite;

    [SerializeField]
    public UnityEvent _startAction;

    public void Start()
    {
        if (_startAction != null)
        {
            _startAction.Invoke();
        }
    }
}
