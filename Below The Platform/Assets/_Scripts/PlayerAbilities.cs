using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAbilities : MonoBehaviour
{
    [Header("Castbar & Input")]
    [SerializeField] private GameInput _gameInput;
    [SerializeField] private Button _destructionButton;
    [SerializeField] private TextMeshProUGUI _destructionAmountText;
    [SerializeField] private AbilityButton[] _abilityButtons = new AbilityButton[_maximumAbilities];
    private readonly AbilityBase[] _castableAbilitiesCollection = new AbilityBase[_maximumAbilities];
    private readonly Dictionary<Type, AbilityBase> _abilityCollection = new();
    private const int _maximumAbilities = 4;
    private IDestructable _currentRoomIfDestructable;
    private int _destructionAmount;

    [Header("Selection Menu")]
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private Button _rerollButton, _skipButton, _banishButton;
    [SerializeField] private TextMeshProUGUI _rerollText, _skipText, _banishText;
    [SerializeField] private AbilitySelectButton[] _selectableAbilityButtons = new AbilitySelectButton[3];
    private List<AbilityBase> _abilityOptionsList;
    private readonly HashSet<AbilityBase> _randomUniquePickedAbilities = new(3);
    private int _levelUps, _rerollAmount, _skipAmount, _banishAmount;
    private bool _isBanishActive;
    private void Start()
    {
        _abilityOptionsList = new(PlayerStats.Instance.AbilitiesToAdd);
        _destructionAmount = PlayerStats.Instance.Destructions;
        _destructionAmountText.text = _destructionAmount.ToString();
        _rerollAmount = PlayerStats.Instance.Rerolls;
        _skipAmount = PlayerStats.Instance.Skips;
        _banishAmount = PlayerStats.Instance.Banishes;
        UpdateCounterAndText(_rerollAmount, _rerollText, _rerollButton);
        UpdateCounterAndText(_skipAmount, _skipText, _skipButton);
        UpdateCounterAndText(_banishAmount, _banishText, _banishButton);
        _skipButton.onClick.AddListener(() =>
        {
            _skipAmount--;
            PlayerExperience.Instance.Experience = (Mathf.RoundToInt(PlayerExperience.Instance.RequiredExperienceToLevelUp * .2f));
            CloseMenu();
            UpdateCounterAndText(_skipAmount, _skipText, _skipButton);
        });
        _rerollButton.onClick.AddListener(() =>
        {
            _rerollAmount--;
            UpdateCounterAndText(_rerollAmount, _rerollText, _rerollButton);
            RerollAbilitySelection();
        });
        _banishButton.onClick.AddListener(() =>
        {
            _isBanishActive = !_isBanishActive;
            for (int i = 0; i < _selectableAbilityButtons.Length; i++)
            {
                _selectableAbilityButtons[i].ToggleBanish(_isBanishActive);
            }
        });
        print(_gameInput.GetBindingKeyText("FirstAbility", 0));        
    }
    private void OnEnable()
    {
        _gameInput.OnAbilityAction += CastAnAbility;
        _gameInput.OnDestructionAction += _gameInput_OnDestructionAction;
        Room.OnRoomChange += Room_OnRoomChange;
        Room.OnRoomCompleted += Room_OnRoomCompleted;
        PlayerExperience.Instance.OnLevelUp += Instance_OnLevelUp;
    }
    private void OnDisable()
    {
        _gameInput.OnAbilityAction -= CastAnAbility;
        _gameInput.OnDestructionAction -= _gameInput_OnDestructionAction;
        Room.OnRoomChange -= Room_OnRoomChange;
        Room.OnRoomCompleted -= Room_OnRoomCompleted;
        PlayerExperience.Instance.OnLevelUp -= Instance_OnLevelUp;
    }
    public void AbilityChosen(AbilityBase abilityChosen)
    {
        if (!_isBanishActive)
        {
            if (abilityChosen)
            {
                UpgradeAndClose(abilityChosen);
                return;
            }
            EarnGojosAndClose();
            return;
        }
        if (!abilityChosen)
        {
            print("cant banish gojos");
            return;
        }
        BanishAndClose(abilityChosen);
    }
    private void EarnGojosAndClose()
    {
        PlayerPrefs.SetInt("Gojos", PlayerPrefs.GetInt("Gojos", 0) + Mathf.RoundToInt(20 * PlayerStats.Instance.Greed));
        CloseMenu();
    }
    private void UpdateCounterAndText(int selectedOptionAmount, TextMeshProUGUI text, Button selectedButton)
    {
        text.text = selectedOptionAmount.ToString();
        if (selectedOptionAmount == 0)
        {
            selectedButton.interactable = false;
        }
    }
    public void UpgradeAndClose(AbilityBase abilitySelected)
    {
        //CHECK IF ABILITY IS PURCHASED
        if (_abilityCollection.TryGetValue(abilitySelected.GetType(), out AbilityBase abilityToUpgrade))
        {
            //UPGRADE ABILITY
            if (abilityToUpgrade.Upgrade() == abilityToUpgrade.MaximumLevel)
            {
                _abilityOptionsList.Remove(abilitySelected);
            }
            CloseMenu();
            return;
        }
        //ADD NEW ABILITY
        AbilityBase newAbilityComp = (AbilityBase)gameObject.AddComponent(abilitySelected.GetType());
        newAbilityComp.Init(abilitySelected, _abilityButtons[_abilityCollection.Count]);
        _abilityButtons[_abilityCollection.Count].Init(abilitySelected.Visuals.Icon);
        _castableAbilitiesCollection[_abilityCollection.Count] = newAbilityComp;
        _abilityCollection.Add(abilitySelected.GetType(), newAbilityComp);
        if (_abilityCollection.Count == _maximumAbilities)
        {
            _abilityOptionsList.RemoveAll(ability => !_abilityCollection.ContainsValue(ability));
        }
        CloseMenu();
    }
    private void RerollAbilitySelection()
    {
        _randomUniquePickedAbilities.Clear();
        for (int i = 0; i < _selectableAbilityButtons.Length; i++)
        {
            if (_maximumAbilities - i > _abilityOptionsList.Count)
            {
                _selectableAbilityButtons[i].NonAbility();
                continue;
            }
            AbilityBase newAbility = GetRandomAbility();
            _randomUniquePickedAbilities.Add(newAbility);
            _selectableAbilityButtons[i].ChangeButtonUI(_abilityCollection.TryGetValue(newAbility.GetType(), out AbilityBase ownedAbility) ? ownedAbility : newAbility);
        }
    }
    private AbilityBase GetRandomAbility()
    {
        AbilityBase newAbility = _abilityOptionsList[UnityEngine.Random.Range(0, _abilityOptionsList.Count)];
        return _randomUniquePickedAbilities.Contains(newAbility) ? GetRandomAbility() : newAbility;
    }
    private void BanishAndClose(AbilityBase abilityBanished)
    {
        _isBanishActive = false;
        for (int i = 0; i < _selectableAbilityButtons.Length; i++)
        {
            _selectableAbilityButtons[i].ToggleBanish(false);
        }
        _abilityOptionsList.Remove(abilityBanished);
        _banishAmount--;
        CloseMenu();
        UpdateCounterAndText(_banishAmount, _banishText, _banishButton);
    }
    private void Instance_OnLevelUp()
    {
        _levelUps++;
        if (!_menuCanvas.activeSelf)
        {
            RerollAbilitySelection();
            _menuCanvas.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private void CloseMenu()
    {
        //If accumulated more than one level menu won't close
        if (--_levelUps > 0)
        {
            RerollAbilitySelection();
            return;
        }
        _menuCanvas.SetActive(false);
        Time.timeScale = 1;
        _gameInput.EnableInput();
    }
    private void CastAnAbility(int abilityIndex)
    {
        //Checks if I have sufficient abilities and gets fitting ability
        if (!_castableAbilitiesCollection[abilityIndex])
        {
            return;
        }
        _castableAbilitiesCollection[abilityIndex].UseAbility();
    }
    private void Room_OnRoomCompleted(Room room)
    {
        _currentRoomIfDestructable = null;
    }
    private void Room_OnRoomChange(Room room)
    {
        if (room is IDestructable && !room.IsCompleted && _destructionAmount > 0)
        {
            _currentRoomIfDestructable = room as IDestructable;
            _destructionButton.interactable = true;
        }      
    }
    private void _gameInput_OnDestructionAction()
    {
        if (!_destructionButton.interactable)
        {
            return;
        }
        _destructionAmount--;
        _destructionAmountText.text = _destructionAmount.ToString();
        _destructionButton.interactable = false;
        _currentRoomIfDestructable.Destruct();
    }
}