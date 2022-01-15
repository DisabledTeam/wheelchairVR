using System;
using UnityEngine;
using Valve.VR;

namespace WheelInput
{
    public class InputConfiguration : MonoBehaviour
    {
        [Serializable]
        class ActionSetNamePair
        {
            public string actionSet;
            public string actionName;

            public ActionSetNamePair(string actionSet, string actionName)
            {
                this.actionSet = actionSet;
                this.actionName = actionName;
            }

            public string GetFull(string defaultActionSet) {
                var aSet = actionSet != "" ? actionSet : defaultActionSet;
                var fullPath = $"/actions/{aSet}/in/{actionName}";
                Debug.Log(fullPath);
                return fullPath;
            }
        }

        [SerializeField] public SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand;
        [SerializeField] public SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;
        
        [SerializeField] private string defaultActionSet = "wheelchair";
        [SerializeField] private ActionSetNamePair firstButtonAction = new ActionSetNamePair(null, "FirstButton");

        [SerializeField] private ActionSetNamePair firstButtonAnalogAction = new ActionSetNamePair(null, "FirstButtonAnalog");

        [SerializeField] private ActionSetNamePair secondButtonAction = new ActionSetNamePair(null, "SecondButton");
        [SerializeField] private ActionSetNamePair joyStickAction = new ActionSetNamePair(null, "JoyStick");
        [SerializeField] private ActionSetNamePair joystickTouchedAction = new ActionSetNamePair(null, "JoystickTouched");

        [HideInInspector] public SteamVR_Action_Boolean firstButton;
        [HideInInspector] public SteamVR_Action_Single firstButtonAnalog;
        [HideInInspector] public SteamVR_Action_Boolean secondButton;
        [HideInInspector] public SteamVR_Action_Boolean joystickTouched;
        [HideInInspector] public SteamVR_Action_Vector2 joyStick;

        /* -- Singleton */


        private static InputConfiguration _instance;

        public static InputConfiguration Instance
        {
            get
            {
                if (_instance == null) _instance = (InputConfiguration) FindObjectOfType(typeof(InputConfiguration));
                if (_instance == null)
                    _instance = new GameObject("InputConfiguration").AddComponent<InputConfiguration>();

                return _instance;
            }
            private set
            {
                if (_instance != null) Debug.LogError("InputProvider should be one on the scene");
                else _instance = value;
            }
        }

        private void Awake()
        {
            Instance = this;

            /* -- */

            InitAllInputs();
        }

        /* -- */

        private void InitAllInputs()
        {
            firstButton = SteamVR_Input.GetAction<SteamVR_Action_Boolean>(firstButtonAction.GetFull(defaultActionSet));
            // firstButtonAnalog =
            //     SteamVR_Input.GetAction<SteamVR_Action_Single>(firstButtonAnalogAction.GetFull(defaultActionSet));
            firstButtonAnalog =
                SteamVR_Input.GetAction<SteamVR_Action_Single>(firstButtonAnalogAction.actionSet, firstButtonAnalogAction.actionName);
            secondButton =
                SteamVR_Input.GetAction<SteamVR_Action_Boolean>(secondButtonAction.GetFull(defaultActionSet));
            joystickTouched = SteamVR_Input.GetAction<SteamVR_Action_Boolean>(joyStickAction.GetFull(defaultActionSet));
            joyStick = SteamVR_Input.GetAction<SteamVR_Action_Vector2>(joystickTouchedAction.GetFull(defaultActionSet));
        }
    }
}