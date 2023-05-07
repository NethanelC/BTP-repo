using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected int _currentLevel, _maximumLevel;
    private float _cooldown;
    protected abstract void UseAbility();
}
