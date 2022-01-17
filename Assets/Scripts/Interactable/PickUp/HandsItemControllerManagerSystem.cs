using System;
using Interact;
using UnityEngine;

namespace Interactable
{
    public class HandsItemControllerManagerSystem : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private PlayerHandPickUpChannel playerHandPickUpChannel;
        [SerializeField] private HandItemHolder leftHolder;
        [SerializeField] private HandItemHolder rightHolder;

        [SerializeField] private Interactor leftInteractor;
        [SerializeField] private Interactor rightInteractor;

        [Header("Monitoring")]
        [SerializeField] private Interact.Interactable leftInteractable;
        [SerializeField] private Interact.Interactable rightInteractable;


        public bool IsLeftHandEmpty => leftHolder.IsEmpty;
        public bool IsRightHandEmpty => rightHolder.IsEmpty;


        private void OnEnable()
        {
            playerHandPickUpChannel.handPickedUpItemEvent.AddListener(OnNewPickUp);
        }

        private void OnDisable()
        {
            playerHandPickUpChannel.handPickedUpItemEvent.RemoveListener(OnNewPickUp);
        }

        private void OnNewPickUp(HandPickedUpItemEventArgs arg0)
        {
            EquipPickUp(arg0.info);
        }

        private void EquipPickUp(PickUpHandInfo info)
        {
            var axis = info.HandAxis;
            var selectedHolder = GetHolder(axis);
            if (!selectedHolder.IsEmpty) DeEquipPickUp(axis);
            selectedHolder.SetUpItem(info.UsedPickUp.HandItem);
            GetInteractor(axis);
            SetInteractable(axis, info.Interactable);
            //TODO   GetInteractor(axis).Lock();
            //TODO  info.Interactable.Lock();
        }


        private void DeEquipPickUp(PlayerHandAxis axis)
        {
            var selectedHolder = GetHolder(axis);
            selectedHolder.RemoveItem();
            GetInteractor(axis);
            var interactable = GetInteractable(axis);
            if (interactable)
            {
                //TODO   GetInteractable(axis).UnLock();
                SetInteractable(axis, null);
            }
            //TODO   GetInteractor(axis).UnLock();
        }

        private HandItemHolder GetHolder(PlayerHandAxis axis)
        {
            var selectedHolder = axis switch
            {
                PlayerHandAxis.LeftHand => leftHolder,
                PlayerHandAxis.RightHand => rightHolder,
                _ => throw new ArgumentOutOfRangeException()
            };
            return selectedHolder;
        }

        private Interactor GetInteractor(PlayerHandAxis axis)
        {
            var interactor = axis switch
            {
                PlayerHandAxis.LeftHand => leftInteractor,
                PlayerHandAxis.RightHand => rightInteractor,
                _ => throw new ArgumentOutOfRangeException()
            };
            return interactor;
        }

        private Interact.Interactable GetInteractable(PlayerHandAxis axis)
        {
            var interactable = axis switch
            {
                PlayerHandAxis.LeftHand => leftInteractable,
                PlayerHandAxis.RightHand => rightInteractable,
                _ => throw new ArgumentOutOfRangeException()
            };
            return interactable;
        }

        private Interact.Interactable SetInteractable(PlayerHandAxis axis, Interact.Interactable interactable)
        {
            switch (axis)
            {
                case PlayerHandAxis.LeftHand:
                    leftInteractable = interactable;
                    break;
                case PlayerHandAxis.RightHand:
                    rightInteractable = interactable;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(axis), axis, null);
            }

            ;
            return interactable;
        }
    }
}