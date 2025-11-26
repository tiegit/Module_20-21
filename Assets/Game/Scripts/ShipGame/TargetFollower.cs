using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    [SerializeField]  private Transform _target;
    private Vector3 _offset;

    private void Awake() => _offset = transform.position - _target.position;

    private void LateUpdate()
    {
        if (_target == null)
            return;

        transform.position = _target.position + _offset;
    }
}
