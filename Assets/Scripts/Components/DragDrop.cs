using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class DragDrop : MonoBehaviour
    {
        private Rigidbody2D _rigidbody2D;
        private bool _dragged;
        private Camera _camera;
        private Transform _transform;
        private float _positionZ;

        private const int DraggedPositionZ = -6;

        public void Awake()
        {
            _camera = Camera.main;
            _transform = transform;
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void OnMouseDown()
        {
            _positionZ = _transform.position.z;
            _rigidbody2D.simulated = false;
            _rigidbody2D.velocity = Vector2.zero;
            _dragged = true;
        }
    
        public void OnMouseUp()
        {
            var position = _transform.position;
            _transform.position = new Vector3(position.x, position.y, _positionZ);
            _rigidbody2D.simulated = true;
            _dragged = false;
        }

        public void Update()
        {
            if (!_dragged)
            {
                return;
            }

            var mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _transform.position = new Vector3(mousePosition.x, mousePosition.y, DraggedPositionZ);
        }
    }
}