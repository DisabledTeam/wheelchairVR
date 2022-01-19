using System;
using InspectorButton;
using UnityEngine;
using UnityEngine.Events;

namespace WheelInput {
	[CreateAssetMenu(menuName = "WheelChairVR/Input/HandInputProvider", fileName = "HandInputProvider", order = 0)]
	[InspectorButtonClass]
	public class HandInputProvider : ScriptableObject {
		
		[SerializeField] private bool _firstButton;

		
		public bool firstButton {
			get => _firstButton;
			set {
				if (_firstButton != value) internal_firstButtonChanged.Invoke(value);
				_firstButton = value;
			}
		}

		public float firstButtonAnalog;

		[SerializeField] private bool _secondButton;

		public bool secondButton {
			get => _secondButton;
			set {
				if (_secondButton != value) internal_secondButtonChanged.Invoke(value);
				_secondButton = value;
			}
		}

		[SerializeField] private bool _joystickTouch;

		public bool joystickTouch {
			get => _joystickTouch;
			set {
				if (_joystickTouch != value) internal_joystickTouchChanged.Invoke(value);
				_joystickTouch = value;
			}
		}

		public Vector2 joyStick;

		/* -- */

		public UnityEvent<bool> internal_firstButtonChanged = new UnityEvent<bool>();
		public UnityEvent<bool> internal_secondButtonChanged = new UnityEvent<bool>();
		public UnityEvent<bool> internal_joystickTouchChanged = new UnityEvent<bool>();

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