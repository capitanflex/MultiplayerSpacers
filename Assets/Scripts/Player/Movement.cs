using Photon.Pun;
using UnityEngine;

namespace Player
{
    public class Movement : MonoBehaviour
    {
        private Rigidbody2D _rigidbody;
        [SerializeField] private float speed;
        private bl_Joystick _joystick;
        private PhotonView _view;
    
    
        void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _joystick = FindObjectOfType<bl_Joystick>();
            _view = GetComponent<PhotonView>();
        }
    
        void Update()
        {
            if (_view.IsMine)
            {
                var xAxis = _joystick.Horizontal;
                var zAxis = _joystick.Vertical;

                MoveWithVelocity(xAxis, zAxis);
                RotateToMoveDirection(xAxis, zAxis);
            
            }
        }

        private void MoveWithVelocity(float xAxis, float zAxis)
        {
            _rigidbody.velocity = new Vector2(xAxis * speed,  zAxis * speed);
        }

        private void RotateToMoveDirection(float xAxis, float zAxis)
        {
            float angle = Mathf.Atan2(xAxis,  zAxis) * Mathf.Rad2Deg ;
        
            if (angle != 0)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, -angle);
            }
        }
    }
}
