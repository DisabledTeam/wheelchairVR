using System;

namespace Interactable
{
    [Serializable]
    public class PickUpHandInfo
    {
        public PickUpHandItem UsedPickUp;
        public PlayerHandAxis HandAxis;

        public PickUpHandInfo(PickUpHandItem usedPickUp, PlayerHandAxis handAxis)
        {
            UsedPickUp = usedPickUp;
            HandAxis = handAxis;
        }
    }
}