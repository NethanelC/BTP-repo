using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
[CreateAssetMenu(fileName = "Rooms", menuName = "ScriptableObjects/Rooms", order = 1)]
public class Rooms : ScriptableObject
{
    public RoomLayoutSprite[] RoomLayouts = new RoomLayoutSprite[15];
    public RoomEventType[] RoomTypes = new RoomEventType[5];
    [Serializable]
    public struct RoomLayoutSprite
    {
        public Sprite RoomSprite;
        public RoomLayout RoomLayout;
    }
    [Serializable]
    public struct RoomEventType
    {
        public Sprite BaseSprite, CompletedSprite;
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
