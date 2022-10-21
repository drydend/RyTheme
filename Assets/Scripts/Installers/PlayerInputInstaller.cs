using UnityEngine;
using Zenject;

public class PlayerInputInstaller : MonoInstaller
{
    [SerializeField]
    private PlayerInput _playerInputPrefab;

    public override void InstallBindings()
    {
        var playerInput = Instantiate(_playerInputPrefab);
        DontDestroyOnLoad(playerInput);

        Container.Bind<PlayerInput>()
            .FromInstance(playerInput)
            .AsSingle();
    }
}
