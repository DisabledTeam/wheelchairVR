using System;
using UnityEngine;
using WheelInput;

namespace Interactable
{
    public class PickUpRigidbody : MonoBehaviour
    {
        [SerializeField] private PickUpHandItem _pickUpHandItem;
        [SerializeField] private Rigidbody _rigidbody;

        private void OnEnable()
        {
            _pickUpHandItem.PickedUp.AddListener(OnPickedUp);
            _pickUpHandItem.HandItem.droppedFromHand.AddListener(OnDropped);
        }
        
        private void OnDisable()
        {
            _pickUpHandItem.PickedUp.RemoveListener(OnPickedUp);
            _pickUpHandItem.HandItem.droppedFromHand.RemoveListener(OnDropped);
        }

        private void OnDropped(HandInputProvider arg0)
        {
            _rigidbody.isKinematic = false; 
        }

        private void OnPickedUp(PlayerHandAxis arg0, HandInputProvider arg1)
        {
            _rigidbody.isKinematic = true;
        }
    }
}