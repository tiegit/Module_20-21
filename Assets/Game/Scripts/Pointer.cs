using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private GameObject _pointerView;

    private IRaycastMover _raycaster;

    private bool _isActive;

    public void Initialize(IRaycastMover raycaster)
    {
        _raycaster = raycaster;

        Hide();
    }

    private void Update()
    {
        if (_raycaster == null)
            return;

        if (_raycaster.CanShowPointer)
        {
            if (!_isActive)
                Show();

            transform.position = _raycaster.PointerPosition;
        }
        else
        {
            if (_isActive)
                Hide();
        }
    }

    private void Show()
    {
        _pointerView.SetActive(true);
        _isActive = true;
    }

    private void Hide()
    {
        _pointerView.SetActive(false);
        _isActive = false;
    }
}