using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageAbility : Ability
{
    [Header("Damage")]
    [SerializeField] protected Collider2D _collider;
    [SerializeField] protected DamageType _damageType;
    [SerializeField][Range(0f, 1f)] protected float _criticalChance;
    [SerializeField] protected int _damageAmount;
    protected bool _isCriticalHit => Random.value < _criticalChance;
    public enum DamageType
    {
        Physical,
        Projectile,
        Magic //elements in the future
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Damageable damageable))
        {
            damageable.Hit(_damageAmount, _isCriticalHit, _damageType);
        }
    }
}
