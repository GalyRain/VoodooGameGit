using UnityEngine;

namespace Game
{
    public class RotationDoll : MonoBehaviour
    {
        private Camera _camera;
        private Vector3 _mousePositionA;
        private Vector3 _mousePositionB;
        private Vector3 _rotation;
        private float _angle;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnMouseDown()
        {
            _rotation = transform.rotation.eulerAngles;
            _mousePositionA = Input.mousePosition - _camera.WorldToScreenPoint(transform.position);
        }

        private void OnMouseDrag()
        {
            _mousePositionB = Input.mousePosition - _camera.WorldToScreenPoint(transform.position);
            var position = transform.position;

            _angle = Vector2.SignedAngle(_mousePositionA - position, _mousePositionB - position);
            transform.rotation = Quaternion.Euler(0, _angle + _rotation.y, 0);
        }
    }
}



