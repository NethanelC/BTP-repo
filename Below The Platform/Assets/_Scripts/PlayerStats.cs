using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }
    private Character SelectedCharacter;
    public int Health => SelectedCharacter.Health + PlayerPrefs.GetInt(nameof(Health), 0);
    public int Armor => SelectedCharacter.Armor + PlayerPrefs.GetInt(nameof(Armor), 0);
    public int Rerolls => SelectedCharacter.Rerolls + PlayerPrefs.GetInt(nameof(Rerolls), 0);
    public int Skips => SelectedCharacter.Skips + PlayerPrefs.GetInt(nameof(Skips), 0);
    public int Banishes => SelectedCharacter.Banishes + PlayerPrefs.GetInt(nameof(Banishes), 0);
    public int Revivals => SelectedCharacter.Revivals + PlayerPrefs.GetInt(nameof(Revivals), 0);
    public int Soulstones => SelectedCharacter.Soulstones + PlayerPrefs.GetInt(nameof(Soulstones), 0);
    public int Destructions => SelectedCharacter.Destructions + PlayerPrefs.GetInt(nameof(Destructions), 0);

    public float Power => 1 + SelectedCharacter.Power + (PlayerPrefs.GetInt(nameof(Power), 0) * .05f);
    public float Cooldown => 1 + SelectedCharacter.Cooldown + (PlayerPrefs.GetInt(nameof(Cooldown), 0) * .05f);
    public float MoveSpeed => 1 + SelectedCharacter.MoveSpeed + (PlayerPrefs.GetInt(nameof(MoveSpeed), 0) * .05f);
    public float Speed => 1 + SelectedCharacter.Speed + (PlayerPrefs.GetInt(nameof(Speed), 0) * .05f);

    public float Luck => 1 + SelectedCharacter.Luck + (PlayerPrefs.GetInt(nameof(Luck), 0) * .1f);
    public float Greed => 1 + SelectedCharacter.Greed + (PlayerPrefs.GetInt(nameof(Greed), 0) * .1f);
    public float Growth => 1 + SelectedCharacter.Growth + (PlayerPrefs.GetInt(nameof(Growth), 0) * .1f);
    public List<AbilityBase> AbilitiesToAdd => SelectedCharacter.AbilitiesToAdd;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
        DontDestroyOnLoad(this);
    }
    private void OnEnable()
    {
        CharacterButton.OnCharacterSelected += CharacterSelectButton_OnCharacterSelected;     
    }
    private void OnDisable()
    {
        CharacterButton.OnCharacterSelected -= CharacterSelectButton_OnCharacterSelected;
    }
    private void CharacterSelectButton_OnCharacterSelected(Character selectedCharacter)
    {
        SelectedCharacter = selectedCharacter;
    }
}
