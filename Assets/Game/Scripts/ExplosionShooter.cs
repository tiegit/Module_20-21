using UnityEngine;

public class ExplosionShooter : IShooter
{
    private PlayerInput _playerInput;
    private GameObject _explosionPrefab;
    private float _force;
    private float _radius;

    public ExplosionShooter(PlayerInput playerInput, GameObject explosionPrefab, float force, float radius)
    {
        _playerInput = playerInput;
        _explosionPrefab = explosionPrefab;
        _force = force;
        _radius = radius;
    }

    public void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(_playerInput.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            Collider[] targets = Physics.OverlapSphere(hitInfo.point, _radius);

            foreach (Collider target in targets)
            {
                if (target.TryGetComponent(out IDamageable damageable))
                {
                    damageable.ApplyEffect(_force, hitInfo.point, _radius);
                }
            }

            var explosionInstance = Object.Instantiate(_explosionPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
            explosionInstance.transform.localScale *= _radius;
        }
    }
}