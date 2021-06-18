using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
using System;

public static class PowerFactory
{
    public static Dictionary<string, Type> powersByName;
    private static bool IsInitialized => powersByName != null;

    private static void InitializeFactory()
    {
        if (IsInitialized)
            return;

        var powerTypes = Assembly.GetAssembly(typeof(Power)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Power)));

        //Dicionario para encontrar isso por nome depois(pode ser enum/id ao inves de uma string)
        powersByName = new Dictionary<string, Type>();

        // Pegar os nomes e colocar eles no dicionário
        foreach (var type in powerTypes)
        {
            var tempEffect = Activator.CreateInstance(type) as Power;
            powersByName.Add(tempEffect.Name, type);
        }
    }

    public static Power GetPower(string powerType)
    {
        InitializeFactory();

        if (powersByName.ContainsKey(powerType))
        {
            Type type = powersByName[powerType];
            var power = Activator.CreateInstance(type) as Power;
            return power;
        }

        return null;
    }

    internal static IEnumerable<string> GetPowerNames()
    {
        UnityEngine.Debug.Log("Test");
        InitializeFactory();
        return powersByName.Keys;
    }
}