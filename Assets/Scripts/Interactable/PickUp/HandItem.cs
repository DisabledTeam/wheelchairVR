using UnityEngine;
using UnityEngine.Events;
using WheelInput;

namespace Interactable
{
    public class HandItem : MonoBehaviour, IHandItem
    {
        [Header("Settings")]
        [SerializeField] private bool destroyOnDrop;
        [SerializeField] private Vector3 positionInHand;
        [SerializeField] private Vector3 rotationInHand;
        
        [Header("Monitoring")]
        [SerializeField] private bool isSetted;
        [SerializeField] private HandItemHolder holder;
        [SerializeField] private HandInputProvider handInputProvider;
        
        public UnityEvent<HandInputProvider>  droppedFromHand = new UnityEvent<HandInputProvider> ();
        public UnityEvent<HandInputProvider> takenInHand = new UnityEvent<HandInputProvider>();


        public void SetInHolder(HandItemHolder holder, HandInputProvider handInputProvider)
        {
            isSetted = true;
            this.holder = holder;
            this.handInputProvider = handInputProvider;
            takenInHand.Invoke(handInputProvider);
        }

        public void RemoveFromHolder()
        {
            isSetted = false;
            droppedFromHand.Invoke(handInputProvider);
            holder = null;
            handInputProvider = null;
            if (destroyOnDrop) Destroy(gameObject);
        }

        public Transform GetTransform()
        {
            return transform;
        }

        public Vector3 GetPositionInHand()
        {
            return positionInHand;
        }

        public Vector3 GetRotationInHand()
        {
            return rotationInHand;
        }
    }
}