using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Character", menuName = "ScriptableObjects/Character", order = 1)]
public class Character : ScriptableObject
{
    [SerializeField] private int _health, _power, _armor, _revival, _destruction, _soulstone, _reroll, _skip, _banish;
    [SerializeField] [Range(0, 1)]private float _cooldown, _speed, _moveSpeed, _luck, _greed, _growth;
    [SerializeField] private List<Ability> _abilitiesToAdd = new();
    [SerializeField] private List<Ability> _abilitiesToRemove = new();
    public List<Ability> AbilitiesToAdd => _abilitiesToAdd;
    public List<Ability> AbilitiesToRemove => _abilitiesToRemove;
    public int Armor => _armor;
    public int Health => _health;
    public int Revivals => _revival;
    public int Destructions => _destruction;
    public int Soulstones => _soulstone;
    public int Rerolls => _reroll;
    public int Skips => _skip;
    public int Banishes => _banish;
    public float Power => _power;
    public float Speed => _speed;
    public float MoveSpeed => _moveSpeed;
    public float Luck => _luck;
    public float Cooldown => _cooldown;
    public float Greed => _greed;
    public float Growth => _growth;
}
