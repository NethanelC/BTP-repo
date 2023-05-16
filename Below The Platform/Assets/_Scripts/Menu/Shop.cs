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
    private Upgrade _currentSelectedUpgrade;
    private int _gojos => PlayerPrefs.GetInt("Gojos", 0);
    private void Awake()
    {
        Upgrade.OnUpgradeButtonSelected += Upgrade_OnUpgradeButtonSelected;
        _purchaseButton.onClick.AddListener(() =>
        {
            PurchaseUpgrade();
        });
    }
    private void OnDisable()
    {
        Upgrade.OnUpgradeButtonSelected -= Upgrade_OnUpgradeButtonSelected;
    }
    private void Upgrade_OnUpgradeButtonSelected(Upgrade selectedUpgrade)
    {
        _purchaseButton.interactable = _gojos > selectedUpgrade.Price && !selectedUpgrade.ReachedMaximum;
        _currentSelectedUpgrade = selectedUpgrade;
        _selectedAbilityPrice.text = selectedUpgrade.Price.ToString();
        _selectedAbilityImage.sprite = selectedUpgrade.Icon;
        _selectedAbilityName.text = selectedUpgrade.Name;
        _selectedAbilityDescription.text = selectedUpgrade.Description;
    }
    private void PurchaseUpgrade()
    {
        PlayerPrefs.SetInt("Gojos", _gojos - _currentSelectedUpgrade.Price);
        _currentSelectedUpgrade.UpgradeOnce();
        _currentGojosText.text = _gojos.ToString();
    }
}
