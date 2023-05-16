using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOS", menuName = "ScriptableObjects/PlayerUpgrades", order = 1)]
public class PlayerUpgrades : ScriptableObject
{
    public readonly Dictionary<Upgrade, int> MaximumOfUpgrade = new()
    {
        { Upgrade.Amount_Of_Babies, 5 },
        { Upgrade.Distance, 5 },
        { Upgrade.Maximum_Deliveries, 5 },
        { Upgrade.Speed, 5 },
        { Upgrade.Lives, 5 }
    };
    public enum Upgrade
    {
        Amount_Of_Babies,
        Distance,
        Maximum_Deliveries,
        Speed,
        Lives
    } 
    public int CurrentUpgradedAmount(Upgrade upgrade)
    {
        return PlayerPrefs.GetInt(upgrade.ToString());
    }
}
