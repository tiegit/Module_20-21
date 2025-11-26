using UnityEngine;

public class Sail : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private PlayerInput _playerInput;
    private int _horizontalInput;

    private void Awake() => _playerInput = new PlayerInput();

    private void Update()
    {
        _horizontalInput = 0;

        if (_playerInput.LeftKeyPressed)
        {
            _horizontalInput = -1;
        }

        if (_playerInput.RightKeyPressed)
        {
            _horizontalInput = 1;
        }

        transform.Rotate(Vector3.up * _horizontalInput * _rotationSpeed * Time.deltaTime, Space.Self);
    }
}