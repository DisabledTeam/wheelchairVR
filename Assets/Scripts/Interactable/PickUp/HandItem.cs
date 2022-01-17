using UnityEngine;
using UnityEngine.Events;

namespace Interactable
{
    public class HandItem : MonoBehaviour, IHandItem
    {
        [Header("Settings")]
        [SerializeField] private bool destroyOnDrop;

        [Header("Monitoring")]
        [SerializeField] private bool isSetted;
        [SerializeField] private HandItemHolder holder;

        public UnityAction droppedFromHand;
        public UnityAction takenInHand;

        public void SetInHolder(HandItemHolder holder)
        {
            isSetted = true;
            this.holder = holder;
            takenInHand.Invoke();
        }

        public void RemoveFromHolder()
        {
            isSetted = false;
            holder = null;
            droppedFromHand.Invoke();
            if (destroyOnDrop) Destroy(gameObject);
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}