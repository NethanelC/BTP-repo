using Zenject;
using UnityEngine;
using System.Collections;

public class UntitledInstaller : MonoInstaller
{
    [SerializeField] private PlayerMovement _playerMovement;
    public override void InstallBindings()
    {
        Container.Bind<PlayerMovement>().FromInstance(_playerMovement).AsSingle();
    }
}