using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour, IUpdateSelectedHandler
{
    public static event Action<Character> OnCharacterSelected;
    [SerializeField] private CharacterSelection _characterSelection;
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite[] _statusSprites = new Sprite[2];
    [SerializeField] private Character _characterSelected;
    [SerializeField] private int _price;
    [SerializeField] private string _description;
    public bool IsUnlocked => PlayerPrefs.GetInt(_characterSelected.name, 0) == 1;
    public Sprite Icon => _icon.sprite;
    public int Price => _price;
    public string Description => _description;
    private void Awake()
    {
        UpdateVisuals();
    }
    private void UpdateVisuals()
    {
        _icon.sprite = _statusSprites[Convert.ToByte(IsUnlocked)];
    }
    public void UnlockCharacter()
    {
        PlayerPrefs.SetInt(_characterSelected.name, 1);
        PlayerPrefs.Save();
        UpdateVisuals();
    }
    public void OnUpdateSelected(BaseEventData eventData)
    {
        _characterSelection.SelectNewButton(this);
        OnCharacterSelected?.Invoke(_characterSelected);
    }
}