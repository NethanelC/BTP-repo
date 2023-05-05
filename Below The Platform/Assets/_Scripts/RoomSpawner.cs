using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    [SerializeField] Room _roomPrefab;
    [SerializeField] LayerMask _roomLayer;
    Dictionary<bool4, int> _roomLayouts = new();
    const float _roomSpacing = 1.05f;
    RoomSpawner() 
    {
        _roomLayouts.Add(new bool4(true, true, true, true), 0);
        _roomLayouts.Add(new bool4(true, false, false, false), 1);
        _roomLayouts.Add(new bool4(false, true, false, false), 2);
        _roomLayouts.Add(new bool4(false, false, true, false), 3);
        _roomLayouts.Add(new bool4(false, false, false, true), 4);
        _roomLayouts.Add(new bool4(true, false, true, false), 5);
        _roomLayouts.Add(new bool4(true, true, false, false), 6);
        _roomLayouts.Add(new bool4(true, false, false, true), 7);
        _roomLayouts.Add(new bool4(false, true, true, false), 8);
        _roomLayouts.Add(new bool4(false, true, false, true), 9);
        _roomLayouts.Add(new bool4(false, false, true, true), 10);
        _roomLayouts.Add(new bool4(true, true, true, false), 11);
        _roomLayouts.Add(new bool4(true, false, true, true), 12);
        _roomLayouts.Add(new bool4(true, true, false, true), 13);
        _roomLayouts.Add(new bool4(false, true, true, true), 14);
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
    void Awake()
    {
        SpawnRooms();
    }
    void SpawnRooms()
    {
        Stack<Room> roomCollection = new();
        Vector2 currentRoomPosition = new();
        while (roomCollection.Count < 10)
        {
            if (!Physics2D.Raycast(currentRoomPosition, Vector2.zero, _roomSpacing, _roomLayer))
            {
                roomCollection.Push(Instantiate(_roomPrefab, currentRoomPosition, Quaternion.identity));
            }
            currentRoomPosition += _roomSpacing * RandomDirection(Range(0, 4));
        }
        roomCollection.Peek().Init(0, _roomLayouts[CheckNeighbors(roomCollection.Peek().transform.position)], Room.RoomType.Boss);
        for (int i = 1; i < 10 ; i++)
        {
            roomCollection.Pop();
            roomCollection.Peek().Init(i, _roomLayouts[CheckNeighbors(roomCollection.Peek().transform.position)], (Room.RoomType)Range(2, 4));
        }
        roomCollection.Peek().Init(roomCollection.Count, _roomLayouts[CheckNeighbors(roomCollection.Peek().transform.position)], Room.RoomType.Spawn);
    }
    Vector2 RandomDirection(int randomNumber) 
    {
        return randomNumber switch
        {
            0 => Vector2.down,
            1 => Vector2.left,
            2 => Vector2.right,
            3 => Vector2.up,
        };
    }
    bool4 CheckNeighbors(Vector2 position)
    {
        bool4 yes = new()
        {
            x = Physics2D.Raycast(position + Vector2.up, Vector2.zero, _roomSpacing, _roomLayer).collider != null,
            y = Physics2D.Raycast(position + Vector2.down, Vector2.zero, _roomSpacing, _roomLayer).collider != null,
            z = Physics2D.Raycast(position + Vector2.right, Vector2.zero, _roomSpacing, _roomLayer).collider != null,
            w = Physics2D.Raycast(position + Vector2.left, Vector2.zero, _roomSpacing, _roomLayer).collider != null
        };
        return yes;
    }

}
