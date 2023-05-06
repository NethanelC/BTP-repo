using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private Button _buttonUnpause;
    [SerializeField] private Canvas _canvas;
    private void Start()
    {
        _gameInput.OnPauseAction += PauseToggle;
        _buttonUnpause.onClick.AddListener(PauseToggle);
    }
    private void PauseToggle()
    {
        Time.timeScale = Time.timeScale == 1 ? 0 : 1;
        _canvas.gameObject.SetActive(!_canvas.gameObject.activeSelf);
    }
}
