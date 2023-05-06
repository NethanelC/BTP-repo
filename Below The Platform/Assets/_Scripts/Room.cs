using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    [SerializeField] private Rooms _roomsInfo;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Tilemap _tileMap;
    [SerializeField] private TileBase _tileToPaint;
    private Minimap _miniMap;
    private int _roomIndex;
    private RoomType _roomType;
    public enum RoomType
    {
        Spawn,
        Boss,
        Treasure,
        Puzzle,
        Enemies,
    }
    public void Init(int roomIndex, RoomType type, Minimap miniMap)
    {
        _roomIndex = roomIndex;
        _miniMap = miniMap;
        _roomType = type;
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
    public void SetRoomLayout(int roomLayout)
    {
        _spriteRenderer.sprite = _roomsInfo.RoomLayouts[roomLayout].RoomSprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _miniMap.ChangeCurrentRoom(_roomIndex, true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _miniMap.ChangeCurrentRoom(_roomIndex, false);
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1);
    }
}
