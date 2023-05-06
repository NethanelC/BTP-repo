using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField] private RectTransform _minimapCanvas;
    [SerializeField] private MinimapRoom _minimapRoomPrefab;
    private Dictionary<int, MinimapRoom> _minimapRooms = new();
    private const float miniMapImageSize = 20, minimapImageSpacing = 5;
    public void SpawnRoom(Vector2 offSet)
    {
        MinimapRoom newRoom = Instantiate(_minimapRoomPrefab, _minimapCanvas);
        newRoom.SetCorrectOffset(offSet * (miniMapImageSize + minimapImageSpacing));
        _minimapRooms.Add(_minimapRooms.Count, newRoom);   
    }
    public void ChangeCurrentRoom(int roomIndex, bool inRoom)
    {
        _minimapRooms[roomIndex].ChangeCurrentRoom(inRoom);
    }
}
