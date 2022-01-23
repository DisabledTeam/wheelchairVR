using System;
using System.Collections.Generic;
using System.Linq;
using Interact;
using UnityEngine;
using UnityEngine.UI;
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

        [SerializeField] private bool isDropDisabled;


        public bool IsLeftHandEmpty => leftHolder.IsEmpty;
        public bool IsDropDisabled => isDropDisabled;
        public bool IsRightHandEmpty => rightHolder.IsEmpty;


        public HandInputProvider LeftHandInputProvider => leftInteractor.HandInputProvider;
        public HandInputProvider RightHandInputProvider => rightInteractor.HandInputProvider;

        public bool IsHandEmpty(PlayerHandAxis axis) => GetHolder(axis).IsEmpty;

        public HandInputProvider GetInputProvider(PlayerHandAxis axis)
        {
            return axis switch
            {
                PlayerHandAxis.LeftHand => LeftHandInputProvider,
                PlayerHandAxis.RightHand => RightHandInputProvider,
                _ => throw new ArgumentOutOfRangeException(nameof(axis), axis, null)
            };
        }


        public void DisableInventoryDrop()
        {
            isDropDisabled = true;
        }

        public void EnableInventoryDrop()
        {
            isDropDisabled = false;
        }

        public void EquipHandItem(PlayerHandAxis handAxis, IHandItem handItem)
        {
            var selectedHolder = GetHolder(handAxis);
            if (!selectedHolder.IsEmpty) DeEquipPickUp(handAxis);
            var handInputProvider = GetInteractor(handAxis).HandInputProvider;

            selectedHolder.SetUpItem(handItem, handInputProvider);
            GetInteractor(handAxis).Lock();
            _combinations.Add(new HolderProviderCombination(selectedHolder, handInputProvider, this, handAxis));
        }


        public void DeEquipPickUp(PlayerHandAxis handAxis)
        {
            foreach (var combination in _combinations.ToList())
            {
                if (combination.PlayerHandAxis == handAxis) DeEquipPickUp(combination);
            }
        }

        public void DeEquipPickUp(HolderProviderCombination combination)
        {
            DisconnectFromHolder(combination.PlayerHandAxis);
            combination.Deconstruct();
            _combinations.Remove(combination);
        }

        public void TryDeEquipHandItem(IHandItem handItem)
        {
            foreach (var combination in _combinations.ToList())
            {
                if (combination.GetHandItem() == handItem) DeEquipPickUp(combination);
            }
        }

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
            _combinations.ForEach(c => c.Deconstruct());
        }

        private void OnNewPickUp(HandPickedUpItemEventArgs arg0)
        {
            EquipPickUp(arg0.info);
        }

        private void EquipPickUp(PickUpHandInfo info)
        {
            EquipHandItem(info.HandAxis, info.UsedPickUp);
            SetInteractable(info.HandAxis, info.Interactable).Lock();
        }


        private void DisconnectFromHolder(PlayerHandAxis axis)
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

        [SerializeField] private bool pressed;


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
            switch (arg0)
            {
                case true:
                    pressed = true;
                    break;
                case false when pressed:
                {
                    if (!HandsItemControllerManagerSystem.IsDropDisabled)
                        HandsItemControllerManagerSystem.DeEquipPickUp(this);
                    break;
                }
            }
        }

        public IHandItem GetHandItem()
        {
            return HandItemHolder.CurrentHandItem;
        }
    }
}