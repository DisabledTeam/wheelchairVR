using Interactable;
using UnityEngine;
using WheelInput;

namespace DefaultNamespace
{
    public abstract class HandItemTool : MonoBehaviour
    {
        [Header("HandItemTool Links")]
        [SerializeField] protected HandItem handItem;

        [Header("Monitoring")]
        [SerializeField] protected bool isActive;
        [SerializeField] protected HandInputProvider handInputProvider;


        protected virtual void ItemTakenInHand()
        {
        }

        protected virtual void ItemDroppedFromHand()
        {
        }

        protected virtual void OnEnable()
        {
            handItem.takenInHand.AddListener(OnTaken);
            handItem.droppedFromHand.AddListener(OnDropped);
        }

        private void OnDropped(HandInputProvider arg0)
        {
            isActive = false;
            ItemDroppedFromHand();
        }

        private void OnTaken(HandInputProvider arg0)
        {
            handInputProvider = arg0;
            isActive = true;
            ItemTakenInHand();
        }

        protected virtual  void OnDisable()
        {
            handItem.takenInHand.RemoveListener(OnTaken);
            handItem.droppedFromHand.RemoveListener(OnDropped);
        }
    }
}