using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _quitButton;
    private void Awake()
    {
        Time.timeScale = 1;
        _quitButton.onClick.AddListener(() => { Application.Quit(); });
        PlayerPrefs.SetInt("Svenos", 1);
    }
}
