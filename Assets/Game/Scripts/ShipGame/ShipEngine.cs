using UnityEngine;

public class ShipEngine : MonoBehaviour
{
    private const float CheckDistance = 0.6f;

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxSpeed;

    [SerializeField] private Rigidbody _movable;
    [SerializeField] private Transform _currentOrientation;

    [SerializeField] private Wind _wind;

    private PlayerInput _playerInput;
    private bool _onGround;
    private int _horizontalInput;
    private Vector3 _sailDirection = Vector3.forward;

    public Vector3 Position => _movable.position;
    public Transform CurrentOrientation => _currentOrientation;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _movable.maxLinearVelocity = _maxSpeed;
    }

    private void Update()
    {
        _horizontalInput = 0;

        _onGround = Physics.Raycast(_movable.position, Vector3.down, out RaycastHit hitInfo, CheckDistance);

        if (_onGround)
        {
            Vector3 targetDirection = Vector3.Cross(_currentOrientation.right, hitInfo.normal);
            _currentOrientation.rotation = Quaternion.LookRotation(targetDirection);
        }

        if (_playerInput.AKeyPressed)
            _horizontalInput = -1;


        if (_playerInput.DKeyPressed)
            _horizontalInput = 1;

        _currentOrientation.Rotate(Vector3.up * _horizontalInput * _rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void FixedUpdate()
    {
        if (_onGround)
        {
            float windAndSailDotProduct = Vector3.Dot(_sailDirection, _wind.Direction);
            float clampedDot = Mathf.Clamp(windAndSailDotProduct, 0f, 1f);

            if (windAndSailDotProduct <= 0)
                _movable.velocity = Vector3.zero;
            else
                _movable.AddForce(_currentOrientation.forward * clampedDot * _wind.Force, ForceMode.Acceleration);

            Debug.Log($"{windAndSailDotProduct} (clamped: {clampedDot}) Скорость: {_movable.velocity}");
        }
    }

    public void SetSailDirection(Vector3 sailDirection) => _sailDirection = sailDirection;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Physics.Raycast(_movable.position, Vector3.down, out RaycastHit hitInfo, CheckDistance);
        Gizmos.DrawRay(_movable.position, Vector3.Cross(_currentOrientation.right, hitInfo.normal) * 5f);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(_movable.position, Vector3.down * 0.6f);
    }
}
