using System.Collections;
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
    private const float _roomSpacing = 1.2f, _horizontalSpacing = 26, _verticalSpacing = 16;
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
        Room spawnRoom = Instantiate(_roomPrefab, FindValidLocation(ref currentRoomPosition, roomsSpawned), Quaternion.identity);
        spawnRoom.Init(roomsSpawned.Count, Room.RoomType.Spawn, miniMap);
        roomsSpawned.Add(currentRoomPosition, spawnRoom);
        while (roomsSpawned.Count < 9)
        {
            Room roomSpawned = Instantiate(_roomPrefab, FindValidLocation(ref currentRoomPosition, roomsSpawned), Quaternion.identity);
            roomSpawned.Init(roomsSpawned.Count, (Room.RoomType)Range(2, 5), miniMap);
            roomsSpawned.Add(currentRoomPosition, roomSpawned);
            if (currentRoomPosition.x > right)
            {
                right = currentRoomPosition.x;
            }
            else if (currentRoomPosition.x < left)
            {
                left = currentRoomPosition.x;
            }
            if (currentRoomPosition.y > up)
            {
                up = currentRoomPosition.y;
            }
            else if (currentRoomPosition.y < down)
            {
                down = currentRoomPosition.y;
            }
        }
        Vector2 roomNormalizePosition = new Vector2((right + left) / _horizontalSpacing, (up + down) / _verticalSpacing) * 0.5f;
        Room bossRoom = Instantiate(_roomPrefab, FindValidLocation(ref currentRoomPosition, roomsSpawned), Quaternion.identity);
        bossRoom.Init(roomsSpawned.Count, Room.RoomType.Boss, miniMap);
        roomsSpawned.Add(currentRoomPosition, bossRoom);
        foreach (var room in roomsSpawned)
        {
            room.Value.SetRoomLayout(CheckNeighbors(room.Key, roomsSpawned));
            miniMap.SpawnRoom(new Vector2(room.Key.x / _horizontalSpacing, room.Key.y / _verticalSpacing) - roomNormalizePosition);
        }
    }
    private Vector2 FindValidLocation(ref Vector2 currentLocation, Dictionary<Vector2, Room> dictionary)
    {
        if (!dictionary.ContainsKey(currentLocation))
        {
            return currentLocation;
        }
        else
        {
            currentLocation += new Vector2(_horizontalSpacing, _verticalSpacing) * RandomDirection(Vector2.zero);
            return FindValidLocation(ref currentLocation, dictionary);
        }
    }
    private Vector2 RandomDirection(Vector2 cameFromSide)
    {
        Dictionary<int, Vector2> directions = new()
        {
            {1, Vector2.up },
            {2, Vector2.down},
            {3, Vector2.right},
            {4, Vector2.left},
        };
        int randomIndex = Range(0, directions.Count);
        if(directions[randomIndex] == cameFromSide)
        {
            directions.Remove(randomIndex);
            return directions[Range(0, directions.Count)];
        }
        return directions[randomIndex];
    }
    private int CheckNeighbors(Vector2 position, Dictionary<Vector2, Room> dictionary)
    {
        bool4 existsOnEachSide = new()
        {
            x = dictionary.ContainsKey(position + (Vector2.up * _verticalSpacing)),
            y = dictionary.ContainsKey(position + (Vector2.down * _verticalSpacing)),
            z = dictionary.ContainsKey(position + (Vector2.right * _horizontalSpacing)),
            w = dictionary.ContainsKey(position + (Vector2.left * _horizontalSpacing))
        };
        return _roomLayouts[existsOnEachSide];
    }
}
