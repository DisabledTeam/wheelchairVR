using System;
using UnityEngine;

namespace Interactable
{
    public class HandPickUpArea : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool allowPickingUp;

        [Header("Links")]
        [SerializeField] private PlayerHandPickUpChannel pickUpChannel;

        private void OnTriggerEnter(Collider other)
        {
            OnInteract(other);
        }

        private void OnInteract(Collider other)
        {
            if (!allowPickingUp) return;
            if (other.TryGetComponent<PickUpHandItem>(out var pickUp))
            {
                PickUp(pickUp);
            }
        }

        private void PickUp(PickUpHandItem pickUp)
        {
            var info = pickUp.PickUp(PlayerHandAxis.LeftHand);
            pickUpChannel.InvokeNewPickUp(info);
        }
    }
}