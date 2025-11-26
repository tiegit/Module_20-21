using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private float _force = 1f;
    [SerializeField] private float _changeInterval = 10f;
    [SerializeField] private float _rotationSpeed = 50;

    private float _timer;
    private Quaternion _targetRotation;

    public float Force => _force;
    public Vector3 Direction => (transform.rotation * Vector3.forward).normalized;

    private void Start()
    {
        _timer = 0;
        _targetRotation = transform.rotation;
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _changeInterval)
        {
            SetRandomDirection();
            _timer = 0;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);
    }

    private void SetRandomDirection()
    {
        float deltaAngle = Random.Range(-180f, 180f);

        _targetRotation = transform.rotation * Quaternion.Euler(0f, deltaAngle, 0f);
    }
}
