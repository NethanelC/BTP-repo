using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityBase : MonoBehaviour
{
    [SerializeField] private AbilityVisuals _visuals;
    [Space(20)]
    [SerializeField] private float _cooldown;
    [SerializeField] private int _maximumLevel;
    [SerializeField] private bool _isActiveAbility;
    public int CurrentLevel { get; private set; }
    private float _lastTimeUsed;
    private AbilityButton _abilityButton;
    [System.Serializable] public struct AbilityVisuals
    {
        public string Name, Description;
        public Sprite Icon;
    }
    public AbilityVisuals Visuals => _visuals;
    public float Cooldown => _cooldown;
    public int MaximumLevel => _maximumLevel;
    public bool IsActiveAbility => _isActiveAbility;
    protected virtual bool _canUse => _lastTimeUsed + _cooldown <= Time.time;
    public virtual void Init(AbilityBase ability, AbilityButton abilityButton)
    {
        CurrentLevel = 1;
        _visuals = ability.Visuals;
        _cooldown = ability.Cooldown;
        _maximumLevel = ability.MaximumLevel;
        _isActiveAbility = ability.IsActiveAbility;
        _abilityButton = abilityButton; 
        _lastTimeUsed = Time.time - _cooldown;
    }
    public void UseAbility()
    {
        if (!_canUse)
        {
            _abilityButton.UseAbility(false, _cooldown);
            return;
        }
        _abilityButton.UseAbility(true, _cooldown);
        CastAbility();
        _lastTimeUsed = Time.time;
    }
    public int Upgrade()
    {
        return ++CurrentLevel;
    }
    public abstract void CastAbility();
}
