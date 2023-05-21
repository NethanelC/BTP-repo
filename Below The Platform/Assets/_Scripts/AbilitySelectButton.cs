using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitySelectButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private AbilitySelectMenu _abilityMenu;
    [SerializeField] private Image _abilityIcon, _background;
    [SerializeField] private TextMeshProUGUI _abilityName, _abilityDescription, _abilityLevelText;
    [SerializeField] private GameObject _passiveActiveText;
    [SerializeField] private Ability.AbilityTemplate _currencyTemplate;
    [SerializeField] private Color _normalColor, _banishColor;
    private Ability _abilityToSelect;
    public void ChangeButtonUI(Ability newAbilityToBeSelected, int abilityLevel)
    {
        _abilityToSelect = newAbilityToBeSelected;
        _abilityIcon.sprite = newAbilityToBeSelected.AbilityVisuals.sprite;
        _abilityName.text = newAbilityToBeSelected.AbilityVisuals.name;
        _abilityDescription.text = newAbilityToBeSelected.AbilityVisuals.description;
        _abilityLevelText.text = abilityLevel == 0? "NEW!" : $"Lvl. {abilityLevel}";
        _passiveActiveText.SetActive(!newAbilityToBeSelected.IsActiveAbility);
    }
    public void NonAbility()
    {
        _abilityToSelect = null;
        _abilityLevelText.text = null;
        _abilityIcon.sprite = _currencyTemplate.sprite;
        _abilityName.text = _currencyTemplate.name;
        _abilityDescription.text = _currencyTemplate.description.Replace("_", Mathf.RoundToInt(20 + (20 * PlayerStats.Instance.Greed)).ToString());
        _passiveActiveText.SetActive(false);
    }
    public void ToggleBanish(bool isBanish)
    {
        if(!_abilityToSelect)
        {
            return;
        }
        _background.color = isBanish ? _banishColor : _normalColor;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
       _abilityMenu.AbilityChosen(_abilityToSelect);    
    }
}