using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private AbilityTemplate _abilityVisuals;
    [Header("Details")]
    [SerializeField] private int _maximumLevel;
    [SerializeField] private float _cooldown;
    public int MaximumLevel => _maximumLevel; 
    public float Cooldown => _cooldown;
    public AbilityTemplate AbilityVisuals => _abilityVisuals;
    public Sprite Sprite => _abilityVisuals.sprite;
    [Serializable] public struct AbilityTemplate
    {
        public Sprite sprite;
        public string name, description;
    }
    public virtual void Cast()
    {
        // Code to cast the ability
    }
}