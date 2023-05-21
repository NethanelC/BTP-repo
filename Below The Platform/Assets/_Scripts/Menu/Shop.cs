using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentGojosText, _selectedAbilityName, _selectedAbilityDescription, _selectedAbilityPrice;
    [SerializeField] private Image _selectedAbilityImage;
    [SerializeField] private Button _purchaseButton;
    private int _gojos => PlayerPrefs.GetInt("Gojos", 0);
    private void Awake()
    {
        _currentGojosText.text = _gojos.ToString();   
    }
    public void SelectUpgradeButton(UpgradeButton selectedUpgrade)
    {
        _purchaseButton.interactable = _gojos > selectedUpgrade.Price && !selectedUpgrade.ReachedMaximum;
        _selectedAbilityPrice.gameObject.SetActive(!selectedUpgrade.ReachedMaximum);
        _selectedAbilityPrice.text = selectedUpgrade.Price.ToString();
        _selectedAbilityName.text = selectedUpgrade.Name;
        _selectedAbilityDescription.text = selectedUpgrade.Description;
        _selectedAbilityImage.sprite = selectedUpgrade.Icon;
        
        _purchaseButton.onClick.RemoveAllListeners();
        _purchaseButton.onClick.AddListener(() =>
        {
            //UPGRADE
            selectedUpgrade.UpgradeOnce();
            if (selectedUpgrade.ReachedMaximum)
            {
                _selectedAbilityPrice.gameObject.SetActive(false);
                _purchaseButton.interactable = false;
            }
            PlayerPrefs.SetInt("Gojos", _gojos - selectedUpgrade.Price);
            _currentGojosText.text = _gojos.ToString();
        });
    }
}
