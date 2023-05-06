using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject _bullet;
    [SerializeField] private GameInput _gameInput;
    private Transform _transform;
    private float _speed = 8;
    void Awake()
    {
        _transform = transform;     
    }
    private void Start()
    {
        _gameInput.OnShootAction += Shoot;
    }
    private void OnDestroy()
    {
        _gameInput.OnShootAction -= Shoot;
    }
    private void Shoot(Vector3 mousePosition)
    {
        Instantiate(_bullet, (Vector2)mousePosition, Quaternion.identity);
    }
    private void FixedUpdate()
    {
        _transform.position += _speed * Time.fixedDeltaTime * (Vector3)_gameInput.GetMovementVectorNormalized();
    }
}
