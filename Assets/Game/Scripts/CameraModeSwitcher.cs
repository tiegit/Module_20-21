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
            camera.gameObject.SetActive(false);

        nextMode.gameObject.SetActive(true);

        _virtualCameras.Enqueue(nextMode);
    }
}