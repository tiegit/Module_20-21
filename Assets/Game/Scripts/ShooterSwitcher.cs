using UnityEngine;

public class ShooterSwitcher
{
    private PlayerInput _playerInput;
    private Player _player;
    private GameObject _explosionPrefab;
    private float _force;
    private float _radius;

    public ShooterSwitcher(PlayerInput playerInput,
                           Player player,
                           GameObject explosionPrefab,
                           float force,
                           float radius)
    {
        _playerInput = playerInput;
        _player = player;
        _explosionPrefab = explosionPrefab;
        _force = force;
        _radius = radius;

        _player.SetShooter(new StandartShooter(_playerInput, _explosionPrefab, _force));
    }

    public void CustomUpdate()
    {
        if (_playerInput.Alpha1Down)
            _player.SetShooter(new StandartShooter(_playerInput, _explosionPrefab, _force));

        if (_playerInput.Alpha2Down)
            _player.SetShooter(new ExplosionShooter(_playerInput, _explosionPrefab, _force, _radius));
    }
}