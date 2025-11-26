using UnityEngine;

public class Pointer : MonoBehaviour
{
    [SerializeField] private GameObject _pointerView;

    private IRaycastMover _raycaster;

    public bool IsActive { get; private set; }

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
            if (!IsActive)
                Show();

            transform.position = _raycaster.PointerPosition;
        }
        else
        {
            if (IsActive)
                Hide();
        }
    }

    private void Show()
    {
        _pointerView.SetActive(true);
        IsActive = true;
    }

    private void Hide()
    {
        _pointerView.SetActive(false);
        IsActive = false;
    }
}