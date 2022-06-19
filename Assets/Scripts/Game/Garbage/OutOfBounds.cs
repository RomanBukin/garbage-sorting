using UnityEngine;
using Zenject;

namespace Game.Garbage
{
    public class OutOfBounds : MonoBehaviour
    { 
        [SerializeField] private Vector2 min = Vector2.zero;
        [SerializeField] private Vector2 max = Vector2.zero;

        private Gameplay _gameplay;

        [Inject]
        private void Construct(Gameplay gameplay)
        {
            _gameplay = gameplay;
        }

        private void FixedUpdate()
        {
            var pos = transform.position;

            if (pos.x < min.x ||
                pos.x > max.x ||
                pos.y < min.y ||
                pos.y > max.y)
            {
                _gameplay.HandleMissed();
                Destroy(gameObject);
            }
        }
    }
}