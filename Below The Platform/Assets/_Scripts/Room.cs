using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class Room : MonoBehaviour
{
    public static event Action<Room> OnRoomChange;
    public static event Action<Room> OnRoomCompleted;
    [SerializeField] private Rooms _roomsInfo;
    [SerializeField] private Door[] _doors = new Door[4];
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] protected Sprite _icon, _completedIcon;
    [SerializeField] protected Color _color;
    private readonly Dictionary<bool4, int> _roomLayouts = new()
    {
        { new (true, true, true, true), 0 },
        { new (true, false, false, false), 1 },
        { new (false, true, false, false), 2 },
        { new (false, false, true, false), 3 },
        { new (false, false, false, true), 4 },
        { new (true, false, true, false), 5 },
        { new (true, true, false, false), 6 },
        { new (true, false, false, true), 7 },
        { new (false, true, true, false), 8 },
        { new (false, true, false, true), 9 },
        { new (false, false, true, true), 10 },
        { new (true, true, true, false), 11 },
        { new (true, false, true, true), 12 },
        { new (true, true, false, true), 13 },
        { new (false, true, true, true), 14 }
    };
    public const float _horizontalSize = 26, _verticalSize = 16;
    protected static int _completedRooms;
    public bool IsCompleted { get; private set; }
    private Minimap _miniMap;
    private int _roomIndex;
    public void Init(int roomIndex, Minimap miniMap)
    {
        _roomIndex = roomIndex;
        _miniMap = miniMap;
        _spriteRenderer.color = _color;
        SpawnInteractables();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        _miniMap.ChangeCurrentRoom(_roomIndex, true);
        OnRoomChange?.Invoke(this);
        if (IsCompleted)
        {
            return;
        }
        for (int i = 0; i < _doors.Length; i++)
        {
            if (_doors[i])
            {
                _doors[i].CloseDoor();
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _miniMap.ChangeCurrentRoom(_roomIndex, false);
        _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1);
    }
    public void SetRoomLayout(bool4 roomLayout)
    {
        _spriteRenderer.sprite = _roomsInfo.RoomLayouts[_roomLayouts[roomLayout]].RoomSprite;
        if (!roomLayout.x)
        {
            _doors[0].gameObject.SetActive(false);
        }
        if (!roomLayout.y)
        {
            _doors[1].gameObject.SetActive(false);
        }
        if (!roomLayout.z)
        {
            _doors[2].gameObject.SetActive(false);
        }
        if (!roomLayout.w)
        {
            _doors[3].gameObject.SetActive(false);
        }
    }
    protected abstract void SpawnInteractables();
    public void CompleteRoom()
    {
        IsCompleted = true;
        _completedRooms++;
        for (int i = 0; i < _doors.Length; i++)
        {
            if (_doors[i])
            {
                _doors[i].OpenDoor();
            }
        }
        OnRoomCompleted?.Invoke(this);
    }
}
