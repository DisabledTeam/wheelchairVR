using System;
using UnityEngine;

namespace Interactable
{
    public class HandsItemControllerManagerSystem : MonoBehaviour
    {
        [Header("Links")]
        [SerializeField] private PlayerHandPickUpChannel playerHandPickUpChannel;
        [SerializeField] private HandItemHolder leftHolder;
        [SerializeField] private HandItemHolder rightHolder;

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
            var selectedHolder = GetHolder(info.HandAxis);
            if (!selectedHolder.IsEmpty) DeEquipPickUp(info.HandAxis);
            selectedHolder.SetUpItem(info.UsedPickUp.HandItem);
        }


        private void DeEquipPickUp(PlayerHandAxis axis)
        {
            var selectedHolder = GetHolder(axis);
            selectedHolder.RemoveItem();
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
    }
}