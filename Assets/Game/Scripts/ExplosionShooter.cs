using UnityEngine;

public class ExplosionShooter : IShooter
{
    private PlayerInput _playerInput;
    private float _radius;

    public ExplosionShooter(PlayerInput playerInput, float radius)
    {
        _playerInput = playerInput;
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
                    damageable.SetEffect(hitInfo.point);
                }
            }
        }
    }
}