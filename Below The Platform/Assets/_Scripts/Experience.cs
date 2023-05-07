using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Experience : MonoBehaviour
{
    public static event Action<int> OnLevelUp;
    [SerializeField] private Slider _progressSlider;
    [SerializeField] private TextMeshProUGUI _percentInText, _levelInText;
    private int _currentExperience, _currentLevel;
    private int _experienceRequiredToLevelUp => _currentLevel * 5;
    private void Awake()
    {
        IExpGiver.OnExperienceAcquired += IExpGiver_OnExperienceAcquired;
    }
    private void OnDestroy()
    {
        IExpGiver.OnExperienceAcquired -= IExpGiver_OnExperienceAcquired;
    }
    private void IExpGiver_OnExperienceAcquired(int expPoints)
    {
        _currentExperience += expPoints;
        if (_currentExperience > _experienceRequiredToLevelUp)
        {
            _currentExperience -= _experienceRequiredToLevelUp;
            _levelInText.text = $"LVL {++_currentLevel}";
            OnLevelUp?.Invoke(_currentLevel);
            /*AudioSource.PlayClipAtPoint()*/ //sound play
        }
        float percent = _currentExperience / _experienceRequiredToLevelUp;
        _progressSlider.value = percent;
        _percentInText.text = Mathf.RoundToInt(percent).ToString();
    }
}
