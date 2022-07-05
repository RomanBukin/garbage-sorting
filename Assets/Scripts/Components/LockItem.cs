using UnityEngine;

namespace Components
{
    public class LockItem : MonoBehaviour
    {
        [SerializeField] private GameObject[] lockedElements;
        [SerializeField] private GameObject[] unlockedElements;

        [SerializeField] private bool locked;

        public bool IsLocked
        {
            get => locked;
            set
            {
                locked = value;
                foreach (var element in lockedElements)
                {
                    element.SetActive(value);
                }
                foreach (var element in unlockedElements)
                {
                    element.SetActive(!value);
                }
            }
        }
    }
}
