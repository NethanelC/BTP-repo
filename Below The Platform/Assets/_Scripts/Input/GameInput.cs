using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    public static event Action OnRebinding;
    public event Action<Vector3> OnShootAction;
    public event Action OnPauseAction;
    public event Action OnDestructionAction;
    public event Action OnDashAction;
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
        _playerInput.Player.DestructionAbility.performed += DestructionAbility_performed;
        _playerInput.Player.DashAbility.performed += DashAbility_performed;
    }
    private void OnEnable()
    {
        PlayerExperience.Instance.OnLevelUp += DisableInput;
    }
    private void OnDisable()
    {
        PlayerExperience.Instance.OnLevelUp -= DisableInput;     
    }
    public void DisableInput()
    {
        _playerInput.Disable();
    }
    public void EnableInput()
    {
        _playerInput.Enable();
    }
    private void DashAbility_performed(InputAction.CallbackContext obj)
    {
        OnDashAction?.Invoke();
    }
    private void DestructionAbility_performed(InputAction.CallbackContext obj)
    {
        OnDestructionAction?.Invoke();
    }
    private void FirstAbility_performed(InputAction.CallbackContext obj)
    {
        OnAbilityAction?.Invoke(0);
    }
    private void SecondAbility_performed(InputAction.CallbackContext obj)
    {
        OnAbilityAction?.Invoke(1);
    }
    private void ThirdAbility_performed(InputAction.CallbackContext obj)
    {
        OnAbilityAction?.Invoke(2);
    }
    private void FourthAbility_performed(InputAction.CallbackContext obj)
    {
        OnAbilityAction?.Invoke(3);
    }
    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPauseAction?.Invoke();
    }
    private void Shoot_performed(InputAction.CallbackContext obj)
    {
        OnShootAction?.Invoke(_camera.ScreenToWorldPoint(Input.mousePosition));
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
