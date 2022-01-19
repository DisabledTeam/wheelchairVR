using System;
using WheelInput;

namespace Interactable
{
    [Serializable]
    public class PickUpHandInfo
    {
        public PickUpHandItem UsedPickUp;
        public PlayerHandAxis HandAxis;
        public HandInputProvider HandInputProvider;
        public Interact.Interactable Interactable;

        public PickUpHandInfo(PickUpHandItem usedPickUp, PlayerHandAxis handAxis, HandInputProvider handInputProvider, Interact.Interactable interactable)
        {
            UsedPickUp = usedPickUp;
            HandAxis = handAxis;
            HandInputProvider = handInputProvider;
            Interactable = interactable;
        }
    }
}