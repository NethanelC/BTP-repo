using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilitySelectMenu : MonoBehaviour
{
    public static event Action OnMenuClosed;
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private PlayerAbilities _playerAbilities;
    [SerializeField] private AbilitySelectButton[] _selectableAbilityButtons = new AbilitySelectButton[3];
    [SerializeField] private Button _rerollButton, _skipButton, _banishButton;
    [SerializeField] private TextMeshProUGUI _rerollText, _skipText, _banishText;
    [SerializeField] private List <Ability> _abilityOptionsList = new();
    private readonly Dictionary<Ability, int> _abilitiesOwned = new();
    private readonly HashSet<Ability> _randomUniquePickedAbilities = new();
    private const int _maximumAbilities = 4;
    private int _levelUps, _rerollAmount, _skipAmount, _banishAmount; //CHANGE LATER TO BE A STAT
    private bool _isBanishActive;
    private void Awake()
    {
        _rerollAmount = PlayerStats.Instance.Rerolls;
        _skipAmount = PlayerStats.Instance.Skips;
        _banishAmount = PlayerStats.Instance.Banishes;
        UpdateCounterAndText(_rerollAmount, _rerollText, _rerollButton);
        UpdateCounterAndText(_skipAmount, _skipText, _skipButton);
        UpdateCounterAndText(_banishAmount, _banishText, _banishButton);
        _abilityOptionsList.RemoveAll(ability => PlayerStats.Instance.AbilitiesToRemove.Contains(ability));
        _abilityOptionsList.InsertRange(0, PlayerStats.Instance.AbilitiesToAdd);
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
        _skipButton.onClick.AddListener(() => 
        {
            _skipAmount--;
            PlayerExperience.Instance.AcquireExperience(Mathf.RoundToInt(PlayerExperience.Instance.RequiredExperienceToLevelUp * 0.2f));
            CloseMenu();
            UpdateCounterAndText(_skipAmount, _skipText, _skipButton);
        });
    }
    private void Start()
    {
        PlayerExperience.Instance.OnLevelUp += Instance_OnLevelUp;
    }
    private void OnDestroy()
    {
        PlayerExperience.Instance.OnLevelUp -= Instance_OnLevelUp;
    }
    public void AbilityChosen(Ability abilityChosen)
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
        if(!abilityChosen)
        {
            print("cant banish gojos");
            return;
        }
        BanishAndClose(abilityChosen);
    }
    private void EarnGojosAndClose()
    {
        PlayerPrefs.SetInt("Gojos", PlayerPrefs.GetInt("Gojos", 0) + Mathf.RoundToInt(20 + (20 * PlayerStats.Instance.Greed)));
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
    private void UpgradeAndClose(Ability abilityUpgraded)
    {
        _playerAbilities.AbilityUpgrade(abilityUpgraded);
        //CHECK IF ABILITY IS PURCHASED
        if (_abilitiesOwned.ContainsKey(abilityUpgraded))
        {
            //UPGRADE ABILITY AND REMOVE IF REACH MAX
            if (++_abilitiesOwned[abilityUpgraded] == abilityUpgraded.MaximumLevel)
            {
                _abilityOptionsList.Remove(abilityUpgraded);
            }
        }
        else
        {
            //PURCHASE ABILITY
            _abilitiesOwned.Add(abilityUpgraded, 1);
            //CHECK IF REACHED MAXIMUM PLAYER ABILITIES
            if (_abilitiesOwned.Count == _maximumAbilities)
            {
                _abilityOptionsList.RemoveAll(ability => !_abilitiesOwned.ContainsKey(ability));
            }
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
            Ability newAbility = GetRandomAbility();
            _randomUniquePickedAbilities.Add(newAbility);
            _selectableAbilityButtons[i].ChangeButtonUI(newAbility, _abilitiesOwned.TryGetValue(newAbility, out int value)? value : 0);
        }
    }
    private Ability GetRandomAbility()
    {
        Ability newAbility = _abilityOptionsList[UnityEngine.Random.Range(0, _abilityOptionsList.Count)];
        if (!_randomUniquePickedAbilities.Contains(newAbility))
        {
            return newAbility;
        }
        return GetRandomAbility();
    }
    private void BanishAndClose(Ability abilityBanished)
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
        RerollAbilitySelection();
        _menuCanvas.SetActive(true);
        Time.timeScale = 0;
    }
    private void CloseMenu()
    {
        if (--_levelUps > 0)
        {
            RerollAbilitySelection();
            return;
        }
        _menuCanvas.SetActive(false);
        Time.timeScale = 1;
        OnMenuClosed?.Invoke();
    }
}