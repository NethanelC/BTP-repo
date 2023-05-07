using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAbility : Ability
{
    [SerializeField] protected Collider2D _collider;
    protected int _damageAmount;
    protected enum AbilityType
    {
        Physical,
        Projectile,
        Magic
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.Hit(_damageAmount);
        }
    }
    protected override void UseAbility()
    {
        throw new System.NotImplementedException();
    }
}
