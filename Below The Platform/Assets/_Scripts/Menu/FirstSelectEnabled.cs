using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;   

public class FirstSelectEnabled : MonoBehaviour
{
    [SerializeField] private Selectable _selectObject;
    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_selectObject.gameObject);
    }
}
