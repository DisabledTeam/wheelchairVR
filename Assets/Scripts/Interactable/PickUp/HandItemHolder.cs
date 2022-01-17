using UnityEngine;

namespace Interactable
{
    public class HandItemHolder : MonoBehaviour
    {
        [Header("Settings")]
        [Header("Links")]
        [SerializeField] private Transform handCenterPoint;
        [Header("Monitoring")]
        [SerializeField] private IHandItem currentHandItem;
        [SerializeField] private bool isEmpty;

        public bool IsEmpty => isEmpty;

        public void SetUpItem(IHandItem handItem)
        {
            if (currentHandItem != null) RemoveItem();
            currentHandItem = handItem;
            currentHandItem.GetTransform().SetParent(handCenterPoint, false);
            currentHandItem.SetInHolder(this);
            isEmpty = false;
        }

        public void RemoveItem()
        {
            currentHandItem.RemoveFromHolder();
            currentHandItem = null;
            isEmpty = true;
        }
    }
}