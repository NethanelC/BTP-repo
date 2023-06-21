using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Zenject;

public class Dash : AbilityBase
{
    [Inject]
    private PlayerMovement _playerMovement;
    public void Construct(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }
    public override void CastAbility()
    {
        _playerMovement.IncreaseSpeed(2, 150);
    }
}
