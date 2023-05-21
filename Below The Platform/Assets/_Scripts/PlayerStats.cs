using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    public int Armor { get; private set; }
    public int Health { get; private set; }
    public int Rerolls { get; private set; }
    public int Skips { get; private set; }
    public int Banishes { get; private set; }
    public int Revivals { get; private set; }
    public int Soulstones { get; private set; }
    public int Destructions { get; private set; }
    public float Power { get; private set; }
    public float Cooldown { get; private set; }
    public float MoveSpeed { get; private set; }
    public float Speed { get; private set; }
    public float Luck { get; private set; }
    public float Greed { get; private set; }
    public float Growth { get; private set; }
    private Character _selectedCharacter;
    private List<Ability> _abilitiesToRemove;
    private List<Ability> _abilitiesToAdd;
    public List<Ability> AbilitiesToAdd => _abilitiesToAdd;
    public List<Ability> AbilitiesToRemove => _abilitiesToRemove;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this);
        CharacterButton.OnCharacterSelected += CharacterSelectButton_OnCharacterSelected;
    }
    private void OnDestroy()
    {
        CharacterButton.OnCharacterSelected -= CharacterSelectButton_OnCharacterSelected;
    }
    private void CharacterSelectButton_OnCharacterSelected(Character selectedCharacter)
    {
        _selectedCharacter = selectedCharacter;
        _abilitiesToRemove = selectedCharacter.AbilitiesToRemove;
        _abilitiesToAdd = selectedCharacter.AbilitiesToAdd;
        Health = selectedCharacter.Health + PlayerPrefs.GetInt(nameof(Health), 0);
        Armor = selectedCharacter.Armor + PlayerPrefs.GetInt(nameof(Armor), 0);
        Rerolls = selectedCharacter.Rerolls + PlayerPrefs.GetInt(nameof(Rerolls), 0);
        Skips = selectedCharacter.Skips + PlayerPrefs.GetInt(nameof(Skips), 0);
        Banishes = selectedCharacter.Banishes + PlayerPrefs.GetInt(nameof(Banishes), 0);
        Revivals = selectedCharacter.Revivals + PlayerPrefs.GetInt(nameof(Revivals), 0);
        Soulstones = selectedCharacter.Soulstones + PlayerPrefs.GetInt(nameof(Soulstones), 0);
        Destructions = selectedCharacter.Destructions + PlayerPrefs.GetInt(nameof(Destructions), 0);

        Growth = selectedCharacter.Growth + (PlayerPrefs.GetInt(nameof(Growth), 0) * 0.1f);
        Greed = selectedCharacter.Greed + (PlayerPrefs.GetInt(nameof(Greed), 0) * 0.1f);
        Luck = selectedCharacter.Luck + (PlayerPrefs.GetInt(nameof(Luck), 0) * 0.1f);
        
        Cooldown = selectedCharacter.Cooldown + (PlayerPrefs.GetInt(nameof(Cooldown), 0) * 0.05f);
        MoveSpeed = selectedCharacter.MoveSpeed + (PlayerPrefs.GetInt(nameof(MoveSpeed), 0) * 0.05f);
        Speed = selectedCharacter.Speed + (PlayerPrefs.GetInt(nameof(Speed), 0) * 0.05f);
        Power = selectedCharacter.Power + (PlayerPrefs.GetInt(nameof(Power), 0) * 0.05f);
        print(Luck);
    }
}
