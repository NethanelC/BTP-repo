using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BindingText : MonoBehaviour
{
    [SerializeField] private string _action;
    [SerializeField] private int _index;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private TextMeshProUGUI _bindingText;
    private void Awake()
    {
        ChangeText();
    }
    private void GameInput_OnRebinding()
    {
        ChangeText();
    }
    private void ChangeText()
    {
        _bindingText.text = _gameInput.GetBindingKeyText(_action, _index);
    }
}
