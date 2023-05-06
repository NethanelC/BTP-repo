using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private Room _roomPrefab;
    [SerializeField] private Minimap _miniMap;
    [SerializeField] private LayerMask _roomLayer;
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
    private const float _roomGapping = 1.2f, _horizontalSpacing = 26, _verticalSpacing = 16;
    private Vector2 _roomSpacing => new(_horizontalSpacing, _verticalSpacing);
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
    private void Awake()
    {
        SpawnRooms();
    }
    private void SpawnRooms()
    {
        Minimap miniMap = Instantiate(_miniMap);
        Dictionary<Vector2, Room> roomsSpawned = new();
        Vector2 currentRoomPosition = new();
        float up = 0;
        float down = 0;
        float left = 0;
        float right = 0;
        Room spawnRoom = Instantiate(_roomPrefab, currentRoomPosition, Quaternion.identity);
        spawnRoom.Init(roomsSpawned.Count, Room.RoomType.Spawn, miniMap);
        roomsSpawned.Add(currentRoomPosition, spawnRoom);
        currentRoomPosition += _roomSpacing * GetUnoccupiedSide(currentRoomPosition, roomsSpawned);
        while (roomsSpawned.Count < 9)
        {
            Room roomSpawned = Instantiate(_roomPrefab, currentRoomPosition, Quaternion.identity);
            roomSpawned.Init(roomsSpawned.Count, (Room.RoomType)Range(2, 5), miniMap);
            roomsSpawned.Add(currentRoomPosition, roomSpawned);
            currentRoomPosition += _roomSpacing * GetUnoccupiedSide(currentRoomPosition, roomsSpawned);
            if (currentRoomPosition.y > up)
            {
                up = currentRoomPosition.y;
            }
            else if (currentRoomPosition.y < down)
            {
                down = currentRoomPosition.y;
            }
            if (currentRoomPosition.x > right)
            {
                right = currentRoomPosition.x;
            }
            else if (currentRoomPosition.x < left)
            {
                left = currentRoomPosition.x;
            }
        }
        Vector2 roomNormalizePosition = new Vector2(right + left, up + down) / _roomSpacing * 0.5f;
        Room bossRoom = Instantiate(_roomPrefab, currentRoomPosition, Quaternion.identity);
        bossRoom.Init(roomsSpawned.Count, Room.RoomType.Boss, miniMap);
        roomsSpawned.Add(currentRoomPosition, bossRoom);
        foreach (var room in roomsSpawned)
        {
            room.Value.SetRoomLayout(_roomLayouts[CheckNeighbors(room.Key, roomsSpawned)]);
            miniMap.SpawnRoom((room.Key / _roomSpacing) - roomNormalizePosition);
        }
    }
    private Vector2 GetUnoccupiedSide(Vector2 currentLocation, Dictionary<Vector2, Room> roomsCollection)
    {
        Dictionary<int, Vector2> directions = new();
        bool4 sidesOccupied = CheckNeighbors(currentLocation, roomsCollection);
        if (!sidesOccupied.x)
        {
            directions.Add(directions.Count, Vector2.up);
        }
        if (!sidesOccupied.y)
        {
            directions.Add(directions.Count, Vector2.down);
        }
        if (!sidesOccupied.z)
        {
            directions.Add(directions.Count, Vector2.right);
        }
        if (!sidesOccupied.w)
        {
            directions.Add(directions.Count, Vector2.left);
        }
        return directions[Range(0, directions.Count)];
    }
    private bool4 CheckNeighbors(Vector2 position, Dictionary<Vector2, Room> roomsCollection)
    {
        bool4 existsOnEachSide = new()
        {
            x = roomsCollection.ContainsKey(position + (Vector2.up * _verticalSpacing)),
            y = roomsCollection.ContainsKey(position + (Vector2.down * _verticalSpacing)),
            z = roomsCollection.ContainsKey(position + (Vector2.right * _horizontalSpacing)),
            w = roomsCollection.ContainsKey(position + (Vector2.left * _horizontalSpacing))
        };
        return existsOnEachSide;
    }
}
