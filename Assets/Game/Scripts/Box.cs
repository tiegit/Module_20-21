using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Box : MonoBehaviour, IGrabbable, IDamageable
{
    private Rigidbody _rigidbody;

    private Vector3 _anchorPointPosition;
    private Vector3 _grabOffset;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
        if (_rigidbody.isKinematic)
            _rigidbody.MovePosition(_anchorPointPosition + _grabOffset);
    }

    public void Grab(Vector3 grabPoint)
    {
        _grabOffset = transform.position - grabPoint;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.isKinematic = true;
    }

    public void Drop() => _rigidbody.isKinematic = false;

    public void SetAnchorPointPosition(Vector3 anchorPointPosition) => _anchorPointPosition = anchorPointPosition;

    public void SetEffect(Vector3 point)
    {
        Debug.Log($"{name}");
    }
}
