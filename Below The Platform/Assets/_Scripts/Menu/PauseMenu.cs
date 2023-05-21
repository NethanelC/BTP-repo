using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private Button _buttonResume;
    [SerializeField] private CanvasGroup _canvasGroup;
    private void Start()
    {
        _gameInput.OnPauseAction += PauseToggle;
        _buttonResume.onClick.AddListener(PauseToggle);
    }
    private void PauseToggle()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        _canvasGroup.gameObject.SetActive(!_canvasGroup.gameObject.activeSelf);
    }
}
