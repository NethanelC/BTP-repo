using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event Action<Vector3> OnShootAction;
    public event Action OnPauseAction;
    public event Action<int> OnAbilityAction;
    [SerializeField] private Camera _camera;
    private PlayerInput _playerInput;
    private void Awake()
    {
        _playerInput = new();
        _playerInput.Enable();
        _playerInput.Player.Shoot.performed += Shoot_performed;
        _playerInput.Player.Pause.performed += Pause_performed;
        _playerInput.Player.FirstAbility.performed += FirstAbility_performed;
        _playerInput.Player.SecondAbility.performed += SecondAbility_performed;
        _playerInput.Player.ThirdAbility.performed += ThirdAbility_performed;
        _playerInput.Player.FourthAbility.performed += FourthAbility_performed;
    }
    private void FirstAbility_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAbilityAction?.Invoke(0);
    }
    private void SecondAbility_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAbilityAction?.Invoke(1);
    }
    private void ThirdAbility_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAbilityAction?.Invoke(2);
    }
    private void FourthAbility_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnAbilityAction?.Invoke(3);
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
    public string GetBindingKeyText(string playerAction, int index)
    {
        return _playerInput.FindAction("Player/" + playerAction).bindings[index].ToDisplayString();
    }
}
