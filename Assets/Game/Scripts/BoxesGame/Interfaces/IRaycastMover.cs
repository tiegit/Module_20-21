using UnityEngine;

public interface IRaycastMover
{
    Vector3 PointerPosition { get; }
    bool CanShowPointer { get; }

    void CustomUpdate();
}