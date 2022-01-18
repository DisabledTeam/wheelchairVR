using System;
using Interactable;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace WheelInput {
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
	}

	[CreateAssetMenu(menuName = "WheelChairVR/Input/HandInputProvider", fileName = "HandInputProvider", order = 0)]
	public class HandInputProvider : ScriptableObject {
		public bool firstButton;
		public float firstButtonAnalog;
		public bool secondButton;
		public bool joystickTouch;
		public Vector2 joyStick;

		/* -- */

		public UnityEvent<bool> firstButtonChanged = new UnityEvent<bool>();
		public UnityEvent<bool> secondButtonChanged = new UnityEvent<bool>();
		public UnityEvent<bool> joystickTouchChanged = new UnityEvent<bool>();
	}

	public class InputUpdater : MonoBehaviour {
		[SerializeField] public SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand;
		[SerializeField] public SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;
		[SerializeReference] private InputProvider inputProvider;

		public SteamVR_Action_Boolean firstButton =
			SteamVR_Input.GetAction<SteamVR_Action_Boolean>("wheelchair", "FirstButton");

		public SteamVR_Action_Single firstButtonAnalog =
			SteamVR_Input.GetAction<SteamVR_Action_Single>("wheelchair", "FirstButtonAnalog");

		public SteamVR_Action_Boolean secondButton =
			SteamVR_Input.GetAction<SteamVR_Action_Boolean>("wheelchair", "SecondButton");

		public SteamVR_Action_Boolean joystickTouch =
			SteamVR_Input.GetAction<SteamVR_Action_Boolean>("wheelchair", "JoystickTouched");

		public SteamVR_Action_Vector2 joyStick =
			SteamVR_Input.GetAction<SteamVR_Action_Vector2>("wheelchair", "JoyStick");

		/* -- Singleton */

		private static InputUpdater _instance;

		public static InputUpdater Instance {
			get {
				if (_instance == null) _instance = (InputUpdater)FindObjectOfType(typeof(InputUpdater));
				if (_instance == null)
					_instance = new GameObject("InputUpdater").AddComponent<InputUpdater>();

				return _instance;
			}
			private set {
				if (_instance != null) Debug.LogError("InputUpdater should be one on the scene");
				else _instance = value;
			}
		}

		private void Awake() {
			Instance = this;
			SteamVR.Initialize();
		}

		private void Start() {
			SteamVR_Input.GetActionSets().ForEach(actionSet => {
				Debug.Log(actionSet.fullPath);
				actionSet.Activate(SteamVR_Input_Sources.Any, 0, false);
			});

			firstButton.onChange += 
				(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState) 
					=> {
					if(fromSource == leftHand) inputProvider.leftHand.firstButtonChanged.Invoke(newState);
					else if(fromSource == rightHand) inputProvider.rightHand.firstButtonChanged.Invoke(newState);
				};

			secondButton.onChange += 
				(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState) 
					=> {
					if(fromSource == leftHand) inputProvider.leftHand.secondButtonChanged.Invoke(newState);
					else if(fromSource == rightHand) inputProvider.rightHand.secondButtonChanged.Invoke(newState);
				};

			joystickTouch.onChange += 
				(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState) 
					=> {
					if(fromSource == leftHand) inputProvider.leftHand.joystickTouchChanged.Invoke(newState);
					else if(fromSource == rightHand) inputProvider.rightHand.joystickTouchChanged.Invoke(newState);
				};
		}

		private void Update() {
			inputProvider.leftHand.firstButton = firstButton.GetState(leftHand);
			inputProvider.leftHand.firstButtonAnalog = firstButtonAnalog.GetAxis(leftHand);
			inputProvider.leftHand.secondButton = secondButton.GetState(leftHand);
			inputProvider.leftHand.joystickTouch = joystickTouch.GetState(leftHand);
			inputProvider.leftHand.joyStick = joyStick.GetAxis(leftHand);

			inputProvider.rightHand.firstButton = firstButton.GetState(rightHand);
			inputProvider.rightHand.firstButtonAnalog = firstButtonAnalog.GetAxis(rightHand);
			inputProvider.rightHand.secondButton = secondButton.GetState(rightHand);
			inputProvider.rightHand.joystickTouch = joystickTouch.GetState(rightHand);
			inputProvider.rightHand.joyStick = joyStick.GetAxis(rightHand);
		}
	}
}