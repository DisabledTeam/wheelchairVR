using System;
using System.Collections.Generic;
using Interact;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using WheelInput;

namespace Interactable
{
    public class PickUpHandItem : MonoBehaviour, IHandItem
    {
        [Header("Pick up Settings")]
        [SerializeField] private bool needDestroyOnDrop;
        [SerializeField] private bool needSwapScripts;
        [SerializeField] private PlayerHandPickUpChannel pickUpChannel;


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


        public UnityEvent<PlayerHandAxis, HandInputProvider> PickedUp =
            new UnityEvent<PlayerHandAxis, HandInputProvider>();

        public bool IsPickedUp => isPickedUp;
        public HandItem HandItem => handItem;

        private static readonly int PickedUpAnimatorBool = Animator.StringToHash("PickedUp");


        public PickUpHandInfo PickUp(PlayerHandAxis handAxis, HandInputProvider handInputProvider)
        {
            var info = new PickUpHandInfo(this, handAxis, handInputProvider, interactable);
            ViewPickUp();

            if (needSwapScripts) SwapScripts(true);
            isPickedUp = true;
            PickedUp.Invoke(handAxis, handInputProvider);
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
            pickUpObject.gameObject.SetActive(!onPickUp);
            handItem.gameObject.SetActive(onPickUp);
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

        public void SetInHolder(HandItemHolder holder, HandInputProvider handInputProvider)
        {
            handItem.SetInHolder(holder, handInputProvider);
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

        private void OnEnable()
        {
            interactable.InteractableInteracted.AddListener(OnInteract);
        }

        private void OnDisable()
        {
            interactable.InteractableInteracted.RemoveListener(OnInteract);
        }

        private void OnInteract(InteractableInteractedEventArgs arg0)
        {
            var info = PickUp(arg0.interactor.PlayerHandAxis, arg0.interactor.handInputProvider);
            pickUpChannel.InvokeNewPickUp(info);
        }
    }
}