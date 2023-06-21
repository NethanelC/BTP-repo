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
    [SerializeField] private TextMeshProUGUI _currentCooldown, _bindingText;
    public void Init(Sprite iconSprite)
    {
        _icon.sprite = iconSprite;
        gameObject.SetActive(true);
    }
    public void UseAbility(bool isCastable, float cooldown)
    {
        _icon.DOColor(Color.gray, .1f).SetEase(Ease.Linear).OnComplete(() => _icon.color = Color.white);
        _currentCooldown.gameObject.SetActive(true);
        if (isCastable)
        {
            _cooldownCoverImage.fillAmount = 1;
            _cooldownCoverImage.DOFillAmount(0, cooldown).SetEase(Ease.Linear).OnComplete(() => { _currentCooldown.gameObject.SetActive(false); });
            UpdateCooldown(cooldown);
        }
    }
    private async void UpdateCooldown(float cooldown)
    {
        for (int i = 0; i < cooldown; i++)
        {
            _currentCooldown.text = Mathf.RoundToInt(cooldown - i).ToString();
            await UniTask.Delay(1000);
        }
    }
}
