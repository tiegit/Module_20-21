using UnityEngine;

public class CameraRaycastObjectMover : IRaycastMover
{
    private const float PositionThreshold = 0.001f;
    private const float RotationThreshold = 0.1f;

    private PlayerInput _playerInput;

    private float _zoomSpeed;
    private float _minGrabDistance;
    private float _maxGrabDistance;

    private IGrabbable _hoveredGrabbable;
    private IGrabbable _grabbedObject;

    private float _grabDistance;

    private Vector3 _previousCameraPosition;
    private Quaternion _previousCameraRotation;

    public bool IsCameraMoving { get; private set; }
    public Vector3 PointerPosition { get; private set; }
    public bool CanShowPointer => _hoveredGrabbable != null;

    public CameraRaycastObjectMover(PlayerInput playerInput, float zoomSpeed, float minGrabDistance, float maxGrabDistance)
    {
        _playerInput = playerInput;
        _zoomSpeed = zoomSpeed;
        _minGrabDistance = minGrabDistance;
        _maxGrabDistance = maxGrabDistance;

        _previousCameraPosition = Camera.main.transform.position;
        _previousCameraRotation = Camera.main.transform.rotation;

        IsCameraMoving = false;
    }

    public void CustomUpdate()
    {
        UpdateCameraMovementStatus();

        Ray ray = Camera.main.ScreenPointToRay(_playerInput.MousePosition);

        ProcessRaycast(ray);

        HandleZoom(ray);

        if (_playerInput.LeftMouseButtonDown)
            TryGrab(ray);

        if (_playerInput.LeftMouseButtonUp)
            TryRelease();
    }

    private void UpdateCameraMovementStatus()
    {
        Vector3 currentPosition = Camera.main.transform.position;
        Quaternion currentRotation = Camera.main.transform.rotation;

        float positionDelta = Vector3.Distance(currentPosition, _previousCameraPosition);
        float rotationDelta = Quaternion.Angle(currentRotation, _previousCameraRotation);

        IsCameraMoving = positionDelta > PositionThreshold || rotationDelta > RotationThreshold;

        _previousCameraPosition = currentPosition;
        _previousCameraRotation = currentRotation;
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
            if (IsCameraMoving)
            {
                _grabDistance = Vector3.Distance(Camera.main.transform.position, PointerPosition);                
            }
            else
            {
                Vector3 anchorPoint = ray.GetPoint(_grabDistance);
                _grabbedObject.SetAnchorPointPosition(anchorPoint);
            }
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

    private void OnDrawGizmos() // без MonoBehaviour не работает
    {
        Gizmos.color = Color.red;
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Gizmos.DrawRay(cameraRay.origin, cameraRay.direction * 20f);
    }
}
