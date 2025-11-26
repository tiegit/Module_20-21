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

    public bool AKeyPressed => Input.GetKey(KeyCode.A);
    public bool DKeyPressed => Input.GetKey(KeyCode.D);
    public bool LeftKeyPressed => Input.GetKey(KeyCode.LeftArrow);
    public bool RightKeyPressed => Input.GetKey(KeyCode.RightArrow);
}