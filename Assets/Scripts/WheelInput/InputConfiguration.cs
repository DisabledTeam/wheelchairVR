using System;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace WheelInput
{
    public class InputConfiguration : MonoBehaviour
    {
        [SerializeField] public SteamVR_Input_Sources leftHand = SteamVR_Input_Sources.LeftHand;
        [SerializeField] public SteamVR_Input_Sources rightHand = SteamVR_Input_Sources.RightHand;
        
        public SteamVR_Action_Boolean firstButton = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("wheelchair","FirstButton");
        public SteamVR_Action_Single firstButtonAnalog = SteamVR_Input.GetAction<SteamVR_Action_Single>("wheelchair","FirstButtonAnalog");
        public SteamVR_Action_Boolean secondButton = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("wheelchair","SecondButton");
        public SteamVR_Action_Boolean joystickTouched = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("wheelchair","JoystickTouched");
        public SteamVR_Action_Vector2 joyStick = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("wheelchair","JoyStick");

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
            SteamVR_Input.GetActionSets().ForEach(actionSet => {
                Debug.Log(actionSet.fullPath);
                actionSet.Activate(SteamVR_Input_Sources.Any, 0, false);
            });
        }
    }
}