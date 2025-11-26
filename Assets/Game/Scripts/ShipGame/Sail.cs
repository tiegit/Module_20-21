using UnityEngine;

public class Sail : MonoBehaviour
{
    [SerializeField] private ShipEngine _shipEngine;
    [SerializeField] private float _rotationSpeed = 150f;
    [SerializeField] private float _sailLimitAngles = 90f;

    private PlayerInput _playerInput;
    private int _horizontalInput;

    private void Awake() => _playerInput = new PlayerInput();

    private void Update()
    {
        _horizontalInput = 0;

        if (_playerInput.LeftKeyPressed)
            _horizontalInput = -1;

        if (_playerInput.RightKeyPressed)
            _horizontalInput = 1;

        Vector3 shipForward = _shipEngine.CurrentOrientation.forward;
        Vector3 shipUp = _shipEngine.CurrentOrientation.up;

        float currentAngle = Vector3.SignedAngle(shipForward, transform.forward, shipUp);

        float deltaAngle = _horizontalInput * _rotationSpeed * Time.deltaTime;

        float newAngle = currentAngle + deltaAngle;

        newAngle = Mathf.Clamp(newAngle, -_sailLimitAngles, _sailLimitAngles);

        Vector3 newSailForward = Quaternion.Euler(0f, newAngle, 0f) * shipForward;

        transform.rotation = Quaternion.LookRotation(newSailForward, shipUp);

        _shipEngine.SetSailDirection(transform.forward);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * 3f);
    }
}
