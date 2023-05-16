using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private AbilityButton[] _abilityButtons = new AbilityButton[5];
    private readonly Ability[] _abilities = new Ability[5];
    private readonly Dictionary<Ability,int> _abilityUpgrades = new();
    private void CastAbility(int abilityIndex)
    {
        //Checks if I have sufficient abilities and gets fitting ability
        if (_abilities[abilityIndex])
        {
            //Checks if ability is on cooldown
            if (!_abilityButtons[abilityIndex].UseAbility())
            {
                _abilities[abilityIndex].Cast();
            }
        }
    }
    private void Awake()
    {
        AbilitySelectButton.OnAbilityUpgrade += AbilitySelectButton_OnAbilityUpgrade;
        _gameInput.OnAbilityAction += CastAbility;
        print(_gameInput.GetBindingKeyText("FirstAbility", 0));
    }
    private void OnDestroy()
    {
        AbilitySelectButton.OnAbilityUpgrade -= AbilitySelectButton_OnAbilityUpgrade;
        _gameInput.OnAbilityAction -= CastAbility;
    }
    private void AbilitySelectButton_OnAbilityUpgrade(Ability abilityUpgraded)
    {
        //CHECK IF ABILITY IS PURCHASED
        if (_abilityUpgrades.TryAdd(abilityUpgraded, 1))
        {
            _abilities[_abilityUpgrades.Count - 1] = abilityUpgraded;
            _abilityButtons[_abilityUpgrades.Count - 1].Init(abilityUpgraded.Sprite, abilityUpgraded.Cooldown);
            return;
        }
        //UPGRADE ABILITY
        _abilityUpgrades[abilityUpgraded]++;
    }
}