using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private TextMeshPro _popupText;
    public async UniTaskVoid Init(int damageAmount, bool criticalHit)
    {
        _popupText.text = damageAmount.ToString();
        if (criticalHit)
        {
            _popupText.color = Color.red;
            await _popupText.gameObject.transform.DOScale(1.1f, 0.8f).AsyncWaitForCompletion();
        }
        else
        {
            _popupText.color = Color.white;
            await _popupText.gameObject.transform.DOScale(1, 1).AsyncWaitForCompletion();
        }
        _popupText.DOFade(0, 5).OnComplete(() => Destroy(gameObject));
    }
    private void OnDestroy()
    {
        DOTween.Kill(this);
    }
}
