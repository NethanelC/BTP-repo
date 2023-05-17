using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class FirstSelectDisabled : MonoBehaviour
{
    [SerializeField] private Selectable _selectObject;
    private void OnDisable()
    {
        EventSystem.current.SetSelectedGameObject(_selectObject.gameObject);
    }
}
