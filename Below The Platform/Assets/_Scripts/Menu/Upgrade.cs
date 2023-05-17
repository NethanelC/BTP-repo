using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour, IUpdateSelectedHandler
{
    public static event Action<Upgrade> OnUpgradeSelected;
    [SerializeField] private Image _icon;
    [Header("Defines upgrade's maximum")]
    [SerializeField] private Image[] _statusImages = new Image[1];
    [SerializeField] private UpgradeKind _upgradeType;
    [SerializeField] private int _initialPrice;
    [SerializeField] private string _description;
    public Sprite Icon => _icon.sprite;
    public string Description => _description;
    public string Name => _upgradeType.ToString().Replace("_", " ");
    public int MaximumUpgrades => _statusImages.Length;
    public int Price => Mathf.RoundToInt(_initialPrice + (0.1f * UpgradeCounter));
    public int UpgradeCounter => PlayerPrefs.GetInt(Name, 0);
    public bool ReachedMaximum => MaximumUpgrades <= UpgradeCounter;
    public enum UpgradeKind
    {
        Power,
        Armor,
        Health,
        Cooldown,
        Speed,
        Move_Speed,
        Luck,
        Greed,
        Growth,
        Revival,
        Destruction,
        Soulstone
    }
    private void Awake()
    {
        UpdateVisuals();
    }
    private void UpdateVisuals()
    {
        for (int i = 0; i < UpgradeCounter; i++)
        {
            _statusImages[i].color = Color.white;
        }
    }
    public void UpgradeOnce()
    {
        PlayerPrefs.SetInt(Name, UpgradeCounter + 1);
        PlayerPrefs.Save();
        _statusImages[UpgradeCounter].color = Color.white;
    }
    public void OnUpdateSelected(BaseEventData eventData)
    {
        OnUpgradeSelected.Invoke(this);
    }
}