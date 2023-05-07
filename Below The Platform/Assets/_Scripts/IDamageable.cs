using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public int _health { get; set; }
    public void Hit(int damageTaken);
    public void Die();
}