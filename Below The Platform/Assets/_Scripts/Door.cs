using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public static event Action<Vector2> OnEnteredDoor;
    [SerializeField] private Collider2D _doorCollider;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private Vector2 _positionToMove;
    private void Awake()
    {
        _positionToMove = 7 * (transform.localPosition.x == 0? Mathf.Sign(transform.localPosition.y) * Vector2.up : Mathf.Sign(transform.localPosition.x) * Vector2.right);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnEnteredDoor?.Invoke(_positionToMove);
        print(collision.name);
    }
    public void CloseDoor()
    {
        _doorCollider.isTrigger = false;
        _spriteRenderer.color = Color.red;
    }
    public void OpenDoor()
    {
        _doorCollider.isTrigger = true;
        _spriteRenderer.color = Color.green;
    }
}
