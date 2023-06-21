using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using TMPro;

public class CharacterSelection : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentGojosText, _buyOrStartText, _nameText, _descriptionText, _priceText;
    [SerializeField] private Image _characterIcon;
    [SerializeField] private Button _buyAndStartButton;
    [Space(5)]
    [SerializeField] private TextMeshProUGUI _armor, _health, _power, _cooldown, _movespeed, _speed, _luck, _greed, _growth, _reroll, _skip, _banish, _revival, _soulstone, _destruction;
    private int _gojos => PlayerPrefs.GetInt("Gojos", 0);
    private void Awake()
    {
        _currentGojosText.text = _gojos.ToString();
    }
    private void OnEnable()
    {
        CharacterButton.OnCharacterSelected += Character_OnCharacterSelected;            
    }
    private void OnDisable()
    {
        CharacterButton.OnCharacterSelected -= Character_OnCharacterSelected;             
    }
    public void SelectNewButton(CharacterButton selectedButton)
    {
        _buyAndStartButton.interactable = selectedButton.IsUnlocked || selectedButton.Price < _gojos;
        _buyOrStartText.text = selectedButton.IsUnlocked ? "Start" : "Buy";
        _priceText.gameObject.SetActive(!selectedButton.IsUnlocked);
        _priceText.text = selectedButton.Price.ToString();
        _characterIcon.sprite = selectedButton.Icon;
        _descriptionText.text = selectedButton.Description;
        _buyAndStartButton.onClick.RemoveAllListeners();
        _buyAndStartButton.onClick.AddListener(() =>
        {
            if(selectedButton.IsUnlocked)
            {
                //START GAME
                LevelsManager.LoadLevel();
            }
            else
            {
                //PURCHASE CHARACTER
                PlayerPrefs.SetInt("Gojos", _gojos - selectedButton.Price);
                selectedButton.UnlockCharacter();
                _currentGojosText.text = _gojos.ToString();
            }    
        });
    }
    private void Character_OnCharacterSelected(Character selectedCharacter)
    {
        _nameText.text = selectedCharacter.name;
        _health.text = selectedCharacter.Health.ToString();
        _armor.text = GetStringFromStat(selectedCharacter.Armor);
        _reroll.text = GetStringFromStat(selectedCharacter.Rerolls);
        _skip.text = GetStringFromStat(selectedCharacter.Skips);
        _banish.text = GetStringFromStat(selectedCharacter.Banishes);
        _revival.text = GetStringFromStat(selectedCharacter.Revivals);
        _soulstone.text = GetStringFromStat(selectedCharacter.Soulstones);
        _destruction.text = GetStringFromStat(selectedCharacter.Destructions);

        _cooldown.text = GetStringFromStat(selectedCharacter.Cooldown).Replace("+", "-");
        _power.text = GetStringFromStat(selectedCharacter.Power);
        _movespeed.text = GetStringFromStat(selectedCharacter.MoveSpeed);
        _speed.text = GetStringFromStat(selectedCharacter.Speed);
        _greed.text = GetStringFromStat(selectedCharacter.Greed);
        _growth.text = GetStringFromStat(selectedCharacter.Growth);
        _luck.text = GetStringFromStat(selectedCharacter.Luck);
    }
    private string GetStringFromStat(float selectedStat)
    {
        return selectedStat == 0? "-" : $"{selectedStat * 100}%";
    }
    private string GetStringFromStat(int selectedStat)
    {
        return selectedStat == 0 ? "-" : $"+{selectedStat}";
    }
}