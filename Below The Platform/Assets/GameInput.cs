using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event Action<Vector3> OnShootAction;
    public event Action OnPauseAction;
    [SerializeField] private Camera _camera;
    private PlayerInput _playerInput;
    private void Awake()
    {
        _playerInput = new();
        _playerInput.Enable();
        _playerInput.Player.Shoot.performed += Shoot_performed;
        _playerInput.Player.Pause.performed += Pause_performed;
    }

    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke();
    }

    private void Shoot_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (Time.timeScale == 1)
        {
            OnShootAction?.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));
        }   
    }

    public Vector2 GetMovementVectorNormalized()
    {
        return _playerInput.Player.Movement.ReadValue<Vector2>().normalized;
    }
}
