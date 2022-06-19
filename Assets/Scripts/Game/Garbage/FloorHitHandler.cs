using UnityEngine;
using Zenject;

namespace Game.Garbage
{
    public class FloorHitHandler : MonoBehaviour
    {
        private Gameplay _gameplay;
        private bool _hit;

        [Inject]
        private void Construct(Gameplay gameplay)
        {
            _gameplay = gameplay;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_hit)
            {
                return;
            }

            if (other.collider.CompareTag("Floor"))
            {
                _hit = true;
                _gameplay.HandleMissed();
            }
        }
    }
}
