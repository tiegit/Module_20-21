using UnityEngine;

public class PointRaycastObjectMover : IRaycastMover // TODO для работы этого класса нужно управление точкой
{
    private PlayerInput _playerInput;
    private Transform _point;

    private IGrabbable _hoveredGrabbable;
    private IGrabbable _grabbedObject;

    private float _grabDistance;

    public PointRaycastObjectMover(PlayerInput playerInput, Transform point)
    {
        _playerInput = playerInput;
        _point = point;
    }

    public Vector3 PointerPosition { get; private set; }
    public bool CanShowPointer => _hoveredGrabbable != null;

    public void CustomUpdate()
    {
        Ray ray = new Ray(_point.position, _point.forward);

        ProcessRaycast(ray);

        if (_playerInput.LeftMouseButtonDown)
            TryGrab(ray);

        if (_playerInput.LeftMouseButtonUp)
            TryRelease();
    }

    private void ProcessRaycast(Ray ray)
    {
        bool hasHit = Physics.Raycast(ray, out RaycastHit hitInfo);

        if (hasHit && hitInfo.collider.TryGetComponent(out IGrabbable grabbable))
        {
            if (_hoveredGrabbable != grabbable)
                _hoveredGrabbable = grabbable;

            PointerPosition = hitInfo.point;
        }
        else
        {
            _hoveredGrabbable = null;
        }

        if (_grabbedObject != null)
        {
            Vector3 anchorPoint = ray.GetPoint(_grabDistance);
            _grabbedObject.SetAnchorPointPosition(anchorPoint);
        }
    }

    private void TryGrab(Ray ray)
    {
        if (_hoveredGrabbable != null && _grabbedObject == null)
        {
            _grabbedObject = _hoveredGrabbable;
            _grabDistance = Vector3.Distance(_point.position, PointerPosition);

            Vector3 anchorPoint = ray.GetPoint(_grabDistance);

            _grabbedObject.Grab(anchorPoint);
            _grabbedObject.SetAnchorPointPosition(anchorPoint);
        }
    }

    private void TryRelease()
    {
        if (_grabbedObject != null)
        {
            _grabbedObject.Drop();
            _grabbedObject = null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Ray pointRay = new Ray(_point.position, _point.forward);
        Gizmos.DrawRay(pointRay.origin, pointRay.direction * 20f);
    }
}
