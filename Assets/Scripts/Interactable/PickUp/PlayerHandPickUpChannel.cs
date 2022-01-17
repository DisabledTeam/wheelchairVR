using System;
using UnityEngine;
using UnityEngine.Events;

namespace Interactable
{
    [CreateAssetMenu(menuName = "WheelChairVR/Hands/Create HandPickUpChannel", fileName = "HandPickUpChannel",
        order = 0)]
    public class PlayerHandPickUpChannel : ScriptableObject
    {
        public HandPickedUpItemEvent handPickedUpItemEvent = new HandPickedUpItemEvent();

        public void InvokeNewPickUp(PickUpHandInfo pickUpHandInfo)
        {
            handPickedUpItemEvent.Invoke(new HandPickedUpItemEventArgs(pickUpHandInfo));
        }
    }

    [Serializable]
    public class HandPickedUpItemEvent : UnityEvent<HandPickedUpItemEventArgs>
    {
    }

    [Serializable]
    public class HandPickedUpItemEventArgs
    {
        public PickUpHandInfo info;

        public HandPickedUpItemEventArgs(PickUpHandInfo info)
        {
            this.info = info;
        }
    }
}