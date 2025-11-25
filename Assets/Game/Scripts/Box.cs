using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Box : MonoBehaviour, IGrabbable, IDamageable
{
    [SerializeField] private float _raycastDistance = 2f;

    private Rigidbody _rigidbody;
    private Vector3 _anchorPointPosition;
    private Vector3 _grabOffset;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
        if (_rigidbody.isKinematic)
        {
            Vector3 targetPosition = _anchorPointPosition + _grabOffset;

            if (TryGetComponent(out Collider collider))
            {
                RaycastHit[] hits = Physics.RaycastAll(transform.position, Vector3.down, _raycastDistance);

                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.gameObject != gameObject)
                    {
                        float groundHeight = hit.point.y;
                        float boxHalfHeight = collider.bounds.extents.y;
                        float minY = groundHeight + boxHalfHeight;

                        if (targetPosition.y < minY)
                            targetPosition.y = minY;

                        break;
                    }
                }
            }

            _rigidbody.MovePosition(targetPosition);
        }
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

    public void ApplyEffect(float force, Vector3 point, float radius)
    {
        if (_rigidbody.isKinematic)
            Drop();
        _rigidbody.AddExplosionForce(force, point, radius);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawRay(transform.position, Vector3.down * _raycastDistance);
    }
}
