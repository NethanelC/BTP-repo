using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] protected Collider2D _collider;
    protected int _damageAmount;
    protected DamageType _damageType;
    public enum DamageType
    {
        Physical,
        Projectile,
        Magic //elements in the future
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.Hit(_damageAmount, _damageType);
        }
    }
}
