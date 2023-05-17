using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;   
using TMPro;

public class Shop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _currentGojosText, _selectedAbilityName, _selectedAbilityDescription, _selectedAbilityPrice;
    [SerializeField] private Image _selectedAbilityImage;
    [SerializeField] private Button _purchaseButton;
    private Upgrade _currentSelectedUpgrade;
    private int _gojos => PlayerPrefs.GetInt("Gojos", 0);
    private void Awake()
    {
        _currentGojosText.text = _gojos.ToString();
        _purchaseButton.onClick.AddListener(() =>
        {
            PurchaseUpgrade();
        });
        Upgrade.OnUpgradeSelected += Upgrade_OnUpgradeButtonSelected;  
    }
    private void OnDestroy()
    {
        Upgrade.OnUpgradeSelected -= Upgrade_OnUpgradeButtonSelected;
    }
    public void Upgrade_OnUpgradeButtonSelected(Upgrade selectedUpgrade)
    {
        _currentSelectedUpgrade = selectedUpgrade;
        _purchaseButton.interactable = _gojos > selectedUpgrade.Price && !selectedUpgrade.ReachedMaximum;
        _selectedAbilityPrice.gameObject.SetActive(!selectedUpgrade.ReachedMaximum);
        _selectedAbilityPrice.text = selectedUpgrade.Price.ToString();
        _selectedAbilityName.text = selectedUpgrade.Name;
        _selectedAbilityDescription.text = selectedUpgrade.Description;
        _selectedAbilityImage.sprite = selectedUpgrade.Icon;
    }
    private void PurchaseUpgrade()
    {
        _currentSelectedUpgrade.UpgradeOnce();
        if (_currentSelectedUpgrade.ReachedMaximum)
        {
            _selectedAbilityPrice.gameObject.SetActive(false);
            _purchaseButton.interactable = false;
        }
        PlayerPrefs.SetInt("Gojos", _gojos - _currentSelectedUpgrade.Price);
        _currentGojosText.text = _gojos.ToString();
    }
}
