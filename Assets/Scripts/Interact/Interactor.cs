using System;
using System.Collections.Generic;
using Interact.Lock;
using Interactable;
using UnityEngine;
using WheelInput;

namespace Interact
{
    public class Interactor : LockableMonoBehaviour
    {
        [SerializeField] private PlayerHandAxis hand;
        [SerializeReference] private InputProvider inputProvider;
        [HideInInspector] public HandInputProvider handInputProvider;

        public PlayerHandAxis PlayerHandAxis => hand;


        //todo: custom interactable condition - custom button etc
        private List<Interactable> clickInteractables = new List<Interactable>();

        private void Awake()
        {
            handInputProvider = inputProvider.getHandInputByHand(hand);
        }

        private void OnEnable()
        {
            handInputProvider.firstButtonChanged.AddListener(OnInteractButtonChanged);
        }

        private void OnDisable()
        {
            handInputProvider.firstButtonChanged.RemoveListener(OnInteractButtonChanged);
        }

        private void OnInteractButtonChanged(bool pressed)
        {
            foreach (var interactable in clickInteractables)
            {
                TryInteract(interactable);
            }
        }

        private bool TryInteract(Interactable withInteractable)
        {
            var interactorCondition =
                !withInteractable.interactorNeedToBeFreeToInteract || // Должна ли рука быть свободной?  
                !GetLock(); // Свободна ли рука?

            var interactableCondition =
                !withInteractable.interactableNeedToBeFreeToInteract || // Должен ли предметь быть свободным?  
                !withInteractable.GetLock(); // Свободен ли предмет?

            if (!interactorCondition || !interactableCondition) return false;
            withInteractable.OnInteract(this);
            return true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent<Interactable>(out var interactable)) return;
            interactable.OnTouch(this);
            if (interactable.interactorNeedToBeClickedToInteract)
                clickInteractables.Add(interactable);
            else
            {
                TryInteract(interactable);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent<Interactable>(out var interactable)) return;
            interactable.OnTouchEnd(this);
            clickInteractables.Remove(interactable);
        }
    }
}