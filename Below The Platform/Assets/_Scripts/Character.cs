using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour, IUpdateSelectedHandler
{
    public static event Action<Character> OnCharacterSelected;
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite[] _statusSprites = new Sprite[2];
    [SerializeField] private CharacterName _nameSelected;
    [SerializeField] private int _price;
    [SerializeField] private string _description;
    public Sprite Icon => _icon.sprite;
    public int Price => _price;
    public string Description => _description;
    public string Name => _nameSelected.ToString().Replace("_", " ");
    public bool IsUnlocked => PlayerPrefs.GetInt(Name, 0) == 1;
    public enum CharacterName
    {
        Svenos,
        Nelsos,
        Baros
    }
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
        PlayerPrefs.SetInt(Name, 1);
        PlayerPrefs.Save();
        UpdateVisuals();
    }
    public void OnUpdateSelected(BaseEventData eventData)
    {
        OnCharacterSelected?.Invoke(this);
    }
}