using UnityEngine;

public class SailView :MonoBehaviour
{
    [SerializeField] private Sail _sail;

    private void Update()
    {
        transform.rotation = _sail.transform.rotation;
        //transform.position = _sail.transform.position;
    }
}
