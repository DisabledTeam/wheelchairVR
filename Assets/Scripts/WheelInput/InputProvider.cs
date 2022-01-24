using Interactable;
using UnityEngine;

namespace WheelInput
{
    [CreateAssetMenu(menuName = "WheelChairVR/Input/InputProvider", fileName = "InputProvider", order = 0)]
    public class InputProvider : ScriptableObject {
        public HandInputProvider leftHand;
        public HandInputProvider rightHand;

        public HandInputProvider getHandInputByHand(PlayerHandAxis hand) {
            return hand switch {
                PlayerHandAxis.LeftHand => leftHand,
                PlayerHandAxis.RightHand => rightHand,
                _ => null
            };
        }

        public void Clear()
        {
            leftHand.Clear();
            rightHand.Clear();
        }
    }
}