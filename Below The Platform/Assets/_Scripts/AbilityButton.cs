using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class AbilityButton : MonoBehaviour
{
    [SerializeField] private Image _icon, _cooldownCoverImage;
    [SerializeField] private TextMeshProUGUI _currentCooldown;
    private float _cooldown, _timestampLastUsed;
    private float _castDeltaTime => Time.time - _timestampLastUsed;
    private bool _isOnCooldown => _castDeltaTime < _cooldown;
    public void Init(Sprite iconSprite, float cooldown)
    {
        _icon.sprite = iconSprite;
        _cooldown = cooldown;
        _timestampLastUsed = Time.time - _cooldown;
        gameObject.SetActive(true);
    }
    public bool UseAbility()
    {
        _icon.DOColor(Color.gray, 0.1f).SetEase(Ease.Linear).OnComplete(() => _icon.color = Color.white);
        _currentCooldown.gameObject.SetActive(true);
        if (!_isOnCooldown)
        {
            _timestampLastUsed = Time.time;
            _cooldownCoverImage.fillAmount = 1;
            _cooldownCoverImage.DOFillAmount(0, _cooldown).SetEase(Ease.Linear).OnComplete(() => { _currentCooldown.gameObject.SetActive(false); });
            UpdateCooldown();
            return false;
        }
        return true;
    }
    private async void UpdateCooldown()
    {
        _currentCooldown.text = Mathf.RoundToInt(_cooldown - (_castDeltaTime)).ToString();
        await UniTask.Delay(1000);
        UpdateCooldown();
    }
}
