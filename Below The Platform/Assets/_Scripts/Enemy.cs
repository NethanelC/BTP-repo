using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Damageable
{
    [SerializeField] private int _expPoints;
    private EnemiesRoom _room;
    public void Init(EnemiesRoom room)
    {
        _room = room;
    }
    public override void Die()
    {
        PlayerExperience.Instance.AcquireExperience(_expPoints);
        _room.EnemyDied();
        Destroy(gameObject);
    }
}
