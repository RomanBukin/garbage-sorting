using System.Collections;
using UnityEngine;
using Zenject;

namespace Game.Garbage
{
    public class GarbageCollector : MonoBehaviour
    {
        private const float DestroyTime = 1.0f;
        private const float RotateSpeed = 500.0f;

        private Gameplay _gameplay;
        private float _halfSizeY;
        private Vector3 _scaleDiff;
        private Vector3 _positionDiff;


        [Inject]
        private void Construct(Gameplay gameplay)
        {
            _gameplay = gameplay;
        }

        void Start()
        {
            var col = GetComponent<Collider2D>();
            _halfSizeY = col.bounds.extents.y;
        }

        void OnTriggerEnter2D(Collider2D target)
        {
            var col = target.GetComponent<CircleCollider2D>();
            col.enabled = false;
            int type = GetTankType(target.tag);

            bool isCorrect = target.CompareTag(tag);

            if (isCorrect)
            {
                _gameplay.HandleCorrect(type);
            }
            else
            {
                _gameplay.HandleIncorrect(type);
            }

            StartCoroutine(nameof(DestroyItem), target.gameObject);
        }

        private int GetTankType(string tagType)
        {
            switch (tagType)
            {
                case "Blue":
                    return 0;
                case "Green":
                    return 1;
                case "Orange":
                    return 2;
                case "Yellow":
                    return 3;
                case "Red":
                    return 4;
                case "Black":
                    return 5;
                default:
                    return -1;
            }
        }

        private IEnumerator DestroyItem(GameObject item)
        {
            // Disable physics
            var body = item.GetComponent<Rigidbody2D>();
            body.simulated = false;
            body.velocity = Vector2.zero;

            var pos = transform.position;
            var itemTrans = item.transform;
            var itemPos = itemTrans.position;

            // Calculate scale per second
            var scale = itemTrans.localScale.x / DestroyTime;
            _scaleDiff = Vector3.one * scale;

            // Calculate position difference per second
            var posX = (pos.x - itemPos.x) / DestroyTime;
            var posY = (pos.y + _halfSizeY - itemPos.y) / DestroyTime;
            _positionDiff = new Vector3(posX, posY, 0f);

            // Apply transformation
            while (itemTrans.localScale.x > 0.0f)
            {
                itemTrans.Rotate(Vector3.forward, RotateSpeed * Time.deltaTime);
                itemTrans.localScale -= _scaleDiff * Time.deltaTime;
                itemTrans.position += _positionDiff * Time.deltaTime;
                yield return null;
            }

            Destroy(item);
        }
    }
}