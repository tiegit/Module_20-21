using UnityEngine;

public class CameraRaycastObjectMover : IRaycastMover
{
    private PlayerInput _playerInput;

    private float _zoomSpeed = 5f;
    private float _minGrabDistance = 0.1f;
    private float _maxGrabDistance = 50f;

    private IGrabbable _hoveredGrabbable;
    private IGrabbable _grabbedObject;

    private float _grabDistance;

    public Vector3 PointerPosition { get; private set; }
    public bool CanShowPointer => _hoveredGrabbable != null;

    public CameraRaycastObjectMover(PlayerInput playerInput, float zoomSpeed, float minGrabDistance, float maxGrabDistance)
    {
        _playerInput = playerInput;
        _zoomSpeed = zoomSpeed;
        _minGrabDistance = minGrabDistance;
        _maxGrabDistance = maxGrabDistance;
    }

    public void CustomUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(_playerInput.MousePosition);

        ProcessRaycast(ray);

        HandleZoom(ray);

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

    private void HandleZoom(Ray ray)
    {
        if (_grabbedObject != null && _playerInput.MouseScrollDelta != 0f)
        {
            _grabDistance += _playerInput.MouseScrollDelta * _zoomSpeed;

            _grabDistance = Mathf.Clamp(_grabDistance, _minGrabDistance, _maxGrabDistance);

            Vector3 newAnchorPoint = ray.GetPoint(_grabDistance);
            _grabbedObject.SetAnchorPointPosition(newAnchorPoint);
        }
    }

    private void TryGrab(Ray ray)
    {
        if (_hoveredGrabbable != null && _grabbedObject == null)
        {
            _grabbedObject = _hoveredGrabbable;
            _grabDistance = Vector3.Distance(Camera.main.transform.position, PointerPosition);

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
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawRay(cameraRay.origin, cameraRay.direction * 20f);
    }
}
