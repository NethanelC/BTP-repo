using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilitySelectMenu : MonoBehaviour
{
    public static bool IsBanishMode;
    [SerializeField] private GameObject _menuCanvas;
    [SerializeField] private List <Ability> _abilityOptionsList = new();
    [SerializeField] private AbilitySelectButton[] _selectableAbilityButtons = new AbilitySelectButton[3];
    [SerializeField] private Button _rerollButton, _skipButton, _banishButton;
    private readonly Dictionary<Ability, int> _abilitiesOwned = new();
    private readonly HashSet<Ability> _randomUniquePickedAbilities = new();
    private int _maximumAbilities = 3; //CHANGE LATER TO BE A STAT
    private void Awake()
    {
        AbilitySelectButton.OnAbilityUpgrade += AbilitySelectButton_OnAbilityUpgrade;
        AbilitySelectButton.OnAbilityBanished += AbilitySelectButton_OnAbilityBanished; 
        _rerollButton.onClick.AddListener(() =>
        {
            RerollAbilitySelection();
        });
        _banishButton.onClick.AddListener(() =>
        {
            IsBanishMode = true;
        });
        _skipButton.onClick.AddListener(() => 
        { 
            PlayerExperience.Instance.AcquireExperience(Mathf.RoundToInt(PlayerExperience.Instance.ExperienceRequiredToLevelUp * 0.2f));
            _menuCanvas.SetActive(false);
        });
    }
    private void Start()
    {
        PlayerExperience.Instance.OnLevelUp += Instance_OnLevelUp;
    }
    private void OnDestroy()
    {
        AbilitySelectButton.OnAbilityUpgrade -= AbilitySelectButton_OnAbilityUpgrade;
        AbilitySelectButton.OnAbilityBanished -= AbilitySelectButton_OnAbilityBanished;
        PlayerExperience.Instance.OnLevelUp -= Instance_OnLevelUp;
    }
    private void AbilitySelectButton_OnAbilityUpgrade(Ability abilityUpgraded)
    {
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
        Ability newAbility = _abilityOptionsList[Random.Range(0, _abilityOptionsList.Count)];
        if (!_randomUniquePickedAbilities.Contains(newAbility))
        {
            return newAbility;
        }
        return GetRandomAbility();
    }
    private void AbilitySelectButton_OnAbilityBanished(Ability abilityBanished)
    {
        IsBanishMode = false;
        _abilityOptionsList.Remove(abilityBanished);
    }
    private void Instance_OnLevelUp(int level)
    {
        RerollAbilitySelection();
        _menuCanvas.SetActive(true); 
    }
}