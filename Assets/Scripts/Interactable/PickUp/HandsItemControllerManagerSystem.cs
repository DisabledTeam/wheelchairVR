using System;
using System.Collections.Generic;
using System.Linq;
using Interact;
using UnityEngine;
using WheelInput;

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
        [SerializeField]
        private List<HolderProviderCombination> _combinations;

        public bool IsLeftHandEmpty => leftHolder.IsEmpty;
        public bool IsRightHandEmpty => rightHolder.IsEmpty;
        public bool IsHandEmpty(PlayerHandAxis axis) => GetHolder(axis).IsEmpty;


        private void OnEnable()
        {
            playerHandPickUpChannel.handPickedUpItemEvent.AddListener(OnNewPickUp);
        }

        private void OnDisable()
        {
            playerHandPickUpChannel.handPickedUpItemEvent.RemoveListener(OnNewPickUp);
        }

        private void OnDestroy()
        {
            _combinations.ForEach(c=>c.Deconstruct());
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
            selectedHolder.SetUpItem(info.UsedPickUp, info.HandInputProvider);
            GetInteractor(axis).Lock();
            SetInteractable(axis, info.Interactable).Lock();

            _combinations.Add(new HolderProviderCombination(selectedHolder, info.HandInputProvider, this, axis));
        }


        public void DeEquipPickUp(HolderProviderCombination combination)
        {
            DeEquipPickUp(combination.PlayerHandAxis);
            combination.Deconstruct();
            _combinations.Remove(combination);
        }

        public void TryDeEquipHandItem(IHandItem handItem)
        {
            foreach (var combination in _combinations.ToList())
            {
                if(combination.GetHandItem()==handItem) DeEquipPickUp(combination);
            }
        }


        private void DeEquipPickUp(PlayerHandAxis axis)
        {
            var selectedHolder = GetHolder(axis);
            selectedHolder.RemoveItem();
            GetInteractor(axis);
            var interactable = GetInteractable(axis);
            if (interactable)
            {
                GetInteractable(axis).Unlock();
                SetInteractable(axis, null);
            }

            GetInteractor(axis).Unlock();
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


    [Serializable]
    public class HolderProviderCombination
    {
        public HandItemHolder HandItemHolder;
        public HandInputProvider HandInputProvider;
        public HandsItemControllerManagerSystem HandsItemControllerManagerSystem;
        public PlayerHandAxis PlayerHandAxis;

        public HolderProviderCombination(HandItemHolder handItemHolder, HandInputProvider handInputProvider,
            HandsItemControllerManagerSystem handsItemControllerManagerSystem, PlayerHandAxis playerHandAxis)
        {
            HandItemHolder = handItemHolder;
            HandInputProvider = handInputProvider;
            HandsItemControllerManagerSystem = handsItemControllerManagerSystem;
            PlayerHandAxis = playerHandAxis;

            HandInputProvider.secondButtonChanged.AddListener(OnSecondButtonChanged);
        }

        public void Deconstruct()
        {
            HandInputProvider.secondButtonChanged.RemoveListener(OnSecondButtonChanged);
        }

        public void OnSecondButtonChanged(bool arg0)
        {
            if (arg0 == false)
            {
                HandsItemControllerManagerSystem.DeEquipPickUp(this);
            }
        }

        public IHandItem GetHandItem()
        {
            return HandItemHolder.CurrentHandItem;
        }
    }
}