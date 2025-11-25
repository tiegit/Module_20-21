using UnityEngine;

public class PlayerInput
{
    private const string MouseScrollWheel = "Mouse ScrollWheel";

    public Vector3 MousePosition => Input.mousePosition;

    public bool LeftMouseButtonDown => Input.GetMouseButtonDown(0);

    public bool LeftMouseButtonUp => Input.GetMouseButtonUp(0);

    public bool RightMouseButtonDown => Input.GetMouseButtonDown(1);

    public float MouseScrollDelta => Input.GetAxis(MouseScrollWheel);

    public bool Alpha1Down => Input.GetKeyDown(KeyCode.Alpha1);

    public bool Alpha2Down => Input.GetKeyDown(KeyCode.Alpha2);

    public bool FKeyPressed => Input.GetKeyDown(KeyCode.F);
}