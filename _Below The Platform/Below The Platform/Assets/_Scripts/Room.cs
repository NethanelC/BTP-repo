using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] Rooms _roomsInfo;
    [SerializeField] SpriteRenderer _spriteRenderer;
    int _roomIndex;
    RoomType _roomType;
    public enum RoomType
    {
        Spawn,
        Boss,
        Treasure,
        Puzzle,
        Enemies,
    }
    public void Init(int roomIndex, int roomLayout, RoomType type)
    {
        _roomIndex = roomIndex;
        _roomType = type;
        _spriteRenderer.sprite = _roomsInfo.RoomVariations[roomLayout]._roomSprite;
        switch (_roomType)
        {
            case RoomType.Spawn:
                _spriteRenderer.color = Color.green;
                break;
            case RoomType.Boss:
                _spriteRenderer.color = Color.red;
                break;
            case RoomType.Treasure:
                _spriteRenderer.color = Color.yellow;
                break;
            case RoomType.Puzzle:
                _spriteRenderer.color = Color.cyan;
                break;
            case RoomType.Enemies:
                _spriteRenderer.color = Color.black;
                break;
        }   
    }
}
