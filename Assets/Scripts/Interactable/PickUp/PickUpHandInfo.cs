using System;

namespace Interactable
{
    [Serializable]
    public class PickUpHandInfo
    {
        public PickUpHandItem UsedPickUp;
        public PlayerHandAxis HandAxis;
        public Interact.Interactable Interactable;
        

        public PickUpHandInfo(PickUpHandItem usedPickUp, PlayerHandAxis handAxis, Interact.Interactable interactable)
        {
            UsedPickUp = usedPickUp;
            HandAxis = handAxis;
            Interactable = interactable;
        }
    }
}