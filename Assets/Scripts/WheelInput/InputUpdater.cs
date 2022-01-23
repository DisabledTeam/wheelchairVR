using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace WheelInput
{
    public class InputUpdater : MonoBehaviour
    {
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
        
        public SteamVR_Action_Boolean grabButton =
            SteamVR_Input.GetAction<SteamVR_Action_Boolean>("wheelchair", "GrabButton");
        
        
        /* -- Singleton */

        private static InputUpdater _instance;

        public static InputUpdater Instance
        {
            get
            {
                if (_instance == null) _instance = (InputUpdater) FindObjectOfType(typeof(InputUpdater));
                if (_instance == null)
                    _instance = new GameObject("InputUpdater").AddComponent<InputUpdater>();

                return _instance;
            }
            private set
            {
                if (_instance != null) Debug.LogError("InputUpdater should be one on the scene");
                else _instance = value;
            }
        }

        private void Awake()
        {
            Instance = this;
            SteamVR.Initialize();
        }

        private void Start()
        {
            SteamVR_Input.GetActionSets().ForEach(actionSet =>
            {
                Debug.Log(actionSet.fullPath);
                actionSet.Activate(leftHand, 0, false);
                actionSet.Activate(rightHand, 0, false);
                // actionSet.Activate(SteamVR_Input_Sources.Any, 0, false);
            });

            firstButton.AddOnChangeListener(
                (SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState) =>
                    inputProvider.leftHand.firstButtonChanged.Invoke(newState), leftHand
            );

            firstButton.AddOnChangeListener(
                (SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState) =>
                    inputProvider.rightHand.firstButtonChanged.Invoke(newState), rightHand
            );

            secondButton.AddOnChangeListener(
                (SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState) =>
                    inputProvider.leftHand.secondButtonChanged.Invoke(newState), leftHand
            );

            secondButton.AddOnChangeListener(
                (SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState) =>
                    inputProvider.rightHand.secondButtonChanged.Invoke(newState), rightHand
            );


            joystickTouch.AddOnChangeListener(
                (SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState) =>
                    inputProvider.leftHand.joystickTouchChanged.Invoke(newState), leftHand
            );

            joystickTouch.AddOnChangeListener(
                (SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState) =>
                    inputProvider.rightHand.joystickTouchChanged.Invoke(newState), rightHand
            );
            
            grabButton.AddOnChangeListener(
                (SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState) =>
                    inputProvider.rightHand.grabButtonChanged.Invoke(newState), rightHand
            );
            
            grabButton.AddOnChangeListener(
                (SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource, bool newState) =>
                    inputProvider.leftHand.grabButtonChanged.Invoke(newState), leftHand
            );
            
            
        }


        private void Update()
        {
            inputProvider.leftHand.firstButton = firstButton.GetState(leftHand);
            inputProvider.leftHand.firstButtonAnalog = firstButtonAnalog.GetAxis(leftHand);
            inputProvider.leftHand.secondButton = secondButton.GetState(leftHand);
            inputProvider.leftHand.joystickTouch = joystickTouch.GetState(leftHand);
            inputProvider.leftHand.joyStick = joyStick.GetAxis(leftHand);
            inputProvider.leftHand.grabButton = grabButton.GetState(leftHand);

            inputProvider.rightHand.firstButton = firstButton.GetState(rightHand);
            inputProvider.rightHand.firstButtonAnalog = firstButtonAnalog.GetAxis(rightHand);
            inputProvider.rightHand.secondButton = secondButton.GetState(rightHand);
            inputProvider.rightHand.joystickTouch = joystickTouch.GetState(rightHand);
            inputProvider.rightHand.joyStick = joyStick.GetAxis(rightHand);
            inputProvider.rightHand.grabButton = grabButton.GetState(rightHand);
        }
    }
}