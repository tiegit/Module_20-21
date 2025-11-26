using UnityEngine;

public class ShipView : MonoBehaviour
{
    [SerializeField] private ShipEngine _shipEngine;

    private void Update()
    {
        transform.position = _shipEngine.Position - Vector3.up * 0.45f;
        transform.rotation = _shipEngine.CurrentOrientation.rotation;
    }
}