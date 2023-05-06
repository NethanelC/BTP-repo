using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

public class MinimapRoom : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage, _coverImage;
    private RoomType _roomType;
    public enum RoomType
    {
        Spawn,
        Boss,
        Treasure,
        Puzzle,
        Enemies,
    }
    public void SetCorrectOffset(Vector2 position)
    {
        _backgroundImage.rectTransform.anchoredPosition = position;
    }   
    public void ChangeRoomMode(RoomType newRoomType)
    {
        _roomType = newRoomType;
    }
    public void ChangeCurrentRoom(bool inRoom)
    {
        _backgroundImage.color = inRoom ? Color.white : Color.gray;
    }
}
