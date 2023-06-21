using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class ExperiencePopup : MonoBehaviour
{
    [SerializeField] private TextMeshPro _popupText;
    [SerializeField] private Gradient _colorGradient;
    public async UniTaskVoid Init(float expAmount)
    {
        _popupText.text = $"+{expAmount:F0}xp";
        _popupText.color = _colorGradient.Evaluate(Mathf.Clamp01(expAmount / 50));
        await _popupText.gameObject.transform.DOScale(1, 1).AsyncWaitForCompletion();
        _popupText.DOFade(0, 5).OnComplete(() => Destroy(gameObject));
    }
    private void OnDestroy()
    {
        DOTween.Kill(this);
    }
}
