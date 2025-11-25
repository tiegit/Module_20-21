using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Pointer _pointerPrefab;
    [SerializeField] private float _zoomSpeed = 5f;
    [SerializeField] private float _minGrabDistance = 0.1f;
    [SerializeField] private float _maxGrabDistance = 50f;

    [SerializeField, Space (15)] private float _explosionRadius = 2f;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private GameObject _explosionPrefab;

    private Player _player;
    private ShooterSwitcher _shooterSwitcher;
    private PlayerInput _playerInput;
    private IRaycastMover _raycaster;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _raycaster = new CameraRaycastObjectMover(_playerInput, _zoomSpeed, _minGrabDistance, _maxGrabDistance);

        Instantiate(_pointerPrefab).Initialize(_raycaster);

        _player = new Player(_playerInput);

        _shooterSwitcher = new ShooterSwitcher(_playerInput, _player, _explosionPrefab, _explosionForce, _explosionRadius);
    }

    private void Update()
    {
        _raycaster?.CustomUpdate();
        _shooterSwitcher?.CustomUpdate();
        _player?.CustomUpdate();
    }
}
