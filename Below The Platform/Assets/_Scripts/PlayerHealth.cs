using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    private int _currentHealth, _maxHealth;
    private bool _isAlive;
    private void Awake()
    {
        _maxHealth = PlayerStats.Instance.Health;
        _currentHealth = _maxHealth;
    }
    public void Hit(int damageTaken, Damager.DamageType damageType)
    {
        _currentHealth -= Mathf.Max(1, damageTaken - PlayerStats.Instance.Armor);
        _healthSlider.value = (float)_currentHealth / _maxHealth;
        if (_currentHealth < 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("Died");
    }
}
