using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[CreateAssetMenu(fileName = "Rooms", menuName = "ScriptableObjects/Rooms", order = 1)]
public class Rooms : ScriptableObject
{
    public RoomDetails[] RoomVariations = new RoomDetails[15];

    [Serializable]
    public struct RoomDetails
    {
        public Sprite _roomSprite;
        public RoomLayout _roomLayout;
    }
    public enum RoomLayout
    {
        All,
        Top,
        Bottom,
        Right,
        Left,
        TopRight,
        TopBottom,
        TopLeft,
        BottomRight,
        BottomLeft,
        RightLeft,
        TopRightBottom,
        TopRightLeft,
        TopBottomLeft,
        BottomRightLeft
    }
}
