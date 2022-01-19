using System;
using InspectorButton;
using UnityEngine;
using UnityEngine.Events;

namespace WheelInput
{
    [CreateAssetMenu(menuName = "WheelChairVR/Input/HandInputProvider", fileName = "HandInputProvider", order = 0)]
    [InspectorButtonClass]
    public class HandInputProvider : ScriptableObject
    {
        public bool firstButton;
        public float firstButtonAnalog;
        public bool secondButton;
        public bool joystickTouch;
        public Vector2 joyStick;

        /* -- */

        public UnityEvent<bool> firstButtonChanged = new UnityEvent<bool>();
        public UnityEvent<bool> secondButtonChanged = new UnityEvent<bool>();
        public UnityEvent<bool> joystickTouchChanged = new UnityEvent<bool>();

        [InspectorButton("FakeFirstButtonChangedTrue")]
        public void FakeFirstButtonChangedTrue() => firstButtonChanged.Invoke(true);

        [InspectorButton("FakeFirstButtonChangedFalse")]
        public void FakeFirstButtonChangedFalse() => firstButtonChanged.Invoke(false);

        [InspectorButton("FakeSecondButtonChangedTrue")]
        public void FakeSecondButtonChangedTrue() => secondButtonChanged.Invoke(true);

        [InspectorButton("FakeSecondButtonChangedFalse")]
        public void FakeSecondButtonChangedFalse() => secondButtonChanged.Invoke(false);

        [InspectorButton("FakeJoystickTouchChangedTrue")]
        public void FakeJoystickTouchChangedTrue() => joystickTouchChanged.Invoke(true);

        [InspectorButton("FakeJoystickTouchChangedFalse")]
        public void FakeJoystickTouchChangedFalse() => joystickTouchChanged.Invoke(false);
    }
}