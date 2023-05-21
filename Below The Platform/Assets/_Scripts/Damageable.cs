using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Damageable : MonoBehaviour
{
    [SerializeField] protected int _currentHealth;
    [SerializeField] protected int _armor;
    [SerializeField] protected int _magicResistance;
    [SerializeField] protected ParticleSystem _hitParticles, _deathParticles;
    [SerializeField] protected AudioClip _hitSound, _critSound, _deathSound;
    [SerializeField] protected DamagePopup _damagePopup;
    public virtual void Hit(int damageTaken, bool criticalHit, DamageAbility.DamageType damageType)
    {
        switch (damageType)
        {
            case DamageAbility.DamageType.Projectile:
                {
                    _currentHealth -= damageTaken * ((100 - _armor) / 100);
                    break;
                }
            case DamageAbility.DamageType.Physical:
                {
                    _currentHealth -= damageTaken * ((100 - _armor) / 100);
                    break;
                }
            case DamageAbility.DamageType.Magic:
                {
                    _currentHealth -= damageTaken * ((100 - _magicResistance) / 100);
                    break;
                }
        }
        var popUp = Instantiate(_damagePopup, transform).Init(damageTaken, criticalHit);
        AudioSource.PlayClipAtPoint(criticalHit ? _critSound : _hitSound, transform.position);
        _hitParticles.Play();
        if (_currentHealth < 0)
        {
            AudioSource.PlayClipAtPoint(_deathSound, transform.position);
            Instantiate(_deathParticles, transform.position, Quaternion.identity).Play();
            Die();
        }
    }
    public abstract void Die();
}
