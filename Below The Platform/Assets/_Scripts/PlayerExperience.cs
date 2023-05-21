using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExperience : MonoBehaviour
{
    public static PlayerExperience Instance { get; private set; }
    public event Action OnLevelUp;
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private TextMeshProUGUI _percentInText, _levelInText;
    [SerializeField] private ExperiencePopup _popup;
    private int _currentLevel;
    private float _currentExperience;
    public int RequiredExperienceToLevelUp => _currentLevel * 3;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        _levelInText.text = $"Lvl. {++_currentLevel}";
    }
    public void AcquireExperience(int expPoints)
    {
        _currentExperience += expPoints + (expPoints * PlayerStats.Instance.Growth);
        var popUp = Instantiate(_popup,transform.position, Quaternion.identity).Init(expPoints);
        if (_currentExperience >= RequiredExperienceToLevelUp)
        {
            LevelUp();
        }
        float percent = _currentExperience / RequiredExperienceToLevelUp;
        _progressSlider.value = percent;
        _percentInText.text = $"{Mathf.Round(percent * 100)} %";
    }
    private void LevelUp()
    {
        _currentExperience -= RequiredExperienceToLevelUp;
        _levelInText.text = $"Lvl. {++_currentLevel}";
        OnLevelUp?.Invoke();
        /*AudioSource.PlayClipAtPoint()*/ //sound play
    }
}
