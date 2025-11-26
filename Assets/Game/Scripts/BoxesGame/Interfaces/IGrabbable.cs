using UnityEngine;

public interface IGrabbable
{
    void Drop();
    void Grab(Vector3 grabPoint);
    void SetAnchorPointPosition(Vector3 anchorPoint);
}
