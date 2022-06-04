using UnityEngine;

namespace Animation
{
    public class Rotate : MonoBehaviour
    {
        public float speed = 1f;

        void Update()
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * -speed * 100f);
        }
    }
}