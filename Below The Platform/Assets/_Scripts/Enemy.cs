using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damageable
{
    [SerializeField] private int _expPoints;
    protected override void Die()
    {
        PlayerExperience.Instance.AcquireExperience(_expPoints);
        Destroy(gameObject);
    }
}
