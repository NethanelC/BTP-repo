using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitySelectButton : MonoBehaviour
{
    public static event Action<Ability> OnAbilityUpgrade, OnAbilityBanished;
    [SerializeField] private Button _button;
    [SerializeField] private Image _abilityIcon;
    [SerializeField] private TextMeshProUGUI _abilityName, _abilityDescription, _abilityLevelText;
    [SerializeField] private Ability.AbilityTemplate _currencyTemplate;
    private Ability _abilityToSelect;
    private void Awake()
    {
        _button.onClick.AddListener(() =>
        {
            if (AbilitySelectMenu.IsBanishMode)
            {
                OnAbilityBanished?.Invoke(_abilityToSelect);
                return;
            }
            OnAbilityUpgrade?.Invoke(_abilityToSelect);
        });
    }
    public void ChangeButtonUI(Ability newAbilityToBeSelected, int abilityLevel)
    {
        _abilityToSelect = newAbilityToBeSelected;
        _abilityIcon.sprite = newAbilityToBeSelected.AbilityVisuals.sprite;
        _abilityName.text = newAbilityToBeSelected.AbilityVisuals.name;
        _abilityDescription.text = newAbilityToBeSelected.AbilityVisuals.description;
        _abilityLevelText.text = abilityLevel == 0? "NEW!" : $"Lvl. {abilityLevel}";
    }
    public void NonAbility()
    {
        _button.onClick.RemoveAllListeners();
        _abilityIcon.sprite = _currencyTemplate.sprite;
        _abilityName.text = _currencyTemplate.name;
        _abilityDescription.text = _currencyTemplate.description;
        _abilityLevelText.text = null;
    }
}