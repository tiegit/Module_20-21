using UnityEngine;

public interface IDamageable
{
    void ApplyEffect(float force, Vector3 point, float radius);
}