using UnityEngine;

public class ShipView : MonoBehaviour
{
    [SerializeField] private ShipEngine _shipEngine;

    private void LateUpdate()
    {
        transform.position = _shipEngine.Position - Vector3.up * 0.5f;
        transform.rotation = _shipEngine.CurrentOrientation.rotation;
    }
}