using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public Bullet _bullet;
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private Transform _transform;
    private float _speed = 8;
    void Awake()
    {
        _speed += _speed * PlayerStats.Instance.MoveSpeed;
        _transform = transform;     
        _gameInput.OnShootAction += Shoot;
        Door.OnEnteredDoor += Door_OnEnteredDoor;
        Room.OnRoomChange += Room_OnRoomChange;
    }
    private void OnDestroy()
    {
        _gameInput.OnShootAction -= Shoot;
        Door.OnEnteredDoor -= Door_OnEnteredDoor;
        Room.OnRoomChange -= Room_OnRoomChange;
    }
    private void FixedUpdate()
    {
        _transform.position += _speed * Time.fixedDeltaTime * (Vector3)_gameInput.GetMovementVectorNormalized();
    }
    private void Door_OnEnteredDoor(Vector2 position)
    {
        transform.position += (Vector3)position;
    }
    private void Room_OnRoomChange(Room room)
    {
        _virtualCamera.transform.position = new Vector3(room.transform.position.x, room.transform.position.y, -10);
    }
    private void Shoot(Vector3 mousePosition)
    {
        Instantiate(_bullet, mousePosition, Quaternion.identity);
    }
}
