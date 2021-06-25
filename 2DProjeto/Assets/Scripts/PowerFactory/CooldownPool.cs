using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownPool : MonoBehaviour
{
    public GameObject cooldownPrefab;

    #region Singleton

    public static CooldownPool Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    public List<GameObject> cooldowns;

    public void SpawnCooldown(string name, float maxTime)
    {
        GameObject obj = Instantiate(cooldownPrefab);
        cooldowns.Add(obj);
        obj.name = (name + " Cooldown");
        obj.GetComponent<Cooldown>().coolName = name;
        obj.GetComponent<Cooldown>().cooldownTime = maxTime;
        obj.transform.SetParent(transform);
    }
}
