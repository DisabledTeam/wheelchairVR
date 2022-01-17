using System;
using System.Text;
using TMPro;
using UnityEngine;
using Valve.VR;

namespace WheelInput
{
    public class InputTester : MonoBehaviour {
        [SerializeReference] private InputProvider inputProvider;
        [SerializeField] private TextMeshProUGUI fullInputText;

        private void Update()
        {
            fullInputText.text = GetInputValuesText();
        }

        private string GetInputValuesText()
        {
            var sb = new StringBuilder();

            sb.Append("firstButton.state:");
            sb.Append(inputProvider.rightHand.firstButton);
            sb.Append("\n");

            
            sb.Append("firstButtonAnalog.axis:");
            sb.Append(inputProvider.rightHand.firstButtonAnalog);
            sb.Append("\n");

            
            sb.Append("secondButton.state:");
            sb.Append(inputProvider.rightHand.secondButton);
            sb.Append("\n");

            
            sb.Append("joyStick.axis:");
            sb.Append(inputProvider.rightHand.joyStick);
            sb.Append("\n");


            sb.Append("joystickTouched.state:");
            sb.Append(inputProvider.rightHand.joystickTouch);
            sb.Append("\n");


            sb.Append("SteamVR_Input.initialized:");
            sb.Append(SteamVR_Input.initialized);
            sb.Append("\n");
            sb.Append("SteamVR_Input.GetActionSet(wheelchair).IsActive()");
            sb.Append(SteamVR_Input.GetActionSet("wheelchair").IsActive());
            sb.Append("\n");

            
            return sb.ToString();
        }
    }
}