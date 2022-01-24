using UnityEngine;
using WheelInput;

namespace Interactable
{
    public class HandItemHolder : MonoBehaviour
    {
        [Header("Settings")]
        [Header("Links")]
        [SerializeField] private Transform handCenterPoint;
        [Header("Monitoring")]
        [SerializeField] private GameObject currentHandItemGO;
        [SerializeField] private bool isEmpty = true;
        [SerializeField] private IHandItem currentHandItem;


        public IHandItem CurrentHandItem => currentHandItem;
        public bool IsEmpty => isEmpty;

        public void SetUpItem(IHandItem handItem, HandInputProvider handInputProvider)
        {
            if (currentHandItem != null) RemoveItem();
            currentHandItemGO = handItem.GetTransform().gameObject;
            currentHandItem = handItem;
            currentHandItem.GetTransform().SetParent(handCenterPoint, false);
            currentHandItem.GetTransform().localPosition = currentHandItem.GetPositionInHand();
            currentHandItem.GetTransform().localRotation = Quaternion.Euler(currentHandItem.GetRotationInHand());

            currentHandItem.SetInHolder(this, handInputProvider);
            isEmpty = false;
        }

        public void RemoveItem()
        {
            if (isEmpty) return;

            currentHandItem.RemoveFromHolder();
            currentHandItem.GetTransform().SetParent(null);
            currentHandItem = null;
            currentHandItemGO = null;
            isEmpty = true;
        }
    }
}