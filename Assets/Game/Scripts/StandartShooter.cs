using UnityEngine;

public class StandartShooter : IShooter
{
    private PlayerInput _playerInput;

    public StandartShooter(PlayerInput playerInput) => _playerInput = playerInput;

    public void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(_playerInput.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.SetEffect(hitInfo.point);
            }
        }
    }
}
