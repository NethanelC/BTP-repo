using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerExperience : MonoBehaviour
{
    public static PlayerExperience Instance { get; private set; }
    public event Action<int> OnLevelUp;
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private TextMeshProUGUI _percentInText, _levelInText;
    [SerializeField] private ExperiencePopup _popup;
    private int _currentExperience, _currentLevel;
    public int ExperienceRequiredToLevelUp => _currentLevel * 3;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
        _levelInText.text = $"Lvl. {++_currentLevel}";
    }
    public void AcquireExperience(int expPoints)
    {
        _currentExperience += expPoints;
        var popUp = Instantiate(_popup,transform.position, Quaternion.identity).Init(expPoints);
        if (_currentExperience >= ExperienceRequiredToLevelUp)
        {
            _currentExperience -= ExperienceRequiredToLevelUp;
            _levelInText.text = $"Lvl. {++_currentLevel}";
            OnLevelUp?.Invoke(_currentLevel);
            /*AudioSource.PlayClipAtPoint()*/ //sound play
        }
        float percent = (float)_currentExperience / ExperienceRequiredToLevelUp;
        _progressSlider.value = percent;
        _percentInText.text = $"{Mathf.Round(percent * 100)} %";
    }
}
