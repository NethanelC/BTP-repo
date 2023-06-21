using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] private SpawnRoom _spawnRoomPrefab;
    [SerializeField] private PuzzleRoom _puzzleRoomPrefab;
    [SerializeField] private EnemiesRoom _enemiesRoomPrefab;
    [SerializeField] private TreasureRoom _treasureRoomPrefab;
    [SerializeField] private BossRoom _bossRoomPrefab;
    [SerializeField] private Minimap _miniMap;
    [SerializeField] private LayerMask _roomLayer;
    private Vector2 _roomSpacing = new(Room.HorizontalSize, Room.VerticalSize);
    private Vector2 _lastSideTurned = new();
    public void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
    private void Start()
    {
        SpawnRooms();
        print(gameObject.name);
    }
    private void SpawnRooms()
    {
        Dictionary<Vector2, Room> roomsSpawned = new();
        Vector2 currentRoomPosition = new();
        float up = 0;
        float down = 0;
        float left = 0;
        float right = 0;
        SpawnRoom spawnRoom = Instantiate(_spawnRoomPrefab, currentRoomPosition, Quaternion.identity);
        spawnRoom.Init(roomsSpawned.Count, _miniMap);
        roomsSpawned.Add(currentRoomPosition, spawnRoom);
        currentRoomPosition += _roomSpacing * GetRandomUnOccupiedSide(currentRoomPosition, roomsSpawned);
        while (roomsSpawned.Count < 9)
        {
            Room roomSpawned = Instantiate(GetRandomRoomByLuck(), currentRoomPosition, Quaternion.identity);
            roomSpawned.Init(roomsSpawned.Count, _miniMap);
            roomsSpawned.Add(currentRoomPosition, roomSpawned);
            currentRoomPosition += _roomSpacing * GetRandomUnOccupiedSide(currentRoomPosition, roomsSpawned);
            up = Mathf.Max(up, currentRoomPosition.y);
            down = Mathf.Min(down, currentRoomPosition.y);
            right = Mathf.Max(right, currentRoomPosition.x);
            left = Mathf.Min(left, currentRoomPosition.x);
        }
        Vector2 roomNormalizePosition = new Vector2(right + left, up + down) / _roomSpacing * 0.5f;
        BossRoom bossRoom = Instantiate(_bossRoomPrefab, currentRoomPosition, Quaternion.identity);
        bossRoom.Init(roomsSpawned.Count, _miniMap);
        roomsSpawned.Add(currentRoomPosition, bossRoom);
        foreach (var room in roomsSpawned)
        {
            room.Value.SetRoomLayout(CheckNeighbors(room.Key, roomsSpawned));
            _miniMap.SpawnRoom((room.Key / _roomSpacing) - roomNormalizePosition);
        }
    }
    private Vector2 GetRandomUnOccupiedSide(Vector2 currentLocation, Dictionary<Vector2, Room> roomsCollection)
    {
        List<Vector2> directions = new(1);
        bool4 sidesOccupied = CheckNeighbors(currentLocation, roomsCollection);
        if (!sidesOccupied.x)
        {
            directions.Add(Vector2.up);
        }
        if (!sidesOccupied.y)
        {
            directions.Add(Vector2.down);
        }
        if (!sidesOccupied.z)
        {
            directions.Add(Vector2.right);
        }
        if (!sidesOccupied.w)
        {
            directions.Add(Vector2.left);
        }
        directions.Remove(-_lastSideTurned);
        _lastSideTurned = directions[Range(0, directions.Count)];
        return _lastSideTurned;
    }
    private bool4 CheckNeighbors(Vector2 position, Dictionary<Vector2, Room> roomsCollection)
    {
        bool4 existsOnEachSide = new()
        {
            x = roomsCollection.ContainsKey(position + (Vector2.up * Room.VerticalSize)),
            y = roomsCollection.ContainsKey(position + (Vector2.down * Room.VerticalSize)),
            z = roomsCollection.ContainsKey(position + (Vector2.right * Room.HorizontalSize)),
            w = roomsCollection.ContainsKey(position + (Vector2.left * Room.HorizontalSize))
        };
        return existsOnEachSide;
    }
    private Room GetRandomRoomByLuck()
    {
        int randomNumber = Range(0, 101);
        print(PlayerStats.Instance.Luck);
        if (randomNumber < 5 * PlayerStats.Instance.Luck)
        {
            return _treasureRoomPrefab;
        }
        else if (randomNumber < 10 * PlayerStats.Instance.Luck)
        {
            return _puzzleRoomPrefab;
        }
        else
        {
            return _enemiesRoomPrefab;
        }
    }
}
