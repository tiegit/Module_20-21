public class Player
{
    private PlayerInput _playerInput;

    private IShooter _shooter;

    public Player(PlayerInput playerInput) => _playerInput = playerInput;

    public void CustomUpdate()
    {
        if (_playerInput.RightMouseButtonDown)
            _shooter.Shoot();
    }

    public void SetShooter(IShooter shooter) => _shooter = shooter;
}
