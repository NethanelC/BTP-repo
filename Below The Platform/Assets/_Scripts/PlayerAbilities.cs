using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private AbilityButton[] _abilityButtons = new AbilityButton[4];
    [SerializeField] private Button _destructionButton;
    [SerializeField] private TextMeshProUGUI _destructionAmountText;
    private readonly Ability[] _abilities = new Ability[4];
    private readonly Dictionary<Ability,int> _abilityUpgrades = new();
    private int _destructionAmount;
    private EnemiesRoom _currentEnemiesRoom;
    private void CastAbility(int abilityIndex)
    {
        //Checks if I have sufficient abilities and gets fitting ability
        if (_abilities[abilityIndex])
        {
            //Checks if ability is on cooldown
            if (!_abilityButtons[abilityIndex].UseAbility())
            {
                _abilities[abilityIndex].Cast();
            }
        }
    }
    private void Awake()
    {
        _destructionAmount = PlayerStats.Instance.Destructions;
        _destructionAmountText.text = _destructionAmount.ToString();
        _gameInput.OnAbilityAction += CastAbility;
        _gameInput.OnDestructionAction += _gameInput_OnDestructionAction;
        Room.OnRoomChange += Room_OnRoomChange;
        Room.OnRoomCompleted += Room_OnRoomCompleted;
        print(_gameInput.GetBindingKeyText("FirstAbility", 0));
    }
    private void Room_OnRoomCompleted(Room room)
    {
        _currentEnemiesRoom = null;
    }
    private void Room_OnRoomChange(Room room)
    {
        if (room is EnemiesRoom && !room.IsCompleted)
        {
            _currentEnemiesRoom = room as EnemiesRoom;
            _destructionButton.interactable = true;
        }      
    }
    private void OnDestroy()
    {
        _gameInput.OnAbilityAction -= CastAbility;
        _gameInput.OnDestructionAction -= _gameInput_OnDestructionAction;
        Room.OnRoomChange -= Room_OnRoomChange;
        Room.OnRoomCompleted -= Room_OnRoomCompleted;
    }
    private void _gameInput_OnDestructionAction()
    {
        if (!_currentEnemiesRoom)
        {
            return;
        }
        if (_destructionAmount == 0)
        {
            return;
        }
        _destructionAmountText.text = $"{--_destructionAmount}";
        _destructionButton.interactable = false;
        _currentEnemiesRoom.DestructRoom();
    }
    public void AbilityUpgrade(Ability abilityUpgraded)
    {
        //CHECK IF ABILITY IS PURCHASED
        if (_abilityUpgrades.TryAdd(abilityUpgraded, 1))
        {
            _abilities[_abilityUpgrades.Count - 1] = abilityUpgraded;
            _abilityButtons[_abilityUpgrades.Count - 1].Init(abilityUpgraded.Sprite, abilityUpgraded.Cooldown);
            return;
        }
        //UPGRADE ABILITY
        _abilityUpgrades[abilityUpgraded]++;
    }
}