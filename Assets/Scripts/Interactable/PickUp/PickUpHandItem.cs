using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Interactable
{
    public class PickUpHandItem : MonoBehaviour, IHandItem
    {
        [Header("Pick up Settings")]
        [SerializeField] private bool needDestroyOnDrop;
        [SerializeField] private bool needSwapScripts;


        [Header("Links")]
        [SerializeField] private Interact.Interactable interactable;

        [Header("Links can be null if dont need swap")]
        [SerializeField] private PickUpObject pickUpObject;
        [SerializeField] private HandItem handItem;

        [Header("Links can be null")]
        [SerializeField] private ParticleSystem pickUpParticles;
        [SerializeField] private Animator animator;

        [Header("Monitoring")]
        [SerializeField] private bool isPickedUp;

        public bool IsPickedUp => isPickedUp;
        public HandItem HandItem => handItem;

        private static readonly int PickedUpAnimatorBool = Animator.StringToHash("PickedUp");


        public PickUpHandInfo PickUp(PlayerHandAxis handAxis)
        {
            var info = new PickUpHandInfo(this, handAxis, interactable);
            ViewPickUp();

            if (needSwapScripts) SwapScripts(true);
            isPickedUp = true;

            return info;
        }

        public void Drop()
        {
            if (needSwapScripts) SwapScripts(false);
            isPickedUp = false;
            if (needDestroyOnDrop) Destroy(gameObject);
        }

        private void SwapScripts(bool onPickUp)
        {
            pickUpObject.PickUp();
            pickUpObject.gameObject.SetActive(onPickUp);
            handItem.gameObject.SetActive(!onPickUp);
        }


        private void ViewPickUp()
        {
            if (pickUpParticles != null)
            {
                var ps = Instantiate(pickUpParticles, transform.position, Quaternion.identity);
            }

            if (animator != null)
            {
                animator.SetBool(PickedUpAnimatorBool, true);
            }
        }

        public void SetInHolder(HandItemHolder holder)
        {
            handItem.SetInHolder(holder);
        }

        public void RemoveFromHolder()
        {
            handItem.RemoveFromHolder();
            Drop();
        }

        public Transform GetTransform()
        {
            return transform;
        }
    }
}