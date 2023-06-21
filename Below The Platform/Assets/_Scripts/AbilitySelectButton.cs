using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitySelectButton : MonoBehaviour, ISubmitHandler, IPointerClickHandler
{
    [SerializeField] private PlayerAbilities _abilityMenu;
    [SerializeField] private Image _abilityIcon, _background;
    [SerializeField] private TextMeshProUGUI _abilityName, _abilityDescription, _abilityLevelText;
    [SerializeField] private GameObject _passiveActiveText;
    [SerializeField] private AbilityBase.AbilityVisuals _currencyTemplate;
    [SerializeField] private Color _normalColor, _banishColor;
    [SerializeField] private string _abilityBindingName;
    private AbilityBase _abilityToSelect;
    public void ChangeButtonUI(AbilityBase newAbilityToBeSelected)
    {
        _abilityToSelect = newAbilityToBeSelected;
        _abilityIcon.sprite = newAbilityToBeSelected.Visuals.Icon;
        _abilityName.text = newAbilityToBeSelected.Visuals.Name;
        _abilityDescription.text = newAbilityToBeSelected.Visuals.Description;
        _abilityLevelText.text = newAbilityToBeSelected.CurrentLevel == 0? "NEW!" : $"Lvl. {newAbilityToBeSelected.CurrentLevel}";
        _passiveActiveText.SetActive(!newAbilityToBeSelected.IsActiveAbility);
    }
    public void NonAbility()
    {
        _abilityToSelect = null;
        _abilityLevelText.text = null;
        _abilityIcon.sprite = _currencyTemplate.Icon;
        _abilityName.text = _currencyTemplate.Name;
        _abilityDescription.text = _currencyTemplate.Description.Replace("_", Mathf.RoundToInt(20 * PlayerStats.Instance.Greed).ToString());
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
    public void OnSubmit(BaseEventData eventData)
    {
       _abilityMenu.AbilityChosen(_abilityToSelect);    
    }
    public void OnPointerClick(PointerEventData eventData)
    {
       _abilityMenu.AbilityChosen(_abilityToSelect);
    }
}