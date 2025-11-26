using UnityEngine;

namespace ShipGame
{

    public class Bootstrap : MonoBehaviour
    {
        //[SerializeField, Space(15)] private float _windMaxForce = 20f;

        private PlayerInput _playerInput;
        
        private void Awake()
        {
            _playerInput = new PlayerInput();            
        }

        private void Update()
        {
        }
    }
}
