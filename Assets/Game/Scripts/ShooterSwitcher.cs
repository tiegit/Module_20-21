public class ShooterSwitcher
{
    private readonly PlayerInput _playerInput;
    private readonly Player _player;
    private float _radius;

    public ShooterSwitcher(PlayerInput playerInput, Player player, float radius)
    {
        _playerInput = playerInput;
        _player = player;
        _radius = radius;

        _player.SetShooter(new StandartShooter(_playerInput));
    }

    public void CustomUpdate()
    { 
        if( _playerInput.Alpha1Down)
            _player.SetShooter(new StandartShooter(_playerInput));

        if( _playerInput.Alpha2Down)
            _player.SetShooter(new ExplosionShooter(_playerInput, _radius));
    }
}