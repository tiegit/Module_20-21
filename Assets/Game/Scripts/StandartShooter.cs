using UnityEngine;

public class StandartShooter : IShooter
{
    private PlayerInput _playerInput;
    private GameObject _explosionPrefab;
    private float _force;

    public StandartShooter(PlayerInput playerInput, GameObject explosionPrefab, float force)
    {
        _playerInput = playerInput;
        _explosionPrefab = explosionPrefab;
        _force = force;
    }

    public void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(_playerInput.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyEffect(_force, hitInfo.point, 0);

                Object.Instantiate(_explosionPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            }
        }
    }
}
