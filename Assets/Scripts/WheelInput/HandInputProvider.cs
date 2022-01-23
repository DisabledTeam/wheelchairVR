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
        [SerializeField] private bool _firstButton;


        public bool firstButton
        {
            get => _firstButton;
            set
            {
                if (_firstButton != value) internal_firstButtonChanged.Invoke(value);
                _firstButton = value;
            }
        }

        public float firstButtonAnalog;

        [SerializeField] private bool _secondButton;

        public bool secondButton
        {
            get => _secondButton;
            set
            {
                if (_secondButton != value) internal_secondButtonChanged.Invoke(value);
                _secondButton = value;
            }
        }

        [SerializeField] private bool _joystickTouch;

        public bool joystickTouch
        {
            get => _joystickTouch;
            set
            {
                if (_joystickTouch != value) internal_joystickTouchChanged.Invoke(value);
                _joystickTouch = value;
            }
        }


        [SerializeField] private bool _grabButton;

        public bool grabButton
        {
            get => _grabButton;
            set
            {
                if (_grabButton != value) internal_grabButtonChanged.Invoke(value);
                _grabButton = value;
            }
        }

        public Vector2 joyStick;

        /* -- */

        public UnityEvent<bool> internal_firstButtonChanged = new UnityEvent<bool>();
        public UnityEvent<bool> internal_secondButtonChanged = new UnityEvent<bool>();
        public UnityEvent<bool> internal_joystickTouchChanged = new UnityEvent<bool>();
        public UnityEvent<bool> internal_grabButtonChanged = new UnityEvent<bool>();

        public UnityEvent<bool> firstButtonChanged = new UnityEvent<bool>();
        public UnityEvent<bool> secondButtonChanged = new UnityEvent<bool>();
        public UnityEvent<bool> joystickTouchChanged = new UnityEvent<bool>();
        public UnityEvent<bool> grabButtonChanged = new UnityEvent<bool>();

        [InspectorButton("FakeFirstButtonChangedTrue")]
        public void FakeFirstButtonChangedTrue()
        {
            firstButton = true;
            firstButtonChanged.Invoke(true);
        }

        [InspectorButton("FakeFirstButtonChangedFalse")]
        public void FakeFirstButtonChangedFalse()
        {
            firstButton = false;
            firstButtonChanged.Invoke(false);
        }

        [InspectorButton("FakeSecondButtonChangedTrue")]
        public void FakeSecondButtonChangedTrue()
        {
            secondButton = true;
            secondButtonChanged.Invoke(true);
        }

        [InspectorButton("FakeSecondButtonChangedFalse")]
        public void FakeSecondButtonChangedFalse()
        {
            secondButton = false;
            secondButtonChanged.Invoke(false);
        }

        [InspectorButton("FakeJoystickTouchChangedTrue")]
        public void FakeJoystickTouchChangedTrue()
        {
            joystickTouch = true;
            joystickTouchChanged.Invoke(true);
        }

        [InspectorButton("FakeJoystickTouchChangedFalse")]
        public void FakeJoystickTouchChangedFalse()
        {
            joystickTouch = false;
            joystickTouchChanged.Invoke(false);
        }

        [InspectorButton("FakeGrabButtonChangedFalse")]
        public void FakeGrabButtonChangedFalse()
        {
            grabButton = false;
            grabButtonChanged.Invoke(false);
        }

        [InspectorButton("FakeGrabButtonChangedTrue")]
        public void FakeGrabButtonChangedTrue()
        {
            grabButton = true;
            grabButtonChanged.Invoke(true);
        }
    }
}