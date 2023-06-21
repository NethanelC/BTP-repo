using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cysharp.Threading.Tasks;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private Rigidbody2D _rb;
    private Transform _transform;
    private float _baseSpeed = 6;
    private float _speed;
    private void Awake()
    {
        _transform = transform;
    }
    private void Start()
    {
        _baseSpeed *= PlayerStats.Instance.MoveSpeed;
        _speed = _baseSpeed;
    }
    private void OnEnable()
    {
        _gameInput.OnShootAction += Shoot;
        _gameInput.OnDashAction += Dash;
        Door.OnEnteredDoor += Door_OnEnteredDoor;
        Room.OnRoomChange += Room_OnRoomChange;      
    }
    private void OnDisable()
    {
        _gameInput.OnShootAction -= Shoot;
        _gameInput.OnDashAction -= Dash;
        Door.OnEnteredDoor -= Door_OnEnteredDoor;
        Room.OnRoomChange -= Room_OnRoomChange;       
    }
    private void Dash()
    {
        IncreaseSpeed(2, 150);
    }
    public async void IncreaseSpeed(float multiplier, int durationInMiliseconds)
    {
        _speed *= multiplier;
        await UniTask.Delay(durationInMiliseconds);
        _speed = _baseSpeed;
    }
    private void FixedUpdate()
    {
        _transform.position += _speed * Time.fixedDeltaTime * (Vector3)_gameInput.GetMovementVectorNormalized();
    }
    private void Door_OnEnteredDoor(Vector2 position)
    {
        _transform.position += (Vector3)position;
    }
    private void Room_OnRoomChange(Room room)
    {
        _virtualCamera.transform.position = new Vector3(room.transform.position.x, room.transform.position.y, -10);
    }
    private void Shoot(Vector3 mousePosition)
    {
        
    }
}
