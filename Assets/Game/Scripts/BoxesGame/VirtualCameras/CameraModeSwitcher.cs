using Cinemachine;
using System.Collections.Generic;

public class CameraModeSwitcher
{
    private PlayerInput _playerInput;
    private Queue<CinemachineVirtualCamera> _virtualCameras;

    public CameraModeSwitcher(PlayerInput playerInput, IEnumerable<CinemachineVirtualCamera> virtualCameras)
    {
        _playerInput = playerInput;
        _virtualCameras = new Queue<CinemachineVirtualCamera>(virtualCameras);

        foreach (var camera in _virtualCameras)
        {
            camera.Priority = 0;
            camera.gameObject.SetActive(true);
        }

        SwitchNextMode();
    }

    public void CustomUpdate()
    {
        if (_playerInput.FKeyPressed)
            SwitchNextMode();
    }

    private void SwitchNextMode()
    {
        CinemachineVirtualCamera nextMode = _virtualCameras.Dequeue();

        foreach (var camera in _virtualCameras)
            camera.Priority = 0;

        nextMode.Priority = 10;

        _virtualCameras.Enqueue(nextMode);
    }
}
