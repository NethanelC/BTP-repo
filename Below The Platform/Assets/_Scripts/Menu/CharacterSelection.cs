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
    [Space(10)]
    [SerializeField] private TextMeshProUGUI _health, _armor;
    private Character _currentSelectedCharacter;
    private int _gojos => PlayerPrefs.GetInt("Gojos", 0);
    private void Awake()
    {
        _currentGojosText.text = _gojos.ToString();
        Character.OnCharacterSelected += Character_OnCharacterSelected;      
    }
    private void OnDestroy()
    {
        Character.OnCharacterSelected -= Character_OnCharacterSelected;       
    }
    private void Character_OnCharacterSelected(Character selectedCharacter)
    {
        _buyAndStartButton.interactable = selectedCharacter.IsUnlocked || selectedCharacter.Price > _gojos;
        _buyOrStartText.text = selectedCharacter.IsUnlocked ? "Start" : "Buy";
        _priceText.gameObject.SetActive(!selectedCharacter.IsUnlocked);
        _currentSelectedCharacter = selectedCharacter;
        _priceText.text = selectedCharacter.Price.ToString();
        _characterIcon.sprite = selectedCharacter.Icon;
        _nameText.text = selectedCharacter.Name;
        _descriptionText.text = selectedCharacter.Description;
        _buyAndStartButton.onClick.RemoveAllListeners();
        _buyAndStartButton.onClick.AddListener(() =>
        {
            if(selectedCharacter.IsUnlocked)
            {
                StartGame();
            }
            else
            {
                PurchaseCharacter();
            }    
        });
    }
    private void StartGame()
    {
        LevelsManager.LoadLevel();
    }
    private void PurchaseCharacter()
    {
        PlayerPrefs.SetInt("Gojos", _gojos - _currentSelectedCharacter.Price);
        _currentSelectedCharacter.UnlockCharacter();
        _currentGojosText.text = _gojos.ToString();
    }
}
